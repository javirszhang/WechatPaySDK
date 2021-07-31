using System;
using System.Collections.Generic;
using System.Text;

namespace WechatPaySDK
{
    public class ArgumentFormatException : WechatSDKException
    {
        public ArgumentFormatException(string msg) : base(msg) { }
        public ArgumentFormatException(string msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
