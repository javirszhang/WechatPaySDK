using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WechatPaySDK.Common;
using WechatPaySDK.Security;

namespace WechatPaySDK
{
    public class WechatClient
    {
        private readonly WechatAccount Account;
        private readonly IHttpClientFactory httpFactory;
        public WechatClient(WechatAccount wechatAccount, IHttpClientFactory factory = null)
        {
            this.Account = wechatAccount;
            this.httpFactory = factory;
        }
        public T Execute<T>(WxPayRequestBase<T> request) where T : WxPayResponseBase, new()
        {
            return ExecuteAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        private static HttpClient sHttpClient;
        private static HttpClient CreateHttpClient()
        {
            if (sHttpClient == null)
            {
                sHttpClient = new HttpClient();
            }
            return sHttpClient;
        }
        public async Task<T> ExecuteAsync<T>(WxPayRequestBase<T> request) where T : WxPayResponseBase, new()
        {
            return await SendRequest(request);
        }
        private async Task<T> SendRequest<T>(WxPayRequestBase<T> request) where T : WxPayResponseBase, new()
        {
            StringBuilder logText = new StringBuilder();

            using (var httpClient = this.httpFactory?.CreateClient() ?? CreateHttpClient())
            {
                httpClient.BaseAddress = new Uri(this.Account.ApiDomain);

                var httpMessage = request.GetHttpMessage(this.Account);
                logText.AppendLine($"{httpMessage.Method} {httpMessage.RequestUri}");
                string contentJson = request.GetContentJson();
                if (!string.IsNullOrEmpty(contentJson))
                {
                    logText.AppendLine($"RequestBody：{contentJson}");
                }
                if (request.Key != null)
                {
                    logText.AppendLine($"KeySerialNo：{request.Key.SerialNo}");
                }
                var httpResponse = await httpClient.SendAsync(httpMessage);

                T result = null;
                string content = null;
                logText.AppendLine($"httpStatusCode: {httpResponse.StatusCode}");
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    content = await httpResponse.Content.ReadAsStringAsync();
                }
                logText.AppendLine($"ResponseContent：{content}");
                result = string.IsNullOrEmpty(content) ? new T() : JsonConvert.DeserializeObject<T>(content);
                result.Account = Account;
                httpResponse.Headers.TryGetValues("Request-ID", out IEnumerable<string> requestID);
                result.OriginalBody = content;
                result.RequestID = requestID == null ? string.Empty : string.Join(" ", requestID);
                result.StatusCode = httpResponse.StatusCode;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                logText.AppendLine("ResponseHeaders:");
                httpResponse.Headers.Aggregate(headers, (h, kv) =>
                {
                    logText.Append(kv.Key).Append("=");
                    if (kv.Value != null)
                    {
                        string value = string.Join(" ", kv.Value);
                        h.Add(kv.Key, value);
                        logText.Append(value);
                    }
                    logText.Append(Environment.NewLine);
                    return h;
                });
                result.Headers = headers;
                DiagnosticHelper.Write("WechatClient.Execute", new DiagnosticModel { Message = logText.ToString() });
                return result;
            }
        }

        public static string BuildJsapiPayload(WechatAccount account, string appId, string prepayId)
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            var jsapiPackage = new SortedDictionary<string, string>(StringComparer.Ordinal);
            jsapiPackage.Add("appId", appId);
            jsapiPackage.Add("nonceStr", Guid.NewGuid().ToString("N"));
            jsapiPackage.Add("package", "prepay_id=" + prepayId);
            jsapiPackage.Add("signType", "MD5");
            jsapiPackage.Add("timeStamp", timestamp.ToString());
            StringBuilder signMessage = new StringBuilder();
            jsapiPackage.Aggregate(signMessage, (sm, kv) => sm.Append(kv.Key).Append("=").Append(kv.Value).Append("&"));
            signMessage.Append("key=").Append(account.MerchantSecret);
            DiagnosticHelper.Write("BuildJsApiPayload", new DiagnosticModel { Message = signMessage.ToString() });
            string sign = MD5Util.Encode(signMessage.ToString());
            jsapiPackage.Add("paySign", sign.ToUpper());
            string payload = JsonConvert.SerializeObject(jsapiPackage);
            return payload;
        }
        public static string BuildAppPayload(WechatAccount account, string appId, string prepayId)
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            var appPayPackage = new SortedDictionary<string, string>(StringComparer.Ordinal);
            appPayPackage.Add("appid", appId);
            appPayPackage.Add("noncestr", Guid.NewGuid().ToString("N"));
            appPayPackage.Add("package", "Sign=WXPay");
            appPayPackage.Add("partnerid", account.MerchantId);
            appPayPackage.Add("prepayid", prepayId);
            appPayPackage.Add("timestamp", timestamp.ToString());
            StringBuilder signMessage = new StringBuilder();
            appPayPackage.Aggregate(signMessage, (b, kv) => b.Append(kv.Key).Append("=").Append(kv.Value).Append("&"));
            signMessage.Append("key=").Append(account.MerchantSecret);
            DiagnosticHelper.Write("BuildAppPayload", new DiagnosticModel { Message = signMessage.ToString() });
            string sign = MD5Util.Encode(signMessage.ToString());
            appPayPackage.Add("sign", sign);
            string payload = JsonConvert.SerializeObject(appPayPackage);
            return payload;
        }
    }
}
