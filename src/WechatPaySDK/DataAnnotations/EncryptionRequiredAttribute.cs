using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.DataAnnotations
{
    /// <summary>
    /// 需要加密
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EncryptionRequiredAttribute : Attribute
    {

    }
}
