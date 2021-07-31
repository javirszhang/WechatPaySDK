using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.DataAnnotations
{
    /// <summary>
    /// 字符串值，同一个枚举类型不允许有相同的StringValue
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// 字符串值，同一个枚举类型不允许有相同的StringValue
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }
        /// <summary>
        /// 字符串值
        /// </summary>
        public string Value { get; set; }
    }
}
