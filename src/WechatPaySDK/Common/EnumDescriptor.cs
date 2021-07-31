using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Common
{
    /// <summary>
    /// 枚举描述
    /// </summary>
    [DebuggerDisplay("Name={Name};Value={Value};StringValue={StringValue};DisplayText={DisplayText}")]
    public class EnumDescriptor
    {
        private static ConcurrentDictionary<Type, List<EnumDescriptor>> cache = new ConcurrentDictionary<Type, List<EnumDescriptor>>();
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 枚举字符串值
        /// </summary>
        public string StringValue { get; set; }
        /// <summary>
        /// 枚举展示文字（第一优先取DescriptionAttribute，再次取DisplayAttribute，再次枚举名称）
        /// </summary>
        public string DisplayText { get; set; }

        internal bool Match(string s, bool ignoreCases = false)
        {
            StringComparison comparison = ignoreCases ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            return s.Equals(Name, comparison) || s.Equals(StringValue, comparison) || s.Equals(this.DisplayText, comparison);
        }
        /// <summary>
        /// 支持枚举（enum）和可空枚举（enum?）
        /// </summary>
        /// <param name="enumType">支持枚举（enum）和可空枚举（enum?）</param>
        /// <returns></returns>
        public static List<EnumDescriptor> GetDescriptors(Type enumType)
        {
            List<EnumDescriptor> enums = cache.GetOrAdd(enumType, typeOfValue =>
            {
                var underlyType = typeOfValue;
                if (typeOfValue.IsGenericType && typeOfValue.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    underlyType = Nullable.GetUnderlyingType(typeOfValue);
                }
                var allFields = underlyType.GetFields();
                List<EnumDescriptor> descriptors = new List<EnumDescriptor>();
                var allValues = Enum.GetValues(underlyType);
                foreach (var enumValue in allValues)
                {
                    EnumDescriptor descriptor = new EnumDescriptor();
                    descriptor.DisplayText = descriptor.Name = Enum.GetName(underlyType, enumValue);
                    descriptor.Value = Convert.ToInt32(enumValue);
                    var field = allFields.FirstOrDefault(t => t.Name == descriptor.Name);
                    var attrs = field.GetCustomAttributes();
                    var description = attrs.OfType<DescriptionAttribute>().FirstOrDefault();
                    if (description != null)
                    {
                        descriptor.DisplayText = description.Description;
                    }
                    if (string.IsNullOrEmpty(descriptor.DisplayText))
                    {
                        var display = attrs.OfType<DisplayAttribute>()?.FirstOrDefault();
                        if (display != null)
                        {
                            descriptor.DisplayText = display.Name;
                        }
                    }
                    var stringValue = attrs.OfType<StringValueAttribute>()?.FirstOrDefault();
                    if (stringValue != null)
                    {

                        descriptor.StringValue = stringValue.Value;
                    }
                    descriptors.Add(descriptor);
                }
                return descriptors;
            });
            return enums;
        }

        public static T ParseTo<T>(string s, bool ignoreCase = false)
        {
            Type enumType = typeof(T);
            return (T)ParseTo(enumType, s, ignoreCase);
        }

        public static object ParseTo(Type enumType, string s, bool ignoreCase = false)
        {
            bool isNullable = enumType.IsGenericType && enumType.GetGenericTypeDefinition() == typeof(Nullable<>);
            //传入的值为空，并且转换类型不是可为空的枚举，抛异常
            if (string.IsNullOrEmpty(s) && !isNullable)
            {
                throw new ArgumentNullException("s");
            }
            //如果传入的值为空，返回null
            if (string.IsNullOrEmpty(s))
            {
                return default;
            }
            if (isNullable)
            {
                //得到可为空类型的真实类型
                enumType = Nullable.GetUnderlyingType(enumType);
            }
            string value = s;
            if (!Regex.IsMatch(s, "^\\d+$"))
            {
                var enumItems = GetDescriptors(enumType);
                List<EnumDescriptor> hits = enumItems.FindAll(i => i.Match(s, ignoreCase));
                EnumDescriptor target = hits.Count == 1 ? hits.First() : hits.Find(h => h.Name == s) ?? hits.Find(h => h.StringValue == s);
                if (target == null)
                {
                    throw new ArgumentFormatException($"枚举转换失败，枚举类型：{enumType.Name}，值：{s}");
                }
                value = target.Value.ToString();
            }
            return Enum.Parse(enumType, value);
        }
    }
}
