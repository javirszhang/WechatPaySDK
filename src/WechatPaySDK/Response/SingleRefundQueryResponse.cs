using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    public class SingleRefundQueryResponse : WxPayResponseBase
    {
        /// <summary>
        /// 金额信息
        /// </summary>
        [JsonProperty("amount")]
        public RefundAmountInfo Amount { get; set; }
        /// <summary>
        /// 退款渠道
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }
        /// <summary>
        /// 退款创建时间
        /// </summary>
        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 资金账户
        /// </summary>
        [JsonProperty("funds_account")]
        public string FundsAccount { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        [JsonProperty("out_refund_no")]
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 优惠退款信息
        /// </summary>
        [JsonProperty("promotion_detail")]
        public object[] PromotionDetail { get; set; }
        /// <summary>
        /// 微信支付退款号
        /// </summary>
        [JsonProperty("refund_id")]
        public string RefundId { get; set; }
        /// <summary>
        /// 退款状态
        /// 退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往商户平台-交易中心，手动处理此笔退款。<para></para>
        /// 枚举值：
        /// SUCCESS：退款成功
        /// CLOSED：退款关闭
        /// PROCESSING：退款处理中
        /// ABNORMAL：退款异常<para></para>
        /// 示例值：SUCCESS
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// 退款成功时间
        /// </summary>
        [JsonProperty("success_time")]
        public DateTime SuccessTime { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
        /// <summary>
        /// 退款入账账户
        /// </summary>
        [JsonProperty("user_received_account")]
        public string UserReceivedAccount { get; set; }
        public class RefundAmountInfo
        {
            /// <summary>
            /// 退款币种
            /// </summary>
            [JsonProperty("currency")] public string Currency { get; set; }
            /// <summary>
            /// 优惠退款金额
            /// </summary>
            [JsonProperty("discount_refund")] public int DiscountRefund { get; set; }
            /// <summary>
            /// 用户退款金额
            /// </summary>
            [JsonProperty("payer_refund")] public int PayerRefund { get; set; }
            /// <summary>
            /// 用户支付金额
            /// </summary>
            [JsonProperty("payer_total")] public int PayerTotal { get; set; }
            /// <summary>
            /// 退款金额
            /// </summary>
            [JsonProperty("refund")] public int Refund { get; set; }
            /// <summary>
            /// 应结退款金额
            /// </summary>
            [JsonProperty("settlement_refund")] public int SettlementRefund { get; set; }
            /// <summary>
            /// 应结订单金额
            /// </summary>
            [JsonProperty("settlement_total")] public int SettlementTotal { get; set; }
            /// <summary>
            /// 订单金额
            /// </summary>
            [JsonProperty("total")] public int Total { get; set; }
        }
    }
}
