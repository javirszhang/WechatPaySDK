using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 二级商户实时余额查询
    /// </summary>
    public class EcommerceFundBalanceRequest : WxPayRequestBase<EcommerceFundBalanceResponse>
    {
        private readonly string SubMerchantId;
        public EcommerceFundBalanceRequest(string subMerchantID)
        {
            this.SubMerchantId = subMerchantID;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/ecommerce/fund/balance/{SubMerchantId}";
        }
    }
}
