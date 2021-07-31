using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Converters;

namespace WechatPaySDK
{
    public abstract class WxPayObject : IWxPayObject
    {
        public virtual string GetJson()
        {
            return JsonConvert.SerializeObject(this, GetSettings());
        }

        private static JsonSerializerSettings _settings;
        public static JsonSerializerSettings GetSettings()
        {
            if (_settings == null)
            {
                _settings = new JsonSerializerSettings();
                _settings.NullValueHandling = NullValueHandling.Ignore;
                _settings.Converters.Add(new EnumToStringConverter());
            }
            return _settings;
        }
    }
}
