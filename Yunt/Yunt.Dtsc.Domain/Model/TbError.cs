﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>TbError</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_error", Description = "", ConnName = "yunt.dtsc", DbType = DatabaseType.MySql)]
    public partial class TbError : ITbError
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

        private Int32 _JobID;
        /// <summary></summary>
        [DisplayName("JobID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("job_id", "", "int(11)")]
        public virtual Int32 JobID
        {
            get { return _JobID; }
            set { if (OnPropertyChanging(__.JobID, value)) { _JobID = value; OnPropertyChanged(__.JobID); } }
        }

        private String _Msg;
        /// <summary></summary>
        [DisplayName("Msg")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("msg", "", "VARCHAR(255)")]
        public virtual String Msg
        {
            get { return _Msg; }
            set { if (OnPropertyChanging(__.Msg, value)) { _Msg = value; OnPropertyChanged(__.Msg); } }
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
                    case __.JobID : return _JobID;
                    case __.Msg : return _Msg;
                    case __.Createtime : return _Createtime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.JobID : _JobID = Convert.ToInt32(value); break;
                    case __.Msg : _Msg = Convert.ToString(value); break;
                    case __.Createtime : _Createtime = Convert.ToInt64(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbError字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field JobID = FindByName(__.JobID);

            ///<summary></summary>
            public static readonly Field Msg = FindByName(__.Msg);

            ///<summary></summary>
            public static readonly Field Createtime = FindByName(__.Createtime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbError字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String JobID = "JobID";

            ///<summary></summary>
            public const String Msg = "Msg";

            ///<summary></summary>
            public const String Createtime = "Createtime";

        }
        #endregion
    }

    /// <summary>TbError接口</summary>
    /// <remarks></remarks>
    public partial interface ITbError
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 JobID { get; set; }

        /// <summary></summary>
        String Msg { get; set; }

        /// <summary></summary>
        Int64 Createtime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}