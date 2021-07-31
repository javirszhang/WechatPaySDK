using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 门店信息
    /// </summary>
    public class StoreInfo
    {
        /// <summary>
        /// 门店编号，商户侧门店编号
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }
        /// <summary>
        /// 商户侧门店名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 地区编码
        /// </summary>
        [JsonProperty("area_code")]
        public string AreaCode { get; set; }
        /// <summary>
        /// 详细的商户门店地址
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
