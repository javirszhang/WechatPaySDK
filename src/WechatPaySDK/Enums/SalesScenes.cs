using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Enums
{
    public enum SalesScenes
    {
        [StringValue("SALES_SCENES_STORE")]
        线下门店,
        [StringValue("SALES_SCENES_MP")]
        公众号,
        [StringValue("SALES_SCENES_MINI_PROGRAM")]
        小程序,
        [StringValue("SALES_SCENES_WEB")]
        互联网,
        [StringValue("SALES_SCENES_APP")]
        APP,
        [StringValue("SALES_SCENES_WEWORK")]
        企业微信
    }
}
