using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    public class Applyment4SubApplymentQueryRequest : WxPayRequestBase<Applyment4SubApplymentQueryResponse>
    {
        private readonly string Voucher;
        private readonly QueryVoucherType VoucherType;
        public Applyment4SubApplymentQueryRequest(string voucher, QueryVoucherType voucherType)
        {
            this.Voucher = voucher;
            this.VoucherType = voucherType;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/applyment4sub/applyment/{this.VoucherType.ToString().ToLower()}/{Voucher}";
        }

        /// <summary>
        /// 查询凭证类型
        /// </summary>
        public enum QueryVoucherType
        {
            BUSINESS_CODE,
            APPLYMENT_ID
        }
    }
}
