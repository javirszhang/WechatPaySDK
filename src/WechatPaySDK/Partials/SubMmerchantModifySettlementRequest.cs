using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Request
{
    public partial class SubMmerchantModifySettlementRequest
    {
        public class SettlementModel : WxPayObject
        {
            /// <summary>
            /// 账户类型
            /// 根据特约商户号的主体类型，可选择的账户类型如下：
            /// 1、小微主体：经营者个人银行卡
            /// 2、个体工商户主体：经营者个人银行卡/ 对公银行账户
            /// 3、企业主体：对公银行账户
            /// 4、党政、机关及事业单位主体：对公银行账户
            /// 5、其他组织主体：对公银行账户
            /// 枚举值：
            /// ACCOUNT_TYPE_BUSINESS：对公银行账户
            /// ACCOUNT_TYPE_PRIVATE：经营者个人银行卡
            /// </summary>
            [JsonProperty("account_type")]
            public string AccountType { get; set; }
            /// <summary>
            /// 开户银行，请填写开户银行名称，详细参见《开户银行对照表》。
            /// </summary>
            [JsonProperty("account_bank")]
            public string AccountBank { get; set; }
            /// <summary>
            /// 开户银行省市编码，需至少精确到市
            /// </summary>
            [JsonProperty("bank_address_code")]
            public string BankAddressCode { get; set; }
            /// <summary>
            /// 开户银行全称（含支行）若开户银行为“其他银行”，则需二选一填写“开户银行全称（含支行）”或“开户银行联行号”。
            /// 填写银行全称，如"深圳农村商业银行XXX支行" 
            /// </summary>
            [JsonProperty("bank_name")]
            public string BankName { get; set; }
            /// <summary>
            /// 开户银行联行号
            /// 若开户银行为“其他银行”，则需二选一填写“开户银行全称（含支行）”或“开户银行联行号”。
            /// 填写银行联行号，详细参见
            /// </summary>
            [JsonProperty("bank_branch_id")]
            public string BankBranchId { get; set; }
            /// <summary>
            /// 银行账号
            /// </summary>
            [JsonProperty("account_number")]
            [EncryptionRequired]
            public string AccountNumber { get; set; }
        }
    }
}
