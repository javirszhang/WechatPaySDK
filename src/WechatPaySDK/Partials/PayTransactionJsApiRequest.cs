using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WechatPaySDK.Models;

namespace WechatPaySDK.Request
{
    public partial class PayTransactionJsApiRequest
    {
        public class JsApiModel : WxPayObject
        {
            /// <summary>
            /// 应用ID
            /// <para>由微信生成的应用ID，全局唯一。请求基础下单接口时请注意APPID的应用属性，例如公众号场景下，需使用应用属性为公众号的APPID</para>
            /// </summary>
            [JsonProperty("appid")]
            [Required]
            public string AppId { get; set; }
            /// <summary>
            /// 直连商户号
            /// <para>直连商户的商户号，由微信支付生成并下发。</para>
            /// </summary>
            [JsonProperty("mchid")]
            [Required]
            public string MchID { get; set; }
            /// <summary>
            /// 商品描述
            /// </summary>
            [JsonProperty("description")]
            [Required]
            public string Description { get; set; }
            /// <summary>
            /// 商户订单号
            /// <para>商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一</para>
            /// </summary>
            [JsonProperty("out_trade_no")]
            [Required]
            public string OutTradeNo { get; set; }
            /// <summary>
            /// 交易结束时间
            /// <para>订单失效时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
            /// </summary>
            [JsonProperty("time_expire")]
            public string TimeExpire { get; set; }
            /// <summary>
            /// 附加数据
            /// <para>附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用</para>
            /// </summary>
            [JsonProperty("attach")]
            public string Attach { get; set; }
            /// <summary>
            /// 通知地址
            /// <para>通知URL必须为直接可访问的URL，不允许携带查询串，要求必须为https地址。</para>
            /// </summary>
            [JsonProperty("notify_url")]
            [Required]
            public string NotifyUrl { get; set; }
            /// <summary>
            /// 订单优惠标记
            /// </summary>
            [JsonProperty("goods_tag")]
            public string GoodsTag { get; set; }
            /// <summary>
            /// 订单金额信息
            /// </summary>
            [JsonProperty("amount"), Required]
            public CurrencyInfo Amount { get; set; }
            /// <summary>
            /// 用户标识
            /// <para>用户在直连商户appid下的唯一标识。 下单前需获取到用户的Openid</para>
            /// </summary>
            [JsonProperty("payer"), Required]
            public DirectPayer Payer { get; set; }
            /// <summary>
            /// 支付场景描述
            /// </summary>
            [JsonProperty("scene_info")]
            public PayScene SceneInfo { get; set; }
            /// <summary>
            /// 结算信息
            /// </summary>
            [JsonProperty("settle_info")]
            public SettleInfo SettleInfo { get; set; }
        }
        public class JsApiPromotion
        {
            /// <summary>
            /// 订单原价
            /// <para>1、商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的交易金额。</para>
            /// <para>2、当订单原价与支付金额不相等，则不享受优惠。</para>
            /// <para>3、该字段主要用于防止同一张小票分多次支付，以享受多次优惠的情况，正常支付订单不必上传此参数。</para>
            /// </summary>
            [JsonProperty("cost_price")]
            public string CostPrice { get; set; }
            /// <summary>
            /// 商品小票ID
            /// </summary>
            [JsonProperty("invoice_id")]
            public string InvoiceId { get; set; }
            /// <summary>
            /// 单品列表信息, 条目个数限制：[1，6000]
            /// </summary>
            [JsonProperty("goods_detail")]
            public List<GoodsInfo> GoodsDetail { get; set; }
        }
    }
}
