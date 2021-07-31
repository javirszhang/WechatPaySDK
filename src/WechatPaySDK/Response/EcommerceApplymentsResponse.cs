using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    public class EcommerceApplymentsResponse : WxPayResponseBase
    {
        [JsonProperty("applyment_id")]
        public string ApplymentId { get; set; }
        [JsonProperty("out_request_no")]
        public string OutRequestNo { get; set; }
    }
}
