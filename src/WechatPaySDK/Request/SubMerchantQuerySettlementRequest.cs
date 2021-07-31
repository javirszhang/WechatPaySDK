using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    public class SubMerchantQuerySettlementRequest : WxPayRequestBase<SubMerchantQuerySettlementResponse>
    {
        private readonly string subMerchantId;
        public SubMerchantQuerySettlementRequest(string _subMerchantId)
        {
            this.subMerchantId = _subMerchantId;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/apply4sub/sub_merchants/{subMerchantId}/settlement";
        }
    }
}
