using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Enums
{
    /// <summary>
    /// 身份证件类型
    /// </summary>
    public enum IdentityType
    {
        [StringValue("IDENTIFICATION_TYPE_IDCARD")]
        身份证,
        [StringValue("IDENTIFICATION_TYPE_OVERSEA_PASSPORT")]
        其他国家护照,
        [StringValue("IDENTIFICATION_TYPE_HONGKONG_PASSPORT")]
        香港居民来往内地通行证,
        [StringValue("IDENTIFICATION_TYPE_MACAO_PASSPORT")]
        澳门居民来往内地通行证,
        [StringValue("IDENTIFICATION_TYPE_TAIWAN_PASSPORT")]
        台湾居民来往内地通行证,
    }
}
