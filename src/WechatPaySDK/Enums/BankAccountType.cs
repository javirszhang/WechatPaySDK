using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Enums
{
    public enum BankAccountType
    {
        [StringValue("BANK_ACCOUNT_TYPE_CORPORATE")]
        对公银行账户,
        [StringValue("BANK_ACCOUNT_TYPE_PERSONAL")]
        个人银行卡
    }
}
