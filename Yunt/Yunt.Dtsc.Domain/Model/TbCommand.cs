﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>TbCommand</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_command", Description = "", ConnName = "yunt.dtsc", DbType = DatabaseType.MySql)]
    public partial class TbCommand : ITbCommand
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

        private Int32 _Jobid;
        /// <summary></summary>
        [DisplayName("Jobid")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("jobid", "", "int(11)")]
        public virtual Int32 Jobid
        {
            get { return _Jobid; }
            set { if (OnPropertyChanging(__.Jobid, value)) { _Jobid = value; OnPropertyChanged(__.Jobid); } }
        }

        private Int32 _Commandtype;
        /// <summary></summary>
        [DisplayName("Commandtype")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("commandtype", "", "int(11)")]
        public virtual Int32 Commandtype
        {
            get { return _Commandtype; }
            set { if (OnPropertyChanging(__.Commandtype, value)) { _Commandtype = value; OnPropertyChanged(__.Commandtype); } }
        }

        private Int64 _Success;
        /// <summary></summary>
        [DisplayName("Success")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("success", "", "bit(1)")]
        public virtual Int64 Success
        {
            get { return _Success; }
            set { if (OnPropertyChanging(__.Success, value)) { _Success = value; OnPropertyChanged(__.Success); } }
        }

        private Int64 _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("time", "", "bigint(20)")]
        public virtual Int64 Time
        {
            get { return _Time; }
            set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } }
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
                    case __.Jobid : return _Jobid;
                    case __.Commandtype : return _Commandtype;
                    case __.Success : return _Success;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Jobid : _Jobid = Convert.ToInt32(value); break;
                    case __.Commandtype : _Commandtype = Convert.ToInt32(value); break;
                    case __.Success : _Success = Convert.ToInt64(value); break;
                    case __.Time : _Time = Convert.ToInt64(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbCommand字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Jobid = FindByName(__.Jobid);

            ///<summary></summary>
            public static readonly Field Commandtype = FindByName(__.Commandtype);

            ///<summary></summary>
            public static readonly Field Success = FindByName(__.Success);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbCommand字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Jobid = "Jobid";

            ///<summary></summary>
            public const String Commandtype = "Commandtype";

            ///<summary></summary>
            public const String Success = "Success";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>TbCommand接口</summary>
    /// <remarks></remarks>
    public partial interface ITbCommand
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 Jobid { get; set; }

        /// <summary></summary>
        Int32 Commandtype { get; set; }

        /// <summary></summary>
        Int64 Success { get; set; }

        /// <summary></summary>
        Int64 Time { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}