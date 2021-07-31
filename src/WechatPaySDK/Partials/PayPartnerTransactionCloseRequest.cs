using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Request
{
    public partial class PayPartnerTransactionCloseRequest
    {
        public class CloseModel : WxPayObject
        {
            /// <summary>
            /// 服务商户号，由微信支付生成并下发
            /// </summary>
            [JsonProperty("sp_mchid")]
            public string SPMchID { get; set; }
            /// <summary>
            /// 二级商户的商户号，有微信支付生成并下发。
            /// </summary>
            [JsonProperty("sub_mchid")]
            public string SubMchID { get; set; }
        }
    }
}
