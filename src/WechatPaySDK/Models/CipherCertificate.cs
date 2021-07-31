using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Domain
{
    public class CipherCertificate
    {
        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("associated_data")]
        public string AssociatedData { get; set; }
        [JsonProperty("ciphertext")]
        public string CipherText { get; set; }
    }
}
