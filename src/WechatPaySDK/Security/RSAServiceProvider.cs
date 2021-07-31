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
            RSAPKCS1SignatureDeformatter rsdf = new RSAPKCS1SignatureDeformatter(this._provider);
            rsdf.SetHashAlgorithm(algorithm);
            return rsdf.VerifySignature(hv, signature);
        }
    }
}
