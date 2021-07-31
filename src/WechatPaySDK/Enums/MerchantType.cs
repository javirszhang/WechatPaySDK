using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Enums
{
    public enum MerchantType
    {
        [StringValue("SUBJECT_TYPE_INDIVIDUAL")]
        个体户,
        [StringValue("SUBJECT_TYPE_ENTERPRISE")]
        企业,
        [StringValue("SUBJECT_TYPE_INSTITUTIONS")]
        党政机关及事业单位,
        [StringValue("SUBJECT_TYPE_OTHERS")]
        其他组织
    }
}
