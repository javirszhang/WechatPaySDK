using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Domain;

namespace WechatPaySDK.Security
{
    public static class AesGcm
    {
        //private const string ALGORITHM = "AES/GCM/NoPadding";
        //private const int TAG_LENGTH_BIT = 128;
        //private const int NONCE_LENGTH_BYTE = 12;
        //private const string AES_KEY = "yourkeyhere";

        public static string AesGcmDecrypt(string aesKey, string associatedData, string nonce, string ciphertext)
        {
            GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            AeadParameters aeadParameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(aesKey)),
                128,
                Encoding.UTF8.GetBytes(nonce),
                Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);

            byte[] data = Convert.FromBase64String(ciphertext);
            byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return Encoding.UTF8.GetString(plaintext);
        }

        public static string AesGcmDecrypt(string aesKey, CipherCertificate certificate)
        {
            return AesGcmDecrypt(aesKey, certificate.AssociatedData, certificate.Nonce, certificate.CipherText);
        }
    }
}
