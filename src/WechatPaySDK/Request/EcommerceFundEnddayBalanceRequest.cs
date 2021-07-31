using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 商户日终余额查询
    /// </summary>
    public class EcommerceFundEnddayBalanceRequest : WxPayRequestBase<EcommerceFundEnddayBalanceResponse>
    {
        private readonly string sub_mchid;
        public EcommerceFundEnddayBalanceRequest(string subMerchantId)
        {
            this.sub_mchid = subMerchantId;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/ecommerce/fund/enddaybalance/{sub_mchid}";
        }
    }
}
