using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    public class EcommerceApplymentsQueryResponse : WxPayResponseBase
    {
        /// <summary>
        /// 申请状态，枚举值：
        /// CHECKING：资料校验中
        /// ACCOUNT_NEED_VERIFY：待账户验证
        /// AUDITING：审核中
        /// REJECTED：已驳回
        /// NEED_SIGN：待签约
        /// FINISH：完成
        /// FROZEN：已冻结
        /// 示例值：FINISH
        /// </summary>
        [JsonProperty("applyment_state")]
        public string ApplymentState { get; set; }
        /// <summary>
        /// 申请状态描述
        /// </summary>
        [JsonProperty("applyment_state_desc")]
        public string ApplymentStateDesc { get; set; }
        /// <summary>
        /// 签约链接，1、当申请状态为NEED_SIGN时才返回。
        /// 2、建议将链接转为二维码展示，需让申请单-管理者用微信扫码打开，完成签约。
        /// </summary>
        [JsonProperty("sign_url")]
        public string SignUrl { get; set; }
        /// <summary>
        /// 电商平台二级商户号,当申请状态为NEED_SIGN或FINISH时才返回。
        /// </summary>
        [JsonProperty("sub_mchid")]
        public string SubMchId { get; set; }
        /// <summary>
        /// 汇款账户验证信息，当申请状态为ACCOUNT_NEED_VERIFY 时有返回，可根据指引汇款，完成账户验证。
        /// </summary>
        [JsonProperty("account_validation")]
        public AccountValidationInfo AccountValidation { get; set; }
        /// <summary>
        /// 法人验证链接
        /// 1、当申请状态为ACCOUNT_NEED_VERIFY，且通过系统校验的申请单，将返回链接。
        /// 2、建议将链接转为二维码展示，让商户法人用微信扫码打开，完成账户验证。
        /// </summary>
        [JsonProperty("legal_validation_url")]
        public string LegalValidationUrl { get; set; }
        /// <summary>
        /// 业务申请编号,提交接口填写的业务申请编号。
        /// </summary>
        [JsonProperty("out_request_no")]
        public string OutRequestNo { get; set; }
        /// <summary>
        /// 微信支付申请单号,微信支付分配的申请单号。
        /// </summary>
        [JsonProperty("applyment_id")]
        public string ApplymentId { get; set; }
        /// <summary>
        /// 驳回原因详情，各项资料的审核情况。当申请状态为REJECTED或 FROZEN时才返回。
        /// </summary>
        [JsonProperty("audit_detail")]
        public List<AuditInfo> AuditDetail { get; set; }


        /// <summary>
        /// 审核信息
        /// </summary>
        public class AuditInfo
        {
            /// <summary>
            /// 参数名称，提交申请单的资料项名称。
            /// </summary>
            [JsonProperty("param_name")]
            public string ParamName { get; set; }
            /// <summary>
            /// 驳回原因，提交资料项被驳回原因。
            /// </summary>
            [JsonProperty("reject_reason")]
            public string RejectReason { get; set; }
        }
        /// <summary>
        /// 汇款账户验证信息，当申请状态为ACCOUNT_NEED_VERIFY 时有返回，可根据指引汇款，完成账户验证。
        /// </summary>
        public class AccountValidationInfo
        {
            /// <summary>
            /// 付款户名，需商户使用该户名的账户进行汇款。
            /// </summary>
            [JsonProperty("account_name")]
            public string AccountName { get; set; }
            /// <summary>
            /// 付款卡号，结算账户为对私时会返回，商户需使用该付款卡号进行汇款。
            /// </summary>
            [JsonProperty("account_no")]
            public string AccountNo { get; set; }
            /// <summary>
            /// 汇款金额，需要汇款的金额(单位：分)。
            /// </summary>
            [JsonProperty("pay_amount")]
            public string PayAmount { get; set; }
            /// <summary>
            /// 收款卡号，收款账户的卡号
            /// </summary>
            [JsonProperty("destination_account_number")]
            public string DestinationAccountNumber { get; set; }
            /// <summary>
            /// 收款户名，收款账户名
            /// </summary>
            [JsonProperty("destination_account_name")]
            public string DestinationAccountName { get; set; }
            /// <summary>
            /// 开户银行，收款账户的开户银行名称。
            /// </summary>
            [JsonProperty("destination_account_bank")]
            public string DestinationAccountBank { get; set; }
            /// <summary>
            /// 省市信息，收款账户的省市
            /// </summary>
            [JsonProperty("city")]
            public string City { get; set; }
            /// <summary>
            /// 备注信息，商户汇款时，需要填写的备注信息。
            /// </summary>
            [JsonProperty("remark")]
            public string Remark { get; set; }
            /// <summary>
            /// 汇款截止时间，请在此时间前完成汇款。
            /// </summary>
            [JsonProperty("deadline")]
            public string Deadline { get; set; }
        }
    }
}
