using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 普通服务商进件
    /// </summary>
    public partial class Applyment4SubApplymentRequest : WxPayRequestBase<Applyment4SubApplymentRequest.Applyment4SubApplymentModel, Applyment4SubApplymentResponse>
    {
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/applyment4sub/applyment/";
        }
    }
}
