using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 优惠信息
    /// </summary>
    public class PromotionInfo
    {
        /// <summary>
        /// 券ID
        /// </summary>
        [JsonProperty("coupon_id")]
        public string CouponId { get; set; }
        /// <summary>
        /// 优惠名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 优惠范围
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
        /// <summary>
        /// 优惠类型
        /// CASH：充值
        /// NOCASH：预充值
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// 优惠券面额，单位分
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        [JsonProperty("stock_id")]
        public string StockId { get; set; }
        /// <summary>
        /// 微信出资
        /// </summary>
        [JsonProperty("wechatpay_contribute")]
        public string WechatPayContribute { get; set; }
        /// <summary>
        /// 商户出资
        /// </summary>
        [JsonProperty("merchant_contribute")]
        public string MerchantContribute { get; set; }
        /// <summary>
        /// 其他出资
        /// </summary>
        [JsonProperty("other_contribute")]
        public string OtherContribute { get; set; }
        /// <summary>
        /// 优惠币种，CNY：人民币，境内商户号仅支持人民币。
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
        /// <summary>
        /// 单品列表信息
        /// </summary>
        [JsonProperty("goods_detail")]
        public List<PromotionGoods> GoodsDetail { get; set; }
    }
}
