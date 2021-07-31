using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 普通服务商进件
    /// </summary>
    public class Applyment4SubApplymentResponse : WxPayResponseBase
    {
        [JsonProperty("applyment_id")]
        public long ApplymentId { get; set; }
    }
}
