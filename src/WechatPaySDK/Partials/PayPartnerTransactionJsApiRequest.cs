using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Models;

namespace WechatPaySDK.Request
{
    public partial class PayPartnerTransactionJsApiRequest
    {
        public class JsApiModel : PayPartnerTransactionAppRequest.AppModel
        {
            /// <summary>
            /// 付款人信息
            /// </summary>
            [JsonProperty("payer")]
            public PartnerPayer Payer { get; set; }
        }
    }
}
