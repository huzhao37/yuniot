using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace XCode.Membership
{
    /// <summary>用户</summary>
    [Serializable]
    [DataObject]
    [Description("用户")]
    [BindIndex("IU_User_Name", true, "Name")]
    [BindIndex("IX_User_RoleID", false, "RoleID")]
    [BindTable("User", Description = "用户", ConnName = "Membership", DbType = DatabaseType.SqlServer)]
    public partial class User<TEntity> : IUser
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "int")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Name;
        /// <summary>名称。登录用户名</summary>
        [DisplayName("名称")]
        [Description("名称。登录用户名")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("Name", "名称。登录用户名", "nvarchar(50)", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private String _Password;
        /// <summary>密码</summary>
        [DisplayName("密码")]
        [Description("密码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Password", "密码", "nvarchar(50)")]
        public String Password { get { return _Password; } set { if (OnPropertyChanging(__.Password, value)) { _Password = value; OnPropertyChanged(__.Password); } } }

        private String _DisplayName;
        /// <summary>昵称</summary>
        [DisplayName("昵称")]
        [Description("昵称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DisplayName", "昵称", "nvarchar(50)")]
        public String DisplayName { get { return _DisplayName; } set { if (OnPropertyChanging(__.DisplayName, value)) { _DisplayName = value; OnPropertyChanged(__.DisplayName); } } }

        private SexKinds _Sex;
        /// <summary>性别。未知、男、女</summary>
        [DisplayName("性别")]
        [Description("性别。未知、男、女")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sex", "性别。未知、男、女", "int")]
        public SexKinds Sex { get { return _Sex; } set { if (OnPropertyChanging(__.Sex, value)) { _Sex = value; OnPropertyChanged(__.Sex); } } }

        private String _Mail;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Mail", "邮件", "nvarchar(50)")]
        public String Mail { get { return _Mail; } set { if (OnPropertyChanging(__.Mail, value)) { _Mail = value; OnPropertyChanged(__.Mail); } } }

        private String _Mobile;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Mobile", "手机", "nvarchar(50)")]
        public String Mobile { get { return _Mobile; } set { if (OnPropertyChanging(__.Mobile, value)) { _Mobile = value; OnPropertyChanged(__.Mobile); } } }

        private String _Code;
        /// <summary>代码。身份证、员工编号等</summary>
        [DisplayName("代码")]
        [Description("代码。身份证、员工编号等")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "代码。身份证、员工编号等", "nvarchar(50)")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private String _Avatar;
        /// <summary>头像</summary>
        [DisplayName("头像")]
        [Description("头像")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Avatar", "头像", "nvarchar(200)")]
        public String Avatar { get { return _Avatar; } set { if (OnPropertyChanging(__.Avatar, value)) { _Avatar = value; OnPropertyChanged(__.Avatar); } } }

        private Int32 _RoleID;
        /// <summary>角色。主要角色</summary>
        [DisplayName("角色")]
        [Description("角色。主要角色")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleID", "角色。主要角色", "int")]
        public Int32 RoleID { get { return _RoleID; } set { if (OnPropertyChanging(__.RoleID, value)) { _RoleID = value; OnPropertyChanged(__.RoleID); } } }

        private String _RoleIDs;
        /// <summary>角色组。次要角色集合</summary>
        [DisplayName("角色组")]
        [Description("角色组。次要角色集合")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("RoleIDs", "角色组。次要角色集合", "nvarchar(200)")]
        public String RoleIDs { get { return _RoleIDs; } set { if (OnPropertyChanging(__.RoleIDs, value)) { _RoleIDs = value; OnPropertyChanged(__.RoleIDs); } } }

        private Boolean _Online;
        /// <summary>在线</summary>
        [DisplayName("在线")]
        [Description("在线")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Online", "在线", "bit")]
        public Boolean Online { get { return _Online; } set { if (OnPropertyChanging(__.Online, value)) { _Online = value; OnPropertyChanged(__.Online); } } }

        private Boolean _Enable;
        /// <summary>启用</summary>
        [DisplayName("启用")]
        [Description("启用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "启用", "bit")]
        public Boolean Enable { get { return _Enable; } set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } } }

        private Int32 _Logins;
        /// <summary>登录次数</summary>
        [DisplayName("登录次数")]
        [Description("登录次数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Logins", "登录次数", "int")]
        public Int32 Logins { get { return _Logins; } set { if (OnPropertyChanging(__.Logins, value)) { _Logins = value; OnPropertyChanged(__.Logins); } } }

        private DateTime _LastLogin;
        /// <summary>最后登录</summary>
        [DisplayName("最后登录")]
        [Description("最后登录")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastLogin", "最后登录", "datetime")]
        public DateTime LastLogin { get { return _LastLogin; } set { if (OnPropertyChanging(__.LastLogin, value)) { _LastLogin = value; OnPropertyChanged(__.LastLogin); } } }

        private String _LastLoginIP;
        /// <summary>最后登录IP</summary>
        [DisplayName("最后登录IP")]
        [Description("最后登录IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("LastLoginIP", "最后登录IP", "nvarchar(50)")]
        public String LastLoginIP { get { return _LastLoginIP; } set { if (OnPropertyChanging(__.LastLoginIP, value)) { _LastLoginIP = value; OnPropertyChanged(__.LastLoginIP); } } }

        private DateTime _RegisterTime;
        /// <summary>注册时间</summary>
        [DisplayName("注册时间")]
        [Description("注册时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("RegisterTime", "注册时间", "datetime")]
        public DateTime RegisterTime { get { return _RegisterTime; } set { if (OnPropertyChanging(__.RegisterTime, value)) { _RegisterTime = value; OnPropertyChanged(__.RegisterTime); } } }

        private String _RegisterIP;
        /// <summary>注册IP</summary>
        [DisplayName("注册IP")]
        [Description("注册IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RegisterIP", "注册IP", "nvarchar(50)")]
        public String RegisterIP { get { return _RegisterIP; } set { if (OnPropertyChanging(__.RegisterIP, value)) { _RegisterIP = value; OnPropertyChanged(__.RegisterIP); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.Name : return _Name;
                    case __.Password : return _Password;
                    case __.DisplayName : return _DisplayName;
                    case __.Sex : return _Sex;
                    case __.Mail : return _Mail;
                    case __.Mobile : return _Mobile;
                    case __.Code : return _Code;
                    case __.Avatar : return _Avatar;
                    case __.RoleID : return _RoleID;
                    case __.RoleIDs : return _RoleIDs;
                    case __.Online : return _Online;
                    case __.Enable : return _Enable;
                    case __.Logins : return _Logins;
                    case __.LastLogin : return _LastLogin;
                    case __.LastLoginIP : return _LastLoginIP;
                    case __.RegisterTime : return _RegisterTime;
                    case __.RegisterIP : return _RegisterIP;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Password : _Password = Convert.ToString(value); break;
                    case __.DisplayName : _DisplayName = Convert.ToString(value); break;
                    case __.Sex : _Sex = (SexKinds)Convert.ToInt32(value); break;
                    case __.Mail : _Mail = Convert.ToString(value); break;
                    case __.Mobile : _Mobile = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Avatar : _Avatar = Convert.ToString(value); break;
                    case __.RoleID : _RoleID = Convert.ToInt32(value); break;
                    case __.RoleIDs : _RoleIDs = Convert.ToString(value); break;
                    case __.Online : _Online = Convert.ToBoolean(value); break;
                    case __.Enable : _Enable = Convert.ToBoolean(value); break;
                    case __.Logins : _Logins = Convert.ToInt32(value); break;
                    case __.LastLogin : _LastLogin = Convert.ToDateTime(value); break;
                    case __.LastLoginIP : _LastLoginIP = Convert.ToString(value); break;
                    case __.RegisterTime : _RegisterTime = Convert.ToDateTime(value); break;
                    case __.RegisterIP : _RegisterIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>名称。登录用户名</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>密码</summary>
            public static readonly Field Password = FindByName(__.Password);

            /// <summary>昵称</summary>
            public static readonly Field DisplayName = FindByName(__.DisplayName);

            /// <summary>性别。未知、男、女</summary>
            public static readonly Field Sex = FindByName(__.Sex);

            /// <summary>邮件</summary>
            public static readonly Field Mail = FindByName(__.Mail);

            /// <summary>手机</summary>
            public static readonly Field Mobile = FindByName(__.Mobile);

            /// <summary>代码。身份证、员工编号等</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>头像</summary>
            public static readonly Field Avatar = FindByName(__.Avatar);

            /// <summary>角色。主要角色</summary>
            public static readonly Field RoleID = FindByName(__.RoleID);

            /// <summary>角色组。次要角色集合</summary>
            public static readonly Field RoleIDs = FindByName(__.RoleIDs);

            /// <summary>在线</summary>
            public static readonly Field Online = FindByName(__.Online);

            /// <summary>启用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            /// <summary>登录次数</summary>
            public static readonly Field Logins = FindByName(__.Logins);

            /// <summary>最后登录</summary>
            public static readonly Field LastLogin = FindByName(__.LastLogin);

            /// <summary>最后登录IP</summary>
            public static readonly Field LastLoginIP = FindByName(__.LastLoginIP);

            /// <summary>注册时间</summary>
            public static readonly Field RegisterTime = FindByName(__.RegisterTime);

            /// <summary>注册IP</summary>
            public static readonly Field RegisterIP = FindByName(__.RegisterIP);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得用户字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>名称。登录用户名</summary>
            public const String Name = "Name";

            /// <summary>密码</summary>
            public const String Password = "Password";

            /// <summary>昵称</summary>
            public const String DisplayName = "DisplayName";

            /// <summary>性别。未知、男、女</summary>
            public const String Sex = "Sex";

            /// <summary>邮件</summary>
            public const String Mail = "Mail";

            /// <summary>手机</summary>
            public const String Mobile = "Mobile";

            /// <summary>代码。身份证、员工编号等</summary>
            public const String Code = "Code";

            /// <summary>头像</summary>
            public const String Avatar = "Avatar";

            /// <summary>角色。主要角色</summary>
            public const String RoleID = "RoleID";

            /// <summary>角色组。次要角色集合</summary>
            public const String RoleIDs = "RoleIDs";

            /// <summary>在线</summary>
            public const String Online = "Online";

            /// <summary>启用</summary>
            public const String Enable = "Enable";

            /// <summary>登录次数</summary>
            public const String Logins = "Logins";

            /// <summary>最后登录</summary>
            public const String LastLogin = "LastLogin";

            /// <summary>最后登录IP</summary>
            public const String LastLoginIP = "LastLoginIP";

            /// <summary>注册时间</summary>
            public const String RegisterTime = "RegisterTime";

            /// <summary>注册IP</summary>
            public const String RegisterIP = "RegisterIP";
        }
        #endregion
    }

    /// <summary>用户接口</summary>
    public partial interface IUser
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称。登录用户名</summary>
        String Name { get; set; }

        /// <summary>密码</summary>
        String Password { get; set; }

        /// <summary>昵称</summary>
        String DisplayName { get; set; }

        /// <summary>性别。未知、男、女</summary>
        SexKinds Sex { get; set; }

        /// <summary>邮件</summary>
        String Mail { get; set; }

        /// <summary>手机</summary>
        String Mobile { get; set; }

        /// <summary>代码。身份证、员工编号等</summary>
        String Code { get; set; }

        /// <summary>头像</summary>
        String Avatar { get; set; }

        /// <summary>角色。主要角色</summary>
        Int32 RoleID { get; set; }

        /// <summary>角色组。次要角色集合</summary>
        String RoleIDs { get; set; }

        /// <summary>在线</summary>
        Boolean Online { get; set; }

        /// <summary>启用</summary>
        Boolean Enable { get; set; }

        /// <summary>登录次数</summary>
        Int32 Logins { get; set; }

        /// <summary>最后登录</summary>
        DateTime LastLogin { get; set; }

        /// <summary>最后登录IP</summary>
        String LastLoginIP { get; set; }

        /// <summary>注册时间</summary>
        DateTime RegisterTime { get; set; }

        /// <summary>注册IP</summary>
        String RegisterIP { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}