using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Response
{
    /// <summary>
    /// 查询结算账户返回值
    /// </summary>
    public class SubMerchantQuerySettlementResponse : WxPayResponseBase
    {
        /// <summary>
        /// 账户类型
        /// 返回特约商户的结算账户类型。
        /// 枚举值：
        /// ACCOUNT_TYPE_BUSINESS：对公银行账户
        /// ACCOUNT_TYPE_PRIVATE：经营者个人银行卡
        /// </summary>
        [JsonProperty("account_type")]
        public string AccountType { get; set; }
        /// <summary>
        /// 开户银行，返回特约商户的结算账户-开户银行全称
        /// </summary>
        [JsonProperty("account_bank")]
        public string AccountBank { get; set; }
        /// <summary>
        /// 开户银行全称（含支行]
        /// </summary>
        [JsonProperty("bank_name")]
        public string BankName { get; set; }
        /// <summary>
        /// 开户银行联行号
        /// </summary>
        [JsonProperty("bank_branch_id")]
        public string BankBranchId { get; set; }
        /// <summary>
        /// 银行账号，脱敏数据
        /// </summary>
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }
        /// <summary>
        /// 汇款验证结果，
        /// 返回特约商户的结算账户-汇款验证结果。
        /// VERIFYING：系统汇款验证中，商户可发起提现尝试。
        /// VERIFY_SUCCESS：系统成功汇款，该账户可正常发起提现。
        /// VERIFY_FAIL：系统汇款失败，该账户无法发起提现，请检查修改。
        /// </summary>
        [JsonProperty("verify_result")]
        public string VerifyResult { get; set; }
    }
}
