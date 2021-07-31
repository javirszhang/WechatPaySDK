using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Models;

namespace WechatPaySDK.Request
{
    public partial class PayPartnerTransactionAppRequest
    {
        public class AppModel : WxPayObject
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
            public string SpMchID { get; set; }
            /// <summary>
            /// 二级商户公众号ID，非必填
            /// </summary>
            [JsonProperty("sub_appid")]
            public string SubAppId { get; set; }
            /// <summary>
            /// 二级商户号
            /// </summary>
            [JsonProperty("sub_mchid")]
            public string SubMchID { get; set; }
            /// <summary>
            /// 商品描述
            /// </summary>
            [JsonProperty("description")]
            public string Description { get; set; }
            /// <summary>
            /// 商户订单号
            /// </summary>
            [JsonProperty("out_trade_no")]
            public string OutTradeNo { get; set; }
            /// <summary>
            /// 交易结束时间，非必填
            /// <para>订单失效时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
            /// </summary>
            [JsonProperty("time_expire")]
            public string TimeExpire { get; set; }
            /// <summary>
            /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
            /// </summary>
            [JsonProperty("attach")]
            public string Attach { get; set; }
            /// <summary>
            /// 通知URL必须为直接可访问的URL，不允许携带查询串。
            /// </summary>
            [JsonProperty("notify_url")]
            public string NotifyUrl { get; set; }
            /// <summary>
            /// 订单优惠标记
            /// </summary>
            [JsonProperty("goods_tag")]
            public string GoodsTag { get; set; }
            /// <summary>
            /// 结算信息
            /// </summary>
            [JsonProperty("settle_info")]
            public PaymentSettleInfo SettleInfo { get; set; }
            /// <summary>
            /// 订单金额信息
            /// </summary>
            [JsonProperty("amount")]
            public CurrencyInfo Amount { get; set; }
            /// <summary>
            /// 优惠功能
            /// </summary>
            [JsonProperty("detail")]
            public DiscountInfo Detail { get; set; }
            /// <summary>
            /// 支付场景描述
            /// </summary>
            [JsonProperty("scene_info")]
            public PayScene SceneInfo { get; set; }
        }

        
        
    }
}
