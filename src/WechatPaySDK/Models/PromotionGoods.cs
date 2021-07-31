using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 优惠商品信息
    /// </summary>
    public class PromotionGoods
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        [JsonProperty("goods_id")]
        public string GoodsId { get; set; }
        /// <summary>
        /// 商品数量，用户购买的数量
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        /// <summary>
        /// 商品单价，单位为分
        /// </summary>
        [JsonProperty("unit_price")]
        public int UnitPrice { get; set; }
        /// <summary>
        /// 商品优惠金额
        /// </summary>
        [JsonProperty("discount_amount")]
        public int DiscountAmount { get; set; }
        /// <summary>
        /// 商品备注信息
        /// </summary>
        [JsonProperty("goods_remark")]
        public string GoodsRemark { get; set; }
    }
}
