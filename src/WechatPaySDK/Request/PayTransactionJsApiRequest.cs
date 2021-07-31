using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 直连商户JSAPI下单
    /// </summary>
    public partial class PayTransactionJsApiRequest : WxPayRequestBase<PayTransactionJsApiRequest.JsApiModel, PayTransactionJsApiResponse>
    {
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return "/v3/pay/transactions/jsapi";
        }
    }
}
