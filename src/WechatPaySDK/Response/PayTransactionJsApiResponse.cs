using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    public class PayTransactionJsApiResponse : WxPayResponseBase
    {
        /// <summary>
        /// 预支付交易会话标识。用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        [JsonProperty("prepay_id")]
        public string PrepareId { get; set; }
    }
}
