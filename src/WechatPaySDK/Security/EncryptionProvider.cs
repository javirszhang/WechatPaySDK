using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Security
{
    public class EncryptionProvider
    {
        private readonly WechatAccount Config;
        public EncryptionProvider(WechatAccount wechatConfig)
        {
            this.Config = wechatConfig;
        }
        private string Encode(string plainText, WechatKey key)
        {
            PemCertificate rsa = PemCertificate.ReadFromKeyString(key.PublicKey);
            string cipherText = rsa.Encrypt(plainText, Encoding.UTF8, true);
            this.IsEncrypted = true;
            return cipherText;
        }
        /// <summary>
        /// 是否已加密
        /// </summary>
        public bool IsEncrypted { get; private set; }

        public void Encrypt(object obj, ref WechatKey key)
        {
            if (key == null)
            {
                key = this.Config.GetPlatformKey();
            }
            if (obj == null)
            {
                return;
            }
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in properties)
            {
                //System.Diagnostics.Debug.Assert(p.Name != "GoodsDetail");                
                if (p.PropertyType.IsArray || (p.PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(p.PropertyType)))
                {
                    var array = p.GetValue(obj) as IEnumerable;
                    if (array != null)
                    {
                        foreach (var item in array)
                        {
                            var itemType = item.GetType();
                            if (itemType.IsClass && itemType != typeof(string))
                            {
                                Encrypt(item, ref key);
                            }
                        }
                    }
                }
                else if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                {
                    Encrypt(p.GetValue(obj), ref key);
                }
                else if (p.GetCustomAttribute<EncryptionRequiredAttribute>() != null)
                {
                    string val = p.GetValue(obj)?.ToString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        p.SetValue(obj, Encode(val, key));
                    }
                }
            }
        }
    }
}
