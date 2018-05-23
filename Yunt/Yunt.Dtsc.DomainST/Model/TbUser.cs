﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>TbUser</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_user", Description = "", ConnName = "yunt.dtsc", DbType = DatabaseType.MySql)]
    public partial class TbUser : ITbUser
    {
        #region 属性
        private Int32 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("id", "", "int(11)")]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("name", "", "VARCHAR(50)", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _Pwd;
        /// <summary></summary>
        [DisplayName("Pwd")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("pwd", "", "VARCHAR(255)")]
        public virtual String Pwd
        {
            get { return _Pwd; }
            set { if (OnPropertyChanging(__.Pwd, value)) { _Pwd = value; OnPropertyChanged(__.Pwd); } }
        }

        private String _Email;
        /// <summary></summary>
        [DisplayName("Email")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("email", "", "VARCHAR(50)")]
        public virtual String Email
        {
            get { return _Email; }
            set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } }
        }

        private Int64 _Createtime;
        /// <summary></summary>
        [DisplayName("Createtime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("createtime", "", "bigint(20)")]
        public virtual Int64 Createtime
        {
            get { return _Createtime; }
            set { if (OnPropertyChanging(__.Createtime, value)) { _Createtime = value; OnPropertyChanged(__.Createtime); } }
        }

        private String _Remark;
        /// <summary></summary>
        [DisplayName("Remark")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("remark", "", "VARCHAR(255)")]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
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
                    case __.Pwd : return _Pwd;
                    case __.Email : return _Email;
                    case __.Createtime : return _Createtime;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Pwd : _Pwd = Convert.ToString(value); break;
                    case __.Email : _Email = Convert.ToString(value); break;
                    case __.Createtime : _Createtime = Convert.ToInt64(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbUser字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field Pwd = FindByName(__.Pwd);

            ///<summary></summary>
            public static readonly Field Email = FindByName(__.Email);

            ///<summary></summary>
            public static readonly Field Createtime = FindByName(__.Createtime);

            ///<summary></summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbUser字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String Pwd = "Pwd";

            ///<summary></summary>
            public const String Email = "Email";

            ///<summary></summary>
            public const String Createtime = "Createtime";

            ///<summary></summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>TbUser接口</summary>
    /// <remarks></remarks>
    public partial interface ITbUser
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        String Pwd { get; set; }

        /// <summary></summary>
        String Email { get; set; }

        /// <summary></summary>
        Int64 Createtime { get; set; }

        /// <summary></summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}