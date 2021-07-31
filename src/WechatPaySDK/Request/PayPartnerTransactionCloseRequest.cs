using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 关闭订单
    /// </summary>
    public partial class PayPartnerTransactionCloseRequest : WxPayRequestBase<PayPartnerTransactionCloseRequest.CloseModel, PayPartnerTransactionCloseResponse>
    {
        private readonly string _outTradeNo;
        public PayPartnerTransactionCloseRequest(string outTradeNo)
        {
            this._outTradeNo = outTradeNo;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/pay/partner/transactions/out-trade-no/{_outTradeNo}/close";
        }
    }
}
