using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.DataAnnotations;

namespace WechatPaySDK.Enums
{
    /// <summary>
    /// 登记证书类型
    /// 登记证书的类型
    ///         枚举值：
    /// <para>CERTIFICATE_TYPE_2388：事业单位法人证书</para>
    /// <para>CERTIFICATE_TYPE_2389：统一社会信用代码证书</para>
    /// <para>CERTIFICATE_TYPE_2390：有偿服务许可证（军队医院适用）</para>
    /// <para>CERTIFICATE_TYPE_2391：医疗机构执业许可证（军队医院适用）</para>
    /// <para>CERTIFICATE_TYPE_2392：企业营业执照（挂靠企业的党组织适用）</para>
    /// <para>CERTIFICATE_TYPE_2393：组织机构代码证（政府机关适用）</para>
    /// <para>CERTIFICATE_TYPE_2394：社会团体法人登记证书</para>
    /// <para>CERTIFICATE_TYPE_2395：民办非企业单位登记证书</para>
    /// <para>CERTIFICATE_TYPE_2396：基金会法人登记证书</para>
    /// <para>CERTIFICATE_TYPE_2397：慈善组织公开募捐资格证书</para>
    /// <para>CERTIFICATE_TYPE_2398：农民专业合作社法人营业执照</para>
    /// <para>CERTIFICATE_TYPE_2399：宗教活动场所登记证</para>
    /// <para>CERTIFICATE_TYPE_2400：其他证书/批文/证明</para>
    /// </summary>
    public enum CertType
    {
        [StringValue("CERTIFICATE_TYPE_2388")]
        事业单位法人证书,
        [StringValue("CERTIFICATE_TYPE_2389")]
        统一社会信用代码证书,
        [StringValue("CERTIFICATE_TYPE_2390")]
        有偿服务许可证,
        [StringValue("CERTIFICATE_TYPE_2391")]
        医疗机构执业许可证,
        [StringValue("CERTIFICATE_TYPE_2392")]
        企业营业执照,
        [StringValue("CERTIFICATE_TYPE_2393")]
        组织机构代码证,
        [StringValue("CERTIFICATE_TYPE_2394")]
        社会团体法人登记证书,
        [StringValue("CERTIFICATE_TYPE_2395")]
        民办非企业单位登记证书,
        [StringValue("CERTIFICATE_TYPE_2396")]
        基金会法人登记证书,
        [StringValue("CERTIFICATE_TYPE_2397")]
        慈善组织公开募捐资格证书,
        [StringValue("CERTIFICATE_TYPE_2398")]
        农民专业合作社法人营业执照,
        [StringValue("CERTIFICATE_TYPE_2399")]
        宗教活动场所登记证,
        [StringValue("CERTIFICATE_TYPE_2400")]
        其他证书
    }
}
