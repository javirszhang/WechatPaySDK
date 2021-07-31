using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 支付场景
    /// </summary>
    public class PayScene
    {
        [JsonProperty("payer_client_ip")]
        public string PayerClientIP { get; set; }
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
        [JsonProperty("store_info")]
        public StoreInfo StoreInfo { get; set; }
    }
}
