using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    public class DomesticRefundResponse : WxPayResponseBase
    {
        /// <summary>
        /// 微信支付退款号
        /// </summary>
        [JsonProperty("refund_id")]
        public string RefundId { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        [JsonProperty("out_refund_no")]
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 微信支付订单号，微信支付交易订单号。
        /// </summary>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
        /// <summary>
        /// 商户订单号,原支付交易对应的商户订单号。
        /// </summary>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 退款渠道
        /// 枚举值：
        /// ORIGINAL：原路退款
        /// BALANCE：退回到余额
        /// OTHER_BALANCE：原账户异常退到其他余额账户
        /// OTHER_BANKCARD：原银行卡异常退到其他银行卡
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }
        /// <summary>
        /// 退款入账账户
        /// 取当前退款单的退款入账方，有以下几种情况：
        /// 1）退回银行卡：{银行名称}{卡类型} { 卡尾号}
        /// 2）退回支付用户零钱：支付用户零钱
        /// 3）退还商户：商户基本账户 / 商户结算银行账户
        /// 4）退回支付用户零钱通：支付用户零钱通。
        /// </summary>
        [JsonProperty("user_received_account")]
        public string UserReceivedAccount { get; set; }
        /// <summary>
        /// 退款成功时间
        /// </summary>
        [JsonProperty("success_time")]
        public string SuccessTime { get; set; }
        /// <summary>
        /// 退款创建时间
        /// </summary>
        [JsonProperty("create_time")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 退款状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// 资金账户
        /// 退款所使用资金对应的资金账户类型。 枚举值：
        /// UNSETTLED : 未结算资金
        /// AVAILABLE : 可用余额
        /// UNAVAILABLE : 不可用余额
        /// OPERATION : 运营户
        /// </summary>
        [JsonProperty("funds_account")]
        public string FundsAccount { get; set; }
        /// <summary>
        /// 金额详细信息。
        /// </summary>
        [JsonProperty("amount")]
        public DomesticRefundResponseMoney Amount { get; set; }
        /// <summary>
        /// 优惠退款信息。
        /// </summary>
        [JsonProperty("promotion_detail")]
        public List<DomesticRefundResponsePromotion> PromotionDetail { get; set; }

        public class DomesticRefundResponseMoney
        {
            [JsonProperty("total")]
            public string Total { get; set; }
            [JsonProperty("refund")]
            public string Refund { get; set; }
            [JsonProperty("payer_total")]
            public string PayerTotal { get; set; }
            [JsonProperty("payer_refund")]
            public string PayerRefund { get; set; }
            [JsonProperty("settlement_refund")]
            public string SettlementRefund { get; set; }
            [JsonProperty("settlement_total")]
            public string SettlementTotal { get; set; }
            [JsonProperty("discount_refund")]
            public string DiscountRefund { get; set; }
            [JsonProperty("currency")]
            public string Currency { get; set; }
        }
        public class DomesticRefundResponsePromotion
        {
            [JsonProperty("promotion_id")]
            public string PromotionId { get; set; }
            [JsonProperty("scope")]
            public string Scope { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("amount")]
            public string Amount { get; set; }
            [JsonProperty("refund_amount")]
            public string RefundAmount { get; set; }
            [JsonProperty("goods_detail")]
            public List<RefundGoodsDetail> GoodsDetail { get; set; }
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
