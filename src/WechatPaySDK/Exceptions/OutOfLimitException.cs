using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK.Exceptions
{
    public class OutOfLimitException : WechatSDKException
    {
        public OutOfLimitException(string msg) : base(msg) { }
    }
}
