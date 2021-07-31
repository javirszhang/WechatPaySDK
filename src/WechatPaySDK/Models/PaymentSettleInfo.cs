using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    public class SettleInfo
    {
        /// <summary>
        /// 是否指定分账
        /// </summary>
        [JsonProperty("profit_sharing")]
        public bool ProfitSharing { get; set; }
    }
    /// <summary>
    /// 支付结算信息
    /// </summary>
    public class PaymentSettleInfo : SettleInfo
    {

        /// <summary>
        /// 补差金额，单位分
        /// </summary>
        [JsonProperty("subsidy_amount")]
        public long SubsidyAmount { get; set; }
    }
}
