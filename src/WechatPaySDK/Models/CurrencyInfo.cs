using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    public class CurrencyInfo
    {
        public CurrencyInfo(int amount)
        {
            this.Amount = amount;
            this.Currency = "CNY";
        }
        /// <summary>
        /// 总金额，订单总金额，单位为分。
        /// </summary>
        [JsonProperty("total")]
        public int Amount { get; set; }
        /// <summary>
        /// 货币类型，CNY：人民币，境内商户号仅支持人民币。
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
