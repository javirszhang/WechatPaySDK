using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    public class PayPartnerTransactionWapResponse : WxPayResponseBase
    {
        /// <summary>
        /// h5_url为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付，h5_url的有效期为5分钟。
        /// </summary>
        [JsonProperty("h5_url")]
        public string H5Url { get; set; }
    }
}
