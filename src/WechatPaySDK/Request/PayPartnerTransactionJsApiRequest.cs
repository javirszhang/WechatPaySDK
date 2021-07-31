using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    public partial class PayPartnerTransactionJsApiRequest : WxPayRequestBase<PayPartnerTransactionJsApiRequest.JsApiModel, PayPartnerTransactionAppResponse>
    {
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return "/v3/pay/partner/transactions/jsapi";
        }
    }
}
