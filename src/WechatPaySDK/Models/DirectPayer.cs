using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 直连商户支付者对象
    /// </summary>
    public class DirectPayer
    {
        [JsonProperty("openid")]
        public string OpenID { get; set; }
    }
}
