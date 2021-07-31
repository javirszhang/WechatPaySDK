using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Request
{
    public partial class MediaFileUploadRequest
    {
        public class MediaFile : WxPayObject
        {
            /// <summary>
            /// 文件内容
            /// </summary>
            public byte[] File { get; set; }
            /// <summary>
            /// 描述信息
            /// </summary>
            public MediaMeta Meta { get; set; }

            public override string GetJson()
            {
                return JsonConvert.SerializeObject(this.Meta, GetSettings());
            }
        }
        public class MediaMeta
        {
            /// <summary>
            /// 文件名称
            /// </summary>
            [JsonProperty("filename")]
            public string FileName { get; set; }
            /// <summary>
            /// 文件摘要
            /// </summary>
            [JsonProperty("sha256")]
            public string SHA256 { get; set; }
        }
    }

}
