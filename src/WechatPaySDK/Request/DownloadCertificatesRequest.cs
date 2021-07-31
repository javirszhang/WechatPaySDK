using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 下载证书
    /// </summary>
    public partial class DownloadCertificatesRequest : WxPayRequestBase<DownloadCertificatesResponse>
    {
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/certificates";
        }
    }
}
