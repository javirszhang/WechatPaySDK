using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace WechatPaySDK.Security
{
    /// <summary>
    /// RSA密钥帮助类
    /// </summary>
    internal class RsaKeyHelper
    {
        /// <summary>
        /// RSA密钥帮助类
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        public RsaKeyHelper(RsaKeyParameters publicKey, RsaPrivateCrtKeyParameters privateKey)
        {
            this.Private = privateKey;
            this.Public = publicKey;
        }
        /// <summary>
        /// 私钥
        /// </summary>
        public RsaPrivateCrtKeyParameters Private
        {
            get; private set;
        }
        /// <summary>
        /// 公钥
        /// </summary>
        public RsaKeyParameters Public
        {
            get; private set;
        }
        /// <summary>
        /// 密钥格式，默认PKCS1
        /// </summary>
        public KeyFormat Format { get; set; } = KeyFormat.pkcs1;
        /// <summary>
        /// 获取密钥字符串
        /// </summary>
        /// <param name="includePrivate"></param>
        /// <returns></returns>
        public string GetKeyString(bool includePrivate = false)
        {
            if (includePrivate && this.Private == null)
            {
                return null;
            }
            object pemObject = includePrivate ? this.Private : this.Public;
            if (includePrivate && this.Format == KeyFormat.pkcs8)
            {
                pemObject = new Pkcs8Generator(this.Private);
            }
            StringWriter sw = new StringWriter();
            PemWriter pWrt = new PemWriter(sw);
            pWrt.WriteObject(pemObject);
            pWrt.Writer.Close();
            return sw.ToString();

            //StringWriter sw = new StringWriter();
            //PemWriter pWrt = new PemWriter(sw);
            //pWrt.WriteObject(rsaKeyParameters);
            //pWrt.Writer.Close();
            //return sw.ToString();
        }
        /// <summary>
        /// 转换为xml密钥
        /// </summary>
        /// <param name="includePrivate"></param>
        /// <returns></returns>
        public string ToXmlString(bool includePrivate)
        {
            if (this.Private != null)
            {
                return PrivateKeyToXml((RsaPrivateCrtKeyParameters)this.Private, includePrivate);
            }
            return PublicToXml((RsaKeyParameters)this.Public);
        }
        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public byte[] EncryptByPrivate(byte[] cipher)
        {
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            engine.Init(true, this.Private);
            int blockSize = engine.GetInputBlockSize(); //(engine.GetInputBlockSize() / 8) - 11;

            List<byte> result = new List<byte>();
            int pos = 0;
            while (pos < cipher.Length)
            {
                int len = cipher.Length - pos < blockSize ? cipher.Length - pos : blockSize;
                byte[] tmp = new byte[len];
                Array.Copy(cipher, pos, tmp, 0, len);
                var buffer = engine.ProcessBlock(tmp, 0, cipher.Length);
                result.AddRange(buffer);
                pos += len;
            }
            return result.ToArray();
        }
        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public byte[] DecryptByPublic(byte[] buffer)
        {
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            engine.Init(false, this.Public);
            return engine.ProcessBlock(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 转换密钥格式
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public RsaKeyHelper ConvertTo(KeyFormat format)
        {
            this.Format = format;
            return this;
        }
        /// <summary>
        /// 生成RSA密钥
        /// </summary>
        /// <param name="size">1024或2048</param>
        /// <returns></returns>
        public static RsaKeyHelper GenRsaKey(int size)
        {
            if (size == 0 || (size % 1024) != 0)
            {
                throw new ArgumentFormatException("size数值不正确，只能在1024和2048选择一个");
            }
            RsaKeyPairGenerator rsaGen = new RsaKeyPairGenerator();
            rsaGen.Init(new KeyGenerationParameters(new SecureRandom(), size));
            AsymmetricCipherKeyPair keys = rsaGen.GenerateKeyPair();
            RsaKeyHelper helper = new RsaKeyHelper((RsaKeyParameters)keys.Public, (RsaPrivateCrtKeyParameters)keys.Private);
            return helper;
        }
        /// <summary>
        /// 从私钥字符串初始化
        /// </summary>
        /// <param name="keyString">密钥字符串，必须带有格式声明</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static RsaKeyHelper FromPemKeyString(string keyString, string password = null)
        {
            try
            {
                keyString = FormatKeyString(keyString);

                PemReader reader = null;
                if (!string.IsNullOrEmpty(password))
                {
                    reader = new PemReader(new StringReader(keyString), new MyPasswordFinder(password));
                }
                else
                {
                    reader = new PemReader(new StringReader(keyString));
                }
                var tmpObj = reader.ReadObject();
                if (tmpObj is AsymmetricCipherKeyPair keys)//pkcs1 private key
                {
                    return new RsaKeyHelper((RsaKeyParameters)keys.Public, (RsaPrivateCrtKeyParameters)keys.Private);
                }
                else if (tmpObj is RsaPrivateCrtKeyParameters pvtKeys)//pkcs8 private key
                {
                    return new RsaKeyHelper(new RsaKeyParameters(false, pvtKeys.Modulus, pvtKeys.PublicExponent), pvtKeys);
                }
                else if (tmpObj is Org.BouncyCastle.X509.X509Certificate cer)//pem cert
                {
                    return new RsaKeyHelper((RsaKeyParameters)cer.GetPublicKey(), null);
                }
                else if (tmpObj is RsaKeyParameters rkp)//public key
                {
                    return new RsaKeyHelper(rkp, null);
                }
                return null;
                /*
                if (format == KeyFormat.pkcs1)
                {
                    privateKey = FormatKeyString(privateKey, format);
                    PemReader reader = new PemReader(new StringReader(privateKey));
                    var keys = reader.ReadObject() as AsymmetricCipherKeyPair;
                    if (keys == null)
                    {
                        return null;
                    }
                    return new RsaKeyHelper((RsaKeyParameters)keys.Public, (RsaPrivateCrtKeyParameters)keys.Private);
                }
                else
                {
                    //pkcs8
                    var tmp = RemoveKeyStringFormat(privateKey, format);
                    byte[] buffer = Convert.FromBase64String(tmp);
                    RsaPrivateCrtKeyParameters prvt = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(buffer);
                    RsaKeyParameters pub = new RsaKeyParameters(false, prvt.Modulus, prvt.Exponent);
                    var helper = new RsaKeyHelper(pub, prvt);
                    return helper;
                }
                */
            }
            catch (Exception ex)
            {
                throw new InvalidKeyFormatException("密钥格式不正确", ex);
            }
        }
        /// <summary>
        /// 从xml密钥初始化
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <returns></returns>
        public static RsaKeyHelper FromXmlKey(string xmlPrivateKey)
        {
            XElement root = XElement.Parse(xmlPrivateKey);
            //Modulus
            var modulus = root.Element("Modulus");
            var M = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(modulus.Value));
            //Exponent
            var exponent = root.Element("Exponent");
            var E = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(exponent.Value));
            Org.BouncyCastle.Math.BigInteger P = null, Q = null, DP = null, DQ = null, QI = null, D = null;
            //P
            var pXml = root.Element("P");
            if (pXml != null)
            {
                P = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(pXml.Value));
            }
            //Q
            var qXml = root.Element("Q");
            if (qXml != null)
            {
                Q = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(qXml.Value));
            }
            //DP
            var dpXml = root.Element("DP");
            if (dpXml != null)
            {
                DP = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(dpXml.Value));
            }
            //DQ
            var dqXml = root.Element("DQ");
            if (dqXml != null)
            {
                DQ = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(dqXml.Value));
            }
            //InverseQ
            var iqXml = root.Element("InverseQ");
            if (iqXml != null)
            {
                QI = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(iqXml.Value));
            }
            //D
            var dXml = root.Element("D");
            if (dXml != null)
            {
                D = new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(dXml.Value));
            }
            RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = null;
            if (D != null)
            {
                rsaPrivateCrtKeyParameters = new RsaPrivateCrtKeyParameters(M, E, D, P, Q, DP, DQ, QI);
            }
            RsaKeyParameters pubc = new RsaKeyParameters(false, M, E);
            var helper = new RsaKeyHelper(pubc, rsaPrivateCrtKeyParameters);
            return helper;
        }
        /// <summary>
        /// 格式化私钥
        /// </summary>
        /// <param name="key"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatPrivateKey(string key, KeyFormat format)
        {
            string flag = format == KeyFormat.pkcs1 ? "-----{0} RSA PRIVATE KEY-----" : "-----{0} PRIVATE KEY-----";
            if (key.StartsWith(string.Format(flag, "BEGIN")))
            {
                return key;
            }
            int pos = 0;
            List<string> lines = new List<string>();
            lines.Add(string.Format(flag, "BEGIN"));
            key = key.Replace("\r", "").Replace("\n", "");
            while (pos < key.Length)
            {
                var count = key.Length - pos < 64 ? key.Length - pos : 64;
                lines.Add(key.Substring(pos, count));
                pos += count;
            }
            lines.Add(string.Format(flag, "END"));
            return string.Join("\r\n", lines);
        }
        /// <summary>
        /// 格式化密钥字符串，格式声明头和尾各独占一行，密钥体每64字节一行
        /// </summary>
        /// <returns></returns>
        public static string FormatKeyString(string keyString)
        {
            if (string.IsNullOrEmpty(keyString))
            {
                return keyString;
            }
            string lineKeyString = keyString.Replace("\r", "").Replace("\n", "");
            string pattern = "(-----BEGIN\\s.+?-----).+?-----END\\s.+?-----$";
            if (!Regex.IsMatch(lineKeyString, pattern))
            {
                return keyString;
            }
            string head = Regex.Replace(lineKeyString, pattern, "$1");
            string end = head.Replace("BEGIN", "END");
            lineKeyString = lineKeyString.Replace(head, "").Replace(end, "");//去掉声明头和声明脚
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(head);
            int pos = 0;
            while (pos < lineKeyString.Length)
            {
                var count = lineKeyString.Length - pos < 64 ? lineKeyString.Length - pos : 64;
                builder.AppendLine(lineKeyString.Substring(pos, count));
                pos += count;
            }
            builder.AppendLine(end);
            return builder.ToString();
        }
        /// <summary>
        /// Format public key
        /// </summary>
        /// <param name="publicString"></param>
        /// <returns></returns>
        public static string FormatPublicKey(string publicString)
        {
            if (publicString.StartsWith("-----BEGIN PUBLIC KEY-----"))
            {
                return publicString;
            }
            publicString = publicString.Replace("\r\n", "");
            List<string> res = new List<string>();
            res.Add("-----BEGIN PUBLIC KEY-----");
            int pos = 0;

            while (pos < publicString.Length)
            {
                var count = publicString.Length - pos < 64 ? publicString.Length - pos : 64;
                res.Add(publicString.Substring(pos, count));
                pos += count;
            }
            res.Add("-----END PUBLIC KEY-----");
            var resStr = string.Join("\r\n", res);
            return resStr;
        }
        /// <summary>
        /// Private Key Convert to xml
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="includePrivate"></param>
        /// <returns></returns>
        private static string PrivateKeyToXml(RsaPrivateCrtKeyParameters parameters, bool includePrivate)
        {
            XElement privatElement = new XElement("RSAKeyValue");
            //Modulus
            XElement primodulus = new XElement("Modulus", Convert.ToBase64String(parameters.Modulus.ToByteArrayUnsigned())); ;
            //Exponent
            XElement priexponent = new XElement("Exponent", Convert.ToBase64String(parameters.PublicExponent.ToByteArrayUnsigned()));
            //D
            XElement prid = new XElement("D", Convert.ToBase64String(parameters.Exponent.ToByteArrayUnsigned()));
            //P
            XElement prip = new XElement("P", Convert.ToBase64String(parameters.P.ToByteArrayUnsigned()));
            //Q
            XElement priq = new XElement("Q", Convert.ToBase64String(parameters.Q.ToByteArrayUnsigned()));
            //DP
            XElement pridp = new XElement("DP", Convert.ToBase64String(parameters.DP.ToByteArrayUnsigned()));
            //DQ
            XElement pridq = new XElement("DQ", Convert.ToBase64String(parameters.DQ.ToByteArrayUnsigned()));
            //InverseQ
            XElement priinverseQ = new XElement("InverseQ", Convert.ToBase64String(parameters.QInv.ToByteArrayUnsigned()));

            privatElement.Add(primodulus);
            privatElement.Add(priexponent);
            if (includePrivate)
            {
                privatElement.Add(prip);
                privatElement.Add(priq);
                privatElement.Add(pridp);
                privatElement.Add(pridq);
                privatElement.Add(priinverseQ);
                privatElement.Add(prid);
            }
            return privatElement.ToString(SaveOptions.DisableFormatting);
        }
        /// <summary>
        /// 获取rsa加密服务对象
        /// </summary>
        /// <param name="rsa"></param>
        /// <returns></returns>
        public RSACryptoServiceProvider RSACryptoServiceProvider()
        {
            try
            {
                RSAParameters parameters;
                if (Private != null)
                {
                    var rsa = Private;
                    parameters = new RSAParameters
                    {
                        DP = this.Private.DP.ToByteArrayUnsigned(),
                        DQ = this.Private.DQ.ToByteArrayUnsigned(),
                        Exponent = this.Private.PublicExponent.ToByteArrayUnsigned(),
                        InverseQ = this.Private.QInv.ToByteArrayUnsigned(),
                        D = this.Private.Exponent.ToByteArrayUnsigned(),
                        P = this.Private.P.ToByteArrayUnsigned(),
                        Modulus = this.Private.Modulus.ToByteArrayUnsigned(),
                        Q = this.Private.Q.ToByteArrayUnsigned()
                    };
                }
                else
                {
                    parameters = new RSAParameters
                    {
                        Modulus = this.Public.Modulus.ToByteArrayUnsigned(),
                        Exponent = this.Public.Exponent.ToByteArrayUnsigned()
                    };
                }
                RSACryptoServiceProvider serviceProvider = new RSACryptoServiceProvider();
                serviceProvider.ImportParameters(parameters);
                return serviceProvider;
            }
            catch (Exception ex)
            {
                throw new InvalidKeyFormatException("无法识别的密钥", ex);
            }
        }
        /*
        public static RSACryptoServiceProvider RSACryptorServiceProvider(AsymmetricAlgorithm privateKey)
        {
            parameters = new RSAParameters
            {
                DP = privateKey.DP.ToByteArrayUnsigned(),
                DQ = this.Private.DQ.ToByteArrayUnsigned(),
                Exponent = this.Private.PublicExponent.ToByteArrayUnsigned(),
                InverseQ = this.Private.QInv.ToByteArrayUnsigned(),
                D = this.Private.Exponent.ToByteArrayUnsigned(),
                P = this.Private.P.ToByteArrayUnsigned(),
                Modulus = this.Private.Modulus.ToByteArrayUnsigned(),
                Q = this.Private.Q.ToByteArrayUnsigned()
            };
        }
        */
        /// <summary>
        /// PEM公钥转为xml公钥
        /// </summary>
        /// <param name="pemPublicKeyString"></param>
        /// <returns></returns>
        public static string PublicPemToXml(string pemPublicKeyString)
        {
            string tmp = FormatPublicKey(pemPublicKeyString);
            PemReader pr = new PemReader(new StringReader(tmp));
            var obj = pr.ReadObject();
            if (!(obj is RsaKeyParameters rsaKey))
            {
                throw new InvalidKeyFormatException("Public key format is incorrect");
            }
            return PublicToXml(rsaKey);
        }
        private static string PublicToXml(RsaKeyParameters parameters)
        {
            XElement publicElement = new XElement("RSAKeyValue");
            //Modulus
            XElement pubmodulus = new XElement("Modulus", Convert.ToBase64String(parameters.Modulus.ToByteArrayUnsigned()));
            //Exponent
            XElement pubexponent = new XElement("Exponent", Convert.ToBase64String(parameters.Exponent.ToByteArrayUnsigned()));

            publicElement.Add(pubmodulus);
            publicElement.Add(pubexponent);
            return publicElement.ToString();
        }
        /// <summary>
        /// xml公钥转为pem格式
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string PublicXmlToPem(string xml)
        {
            XElement root = XElement.Parse(xml);
            //Modulus
            var modulus = root.Element("Modulus");
            //Exponent
            var exponent = root.Element("Exponent");

            RsaKeyParameters rsaKeyParameters = new RsaKeyParameters(false, new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(modulus.Value)), new Org.BouncyCastle.Math.BigInteger(1, Convert.FromBase64String(exponent.Value)));

            StringWriter sw = new StringWriter();
            PemWriter pWrt = new PemWriter(sw);
            pWrt.WriteObject(rsaKeyParameters);
            pWrt.Writer.Close();
            return sw.ToString();
        }
        /// <summary>
        /// 密钥格式
        /// </summary>
        public enum KeyFormat
        {
            /// <summary>
            /// pkcs1
            /// </summary>
            pkcs1,
            /// <summary>
            /// pkcs8
            /// </summary>
            pkcs8
        }

        internal class MyPasswordFinder : IPasswordFinder
        {
            private readonly string password;
            public MyPasswordFinder(string pwd)
            {
                this.password = pwd;
            }
            public char[] GetPassword()
            {
                return password.ToCharArray();
            }
        }
    }
}
