using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WechatPaySDK
{
    internal class DiagnosticHelper
    {
        private static DiagnosticListener diagnostic = new DiagnosticListener("WechatPaySDK");
        public static void Write(string key,object value)
        {
            diagnostic.Write(key, value);
        }
    }
}
