using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WechatPaySDK.Security;

namespace WechatPaySDK
{
    public abstract class WxPayRequestBase
    {
        public const string USER_AGENT = "WechatPaySDK.NET Core";
        public const string AuthorizationSchema = "WECHATPAY2-SHA256-RSA2048";
        internal virtual HttpRequestMessage GetHttpMessage(WechatAccount config)
        {
            var httpMessage = new HttpRequestMessage(GetHttpMethod(), GetRequestPath());
            
            httpMessage.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);
            httpMessage.Headers.TryAddWithoutValidation("Accept", "application/json");
            EncryptContent(httpMessage, config);
            string contentJson = GetContentJson();
            if (!string.IsNullOrEmpty(contentJson))
            {
                httpMessage.Content = new StringContent(contentJson, Encoding.UTF8, "application/json");
            }
            AddSignature(httpMessage, config);
            return httpMessage;
        }
        internal virtual string GetContentJson()
        {
            return null;
        }
        protected virtual void EncryptContent(HttpRequestMessage httpRequest, WechatAccount config)
        {

        }
        private void AddSignature(HttpRequestMessage request, WechatAccount config)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            string nonce = Guid.NewGuid().ToString("N");
            string method = request.Method.Method;
            string path = request.RequestUri.PathAndQuery;
            string body = null;
            if ("post".Equals(method, StringComparison.OrdinalIgnoreCase) || "put".Equals(method, StringComparison.OrdinalIgnoreCase) || "patch".Equals(method, StringComparison.OrdinalIgnoreCase))
            {
                body = this.GetContentJson();
            }
            var key = config.Keys.First(t => t.Owner == "merchant");
            string signMessage = $"{method}\n{path}\n{timestamp}\n{nonce}\n{body}\n";
            string signature = RSAUtils.SignWithSHA256(key.PrivateKey, signMessage);
            string authorization = $"mchid=\"{config.MerchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{key.SerialNo}\",signature=\"{signature}\"";
            request.Headers.TryAddWithoutValidation("Authorization", $"{AuthorizationSchema} {authorization}");
        }
        protected abstract HttpMethod GetHttpMethod();

        protected abstract string GetRequestPath();
        internal WechatKey Key { get; set; }
    }
    public abstract class WxPayRequestBase<TResponse> : WxPayRequestBase where TResponse : WxPayResponseBase
    {

    }
    public abstract class WxPayRequestBase<TModel, TResponse> : WxPayRequestBase where TResponse : WxPayResponseBase where TModel : IWxPayObject
    {
        public TModel Content { get; set; }
        protected override void EncryptContent(HttpRequestMessage httpMessage, WechatAccount config)
        {
            if (Content != null)
            {
                WechatKey key = null;
                EncryptionProvider encryption = new EncryptionProvider(config);
                encryption.Encrypt(Content, ref key);
                if (encryption.IsEncrypted)
                {
                    httpMessage.Headers.TryAddWithoutValidation("Wechatpay-Serial", key.SerialNo);
                }
                this.Key = key;
            }
        }
        internal override string GetContentJson()
        {
            return Content?.GetJson();
        }
    }

}
