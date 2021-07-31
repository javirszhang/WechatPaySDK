using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WechatPaySDK.Security
{
    /// <summary>
    /// RSA加密服务提供类
    /// </summary>
    public class RSAServiceProvider
    {
        /// <summary>
        /// .NET内置RSA加解密对象
        /// </summary>
        protected RSACryptoServiceProvider _provider;
        /// <summary>
        /// 初始化RSAServiceProvider实例
        /// </summary>
        protected RSAServiceProvider() { }
        /// <summary>
        /// 使用一个RSACryptoServiceProvider初始化实例
        /// </summary>
        /// <param name="provider">内置RSA加解密对象</param>
        public RSAServiceProvider(RSACryptoServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            this._provider = provider;
        }
        /// <summary>
        /// 使用XML密钥字符串初始化实例
        /// </summary>
        /// <param name="xmlKey">xml密钥字符串</param>
        public RSAServiceProvider(string xmlKey)
        {
            if (string.IsNullOrEmpty(xmlKey))
            {
                throw new ArgumentNullException("xmlkey");
            }
            this._provider = new RSACryptoServiceProvider();
            this._provider.FromXmlString(xmlKey);
        }
        /// <summary>
        /// 解密，默认UTF8编码方式
        /// </summary>
        /// <param name="value">需要解密的base64密文</param>
        /// <returns>解密出来的明文</returns>
        public string Decrypt(string value)
        {
            return Decrypt(value, Encoding.UTF8);
        }
        /// <summary>
        /// 解密，使用指定的编码方式解密
        /// </summary>
        /// <param name="value">需要解密的BASE64密文</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密出来的明文</returns>
        public string Decrypt(string value, Encoding encoding)
        {
            return Decrypt(value, encoding, false);
        }
        /// <summary>
        /// 解密，使用指定的编码方式与填充方式进行解密
        /// </summary>
        /// <param name="value">需要解密的base64密文</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="doOAEPPadding">是否进行OAEP填充</param>
        /// <returns>解密出来的明文</returns>
        public string Decrypt(string value, Encoding encoding, bool doOAEPPadding)
        {
            var data = Convert.FromBase64String(value);
            var res = Decrypt(data, doOAEPPadding);
            return encoding.GetString(res);
        }
        /// <summary>
        /// 解密，指定是否使用OAEP填充方式解密
        /// </summary>
        /// <param name="bytes">需要解密的字节组，如大于密钥字节长度实行分段解密</param>
        /// <param name="doOAEPPadding">是否使用OAEP填充</param>
        /// <returns>解密的明文字节组</returns>
        public byte[] Decrypt(byte[] bytes, bool doOAEPPadding)
        {
            int keysize = _provider.KeySize / 8;
            if (bytes.Length > keysize)
            {
                List<byte> array = new List<byte>();
                MemoryStream ms = new MemoryStream(bytes);
                BinaryReader reader = new BinaryReader(ms);
                for (int i = 1; i <= ms.Length / keysize; i++)
                {
                    var buffer = reader.ReadBytes(keysize);
                    var bufferRes = _provider.Decrypt(buffer, doOAEPPadding);
                    array.AddRange(bufferRes);
                }
                return array.ToArray();
            }
            else
            {
                return _provider.Decrypt(bytes, doOAEPPadding);
            }
            //return this._provider.Decrypt(bytes, doOAEPPadding);
        }
        /// <summary>
        /// 加密，默认使用UTF8编码方式加密
        /// </summary>
        /// <param name="value">需要加密的明文</param>
        /// <returns>密文字节组的base64格式</returns>
        public string Encrypt(string value)
        {
            return Encrypt(value, Encoding.UTF8);
        }
        /// <summary>
        /// 加密，使用指定的编码格式
        /// </summary>
        /// <param name="value">需要加密的明文，如大于密钥字节长度，会实行分段加密</param>
        /// <param name="encoding"></param>
        /// <returns>密文字节组的base64格式</returns>
        public string Encrypt(string value, Encoding encoding)
        {
            return Encrypt(value, encoding, false);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">需要加密的明文，如大于密钥字节长度，会实行分段加密</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="doOAEPPadding">是否使用OAEP填充</param>
        /// <returns>密文字节组的base64格式</returns>
        public string Encrypt(string value, Encoding encoding, bool doOAEPPadding)
        {
            var data = encoding.GetBytes(value);
            var res = Encrypt(data, doOAEPPadding);
            return Convert.ToBase64String(res);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="bytes">需要加密的明文字节组，如大于密钥字节长度，会实行分段加密</param>
        /// <param name="doOAEPPadding">是否使用OAEP填充</param>
        /// <returns>密文字节组</returns>
        public byte[] Encrypt(byte[] bytes, bool doOAEPPadding)
        {
            byte[] res = null;
            int keysize = _provider.KeySize / 8;
            int buffersize = keysize - 11;
            if (bytes.Length <= buffersize)
            {
                res = _provider.Encrypt(bytes, doOAEPPadding);
            }
            else
            {

                List<byte> array = new List<byte>();
                MemoryStream ms = new MemoryStream(bytes);
                BinaryReader reader = new BinaryReader(ms);
                while (reader.BaseStream.Length - reader.BaseStream.Position >= 1)
                {
                    long count = reader.BaseStream.Length - reader.BaseStream.Position >= buffersize ? buffersize : reader.BaseStream.Length - reader.BaseStream.Position;
                    array.AddRange(_provider.Encrypt(reader.ReadBytes((int)count), doOAEPPadding));
                }
                res = array.ToArray();
            }
            return res;
        }
        /// <summary>
        /// 错误发生时提供外部订阅的事件
        /// </summary>
        public event Action<Exception> ErrorOccurs;
        /// <summary>
        /// 发送事件订阅通知
        /// </summary>
        /// <param name="ex"></param>
        protected void OnErrorOccurs(Exception ex)
        {
            if (ErrorOccurs != null)
                ErrorOccurs(ex);
        }
        /// <summary>
        /// 密钥是否包含私钥
        /// </summary>
        public bool HasPrivateKey
        {
            get { return !this._provider.PublicOnly; }
        }
        /// <summary>
        /// 私钥的xml格式字符串
        /// </summary>
        public string PrivateKey
        {
            get
            {
                return this._provider.ToXmlString(true);
            }
        }
        /// <summary>
        /// 公钥的xml格式字符串
        /// </summary>
        public string PublicKey
        {
            get { return this._provider.ToXmlString(false); }
        }
        /// <summary>
        /// 使用RSA私钥对数据进行签名，PKCS#1签名，使用MD5摘要，输出base64签名字符串
        /// </summary>
        /// <param name="message">需要签名的数据</param>
        /// <returns></returns>
        public string SignData(string message)
        {
            return SignData(message, "MD5");
        }
        /// <summary>
        /// 对字符串进行签名
        /// </summary>
        /// <param name="message">需要签名的字符串</param>
        /// <param name="algorithm">摘要算法，可用选项：MD5, SHA1, SHA256</param>
        /// <returns></returns>
        public string SignData(string message, string algorithm)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            byte[] res = SignData(buffer, algorithm);
            return Convert.ToBase64String(res);
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="algorithm">可用选项：MD5, SHA1, SHA256</param>
        /// <returns></returns>
        public byte[] SignData(byte[] buffer, string algorithm)
        {
            byte[] hv;
            if ("MD5".Equals(algorithm, StringComparison.OrdinalIgnoreCase))
            {
                hv = MD5.Create().ComputeHash(buffer);
            }
            else if ("SHA1".Equals(algorithm, StringComparison.OrdinalIgnoreCase))
            {
                hv = SHA1.Create().ComputeHash(buffer);
            }
            else if ("SHA256".Equals(algorithm, StringComparison.OrdinalIgnoreCase))
            {
                hv = SHA256.Create().ComputeHash(buffer);
            }
            else
            {
                throw new InvalidOperationException("algorithm");
            }
            RSAPKCS1SignatureFormatter rsf = new RSAPKCS1SignatureFormatter(this._provider);
            rsf.SetHashAlgorithm(algorithm);
            return rsf.CreateSignature(hv);
        }
        /// <summary>
        /// 使用RSA公钥对数据进行签名验证,MD5算法
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public bool VerifyData(byte[] buffer, byte[] signature)
        {
            //byte[] hv = System.Security.Cryptography.MD5.Create().ComputeHash(buffer);
            //RSAPKCS1SignatureDeformatter rsdf = new RSAPKCS1SignatureDeformatter(this._provider);
            //rsdf.SetHashAlgorithm("MD5");
            //return rsdf.VerifySignature(hv, signature);
            return VerifyData(buffer, signature, "MD5");
        }
        /// <summary>
        /// 使用RSA公钥进行签名验证,PKCS#1签名验证
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="signature"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public bool VerifyData(byte[] buffer, byte[] signature, string algorithm)
        {
            byte[] hv = null;
            if ("MD5".Equals(algorithm, StringComparison.OrdinalIgnoreCase))
            {
                hv = System.Security.Cryptography.MD5.Create().ComputeHash(buffer);
            }
            else if ("SHA1".Equals(algorithm, StringComparison.OrdinalIgnoreCase))
            {
                hv = System.Security.Cryptography.SHA1.Create().ComputeHash(buffer);
            }
            else if ("SHA256".Equals(algorithm, StringComparison.OrdinalIgnoreCase))
            {
                hv = System.Security.Cryptography.SHA256.Create().ComputeHash(buffer);
            }
            else
            {
                throw new InvalidOperationException("algorithm");
            }
            RSAPKCS1SignatureDeformatter rsdf = new RSAPKCS1SignatureDeformatter(this._provider);
            rsdf.SetHashAlgorithm(algorithm);
            return rsdf.VerifySignature(hv, signature);
        }
        /// <summary>
        /// RSA标准算法，不涉及任何填充规则
        /// </summary>
        /// <param name="data"></param>
        /// <param name="E"></param>
        /// <param name="M"></param>
        /// <returns></returns>
        public static byte[] RSA(byte[] data, byte[] E, byte[] M)
        {
            if (data == null || data.Length <= 0)
            {
                return null;
            }
            List<byte> resBytes = new List<byte>();
            int keysize = M.Length;
            if (data.Length > keysize)
            {
                MemoryStream ms = new MemoryStream(data);
                BinaryReader reader = new BinaryReader(ms);
                while (reader.BaseStream.Length - reader.BaseStream.Position > 0)
                {
                    long count = reader.BaseStream.Length - reader.BaseStream.Position >= keysize ? keysize : reader.BaseStream.Length - reader.BaseStream.Position;
                    byte[] part = reader.ReadBytes((int)count);
                    resBytes.AddRange(RSA(part, E, M));
                }
            }
            else
            {
                //System.Numerics.BigInteger 默认是小端编码（litter endian），即低位在前
                //而System.Security.Cryptography.RSAParameter导出的秘钥字节是大端编码（big endian），即高位在前
                //所以初始化大整数对象时需要反转字节数组，即将低位在前的小端编码转换为高位在前的大端编码
                System.Numerics.BigInteger cryptoData = new System.Numerics.BigInteger(data.Reverse().ToArray());
                var e = new System.Numerics.BigInteger(E.Reverse().ToArray());
                var m = new System.Numerics.BigInteger(M.Reverse().ToArray());
                var resInteger = System.Numerics.BigInteger.ModPow(cryptoData, e, m);
                resBytes.AddRange(resInteger.ToByteArray().Reverse());
            }
            return resBytes.ToArray();
        }
        /// <summary>
        /// 移除PKCS#1填充内容
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static byte[] RemovePadding(byte[] buffer)
        {
            //对于PKCS#1填充，若第二个字节为0x01，表示密文为私钥加密。
            byte[] temp = new byte[buffer.Length];
            if ((buffer.Length == 128 && buffer[0] != 0x00) || (buffer.Length == 127 && buffer[0] > 0x02))//若第一个字节不是0x00表示未有PKCS#1填充
            {
                return buffer;
            }
            for (int i = 1; i < buffer.Length; i++)
            {
                if (buffer[i] != 0x00)
                {
                    continue;
                }
                byte[] res = new byte[buffer.Length - i - 1];
                Array.Copy(buffer, i + 1, res, 0, res.Length);
                return res;
            }
            return null;
        }
        /// <summary>
        /// 添加PKCS#1填充
        /// <para>EM = 0x00 + BT + PS + 0x00 + 明文</para>
        /// <para>其中PS至少需要8个字节长度</para>
        /// </summary>
        /// <param name="buffer">需要加密数据</param>
        /// <param name="keysize">密钥字节长度</param>
        /// <param name="isPublicKey">使用使用公钥规则填充</param>
        /// <returns></returns>
        public static byte[] AddPKCS1Padding(byte[] buffer, int keysize, bool isPublicKey)
        {
            if (buffer == null)
            {
                return null;
            }
            //BT
            byte flag = isPublicKey ? (byte)0x02 : (byte)0x01;
            //int singlePartLength = keysize;
            if (buffer.Length <= keysize - 11)
            {
                byte[] res = new byte[keysize];
                int startIndex = res.Length - buffer.Length;
                Array.Copy(buffer, 0L, res, startIndex, buffer.Length);
                res[0] = 0x00;
                res[1] = flag;
                res[startIndex - 1] = 0x00;
                var rand = new Random();
                //PS：公钥加密时，填充随机数；私钥加密时，固定填充0xFF
                for (int i = (startIndex - 2); i > 1; i--)
                {
                    res[i] = flag == 0x02 ? (byte)(rand.Next(1, 0xFF)) : (byte)0xFF;
                }
                return res;
            }
            else
            {
                int diagram = buffer.Length / (keysize - 11);
                if (buffer.Length % (keysize - 11) > 0)
                {
                    diagram++;
                }
                MemoryStream ms = new MemoryStream(buffer);
                BinaryReader reader = new BinaryReader(ms);
                byte[] res = new byte[diagram * keysize];
                for (int i = 0; i < diagram; i++)
                {
                    int count = keysize - 11;
                    if (reader.BaseStream.Length - reader.BaseStream.Position < keysize - 11)
                    {
                        count = (int)(reader.BaseStream.Length - reader.BaseStream.Position);
                    }
                    Array.Copy(AddPKCS1Padding(reader.ReadBytes(count), keysize, isPublicKey), 0, res, i * keysize, keysize);
                }
                return res;
            }
        }
        /// <summary>
        /// RSA加密，使用公钥或者私钥加密都可以
        /// </summary>
        /// <param name="data">需要加密的数据</param>
        /// <param name="doOAEPPadding">是否进行OAEP填充</param>
        /// <param name="usePublicKey">是否使用公钥加密</param>
        /// <returns>密文字节组</returns>
        public byte[] Encrypt(byte[] data, bool doOAEPPadding, bool usePublicKey)
        {
            if (usePublicKey)
            {
                return Encrypt(data, doOAEPPadding);
            }
            if (!this.HasPrivateKey)
            {
                return null;
            }

            var paras = this._provider.ExportParameters(true);
            var paddedData = AddPKCS1Padding(data, this._provider.KeySize / 8, usePublicKey);//添加PKCS#1填充
            var res = RSA(paddedData, paras.D, paras.Modulus);//对D取指数幂，对Modulus取模，得到加密结果
            return res;
        }
        /// <summary>
        /// 解密，使用公钥或私钥解密都可以
        /// </summary>
        /// <param name="data">需要解密的数据</param>
        /// <param name="doOAEPPadding">是否有进行OAEP填充</param>
        /// <param name="usePublicKey">是否使用公钥解密</param>
        /// <returns>解密的明文字节组</returns>
        public byte[] Decrypt(byte[] data, bool doOAEPPadding, bool usePublicKey)
        {
            if (!usePublicKey)
            {
                return Decrypt(data, doOAEPPadding);
            }
            var decryptoData = new byte[data.Length];
            var paras = this._provider.ExportParameters(false);
            MemoryStream ms = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(ms);
            List<byte> resArray = new List<byte>();
            while (reader.BaseStream.Length - reader.BaseStream.Position > 0)
            {
                var tmp = reader.ReadBytes(this._provider.KeySize / 8);
                var decryptTmp = RemovePadding(RSA(tmp, paras.Exponent, paras.Modulus));
                resArray.AddRange(decryptTmp);
            }
            return resArray.ToArray();
        }
        /// <summary>
        /// 使用私钥加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] EncryptByPrivate(byte[] data)
        {
            //var xml = this._provider.ToXmlString(true);
            var parameter = this._provider.ExportParameters(true);
            var pub = new Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters(false, new Org.BouncyCastle.Math.BigInteger(1, parameter.Modulus),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.Exponent));
            var pvt = new Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters(
                new Org.BouncyCastle.Math.BigInteger(1, parameter.Modulus),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.Exponent),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.D),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.P),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.Q),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.DP),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.DQ),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.InverseQ)
                );
            RsaKeyHelper helper = new RsaKeyHelper(pub, pvt);
            //var helper = RsaKeyHelper.FromXmlKey(xml);
            return helper.EncryptByPrivate(data);
        }
        /// <summary>
        /// 使用公钥解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] DecryptByPublic(byte[] data)
        {
            //var xml = this._provider.ToXmlString(false);
            var parameter = this._provider.ExportParameters(false);
            var pub = new Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters(false,
                new Org.BouncyCastle.Math.BigInteger(1, parameter.Modulus),
                new Org.BouncyCastle.Math.BigInteger(1, parameter.Exponent));
            var helper = new RsaKeyHelper(pub, null);
            return helper.DecryptByPublic(data);
        }
    }
}
