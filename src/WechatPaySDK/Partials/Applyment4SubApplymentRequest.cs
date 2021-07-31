using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatPaySDK.Converters;
using WechatPaySDK.DataAnnotations;
using WechatPaySDK.Enums;

namespace WechatPaySDK.Request
{
    public partial class Applyment4SubApplymentRequest
    {
        public class Applyment4SubApplymentModel : WxPayObject
        {
            /// <summary>
            /// 业务申请编号
            /// 1、服务商自定义的唯一编号。
            /// 2、每个编号对应一个申请单，每个申请单审核通过后会生成一个微信支付商户号。
            /// 3、若申请单被驳回，可填写相同的“业务申请编号”，即可覆盖修改原申请单信息。
            /// </summary>
            [JsonProperty("business_code")]
            public string BusinessCode { get; set; }
            /// <summary>
            /// 超级管理员需在开户后进行签约，并接收日常重要管理信息和进行资金操作，请确定其为商户法定代表人或负责人。
            /// </summary>
            [JsonProperty("contact_info")]
            public ContactInfo ContactInfo { get; set; }
            /// <summary>
            /// 请填写商家的营业执照/登记证书、经营者/法人的证件等信息。
            /// </summary>
            [JsonProperty("subject_info")]
            public SubjectInfo SubjectInfo { get; set; }
            /// <summary>
            /// 请填写商家的经营业务信息、售卖商品/提供服务场景信息。
            /// </summary>
            [JsonProperty("business_info")]
            public BusinessInfo BusinessInfo { get; set; }
            /// <summary>
            /// 请填写商家的结算费率规则、特殊资质等信息。
            /// </summary>
            [JsonProperty("settlement_info")]
            public SettlementInfo SettlementInfo { get; set; }
            /// <summary>
            /// 结算银行账户
            /// 1、请填写商家提现收款的银行账户信息。
            /// 2、若结算规则id为“719、721、716、717、730、739、727、738、726”，可选填结算账户。
            /// </summary>
            [JsonProperty("bank_account_info")]
            public BankAccountInfo BankAccountInfo { get; set; }
            /// <summary>
            /// 补充材料
            /// </summary>
            [JsonProperty("addition_info")]
            public AdditionInfo AdditionInfo { get; set; }
        }
        /// <summary>
        /// 超级管理员信息
        /// </summary>
        public class ContactInfo
        {
            /// <summary>
            /// 超级管理员姓名
            /// 1、该字段需进行加密处理
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("contact_name")]
            public string ContactName { get; set; }
            /// <summary>
            /// 超级管理员身份证件号码
            /// 1、“超级管理员身份证号码”与“超级管理员微信openid”，二选一必填。
            /// 2、超级管理员签约时，校验微信号绑定的银行卡实名信息，是否与该证件号码一致。
            /// 3、可传身份证、来往内地通行证、来往大陆通行证、护照等证件号码。
            /// 4、该字段需进行加密处理
            /// </summary>
            [JsonProperty("contact_id_number")]
            [EncryptionRequired]
            public string ContactIdNumber { get; set; }
            /// <summary>
            /// 超级管理员微信openid
            /// 1、“超级管理员身份证件号码”与“超级管理员微信openid”，二选一必填。
            /// 2、超级管理员签约时，校验微信号是否与该微信openid一致。
            /// </summary>
            [JsonProperty("openid")]
            public string OpenID { get; set; }
            /// <summary>
            /// 联系手机
            /// 1、11位数字。
            /// 2、用于接收微信支付的重要管理信息及日常操作验证码。
            /// 3、该字段需进行加密处理
            /// </summary>
            [JsonProperty("mobile_phone")]
            [EncryptionRequired]
            public string MobilePhone { get; set; }
            /// <summary>
            /// 联系邮箱
            /// 1、用于接收微信支付的开户邮件及日常业务通知。
            /// 2、需要带@，遵循邮箱格式校验，该字段需进行加密处理
            /// </summary>
            [JsonProperty("contact_email")]
            [EncryptionRequired]
            public string ContactEmail { get; set; }
        }
        /// <summary>
        /// 商户主体资料
        /// </summary>
        public class SubjectInfo
        {
            /// <summary>
            /// 主体类型
            /// 主体类型需与营业执照/登记证书上一致，可参考《选择主体指引》
            /// SUBJECT_TYPE_INDIVIDUAL（个体户）：营业执照上的主体类型一般为个体户、个体工商户、个体经营；
            /// SUBJECT_TYPE_ENTERPRISE（企业）：营业执照上的主体类型一般为有限公司、有限责任公司；
            /// SUBJECT_TYPE_INSTITUTIONS（党政、机关及事业单位）：包括国内各级、各类政府机构、事业单位等（如：公安、党团、司法、交通、旅游、工商税务、市政、医疗、教育、学校等机构）；
            /// SUBJECT_TYPE_OTHERS（其他组织）：不属于企业、政府/事业单位的组织机构（如社会团体、民办非企业、基金会），要求机构已办理组织机构代码证。
            /// </summary>
            [JsonProperty("subject_type"), JsonConverter(typeof(EnumToStringConverter))]
            public MerchantType SubjectType { get; set; }
            /// <summary>
            /// 1、主体为个体户/企业，必填。
            /// 2、请上传“营业执照”，需年检章齐全，当年注册除外。
            /// </summary>
            [JsonProperty("business_license_info")]
            public BusinessLicenseInfo BusinessLicenseInfo { get; set; }
            /// <summary>
            /// 登记证书
            /// 主体为党政、机关及事业单位/其他组织，必填。
            /// 1、党政、机关及事业单位：请上传相关部门颁发的证书，如：事业单位法人证书、统一社会信用代码证书。
            /// 2、其他组织：请上传相关部门颁发的证书，如：社会团体法人登记证书、民办非企业单位登记证书、基金会法人登记证书。
            /// </summary>
            [JsonProperty("certificate_info")]
            public CertificateInfo CertificateInfo { get; set; }
            /// <summary>
            /// 组织机构代码证
            /// 主体为企业/党政、机关及事业单位/其他组织，且证件号码不是18位时必填。
            /// </summary>
            [JsonProperty("organization_info")]
            public OrganizationInfo OrganizationInfo { get; set; }
            /// <summary>
            /// 单位证明函照片
            /// 1、主体类型为党政、机关及事业单位必填。
            /// 2、请参照示例图打印单位证明函，全部信息需打印，不支持手写商户信息，并加盖公章。
            /// 3、可上传1张图片，请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("certificate_letter_copy")]
            public string CertificateLetterCopy { get; set; }
            /// <summary>
            /// 经营者/法人身份证件
            /// 1、个体户：请上传经营者的身份证件。
            /// 2、企业/党政、机关及事业单位/其他组织：请上传法人的身份证件。
            /// </summary>
            [JsonProperty("identity_info")]
            public IdentityInfo IdentityInfo { get; set; }
            /// <summary>
            /// 最终受益人信息
            /// 若经营者/法人不是最终受益所有人，则需提填写受益所有人信息。
            ///         根据国家相关法律法规，需要提供公司受益所有人信息，受益所有人需符合至少以下条件之一：
            /// 1、直接或者间接拥有超过25%公司股权或者表决权的自然人。
            /// 2、通过人事、财务等其他方式对公司进行控制的自然人。
            /// 3、公司的高级管理人员，包括公司的经理、副经理、财务负责人、上市公司董事会秘书和公司章程规定的其他人员。
            /// </summary>
            [JsonProperty("ubo_info")]
            public UboInfo UboInfo { get; set; }
        }

        public class BusinessLicenseInfo
        {
            /// <summary>
            /// 营业执照照片
            /// </summary>
            [JsonProperty("license_copy")]
            public string LicenseCopy { get; set; }
            /// <summary>
            /// 注册号/统一社会信用代码
            /// </summary>
            [JsonProperty("license_number")]
            public string LicenseNumber { get; set; }
            /// <summary>
            /// 商户名称
            /// </summary>
            [JsonProperty("merchant_name")]
            public string MerchantName { get; set; }
            /// <summary>
            /// 个体户经营者/法人姓名
            /// </summary>
            [JsonProperty("legal_person")]
            public string LegalPerson { get; set; }
        }
        /// <summary>
        /// 登记证书
        /// </summary>
        public class CertificateInfo
        {
            /// <summary>
            /// 登记证书照片
            /// 1、请填写通过《图片上传API》预先生成的MediaID，上传1张图片即可；
            /// 2、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水 印（如微信支付认证） 。
            /// </summary>
            [JsonProperty("cert_copy")]
            public string CertCopy { get; set; }
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
            /// <para>示例值：CERTIFICATE_TYPE_2388</para>
            /// </summary>
            [JsonProperty("cert_type"), JsonConverter(typeof(EnumToStringConverter))]
            public CertType CertType { get; set; }
            /// <summary>
            /// 证书号
            /// </summary>
            [JsonProperty("cert_number")]
            public string CertNumber { get; set; }
            /// <summary>
            /// 商户名称
            /// </summary>
            [JsonProperty("merchant_name")]
            public string MerchantName { get; set; }
            /// <summary>
            /// 注册地址
            /// </summary>
            [JsonProperty("company_address")]
            public string CompanyAddress { get; set; }
            /// <summary>
            /// 法人姓名
            /// </summary>
            [JsonProperty("legal_person")]
            public string LegalPerson { get; set; }
            /// <summary>
            /// 有效期限开始日期
            /// 1、必填， 请参考示例值填写。
            /// 2、开始日期，开始日期需大于当前日期
            /// 示例值：2019-08-01
            /// </summary>
            [JsonProperty("period_begin")]
            public string PeriodBegin { get; set; }
            /// <summary>
            /// 有效期限结束日期
            /// 1、必填，请参考示例值填写。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、结束日期大于开始日期。
            /// 4、有效期必须大于60天。
            /// 示例值：2019-08-01，长期
            /// </summary>
            [JsonProperty("period_end")]
            public string PeriodEnd { get; set; }
        }
        /// <summary>
        /// 组织机构代码证
        /// </summary>
        public class OrganizationInfo
        {
            /// <summary>
            /// 组织机构代码证照片
            /// </summary>
            [JsonProperty("organization_copy")]
            public string OrganizationCopy { get; set; }
            /// <summary>
            /// 组织机构代码
            /// </summary>
            [JsonProperty("organization_code")]
            public string OrganizationCode { get; set; }
            /// <summary>
            /// 组织机构代码证有效期开始日期
            /// 1、请按照示例值填写。
            /// 2、证件有效期需大于60天。
            /// 示例值：2019-08-01
            /// </summary>
            [JsonProperty("org_period_begin")]
            public string OrgPeriodBegin { get; set; }
            /// <summary>
            /// 组织机构代码证有效期结束日期
            /// 1、请按照示例值填写。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、结束日期大于开始日期。
            /// 4、证件有效期需大于60天。
            /// 示例值：2019-08-01，长期
            /// </summary>
            [JsonProperty("org_period_end")]
            public string OrgPeriodEnd { get; set; }
        }
        /// <summary>
        /// 经营者/法人身份证件
        /// </summary>
        public class IdentityInfo
        {
            /// <summary>
            /// 证件类型
            /// 个体户/企业/党政、机关及事业单位/其他组织：可选择任一证件类型。
            /// 枚举值：
            /// IDENTIFICATION_TYPE_IDCARD：中国大陆居民-身份证
            /// IDENTIFICATION_TYPE_OVERSEA_PASSPORT：其他国家或地区居民-护照
            /// IDENTIFICATION_TYPE_HONGKONG_PASSPORT：中国香港居民-来往内地通行证
            /// IDENTIFICATION_TYPE_MACAO_PASSPORT：中国澳门居民-来往内地通行证
            /// IDENTIFICATION_TYPE_TAIWAN_PASSPORT：中国台湾居民-来往大陆通行证
            /// 示例值：IDENTIFICATION_TYPE_IDCARD
            /// </summary>
            [JsonProperty("id_doc_type"), JsonConverter(typeof(EnumToStringConverter))]
            public IdentityType IdDocType { get; set; }
            /// <summary>
            /// 证件类型为“身份证”时填写。
            /// </summary>
            [JsonProperty("id_card_info")]
            public IdCardInfo IdCardInfo { get; set; }
            /// <summary>
            /// 证件类型为“来往内地通行证、来往大陆通行证、护照”时填写。
            /// </summary>
            [JsonProperty("id_doc_info")]
            public IdDocInfo IdDocInfo { get; set; }
            /// <summary>
            /// 经营者/法人是否为受益人
            /// </summary>
            [JsonProperty("owner")]
            public bool Owner { get; set; }
        }

        public class IdCardInfo
        {
            /// <summary>
            /// 身份证人像面照片
            /// </summary>
            [JsonProperty("id_card_copy")]
            public string IdCardCopy { get; set; }
            /// <summary>
            /// 身份证国徽面照片
            /// </summary>
            [JsonProperty("id_card_national")]
            public string IdCardNational { get; set; }
            /// <summary>
            /// 身份证姓名
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_card_name")]
            public string IdCardName { get; set; }
            /// <summary>
            /// 身份证号码
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_card_number")]
            public string IdCardNumber { get; set; }
            /// <summary>
            /// 身份证有效期开始时间
            /// </summary>
            [JsonProperty("card_period_begin")]
            public string CardPeriodBegin { get; set; }
            /// <summary>
            /// 身份证有效期结束时间
            /// 1、必填，请按照示例值填写。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、结束时间大于开始时间。
            /// 4、证件有效期需大于60天。
            /// 示例值：2026-06-06
            /// </summary>
            [JsonProperty("card_period_end")]
            public string CardPeriodEnd { get; set; }
        }

        public class IdDocInfo
        {
            /// <summary>
            /// 证件照片
            /// </summary>
            [JsonProperty("id_doc_copy")]
            public string IdDocCopy { get; set; }
            /// <summary>
            /// 证件姓名
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_doc_name")]
            public string IdDocName { get; set; }
            /// <summary>
            /// 证件号码
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_doc_number")]
            public string IdDocNumber { get; set; }
            /// <summary>
            /// 证件有效期开始时间
            /// 1、选填，请按照示例值填写。
            /// 2、结束时间大于开始时间。
            /// 示例值：2019-06-06
            /// </summary>
            [JsonProperty("doc_period_begin")]
            public string DocPeriodBegin { get; set; }
            /// <summary>
            /// 证件有效期结束时间
            /// 1、必填，请按照示例值填写。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、结束时间大于开始时间。
            /// 4、证件有效期需大于60天。
            /// 示例值：2026-06-06
            /// </summary>
            [JsonProperty("doc_period_end")]
            public string DocPeriodEnd { get; set; }
        }
        /// <summary>
        /// 最终受益人信息(UBO]
        /// 若经营者/法人不是最终受益所有人，则需提填写受益所有人信息。
        /// 根据国家相关法律法规，需要提供公司受益所有人信息，受益所有人需符合至少以下条件之一：
        /// 1、直接或者间接拥有超过25%公司股权或者表决权的自然人。
        /// 2、通过人事、财务等其他方式对公司进行控制的自然人。
        /// 3、公司的高级管理人员，包括公司的经理、副经理、财务负责人、上市公司董事会秘书和公司章程规定的其他人员。
        /// </summary>
        public class UboInfo
        {
            /// <summary>
            /// 证件类型
            /// </summary>
            [JsonProperty("id_type")]
            public string IdType { get; set; }
            /// <summary>
            /// 填写受益人的证件类型。
            /// 枚举值：
            /// IDENTIFICATION_TYPE_IDCARD：中国大陆居民-身份证
            /// IDENTIFICATION_TYPE_OVERSEA_PASSPORT：其他国家或地区居民-护照
            /// IDENTIFICATION_TYPE_HONGKONG_PASSPORT：中国香港居民-来往内地通行证
            /// IDENTIFICATION_TYPE_MACAO_PASSPORT：中国澳门居民-来往内地通行证
            /// IDENTIFICATION_TYPE_TAIWAN_PASSPORT：中国台湾居民-来往大陆通行证
            /// 示例值：IDENTIFICATION_TYPE_IDCARD
            /// </summary>
            [JsonProperty("id_card_copy"), JsonConverter(typeof(EnumToStringConverter))]
            public IdentityType IdCardCopy { get; set; }
            /// <summary>
            /// 身份证人像面照片
            /// 1、受益人的证件类型为“身份证”时，上传身份证人像面照片。
            /// 2、可上传1张图片，请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// 3、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
            /// </summary>
            [JsonProperty("id_card_national")]
            public string IdCardNational { get; set; }
            /// <summary>
            /// 身份证国徽面照片
            /// 1、受益人的证件类型为“来往内地通行证、来往大陆通行证、护照”时，上传证件照片。
            /// 2、可上传1张图片，请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// 3、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
            /// </summary>
            [JsonProperty("id_doc_copy")]
            public string IdDocCopy { get; set; }
            /// <summary>
            /// 受益人姓名
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("name")]
            public string Name { get; set; }
            /// <summary>
            /// 证件号码
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("id_number")]
            public string IdNumber { get; set; }
            /// <summary>
            /// 证件有效期开始时间
            /// </summary>
            [JsonProperty("id_period_begin")]
            public string IdPeriodBegin { get; set; }
            /// <summary>
            /// 证件有效期结束时间
            /// </summary>
            [JsonProperty("id_period_end")]
            public string IdPeriodEnd { get; set; }
        }
        /// <summary>
        /// 经营资料
        /// </summary>
        public class BusinessInfo
        {
            /// <summary>
            /// 商户简称
            /// 	1、请输入2-30个字符，支持中文/字母/数字/特殊符号
            /// 2、在支付完成页向买家展示，需与微信经营类目相关；
            /// 3、简称要求
            ///     （1）不支持单纯以人名来命名，若为个体户经营，可用“个体户+经营者名称”或“经营者名 称+业务”命名，如“个体户张三”或“张三餐饮店”；
            ///     （2）不支持无实际意义的文案，如“XX特约商户”、“800”、“XX客服电话XXX”；
            /// 示例值：张三餐饮店
            /// </summary>
            [JsonProperty("merchant_shortname")]
            public string MerchantShortname { get; set; }
            /// <summary>
            /// 客服电话
            /// 1、请填写真实有效的客服电话，将在交易记录中向买家展示，提供咨询服务；
            /// 2、请确保电话畅通，以便入驻后平台回拨确认。
            /// 示例值：0758XXXXX
            /// </summary>
            [JsonProperty("service_phone")]
            public string ServicePhone { get; set; }
            /// <summary>
            /// 经营场景
            /// </summary>
            [JsonProperty("sales_info")]
            public SalesInfo SalesInfo { get; set; }
        }

        public class SalesInfo
        {
            /// <summary>
            /// 经营场景类型
            /// 1、请勾选实际售卖商品/提供服务场景（至少一项），以便为你开通需要的支付权限。
            /// 2、建议只勾选目前必须的场景，以便尽快通过入驻审核，其他支付权限可在入驻后再根据实际需要发起申请。
            /// 枚举值：
            /// SALES_SCENES_STORE：线下门店
            /// SALES_SCENES_MP：公众号
            /// SALES_SCENES_MINI_PROGRAM：小程序
            /// SALES_SCENES_WEB：互联网
            /// SALES_SCENES_APP：APP
            /// SALES_SCENES_WEWORK：企业微信
            /// 示例值： ”SALES_SCENES_STORE”,”SALES_SCENES_MP”
            /// </summary>
            [JsonProperty("sales_scenes_type"), JsonConverter(typeof(EnumToStringConverter))]
            public SalesScenes[] SalesScenesType { get; set; }
            /// <summary>
            /// 线下门店场景
            /// </summary>
            [JsonProperty("biz_store_info")]
            public BizStoreInfo BizStoreInfo { get; set; }
            [JsonProperty("mp_info")]
            public MpInfo MPInfo { get; set; }
            [JsonProperty("mini_program_info")]
            public MiniProgramInfo MiniProgramInfo { get; set; }
            [JsonProperty("app_info")]
            public AppInfo AppInfo { get; set; }
            [JsonProperty("web_info")]
            public WebInfo WebInfo { get; set; }
            [JsonProperty("wework_info")]
            public WeworkInfo WeworkInfo { get; set; }
        }
        /// <summary>
        /// 线下门店场景
        /// </summary>
        public class BizStoreInfo
        {
            /// <summary>
            /// 门店名称
            /// </summary>
            [JsonProperty("biz_store_name")]
            public string BizStoreName { get; set; }
            /// <summary>
            /// 门店省市编码
            /// </summary>
            [JsonProperty("biz_address_code")]
            public string BizAddressCode { get; set; }
            /// <summary>
            /// 门店地址
            /// </summary>
            [JsonProperty("biz_store_address")]
            public string BizStoreAddress { get; set; }
            /// <summary>
            /// 门店门头照片
            /// </summary>
            [JsonProperty("store_entrance_pic")]
            public string[] StoreEntrancePic { get; set; }
            /// <summary>
            /// 店内环境照片
            /// </summary>
            [JsonProperty("indoor_pic")]
            public string[] IndoorPic { get; set; }
            /// <summary>
            /// 线下场所对应的商家APPID
            /// 1、可填写已认证的公众号、小程序、应用的APPID，其中公众号APPID需是已认证的服务 号、政府或媒体类型的订阅号。
            /// 2、完成进件后，系统发起特约商户号与该AppID的绑定（即配置为sub_appid，可在发起 支付时传入）
            /// （1）若APPID主体与商家主体一致，则直接完成绑定；
            /// （2）若APPID主体与商家主体不一致，则商户签约时显示《联合营运承诺函》，并且 AppID的管理员需登录公众平台确认绑定意愿；（ 暂不支持绑定异主体的应用APPID）
            /// </summary>
            [JsonProperty("biz_sub_appid")]
            public string BizSubAppid { get; set; }
        }
        /// <summary>
        /// 公众号场景
        /// </summary>
        public class MpInfo
        {
            /// <summary>
            /// 服务商公众号APPID
            /// 1、服务商公众号APPID与商家公众号APPID，二选一必填。
            /// 2、可填写当前服务商商户号已绑定的公众号APPID。
            /// </summary>
            [JsonProperty("mp_appid")]
            public string MPAppId { get; set; }
            /// <summary>
            /// 商家公众号APPID
            /// 1、服务商公众号APPID与商家公众号APPID，二选一必填。
            /// 2、可填写与商家主体一致且已认证的公众号APPID，需是已认证的服务号、政府或媒体类型的订阅号。
            /// 3、审核通过后，系统将发起特约商家商户号与该AppID的绑定（即配置为sub_appid），服务商随后可在发起支付时选择传入该appid，以完成支付，并获取sub_openid用于数据统计，营销等业务场景 。
            /// 示例值：wx1234567890123456
            /// </summary>
            [JsonProperty("mp_sub_appid")]
            public string MPSubAppId { get; set; }
            /// <summary>
            /// 公众号页面截图
            /// 1、请提供展示商品/服务的页面截图/设计稿（最多5张），若公众号未建设完善或未上线请务必提供。
            /// 2、请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("mp_pics")]
            public string[] MPPics { get; set; } = new string[0];
        }
        /// <summary>
        /// 小程序场景
        /// </summary>
        public class MiniProgramInfo
        {
            /// <summary>
            /// 服务商小程序APPID
            /// 1、服务商小程序APPID与商家小程序APPID，二选一必填。
            /// 2、可填写当前服务商商户号已绑定的小程序APPID。
            /// </summary>
            [JsonProperty("mini_program_appid")]
            public string MiniProgramAppId { get; set; }
            /// <summary>
            /// 商家小程序APPID
            /// 1、服务商小程序APPID与商家小程序APPID，二选一必填；
            /// 2、请填写已认证的小程序APPID；
            /// 3、完成进件后，系统发起特约商户号与该AppID的绑定（即配置为sub_appid可在发起支付时传入）
            /// （1）若APPID主体与商家主体/服务商主体一致，则直接完成绑定；
            /// （2）若APPID主体与商家主体/服务商主体不一致，则商户签约时显示《联合营运承诺 函》，并且AppID的管理员需登录公众平台确认绑定意愿；
            /// 示例值：wx1234567890123456
            /// </summary>
            [JsonProperty("mini_program_sub_appid")]
            public string MiniProgramSubAppId { get; set; }
            /// <summary>
            /// 小程序截图
            /// 1、请提供展示商品/服务的页面截图/设计稿（最多5张），若小程序未建设完善或未上线 请务必提供；
            /// 2、请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("mini_program_pics")]
            public string[] MiniProgramPics { get; set; }
        }
        /// <summary>
        /// APP场景
        /// </summary>
        public class AppInfo
        {
            /// <summary>
            /// 服务商应用APPID
            /// </summary>
            [JsonProperty("app_appid")]
            public string AppAppId { get; set; }
            /// <summary>
            /// 商家应用APPID
            /// </summary>
            [JsonProperty("app_sub_appid")]
            public string AppSubAppId { get; set; }
            /// <summary>
            /// APP截图
            /// 1、请提供APP首页截图、尾页截图、应用内截图、支付页截图各1张。
            /// 2、请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("app_pics")]
            public string[] AppPics { get; set; }
        }
        /// <summary>
        /// 互联网网站场景
        /// </summary>
        public class WebInfo
        {
            /// <summary>
            /// 互联网网站域名
            /// 1、如为PC端商城、智能终端等场景，可上传官网链接。
            /// 2、网站域名需ICP备案，若备案主体与申请主体不同，请上传加盖公章的网站授权函。
            /// 示例值：http://www.qq.com
            /// </summary>
            [JsonProperty("domain")]
            public string Domain { get; set; }
            /// <summary>
            /// 网站授权函
            /// 1、若备案主体与申请主体不同，请务必上传加盖公章的网站授权函。
            /// 2、请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("web_authorisation")]
            public string WebAuthorisation { get; set; }
            /// <summary>
            /// 互联网网站对应的商家APPID
            /// 1、可填写已认证的公众号、小程序、应用的APPID，其中公众号APPID需是已认证的服务 号、政府或媒体类型的订阅号；
            /// 2、完成进件后，系统发起特约商户号与该AppID的绑定（即配置为sub_appid，可在发起 支付时传入）
            ///    （1）若APPID主体与商家主体一致，则直接完成绑定；
            ///    （2）若APPID主体与商家主体不一致，则商户签约时显示《联合营运承诺函》，并且 AppID的管理员需登录公众平台确认绑定意愿；（ 暂不支持绑定异主体的应用APPID）。
            /// </summary>
            [JsonProperty("web_appid")]
            public string WebAppid { get; set; }
        }

        public class WeworkInfo
        {
            [JsonProperty("corp_id")]
            public string CorpId { get; set; }
            [JsonProperty("sub_corp_id")]
            public string SubCorpId { get; set; }
            [JsonProperty("wework_pics")]
            public string[] WeworkPics { get; set; }
        }
        /// <summary>
        /// 结算规则
        /// </summary>
        public class SettlementInfo
        {
            /// <summary>
            /// 入驻结算规则ID，个体户719，企业716
            /// </summary>
            [JsonProperty("settlement_id")]
            public string SettlementId { get; set; }
            /// <summary>
            /// 所属行业
            /// </summary>
            [JsonProperty("qualification_type")]
            public string QualificationType { get; set; }
            /// <summary>
            /// 特殊资质图片
            /// 1、根据所属行业的特殊资质要求提供，详情查看《费率结算规则对照表》。
            /// 2、请提供为“申请商家主体”所属的特殊资质，可授权使用总公司/分公司的特殊资 质；
            /// 3、最多可上传5张照片，请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("qualifications")]
            public string[] Qualifications { get; set; } = new string[0];
            /// <summary>
            /// 优惠费率活动ID
            /// 选择指定活动ID，详细参见《优惠费率活动对照表》。
            /// 示例值：20191030111cff5b5e
            /// </summary>
            [JsonProperty("activities_id")]
            public string ActivitiesId { get; set; }
            /// <summary>
            /// 优惠费率活动值
            /// 根据优惠费率活动规则，由服务商自定义填写，支持两个小数点，需在优惠费率活动ID指定费率范围内，如0.6%（接口无需传%，只需传数字）。
            /// 示例值：0.6
            /// </summary>
            [JsonProperty("activities_rate")]
            public string ActivitiesRate { get; set; }
            /// <summary>
            /// 优惠费率活动补充材料
            /// 	1、根据所选优惠费率活动，提供相关材料，详细参见《优惠费率活动对照表》。
            /// 2、最多可上传5张照片，请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("activities_additions")]
            public string[] ActivitiesAdditions { get; set; } = new string[0];
        }
        /// <summary>
        /// 结算银行账户
        /// </summary>
        public class BankAccountInfo
        {
            /// <summary>
            /// 账户类型
            /// 1、若主体为企业/党政、机关及事业单位/其他组织，可填写：对公银行账户。
            /// 2、若主体为个体户，可选择填写：对公银行账户或经营者个人银行卡。
            /// 枚举值：

            /// BANK_ACCOUNT_TYPE_CORPORATE：对公银行账户
            /// BANK_ACCOUNT_TYPE_PERSONAL：经营者个人银行卡
            /// 示例值：BANK_ACCOUNT_TYPE_CORPORATE
            /// </summary>
            [JsonProperty("bank_account_type"), JsonConverter(typeof(EnumToStringConverter))]
            public BankAccountType BankAccountType { get; set; }
            /// <summary>
            /// 开户名称
            /// 1、选择“经营者个人银行卡”时，开户名称必须与“经营者证件姓名”一致。
            /// 2、选择“对公银行账户”时，开户名称必须与营业执照/登记证书的“商户名称”一致。
            /// 3、该字段需进行加密处理，加密方法详见《敏感信息加密说明》。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// </summary>
            [EncryptionRequired]
            [JsonProperty("account_name")]
            public string AccountName { get; set; }
            /// <summary>
            /// 开户银行
            /// </summary>
            [JsonProperty("account_bank")]
            public string AccountBank { get; set; }
            /// <summary>
            /// 开户银行省市编码
            /// </summary>
            [JsonProperty("bank_address_code")]
            public string BankAddressCode { get; set; }
            /// <summary>
            /// 开户银行联行号
            /// </summary>
            [JsonProperty("bank_branch_id")]
            public string BankBranchId { get; set; }
            /// <summary>
            /// 开户银行全称（含支行]
            /// </summary>
            [JsonProperty("bank_name")]
            public string BankName { get; set; }
            /// <summary>
            /// 银行账号
            /// </summary>
            [JsonProperty("account_number")]
            [EncryptionRequired]
            public string AccountNumber { get; set; }
        }
        /// <summary>
        /// 补充材料
        /// </summary>
        public class AdditionInfo
        {
            /// <summary>
            /// 法人开户承诺函
            /// </summary>
            [JsonProperty("legal_person_commitment")]
            public string LegalPersonCommitment { get; set; }
            /// <summary>
            /// 法人开户意愿视频
            /// 1、建议法人按如下话术录制“法人开户意愿视频”：
            ///         我是#公司全称#的法定代表人（或负责人），特此证明本公司申请的商户号为我司真实意愿开立且用于XX业务（或XX服务）。我司现有业务符合法律法规及腾讯的相关规定。
            /// 2、支持上传20M内的视频，格式可为avi、wmv、mpeg、mp4、mov、mkv、flv、f4v、m4v、rmvb。
            /// 3、请填写通过《视频上传API》预先上传视频生成好的MediaID。
            /// </summary>
            [JsonProperty("legal_person_video")]
            public string LegalPersonVideo { get; set; }
            /// <summary>
            /// 补充材料
            /// 1、根据驳回要求提供额外信息，如：
            ///     （1）业务模式不清晰时，需详细描述支付场景或提供相关材料（如业务说明/门店照/ 手持证件照等）；
            ///     （2）特殊业务要求提供相关的协议材料等；
            /// 2、请填写通过《图片上传API》预先上传图片生成好的MediaID。
            /// </summary>
            [JsonProperty("business_addition_pics")]
            public string[] BusinessAdditionPics { get; set; }
            /// <summary>
            /// 补充说明
            /// 根据驳回要求提供额外信息，如：业务模式不清晰时，请详细描述支付场景。
            /// 示例值：特殊情况，说明原因
            /// </summary>
            [JsonProperty("business_addition_msg")]
            public string BusinessAdditionMsg { get; set; }
        }
    }
}
