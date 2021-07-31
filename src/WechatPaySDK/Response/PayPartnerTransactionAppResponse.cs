using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 普通支付APP下单返回参数
    /// </summary>
    public class PayPartnerTransactionAppResponse : WxPayResponseBase
    {
        /// <summary>
        /// 预支付交易会话标识
        /// </summary>
        [JsonProperty("prepay_id")]
        public string PrepayId { get; set; }
    }
}
