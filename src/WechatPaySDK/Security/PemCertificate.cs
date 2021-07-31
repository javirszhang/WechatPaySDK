using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WechatPaySDK.Security
{
    /// <summary>
    /// PEM证书
    /// </summary>
    public class PemCertificate : RSAServiceProvider
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        protected PemCertificate() { }
        /// <summary>
        /// 从PEM密钥文件初始化PEM证书
        /// </summary>
        /// <param name="fullpath"></param>
        /// <exception cref="InvalidKeyFormatException"></exception>
        /// <returns></returns>
        public static PemCertificate ReadFromPemFile(string fullpath)
        {
            using (FileStream fs = File.OpenRead(fullpath))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                string pem = Encoding.UTF8.GetString(data);
                return ReadFromKeyString(pem);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyString">密钥字符串</param>
        /// <exception cref="InvalidKeyFormatException"></exception>
        /// <returns></returns>
        public static PemCertificate ReadFromKeyString(string keyString)
        {
            PemCertificate pemcert = new PemCertificate();
            RsaKeyHelper helper = null;
            if (keyString.StartsWith("-----"))
            {
                helper = RsaKeyHelper.FromPemKeyString(keyString);
            }
            else
            {
                //纯密钥文本，不带声明格式
                keyString = keyString.Replace("\r", "").Replace("\n", "");
                if (Convert.FromBase64String(keyString).Length < 400)
                {
                    keyString = RsaKeyHelper.FormatPublicKey(keyString);
                    helper = RsaKeyHelper.FromPemKeyString(keyString);
                }
                else
                {
                    helper = TryResolvePrivateKeyWithNoFormatDeclare(keyString, RsaKeyHelper.KeyFormat.pkcs1) ??
                        TryResolvePrivateKeyWithNoFormatDeclare(keyString, RsaKeyHelper.KeyFormat.pkcs8);
                }
            }
            if (helper == null)
            {
                throw new InvalidKeyFormatException("无法识别的PEM密钥格式");
            }
            pemcert._provider = helper.RSACryptoServiceProvider();
            return pemcert;           
        }
        private static RsaKeyHelper TryResolvePrivateKeyWithNoFormatDeclare(string keyString, RsaKeyHelper.KeyFormat format)
        {
            try
            {
                string pkcs1 = RsaKeyHelper.FormatPrivateKey(keyString, format);
                var helper = RsaKeyHelper.FromPemKeyString(pkcs1);
                helper.RSACryptoServiceProvider();
                return helper;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 证书导出为PEM格式字符串秘钥
        /// </summary>
        /// <param name="rsaParams"></param>
        /// <param name="includePrivate"></param>
        /// <returns></returns>
        public static string ToPemString(RSAParameters rsaParams, bool includePrivate)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            if (includePrivate)
            {
                bw.Write((ushort)0x8230);
                bw.Write((short)0x5F02);
                bw.Write((ushort)0x0102);
                bw.Write((byte)0x00);
                WriteParameter(bw, rsaParams.Modulus);
                WriteParameter(bw, rsaParams.Exponent);
                WriteParameter(bw, rsaParams.D);
                WriteParameter(bw, rsaParams.P);
                WriteParameter(bw, rsaParams.Q);
                WriteParameter(bw, rsaParams.DP);
                WriteParameter(bw, rsaParams.DQ);
                WriteParameter(bw, rsaParams.InverseQ);
            }
            else
            {
                bw.Write((ushort)0x8130);
                bw.Write((byte)0x9F);
                byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
                bw.Write(SeqOID);
                bw.Write((ushort)0x8103);
                bw.Write((byte)0x8d);
                bw.Write((byte)0x00);
                bw.Write((ushort)0x8130);
                bw.Write((byte)0x89);
                ushort flag = rsaParams.Modulus.Length > byte.MaxValue ? (ushort)0x8202 : (ushort)0x8102;
                bw.Write(flag);
                if (flag == 0x8102)
                {
                    byte len = (byte)(rsaParams.Modulus.Length + 1);
                    bw.Write(len);
                    bw.Write((byte)0x00);
                }
                else
                {
                    bw.Write((ushort)rsaParams.Modulus.Length);
                }

                bw.Write(rsaParams.Modulus);
                bw.Write((byte)0x02);
                bw.Write((byte)rsaParams.Exponent.Length);
                bw.Write(rsaParams.Exponent);
            }
            bw.Flush();
            bw.Close();
            byte[] buffer = ms.ToArray();
            ms.Close();
            return Convert.ToBase64String(buffer);
        }

        private static void WriteParameter(BinaryWriter bw, byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 0)
            {
                return;
            }
            int len = buffer.Length;
            bw.Write((byte)0x02);
            if (len > byte.MaxValue)
            {
                bw.Write((byte)0x82);
                bw.Write((ushort)len);
            }
            else
            {
                bw.Write((byte)0x81);
                byte bytelen = (byte)(len + 1);
                bw.Write(bytelen);
                bw.Write((byte)0x00);
            }
            bw.Write(buffer);
        }
    }
}
