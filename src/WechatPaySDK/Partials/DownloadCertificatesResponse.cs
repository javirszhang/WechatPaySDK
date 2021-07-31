using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Domain;

namespace WechatPaySDK.Response
{
    public partial class DownloadCertificatesResponse
    {
        public class Result
        {
            [JsonProperty("serial_no")]
            public string SerialNo { get; set; }
            [JsonProperty("effective_time")]
            public DateTime EffectiveTime { get; set; }
            [JsonProperty("expire_time")]
            public DateTime ExpireTime { get; set; }
            [JsonProperty("encrypt_certificate")]
            public CipherCertificate EncryptCertificate { get; set; }
        }
    }
}
