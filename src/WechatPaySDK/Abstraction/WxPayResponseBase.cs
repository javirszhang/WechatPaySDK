using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WechatPaySDK.Security;

namespace WechatPaySDK
{
    public abstract class WxPayResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        internal WechatAccount Account { get; set; }
        /// <summary>
        /// 请求响应原body字符串
        /// </summary>
        [JsonIgnore]
        public string OriginalBody { get; set; }
        /// <summary>
        /// 本次请求的ID
        /// </summary>
        [JsonIgnore]
        public string RequestID { get; set; }
        /// <summary>
        /// http响应状态码
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 请求响应http头
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string> Headers { get; set; }
        /// <summary>
        /// 指示本次请求是否成功
        /// </summary>
        /// <returns></returns>
        public virtual bool Success()
        {
            bool res = this.StatusCode == HttpStatusCode.OK || this.StatusCode == HttpStatusCode.NoContent || this.StatusCode == HttpStatusCode.Accepted;
            if (res && !VerifyResponseAsync(Headers, OriginalBody))
            {
                return false;
            }
            if (!res && !string.IsNullOrEmpty(OriginalBody))
            {
                this.Error = JsonConvert.DeserializeObject<ErrorInfo>(this.OriginalBody);
            }
            return res;
        }
        /// <summary>
        /// 是否需要验证响应
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsVerifyResponse()
        {
            return true;
        }
        [JsonIgnore]
        public ErrorInfo Error { get; set; }
        private bool VerifyResponseAsync(Dictionary<string, string> headers, string body)
        {
            if (!IsVerifyResponse())
            {
                DiagnosticHelper.Write("WechatClient.Response.Verification", new { Message = "Current request is configured to ignore response signature verification" });
                return true;
            }
            try
            {
                headers.TryGetValue("Wechatpay-Timestamp", out string timestamp);
                headers.TryGetValue("Wechatpay-Nonce", out string nonce);
                headers.TryGetValue("Wechatpay-Signature", out string signature);
                headers.TryGetValue("Wechatpay-Serial", out string serial_no);
                WechatKey key = Account.GetPlatformKey(serial_no);
                string signMessage = $"{timestamp}\n{nonce}\n{body}\n";

                if (key == null)
                {
                    DiagnosticHelper.Write("WechatClient.Response.Verification", new { Message = "no keys matched" });
                    return false;
                }
                bool res = RSAUtils.VerifySign(signature, signMessage, key.PublicKey);
                return res;
            }
            catch (Exception ex)
            {
                DiagnosticHelper.Write("WechatClient.Response.Verification", ex);
                return false;
            }
        }
    }

    public class ErrorInfo
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
