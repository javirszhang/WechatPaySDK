using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Domain;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 下载证书响应参数
    /// </summary>
    public partial class DownloadCertificatesResponse : WxPayResponseBase
    {
        protected override bool IgnoreSignature()
        {
            Headers.TryGetValue("Wechatpay-Serial", out string serial_no);
            //如果是首次下载微信支付平台证书那么不验证相应值
            return !string.IsNullOrEmpty(serial_no) && this.Account.Keys.Find(t => t.SerialNo == serial_no) != null;
        }
        [JsonProperty("data")]
        public Result[] Data { get; set; }
    }
}
