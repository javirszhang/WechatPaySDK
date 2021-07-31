using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.DataAnnotations
{
    /// <summary>
    /// 标识字段需要解密
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DecryptionRequiredAttribute : Attribute
    {
    }
}
