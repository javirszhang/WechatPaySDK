using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Converters;
using WechatPaySDK.Enums;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 普通服务商进件特约商户审核查询响应
    /// </summary>
    public class Applyment4SubApplymentQueryResponse : WxPayResponseBase
    {
        /// <summary>
        /// 业务申请编号
        /// </summary>
        [JsonProperty("business_code")]
        public string BusinessCode { get; set; }
        /// <summary>
        /// 微信支付申请单号
        /// </summary>
        [JsonProperty("applyment_id")]
        public long ApplymentId { get; set; }
        /// <summary>
        /// 特约商户号
        /// </summary>
        [JsonProperty("sub_mchid")]
        public string SubMchId { get; set; }
        /// <summary>
        /// 超级管理员签约链接
        /// </summary>
        [JsonProperty("sign_url")]
        public string SignUrl { get; set; }
        /// <summary>
        /// 申请单状态
        /// </summary>
        [JsonProperty("applyment_state"), JsonConverter(typeof(EnumToStringConverter))]
        public ApplymentState ApplymentState { get; set; }
        /// <summary>
        /// 申请状态描述
        /// </summary>
        [JsonProperty("applyment_state_msg")]
        public string ApplymentStateMsg { get; set; }
        /// <summary>
        /// 驳回原因详情
        /// </summary>
        [JsonProperty("audit_detail")]
        public Audit_Detail[] AuditDetail { get; set; }
        public class Audit_Detail
        {
            /// <summary>
            /// 字段名
            /// </summary>
            public string Field { get; set; }
            /// <summary>
            /// 字段名称
            /// </summary>
            public string FieldName { get; set; }
            /// <summary>
            /// 驳回原因
            /// </summary>
            public string RejectReason { get; set; }
        }
    }
}
