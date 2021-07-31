using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Security
{
    public static class RSAUtils
    {
        public static string SignWithSHA256(string privateKey, string signMessage)
        {
            var rsa = PemCertificate.ReadFromKeyString(privateKey);
            var buffer = rsa.SignData(Encoding.UTF8.GetBytes(signMessage), "SHA256");
            return Convert.ToBase64String(buffer);
        }

        public static bool VerifySign(byte[] signature, byte[] content, string publicKey)
        {
            var rsa = PemCertificate.ReadFromKeyString(publicKey);
            return rsa.VerifyData(content, signature, "SHA256");
        }

        public static bool VerifySign(string signature, string content, string publicKey)
        {
            byte[] signBuf = Convert.FromBase64String(signature);
            byte[] cntBuf = Encoding.UTF8.GetBytes(content);
            return VerifySign(signBuf, cntBuf, publicKey);
        }
    }
}
