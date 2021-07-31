using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 媒体文件上传响应值
    /// </summary>
    public class MediaFileUploadResponse : WxPayResponseBase
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
    }
}
