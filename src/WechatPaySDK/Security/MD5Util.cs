using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WechatPaySDK.Security
{
    public static class MD5Util
    {
        public static string Encode(string text)
        {
            byte[] buf = Encoding.UTF8.GetBytes(text);
            var res = Encode(buf);
            return ToHexString(res);
        }
        public static byte[] Encode(byte[] buf)
        {
            return MD5.Create().ComputeHash(buf);
        }
        private static string ToHexString(byte[] buf)
        {
            StringBuilder s = new StringBuilder();
            foreach (var b in buf)
            {
                s.AppendFormat("{0:x2}", b);
            }
            return s.ToString();
        }
    }
}
