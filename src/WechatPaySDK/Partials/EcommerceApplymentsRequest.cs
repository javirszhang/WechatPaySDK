using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Converters;
using WechatPaySDK.DataAnnotations;
using WechatPaySDK.Enums;

namespace WechatPaySDK.Request
{
    public partial class EcommerceApplymentsRequest
    {
        public class EcommerceApplymentsModel : WxPayObject
        {
            /// <summary>
            /// 1、服务商自定义的商户唯一编号。
            /// <para>2、每个编号对应一个申请单，每个申请单审核通过后会生成一个微信支付商户号。</para>
            /// <para>3、若申请单被驳回，可填写相同的“业务申请编号”，即可覆盖修改原申请单信息 。</para>
            /// </summary>
            [JsonProperty("out_request_no")]
            public string OutRequestNo { get; set; }
            /// <summary>
            /// 非小微的主体类型需与营业执照/登记证书上一致，可参考选择主体指引，枚举值如下。
            /// <para>2401：小微商户，指无营业执照的个人商家。</para>
            /// <para>2500：个人卖家，指无营业执照，已持续从事电子商务经营活动满6个月，且期间经营收入累计超过20万元的个人商家。（若选择该主体，请在“补充说明”填写相关描述）</para>
            /// <para>4：个体工商户，营业执照上的主体类型一般为个体户、个体工商户、个体经营。</para>
            /// <para>2：企业，营业执照上的主体类型一般为有限公司、有限责任公司。</para>
            /// <para>3：党政、机关及事业单位，包括国内各级、各类政府机构、事业单位等（如：公安、党 团、司法、交通、旅游、工商税务、市政、医疗、教育、学校等机构）。</para>
            /// <para>1708：其他组织，不属于企业、政府/事业单位的组织机构（如社会团体、民办非企业、基 金会），要求机构已办理组织机构代码证。</para>
            /// </summary>
            [JsonProperty("organization_type")]
            public string OrganizationType { get; set; }
            /// <summary>
            /// 1、主体为“小微/个人卖家”时，不填。
            /// <para>2、主体为“个体工商户/企业”时，请上传营业执照。</para>
            /// <para>3、主体为“党政、机关及事业单位/其他组织”时，请上传登记证书。</para>
            /// </summary>
            [JsonProperty("business_license_info")]
            public LicenseInfo BusinessLicenseInfo { get; set; }
            /// <summary>
            /// 主体为“企业/党政、机关及事业单位/其他组织”，且营业执照/登记证书号码不是18位时必填。
            /// </summary>
            [JsonProperty("organization_cert_info")]
            public OrganizationCert OrganizationCert { get; set; }
            /// <summary>
            /// 1、主体为“小微/个人卖家”，可选择：身份证。
            /// <para>2、主体为“个体户/企业/党政、机关及事业单位/其他组织”，可选择：以下任一证件类型。</para>
            /// <para>3、若没有填写，系统默认选择：身份证。</para>
            /// <para>枚举值:</para>
            /// <para>IDENTIFICATION_TYPE_MAINLAND_IDCARD：中国大陆居民-身份证</para>
            /// <para>IDENTIFICATION_TYPE_OVERSEA_PASSPORT：其他国家或地区居民-护照</para>
            /// <para>IDENTIFICATION_TYPE_HONGKONG：中国香港居民–来往内地通行证</para>
            /// <para>IDENTIFICATION_TYPE_MACAO：中国澳门居民–来往内地通行证</para>
            /// <para>IDENTIFICATION_TYPE_TAIWAN：中国台湾居民–来往大陆通行证</para>
            /// </summary>
            [JsonProperty("id_doc_type")]
            public string IdDocType { get; set; }
            /// <summary>
            /// 请填写经营者/法人的身份证信息证件类型为“身份证”时填写。
            /// </summary>
            [JsonProperty("id_card_info")]
            public IdCardInfo IdCard { get; set; }
            /// <summary>
            /// 经营者/法人其他类型证件信息
            /// 证件类型为“来往内地通行证、来往大陆通行证、护照”时填写。
            /// </summary>
            [JsonProperty("id_doc_info")]
            public LegalDocInfo IdDocInfo { get; set; }
            /// <summary>
            ///  1、可根据实际情况，填写“true”或“false”。
            /// 1）若为“true”，则需填写结算银行账户。
            /// 2）若为“false”，则无需填写结算银行账户。
            /// 2、若入驻时未填写结算银行账户，则需入驻后调用修改结算账户API补充信息，才能发起提现。
            /// </summary>
            [JsonProperty("need_account_info")]
            public bool NeedAccountInfo { get; set; }
            /// <summary>
            /// 结算银行账户
            /// 若"是否填写结算账户信息"填写为“true”, 则必填，填写为“false”不填 。
            /// </summary>
            [JsonProperty("account_info")]
            public SettleBankAccount AccountInfo { get; set; }
            /// <summary>
            /// 超级管理员信息
            /// </summary>
            [JsonProperty("contact_info")]
            public ContactInfo ContactInfo { get; set; }
            /// <summary>
            /// 店铺信息
            /// 请填写店铺信息
            /// </summary>
            [JsonProperty("sales_scene_info")]
            public ShopInfo SalesSceneInfo { get; set; }
            /// <summary>
            /// 商户简称
            /// </summary>
            [JsonProperty("merchant_shortname")]
            public string MerchantShortname { get; set; }
            /// <summary>
            /// 特殊资质
            /// 1、根据所属行业的特殊资质要求提供，详情查看《行业对应特殊资质》。
            /// 2、请提供为“申请商家主体”所属的特殊资质，可授权使用总公司/分公司的特殊资 质；
            /// 3、最多可上传5张照片，请填写通过图片上传接口预先上传图片生成好的MediaID 。
            /// </summary>
            [JsonProperty("qualifications")]
            public string Qualifications { get; set; }
            /// <summary>
            /// 补充材料
            /// 最多可上传5张照片，请填写通过图片上传接口预先上传图片生成好的MediaID 
            /// </summary>
            [JsonProperty("business_addition_pics")]
            public string BusinessAdditionPics { get; set; }
            /// <summary>
            /// 补充说明
            /// 1、可填写512字以内 。
            /// 2、若主体为“个人卖家”，则需填写描述“ 该商户已持续从事电子商务经营活动满6个月，且期间经营收入累计超过20万元。”。
            /// </summary>
            [JsonProperty("business_addition_desc")]
            public string BusinessAdditionDesc { get; set; }

        }

        public class LicenseInfo
        {
            /// <summary>
            /// 1、主体为“个体工商户/企业”时，请上传营业执照的证件图片。
            /// <para>2、主体为“党政、机关及事业单位/其他组织”时，请上传登记证书的证件图片。</para>
            /// <para>3、可上传1张图片，请填写通过图片上传接口预先上传图片生成好的MediaID 。</para>
            /// <para>4、图片要求：</para>
            /// <para>（1）请上传证件的彩色扫描件或彩色数码拍摄件，黑白复印件需加盖公章（公章信息需完整） 。</para>
            /// <para>（2）不得添加无关水印（非微信支付商户申请用途的其他水印）。</para>
            /// <para>（3）需提供证件的正面拍摄件，完整、照面信息清晰可见。信息不清晰、扭曲、压缩变形、反光、不完整均不接受。</para>
            /// <para>（4）不接受二次剪裁、翻拍、PS的证件照片。</para>
            /// </summary>
            [JsonProperty("business_license_copy")]
            public string BusinessLicenseCopy { get; set; }
            /// <summary>
            /// 1、主体为“个体工商户/企业”时，请填写营业执照上的注册号/统一社会信用代码，须为15位数字或 18位数字|大写字母。
            /// <para>2、主体为“党政、机关及事业单位/其他组织”时，请填写登记证书的证书编号。</para>
            /// </summary>
            [JsonProperty("business_license_number")]
            public string BusinessLicenseNumber { get; set; }
            /// <summary>
            /// 1、请填写营业执照/登记证书的商家名称，2~110个字符，支持括号 。
            /// <para>2、个体工商户/党政、机关及事业单位，不能以“公司”结尾。</para>
            /// <para>3、个体工商户，若营业执照上商户名称为空或为“无”，请填写"个体户+经营者姓名"，如“个体户张三”</para>
            /// </summary>
            [JsonProperty("merchant_name")]
            public string MerchantName { get; set; }
            /// <summary>
            /// 请填写证件的经营者/法定代表人姓名
            /// </summary>
            [JsonProperty("legal_person")]
            public string LegalPerson { get; set; }
            /// <summary>
            /// 主体为“党政、机关及事业单位/其他组织”时必填，请填写登记证书的注册地址。
            /// </summary>
            [JsonProperty("company_address")]
            public string CompanyAddress { get; set; }
            /// <summary>
            /// 1、主体为“党政、机关及事业单位/其他组织”时必填，请填写证件有效期。
            /// <para>2、若证件有效期为长期，请填写：长期。</para>
            /// <para>3、结束时间需大于开始时间。</para>
            /// <para>4、有效期必须大于60天，即结束时间距当前时间需超过60天。</para>
            /// </summary>
            [JsonProperty("business_time")]
            public string BusinessTime { get; set; }
        }

        public class OrganizationCert
        {
            /// <summary>
            /// 组织机构代码证照片
            /// </summary>
            [JsonProperty("organization_copy")]
            public string OrganizationCopy { get; set; }
            /// <summary>
            /// 1、请填写组织机构代码证上的组织机构代码。
            /// 2、可填写9或10位 数字|字母|连字符。
            /// </summary>
            [JsonProperty("organization_number")]
            public string OrganizationNumber { get; set; }
            /// <summary>
            /// 1、请填写组织机构代码证的有效期限，注意参照示例中的格式。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、结束时间需大于开始时间。
            /// 4、有效期必须大于60天，即结束时间距当前时间需超过60天。
            /// </summary>
            [JsonProperty("organization_time")]
            public string OrganizationTime { get; set; }
        }

        public class IdCardInfo
        {
            /// <summary>
            /// 身份证人像面照片
            /// 1、请上传经营者/法定代表人的身份证人像面照片。
            /// 2、可上传1张图片，请填写通过图片上传接口预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("id_card_copy")]
            public string IdCardCopy { get; set; }
            /// <summary>
            /// 身份证国徽面照片
            /// 1、请上传经营者/法定代表人的身份证国徽面照片。
            /// 2、可上传1张图片，请填写通过图片上传接口预先上传图片生成好的MediaID 。
            /// </summary>
            [JsonProperty("id_card_national")]
            public string IdCardNational { get; set; }
            /// <summary>
            /// 身份证姓名
            /// 1、请填写经营者/法定代表人对应身份证的姓名，2~30个中文字符、英文字符、符号。
            /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_card_name")]
            public string IdCardName { get; set; }
            /// <summary>
            /// 身份证号码
            /// 1、请填写经营者/法定代表人对应身份证的号码。
            /// 2、15位数字或17位数字+1位数字|X ，该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_card_number")]
            public string IdCardNumber { get; set; }
            /// <summary>
            /// 身份证有效期限
            /// 1、请填写身份证有效期的结束时间，注意参照示例中的格式。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、证件有效期需大于60天。
            /// 示例值：2026-06-06，长期
            /// </summary>
            [JsonProperty("id_card_valid_time")]
            public string IdCardValidTime { get; set; }
        }
        /// <summary>
        /// 法人证件信息
        /// </summary>
        public class LegalDocInfo
        {
            /// <summary>
            /// 请填写经营者/法人姓名。
            /// </summary>
            [JsonProperty("id_doc_name")]
            public string id_doc_name { get; set; }
            /// <summary>
            /// 证件号码，7~11位 数字|字母|连字符 。
            /// </summary>
            [JsonProperty("id_doc_number")]
            public string id_doc_number { get; set; }
            /// <summary>
            /// 证件照片
            /// 1、可上传1张图片，请填写通过图片上传接口预先上传图片生成好的MediaID。
            /// 2、2M内的彩色图片，格式可为bmp、png、jpeg、jpg或gif 。
            /// </summary>
            [JsonProperty("id_doc_copy")]
            public string id_doc_copy { get; set; }
            /// <summary>
            /// 证件结束日期
            /// 1、请按照示例值填写。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、证件有效期需大于60天 。
            /// 示例值：2020-01-02
            /// </summary>
            [JsonProperty("doc_period_end")]
            public string doc_period_end { get; set; }
        }
        /// <summary>
        /// 结算银行卡信息
        /// </summary>
        public class SettleBankAccount
        {
            /// <summary>
            /// 账户类型
            /// 1、若主体为企业/党政、机关及事业单位/其他组织，可填写：74-对公账户。
            /// 2、主体为“小微/个人卖家”，可选择：75-对私账户。
            /// 3、若主体为个体工商户，可填写：74-对公账户、75-对私账户。
            /// </summary>
            [JsonProperty("bank_account_type")]
            public string BankAccountType { get; set; }
            /// <summary>
            /// 开户银行
            /// </summary>
            [JsonProperty("account_bank")]
            public string AccountBank { get; set; }
            /// <summary>
            /// 开户名称
            /// 1、选择经营者个人银行卡时，开户名称必须与身份证姓名一致。
            /// 2、选择对公账户时，开户名称必须与营业执照上的“商户名称”一致。
            /// 3、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("account_name")]
            public string AccountName { get; set; }
            /// <summary>
            /// 开户银行省市编码，至少精确到市
            /// </summary>
            [JsonProperty("bank_address_code")]
            public string BankAddressCode { get; set; }
            /// <summary>
            /// 1、17家直连银行无需填写，如为其他银行，开户银行全称（含支行）和开户银行联行号二选一。
            /// 2、详细参见开户银行全称（含支行）对照表。
            /// </summary>
            [JsonProperty("bank_branch_id")]
            public string BankBranchId { get; set; }
            /// <summary>
            /// 开户银行全称 （含支行]
            /// 1、17家直连银行无需填写，如为其他银行，开户银行全称（含支行）和开户银行联行号二选一。
            /// 2、需填写银行全称，如"深圳农村商业银行XXX支行" 。
            /// 3、详细参见开户银行全称（含支行）对照表。
            /// </summary>
            [JsonProperty("bank_name")]
            public string BankName { get; set; }
            /// <summary>
            /// 银行账号
            /// 1、数字，长度遵循系统支持的对公/对私卡号长度要求表。
            /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("account_number")]
            public string AccountNumber { get; set; }
        }
        /// <summary>
        /// 联系人信息
        /// </summary>
        public class ContactInfo
        {
            /// <summary>
            /// 超级管理员类型
            /// 1、主体为“小微/个人卖家 ”，可选择：65-经营者/法人。
            /// 2、主体为“个体工商户/企业/党政、机关及事业单位/其他组织”，可选择：65-经营者/法人、66- 负责人。 （负责人：经商户授权办理微信支付业务的人员，授权范围包括但不限于签约，入驻过程需完成账户验证）。
            /// </summary>
            [JsonProperty("contact_type")]
            public string ContactType { get; set; }
            /// <summary>
            /// 超级管理员姓名
            /// 1、若管理员类型为“法人”，则该姓名需与法人身份证姓名一致。
            /// 2、若管理员类型为“经办人”，则可填写实际经办人的姓名。
            /// 3、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            ///（后续该管理员需使用实名微信号完成签约）
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("contact_name")]
            public string ContactName { get; set; }
            /// <summary>
            /// 超级管理员身份证件号码
            /// 1、若管理员类型为法人，则该身份证号码需与法人身份证号码一致。若管理员类型为经办人，则可填写实际经办人的身份证号码。
            /// 2、可传身份证、来往内地通行证、来往大陆通行证、护照等证件号码。
            /// 3、超级管理员签约时，校验微信号绑定的银行卡实名信息，是否与该证件号码一致。
            /// 4、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("contact_id_card_number")]
            public string IdCardNo { get; set; }
            /// <summary>
            /// 超级管理员手机
            /// 1、请填写管理员的手机号，11位数字， 用于接收微信支付的重要管理信息及日常操作验证码 。
            /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("mobile_phone")]
            public string MobilePhone { get; set; }
            /// <summary>
            /// 超级管理员邮箱
            /// 1、主体类型为“小微商户/个人卖家”可选填，其他主体需必填。
            /// 2、用于接收微信支付的开户邮件及日常业务通知。
            /// 3、需要带@，遵循邮箱格式校验 。
            /// 4、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("contact_email")]
            public string ContactEmail { get; set; }
        }
        /// <summary>
        /// 店铺信息
        /// </summary>
        public class ShopInfo
        {
            /// <summary>
            /// 店铺名称
            /// </summary>
            [JsonProperty("store_name")]
            public string StoreName { get; set; }
            /// <summary>
            /// 店铺链接
            /// 1、店铺二维码or店铺链接二选一必填。
            /// 2、请填写店铺主页链接，需符合网站规范。
            /// </summary>
            [JsonProperty("store_url")]
            public string StoreUrl { get; set; }
            /// <summary>
            /// 店铺二维码
            /// 1、店铺二维码 or 店铺链接二选一必填。
            /// 2、若为电商小程序，可上传店铺页面的小程序二维码。
            /// 3、请填写通过图片上传接口预先上传图片生成好的MediaID，仅能上传1张图片 。
            /// </summary>
            [JsonProperty("store_qr_code")]
            public string StoreQrCode { get; set; }
            /// <summary>
            /// 小程序AppID
            /// 1、可填写已认证的小程序AppID，认证主体需与二级商户主体一致；
            /// 2、完成入驻后， 系统发起二级商户号与该AppID的绑定（即配置为sub_appid，可在发起支付时传入）
            /// </summary>
            [JsonProperty("mini_program_sub_appid")]
            public string AppletId { get; set; }
        }
    }
}
