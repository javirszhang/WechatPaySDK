using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WechatPaySDK.Domain;
using WechatPaySDK.Exceptions;
using WechatPaySDK.Response;

namespace WechatPaySDK.Request
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MediaFileUploadRequest : WxPayRequestBase<MediaFileUploadRequest.MediaFile, MediaFileUploadResponse>
    {
        protected override HttpMethod GetHttpMethod()
        {
            return new HttpMethod("POST");
        }

        protected override string GetRequestPath()
        {
            return "/v3/merchant/media/upload";
        }
        internal override HttpRequestMessage GetHttpMessage(WechatAccount account)
        {
            if (this.Content == null)
            {
                throw new WechatSDKException("请设置要上传的文件信息");
            }
            if (this.Content.File.Length > 1024 * 1024 * 2)// 2M = 2(m) * 1024(kb) * 1024(b)
            {
                throw new OutOfLimitException("图片大小不能超过2M，请压缩图片后重试");
            }
            var httpMessage = new HttpRequestMessage(GetHttpMethod(), GetRequestPath());
            httpMessage.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);
            httpMessage.Headers.TryAddWithoutValidation("Accept", "application/json");
            var multiData = new MultipartFormDataContent();
            multiData.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            string json = GetContentJson();
            multiData.Add(new StringContent(json, Encoding.UTF8, "application/json"), "\"meta\"");
            var streamContent = new ByteArrayContent(this.Content.File);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(GetImageMediaType(this.Content.Meta.FileName));
            multiData.Add(streamContent, "\"file\"", $"\"{this.Content.Meta.FileName}\"");
            httpMessage.Content = multiData;
            return httpMessage;
        }

        private static string GetImageMediaType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext))
            {
                throw new ArgumentFormatException("不支持的文件格式，请上传[jpg/png/bmp]格式的图片");
            }
            switch (ext.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpg";
                case ".png":
                    return "image/png";
                case ".bmp":
                    return "image/bmp";
                default:
                    throw new ArgumentFormatException("不支持的文件格式，请上传[jpg/png/bmp]格式的图片");
            }
        }
        /// <summary>
        /// 设置要上传的媒体文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MediaFileUploadRequest SetMedia(MediaFile model)
        {
            this.Content = model;
            if (this.Content.File.Length > 1024 * 1024 * 2)// 2M = 2(m) * 1024(kb) * 1024(b)
            {
                throw new OutOfLimitException("图片大小不能超过2M，请压缩图片后重试");
            }
            return this;
        }
    }
}
