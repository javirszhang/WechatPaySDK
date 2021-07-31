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
                    helper = TryResolvePrivateKeyWithoutFormatDeclare(keyString, RsaKeyHelper.KeyFormat.pkcs1) ??
                        TryResolvePrivateKeyWithoutFormatDeclare(keyString, RsaKeyHelper.KeyFormat.pkcs8);
                }
            }
            if (helper == null)
            {
                throw new InvalidKeyFormatException("无法识别的PEM密钥格式");
            }
            pemcert._provider = helper.RSACryptoServiceProvider();
            return pemcert;           
        }
        private static RsaKeyHelper TryResolvePrivateKeyWithoutFormatDeclare(string keyString, RsaKeyHelper.KeyFormat format)
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
    }
}
