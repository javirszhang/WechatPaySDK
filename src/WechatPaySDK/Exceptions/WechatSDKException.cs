using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK
{
    public class WechatSDKException : Exception
    {
        public WechatSDKException() : base() { }
        public WechatSDKException(string msg) : base(msg) { }
        public WechatSDKException(string msg, Exception ex) : base(msg, ex) { }
    }
}
