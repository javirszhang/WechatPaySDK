using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 交易查询
    /// </summary>
    public class PayPartnerTransactionsQueryRequest : WxPayRequestBase<PayPpartnerTransactionsQueryResponse>
    {
        private readonly string isvMchID;
        private readonly string subMchID;
        private readonly string orderNo;
        private readonly VoucherType Type;
        public PayPartnerTransactionsQueryRequest(string _isvMchID, string _subMchID, string _orderNo, VoucherType _type)
        {
            this.isvMchID = _isvMchID;
            this.subMchID = _subMchID;
            this.orderNo = _orderNo;
            this.Type = _type;
        }

        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            string slot = this.Type == VoucherType.OUT_TRADE_NO ? "out-trade-no" : "id";
            return $"/v3/pay/partner/transactions/{slot}/{orderNo}?sp_mchid={isvMchID}&sub_mchid={subMchID}";
        }


        public enum VoucherType
        {
            TRANSACTION_ID,
            OUT_TRADE_NO
        }
    }
}
