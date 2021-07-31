using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Models
{
    /// <summary>
    /// 优惠信息
    /// </summary>
    public class DiscountInfo
    {
        /// <summary>
        /// 订单原价
        /// 1、商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的交易金额。
        /// 2、当订单原价与支付金额不相等，则不享受优惠。
        /// 3、该字段主要用于防止同一张小票分多次支付，以享受多次优惠的情况，正常支付订单不必上传此参数。
        /// </summary>
        [JsonProperty("cost_price")]
        public string CostPrice { get; set; }
        /// <summary>
        /// 商品小票ID
        /// </summary>
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }
        /// <summary>
        /// 单品列表
        /// </summary>
        [JsonProperty("goods_detail")]
        public List<GoodsInfo> GoodsDetail { get; set; }
    }
}
