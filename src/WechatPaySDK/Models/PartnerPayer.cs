using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 收付通支付者对象
    /// </summary>
    public class PartnerPayer
    {
        /// <summary>
        /// 用户服务标识，用户在服务商appid下的唯一标识。
        /// </summary>
        [JsonProperty("sp_openid")]
        public string SPOpenID { get; set; }
        /// <summary>
        /// 用户子标识，用户在子商户appid下的唯一标识。
        /// </summary>
        [JsonProperty("sub_openid")]
        public string SubOpenID { get; set; }
    }
}
