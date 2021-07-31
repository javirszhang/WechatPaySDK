using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WechatPaySDK.Request;
using WechatPaySDK.Security;

namespace WechatPaySDK
{
    /// <summary>
    /// 微信支付配置
    /// </summary>
    public sealed class WechatAccount
    {

        private WechatAccount()
        { }
        public static WechatAccount GetConfig(string merchantId, string apiV3Key, string apiDomain, WechatKey merchantKey)
        {
            var config = new WechatAccount
            {
                MerchantId = merchantId,
                ApiV3Key = apiV3Key,
                ApiDomain = apiDomain
            };
            config.Keys.Add(merchantKey);
            return config;
        }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantId { get; set; }
        /// <summary>
        /// APIv3的秘钥
        /// </summary>
        public string ApiV3Key { get; set; }
        /// <summary>
        /// API主域名，不要斜杆结尾
        /// </summary>
        public string ApiDomain { get; set; }
        /// <summary>
        /// 微信支付CA证书集合
        /// </summary>
        public List<WechatKey> Keys { get; set; } = new List<WechatKey>();
        private static readonly object lockObj = new object();
        public WechatKey GetPlatformKey(string serialNo = null)
        {
            WechatKey key = string.IsNullOrEmpty(serialNo) ? Keys.FirstOrDefault(t => t.Owner == "platform") : Keys.Find(t => t.SerialNo == serialNo);
            if (key != null)
            {
                return key;
            }
            lock (lockObj)
            {
                if (key == null || (key.ExpireTime.HasValue && key.ExpireTime.Value < DateTime.Now.AddHours(-12)))
                {
                    var client = new WechatClient(this, null);
                    var response = client.Execute(new DownloadCertificatesRequest());
                    if (response.Success())
                    {
                        foreach (var item in response.Data)
                        {
                            var plainKey = AesGcm.AesGcmDecrypt(this.ApiV3Key, item.EncryptCertificate);
                            key = new WechatKey
                            {
                                Owner = "platform",
                                KeyFormat = "cert",
                                PublicKey = plainKey,
                                SerialNo = item.SerialNo,
                                ExpireTime = item.ExpireTime
                            };
                            this.Keys.RemoveAll(k => k.SerialNo == item.SerialNo);
                            this.Keys.Add(key);
                        }
                    }
                }
            }
            return string.IsNullOrEmpty(serialNo) ? Keys.FirstOrDefault(t => t.Owner == "platform") : Keys.Find(t => t.SerialNo == serialNo);
        }
    }

    /// <summary>
    /// 证书秘钥信息
    /// </summary>
    public class WechatKey
    {
        /// <summary>
        /// 证书序列号
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }
        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; set; }
        /// <summary>
        /// 密钥格式（pkcs1，pkcs8，cert）
        /// </summary>
        public string KeyFormat { get; set; }
        /// <summary>
        /// 密钥拥有者，商户密钥=merchant，微信支付平台密钥=platform
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 秘钥过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }
    }
}
