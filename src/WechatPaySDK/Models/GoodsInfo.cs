using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 商品单品信息
    /// </summary>
    public class GoodsInfo
    {
        /// <summary>
        /// 商户侧商品编码。由半角的大小写字母、数字、中划线、下划线中的一种或几种组成。
        /// </summary>
        [JsonProperty("merchant_goods_id")]
        public string MerchantGoodsId { get; set; }
        /// <summary>
        /// 微信侧商品编码，微信支付定义的统一商品编号（没有可不传）
        /// </summary>
        [JsonProperty("wechatpay_goods_id")]
        public string WechatPayGoodsId { get; set; }
        /// <summary>
        /// 商品的实际名称
        /// </summary>
        [JsonProperty("goods_name")]
        public string GoodsName { get; set; }
        /// <summary>
        /// 用户购买的数量
        /// </summary>
        [JsonProperty("quantity")]
        public string Quantity { get; set; }
        /// <summary>
        /// 商品单价，单位为分
        /// </summary>
        [JsonProperty("unit_price")]
        public string UnitPrice { get; set; }
    }
}
