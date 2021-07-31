using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    public class PayTransactionQueryRequest : WxPayRequestBase<PayTransactionQueryResponse>
    {
        private readonly OrderNumberType orderNumberType;
        private readonly string OrderNo;
        private readonly string merchantId;
        public PayTransactionQueryRequest(string merchantId, OrderNumberType numberType, string orderNo)
        {
            this.orderNumberType = numberType;
            this.OrderNo = orderNo;
            this.merchantId = merchantId;
        }
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("GET");
        }

        protected override string GetRequestPath()
        {
            string slot = this.orderNumberType == OrderNumberType.TransactionID ? "id" : "out-trade-no";
            return $"/v3/pay/transactions/{slot}/{OrderNo}?mchid={this.merchantId}";
        }

        public enum OrderNumberType
        {
            TransactionID,
            OutTradeNo
        }
    }
}
