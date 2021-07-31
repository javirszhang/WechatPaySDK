using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK
{
    /// <summary>
    /// 秘钥格式不正确
    /// </summary>
    public class InvalidKeyFormatException : WechatSDKException
    {
        public InvalidKeyFormatException(string msg) : base(msg) { }
        public InvalidKeyFormatException(string msg, Exception inner) : base(msg, inner) { }
    }
}
