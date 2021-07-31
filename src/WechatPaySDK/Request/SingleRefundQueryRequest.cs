using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    public class SingleRefundQueryRequest : WxPayRequestBase<SingleRefundQueryResponse>
    {
        private readonly string outRefundNo;
        public string SubMerchantId { get; set; }
        /// <summary>
        /// 商户唯一退款单号
        /// </summary>
        /// <param name="out_refund_no"></param>
        public SingleRefundQueryRequest(string out_refund_no, string subMerchantId)
        {
            this.outRefundNo = out_refund_no;
            this.SubMerchantId = subMerchantId;
        }
        public SingleRefundQueryRequest(string out_refund_no)
        {
            this.outRefundNo = out_refund_no;
        }
        protected override HttpMethod GetHttpMethod()
        {
            return HttpMethod.Get;
        }

        protected override string GetRequestPath()
        {
            string url = $"/v3/refund/domestic/refunds/{outRefundNo}";
            if (!string.IsNullOrEmpty(SubMerchantId))
            {
                url += "?sub_mchid=" + this.SubMerchantId;
            }
            return url;
        }
    }
}
