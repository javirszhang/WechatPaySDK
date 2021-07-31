using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 收付通二级商户进件
    /// </summary>
    public partial class EcommerceApplymentsRequest : WxPayRequestBase<EcommerceApplymentsRequest.EcommerceApplymentsModel, EcommerceApplymentsResponse>
    {
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return "/v3/ecommerce/applyments/";
        }
    }
}
