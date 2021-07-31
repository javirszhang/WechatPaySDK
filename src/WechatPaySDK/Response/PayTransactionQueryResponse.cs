﻿using Newtonsoft.Json;
using WechatPaySDK.Models;

namespace WechatPaySDK.Response
{
    public class PayTransactionQueryResponse : WxPayResponseBase
    {
        /// <summary>
        /// 直连商户申请的公众号或移动应用appid。
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }
        /// <summary>
        /// 直连商户的商户号，由微信支付生成并下发。
        /// </summary>
        [JsonProperty("mchid")]
        public string MchID { get; set; }
        /// <summary>
        /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一，详见【商户订单号】。
        /// </summary>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 微信支付系统生成的订单号。
        /// </summary>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
        /// <summary>
        /// 交易类型，枚举值：
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
        /// ACCEPT：已接收，等待扣款
        /// </summary>
        [JsonProperty("trade_state")]
        public string TradeState { get; set; }
        /// <summary>
        /// 交易状态描述
        /// </summary>
        [JsonProperty("trade_state_desc")]
        public string TradeStateDesc { get; set; }
        /// <summary>
        /// 银行类型，采用字符串类型的银行标识。银行标识请参考《银行类型对照表》
        /// </summary>
        [JsonProperty("bank_type")]
        public string BankType { get; set; }
        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
        /// </summary>
        [JsonProperty("attach")]
        public string Attach { get; set; }
        /// <summary>
        /// 支付完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。
        /// </summary>
        [JsonProperty("success_time")]
        public string SuccessTime { get; set; }
        [JsonProperty("payer")]
        public DirectPayer Payer { get; set; }
    }
}