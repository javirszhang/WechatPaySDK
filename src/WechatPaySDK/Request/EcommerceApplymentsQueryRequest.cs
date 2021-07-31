using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 收付通二级商户进件查询
    /// </summary>
    public class EcommerceApplymentsQueryRequest : WxPayRequestBase<EcommerceApplymentsQueryResponse>
    {
        private readonly string VoucherNo;
        private readonly VoucherType Type;
        public EcommerceApplymentsQueryRequest(string voucherNo, VoucherType voucherType)
        {
            this.VoucherNo = voucherNo;
            this.Type = voucherType;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/ecommerce/applyments/{(this.Type == VoucherType.APPLY_ID ? null : "out-request-no/")}{this.VoucherNo}";
        }

        /// <summary>
        /// 凭证类型
        /// </summary>
        public enum VoucherType
        {
            APPLY_ID,
            OUT_REQUEST_NO
        }
    }
}
