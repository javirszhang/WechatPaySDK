using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 普通支付，APP支付下单
    /// </summary>
    public partial class PayPartnerTransactionAppRequest : WxPayRequestBase<PayPartnerTransactionAppRequest.AppModel, PayPartnerTransactionAppResponse>
    {
        public PayPartnerTransactionAppRequest()
        {
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return "/v3/pay/partner/transactions/app";
        }
    }
}
