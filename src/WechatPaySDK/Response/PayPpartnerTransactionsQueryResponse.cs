using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Models;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 交易查询返回值
    /// </summary>
    public class PayPpartnerTransactionsQueryResponse : WxPayResponseBase
    {
        /// <summary>
        /// 服务商公众号ID
        /// </summary>
        [JsonProperty("sp_appid")]
        public string SpAppId { get; set; }
        /// <summary>
        /// 服务商户号
        /// </summary>
        [JsonProperty("sp_mchid")]
        public string SpMerchantID { get; set; }
        /// <summary>
        /// 二级商户公众号ID
        /// </summary>
        [JsonProperty("sub_appid")]
        public string SubAppId { get; set; }
        /// <summary>
        /// 二级商户号
        /// </summary>
        [JsonProperty("sub_mchid")]
        public string SubMerchantId { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
        /// <summary>
        /// 交易类型；枚举值：
        /// JSAPI：公众号支付
        /// NATIVE：扫码支付
        /// APP：APP支付
        /// MICROPAY：付款码支付
        /// MWEB：H5支付
        /// FACEPAY：刷脸支付
        /// </summary>
        [JsonProperty("trade_type")]
        public string TradeType { get; set; }
        /// <summary>
        /// 交易状态，枚举值：
        /// SUCCESS：支付成功
        /// REFUND：转入退款
        /// NOTPAY：未支付
        /// CLOSED：已关闭
        /// REVOKED：已撤销（付款码支付）
        /// USERPAYING：用户支付中（付款码支付）
        /// PAYERROR：支付失败(其他原因，如银行返回失败)
        /// </summary>
        [JsonProperty("trade_state")]
        public string TradeState { get; set; }
        /// <summary>
        /// 交易状态描述
        /// </summary>
        [JsonProperty("trade_state_desc")]
        public string TradeStateDesc { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        [JsonProperty("bank_type")]
        public string BankType { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        [JsonProperty("attach")]
        public string Attach { get; set; }
        /// <summary>
        /// 支付完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。
        /// </summary>
        [JsonProperty("success_time")]
        public string SuccessTime { get; set; }
        /// <summary>
        /// 支付者信息
        /// </summary>
        [JsonProperty("payer")]
        public PartnerPayer Payer { get; set; }
        [JsonProperty("amount")]
        public CurrencyInfo Amount { get; set; }
        [JsonProperty("scene_info")]
        public PayScene SceneInfo { get; set; }
        [JsonProperty("promotion_detail")]
        public List<PromotionInfo> PromotionDetail { get; set; }
    }
}
