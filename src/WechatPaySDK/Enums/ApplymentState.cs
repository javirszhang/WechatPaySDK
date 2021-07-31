using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Enums
{
    public enum ApplymentState
    {
        [StringValue("APPLYMENT_STATE_EDITTING")]
        编辑中,
        [StringValue("APPLYMENT_STATE_AUDITING")]
        审核中,
        [StringValue("APPLYMENT_STATE_REJECTED")]
        已驳回,
        [StringValue("APPLYMENT_STATE_TO_BE_CONFIRMED")]
        待账户验证,
        [StringValue("APPLYMENT_STATE_TO_BE_SIGNED")]
        待签约,
        [StringValue("APPLYMENT_STATE_SIGNING")]
        权限开通中,
        [StringValue("APPLYMENT_STATE_FINISHED")]
        已完成,
        [StringValue("APPLYMENT_STATE_CANCELED")]
        已作废,

    }
}
