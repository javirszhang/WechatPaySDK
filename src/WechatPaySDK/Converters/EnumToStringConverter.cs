using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WechatPaySDK.Common;

namespace WechatPaySDK.Converters
{
    /// <summary>
    /// 枚举序列化成字符串（首选StringValue特性，其次枚举名称）
    /// </summary>
    public class EnumToStringConverter : JsonConverter
    {
        private static ConcurrentDictionary<Type, List<EnumDescriptor>> cache = new ConcurrentDictionary<Type, List<EnumDescriptor>>();
        /// <summary>
        /// 是否输出枚举名称
        /// </summary>
        private readonly bool _outputEnumText;
        /// <summary>
        /// 枚举序列化成字符串（首选StringValue特性，其次枚举名称）
        /// </summary>
        public EnumToStringConverter() : this(false)
        {

        }
        /// <summary>
        /// 枚举序列化成字符串（首选StringValue特性，其次枚举名称）
        /// </summary>
        /// <param name="outputEnumText">是否输出枚举名称</param>
        public EnumToStringConverter(bool outputEnumText)
        {
            this._outputEnumText = outputEnumText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            bool res = objectType.IsEnum || (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>) && objectType.GetGenericArguments().First().IsEnum);
            return res;
        }
        /// <summary>
        /// 反序列
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            object currentValue = reader.Value;
            if (currentValue == null)
            {
                return null;
            }
            var enumItems = GetDescriptors(objectType);
            if (objectType.IsGenericType)
            {
                objectType = Nullable.GetUnderlyingType(objectType);
            }
            List<EnumDescriptor> hits = null;
            if (currentValue.GetType() == typeof(string) && !Regex.IsMatch(currentValue.ToString(), "^\\d+$"))
            {
                //如果传递的是字符串
                hits = enumItems.FindAll(i => i.Name == currentValue.ToString() || i.StringValue == currentValue.ToString() || i.DisplayText == currentValue.ToString());
            }
            else
            {
                //如果传递的是数值
                return Enum.ToObject(objectType, Convert.ToInt32(currentValue));
            }
            if (hits.Count == 1)
            {
                return Enum.Parse(objectType, hits.First().Name);
            }
            else
            {
                throw new ApplicationException($"反序列枚举项出现异常，枚举类型：{objectType.Name}，Json值：{currentValue}");
            }
        }
        /// <summary>
        /// 序列化为Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
            {
                return;
            }
            Type enumType = value.GetType();
            if (!enumType.IsEnum && !enumType.IsArray && enumType.GetInterface(nameof(IEnumerable)) == null)
            {
                serializer.Serialize(writer, value);
            }
            else if (value is IEnumerable array)
            {
                writer.WriteStartArray();
                foreach (var item in array)
                {
                    var itemType = item.GetType();
                    if (!itemType.IsEnum)
                    {
                        serializer.Serialize(writer, item);
                        continue;
                    }
                    var descriptor = GetEnumDescriptor(itemType, item);
                    WriteJsonValue(writer, descriptor);
                }
                writer.WriteEndArray();
            }
            else
            {
                var enumDescriptor = value is Enum enumValue ? enumValue.GetDescriptor() : GetEnumDescriptor(enumType, value);
                if (enumDescriptor == null)
                {
                    writer.WriteValue(value);
                    return;
                }
                WriteJsonValue(writer, enumDescriptor);
                AppendProperty(writer, enumDescriptor.DisplayText ?? enumDescriptor.Name);
            }
        }
        private void WriteJsonValue(JsonWriter writer, EnumDescriptor descriptor)
        {
            writer.WriteValue(string.IsNullOrEmpty(descriptor.StringValue) ? (object)descriptor.Value : descriptor.StringValue);
        }
        private void AppendProperty(JsonWriter writer, string value)
        {
            if (_outputEnumText && writer.WriteState != WriteState.Array)
            {
                string path = writer.Path;
                if (path.Contains("."))
                {
                    path = path.Substring(path.LastIndexOf(".") + 1);
                }
                writer.WritePropertyName(path + "Text");
                writer.WriteValue(value);
            }
        }
        private static EnumDescriptor GetEnumDescriptor(Type enumType, object value)
        {
            var val = EnumDescriptor.ParseTo(enumType, Convert.ToInt32(value).ToString()) as Enum;
            return val.GetDescriptor();
        }
        /// <summary>
        /// 支持枚举（enum）和可空枚举（enum?）
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        internal static List<EnumDescriptor> GetDescriptors(Type enumType)
        {
            return EnumDescriptor.GetDescriptors(enumType);
        }
    }
}
