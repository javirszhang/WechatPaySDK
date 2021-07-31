using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 修改结算账户
    /// </summary>
    public partial class SubMmerchantModifySettlementRequest : WxPayRequestBase<SubMmerchantModifySettlementRequest.SettlementModel, SubMmerchantModifySettlementResponse>
    {
        private readonly string subMerchantId;
        public SubMmerchantModifySettlementRequest(string _subMerchantId)
        {
            this.subMerchantId = _subMerchantId;
        }
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return $"/v3/apply4sub/sub_merchants/{subMerchantId}/modify-settlement";
        }
    }
}
