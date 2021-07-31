using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Request
{
    public partial class DomesticRefundRequest
    {
        public class DomesticRefundModel : WxPayObject
        {
            /// <summary>
            /// 子商户的商户号，由微信支付生成并下发。必填
            /// </summary>
            [JsonProperty("sub_mchid")]
            public string SubMchId { get; set; }
            /// <summary>
            /// 原支付交易对应的微信订单号。二选一
            /// </summary>
            [JsonProperty("transaction_id")]
            public string TransactionID { get; set; }
            /// <summary>
            /// 原支付交易对应的商户订单号。二选一
            /// </summary>
            [JsonProperty("out_trade_no")]
            public string OutTradeNo { get; set; }
            /// <summary>
            /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
            /// </summary>
            [JsonProperty("out_refund_no")]
            public string OutRefundNo { get; set; }
            /// <summary>
            /// 若商户传入，会在下发给用户的退款消息中体现退款原因。
            /// </summary>
            [JsonProperty("reason")]
            public string Reason { get; set; }
            /// <summary>
            /// 异步接收微信支付退款结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。 如果参数中传了notify_url，则商户平台上配置的回调地址将不会生效，优先回调当前传的这个地址。
            /// </summary>
            [JsonProperty("notify_url")]
            public string NotifyUrl { get; set; }
            /// <summary>
            /// 若传递此参数则使用对应的资金账户退款，否则默认使用未结算资金退款（仅对老资金流商户适用）。枚举值：AVAILABLE
            /// </summary>
            [JsonProperty("funds_account")]
            public string FundsAccount { get; set; }
            /// <summary>
            /// 订单金额信息。
            /// </summary>
            [JsonProperty("amount")]
            public RefundAmountMoney Amount { get; set; }
            /// <summary>
            /// 指定商品退款需要传此参数，其他场景无需传递。
            /// </summary>
            [JsonProperty("goods_detail")]
            public List<RefundGoodsDetail> GoodsDetail { get; set; } = new List<RefundGoodsDetail>();
        }
        public class RefundAmountMoney
        {
            /// <summary>
            /// 退款金额，单位为分，只能为整数，不能超过原订单支付金额
            /// </summary>
            [JsonProperty("refund")]
            public int Refund { get; set; }
            /// <summary>
            /// 原支付交易的订单总金额，单位为分，只能为整数。
            /// </summary>
            [JsonProperty("total")]
            public int Total { get; set; }
            /// <summary>
            /// 符合ISO 4217标准的三位字母代码，目前只支持人民币：CNY。
            /// </summary>
            [JsonProperty("currency")]
            public string Currency { get; set; }
        }
        public class RefundGoodsDetail
        {
            /// <summary>
            /// 商户侧商品编码,由半角的大小写字母、数字、中划线、下划线中的一种或几种组成。
            /// </summary>
            [JsonProperty("merchant_goods_id")]
            public string MerchantGoodsId { get; set; }
            /// <summary>
            /// 微信侧商品编码，微信支付定义的统一商品编号（没有可不传）。
            /// </summary>
            [JsonProperty("wechatpay_goods_id")]
            public string WechatpayGoodsId { get; set; }
            /// <summary>
            /// 商品名称
            /// </summary>
            [JsonProperty("goods_name")]
            public string GoodsName { get; set; }
            /// <summary>
            /// 商品单价，单位为分。
            /// </summary>
            [JsonProperty("unit_price")]
            public string UnitPrice { get; set; }
            /// <summary>
            /// 商品退款金额，单位为分。
            /// </summary>
            [JsonProperty("refund_amount")]
            public string RefundAmount { get; set; }
            /// <summary>
            /// 商品退货数量
            /// </summary>
            [JsonProperty("refund_quantity")]
            public string RefundQuantity { get; set; }
        }
    }
}
