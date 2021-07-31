using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WechatPaySDK.Common;

namespace WechatPaySDK
{
    internal static class ExntensionFunctions
    {
        public static string GetStringValue(this Enum type)
        {
            var descriptor = type.GetDescriptor();
            return descriptor.StringValue ?? descriptor.Name;
        }
        public static string GetDescription(this Enum type)
        {
            var descriptor = type.GetDescriptor();
            return descriptor.DisplayText;
        }
        public static EnumDescriptor GetDescriptor(this Enum type)
        {
            var enumNumber = Convert.ToInt32(type);
            Type typeOfEnum = type.GetType();
            var descriptors = EnumDescriptor.GetDescriptors(typeOfEnum);
            var des = descriptors.Find(d => d.Value == enumNumber);
            if (des == null && typeOfEnum.GetCustomAttribute<FlagsAttribute>() != null)
            {
                var matches = descriptors.Where(x => (x.Value & enumNumber) == x.Value);
                des = new EnumDescriptor();
                des.Value = enumNumber;
                des.Name = matches.Select(x => x.Name).Join(',');
                des.DisplayText = matches.Select(x => x.DisplayText).Join(',');
                des.StringValue = matches.Select(x => x.StringValue).Join(',');
            }
            return des;
        }
        public static T ToEnum<T>(this string s, bool ignoreCase = false)
        {
            return EnumDescriptor.ParseTo<T>(s, ignoreCase);
        }
        public static string Join(this IEnumerable<string> list, char separator)
        {
            return string.Join(separator, list);
        }
    }
}
