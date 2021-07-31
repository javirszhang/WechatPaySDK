using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 二级商户实时余额查询
    /// </summary>
    public class EcommerceFundBalanceResponse : WxPayResponseBase
    {
        /// <summary>
        /// 二级商户号
        /// </summary>
        [JsonProperty("sub_mchid")]
        public string SubMchID { get; set; }
        /// <summary>
        /// 当前实时可用余额
        /// </summary>
        [JsonProperty("available_amount")]
        public string AvailableAmount { get; set; }
        /// <summary>
        /// 在途金额，不可用余额，待入账金额
        /// </summary>
        [JsonProperty("pending_amount")]
        public string PendingAmount { get; set; }
    }
}
