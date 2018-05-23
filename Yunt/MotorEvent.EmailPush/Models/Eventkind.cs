﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorEvent.EmailPush.Models
{
    /// <summary>Eventkind</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("PK__eventkin__3213E83F0425A276", true, "id,code")]
    [BindTable("eventkind", Description = "", ConnName = "unitooniot_analysis", DbType = DatabaseType.SqlServer)]
    public partial class Eventkind : IEventkind
    {
        #region 属性
        private Int64 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("id", "", "bigint")]
        public virtual Int64 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("description", "", "varchar(255)")]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _Code;
        /// <summary></summary>
        [DisplayName("Code")]
        [Description("")]
        [DataObjectField(true, false, false, 255)]
        [BindColumn("code", "", "varchar(255)")]
        public virtual String Code
        {
            get { return _Code; }
            set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } }
        }

        private String _Regulation;
        /// <summary></summary>
        [DisplayName("Regulation")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("regulation", "", "varchar(255)")]
        public virtual String Regulation
        {
            get { return _Regulation; }
            set { if (OnPropertyChanging(__.Regulation, value)) { _Regulation = value; OnPropertyChanged(__.Regulation); } }
        }

        private Int64 _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("time", "", "bigint")]
        public virtual Int64 Time
        {
            get { return _Time; }
            set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } }
        }

        private String _MotortypeName;
        /// <summary></summary>
        [DisplayName("MotortypeName")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("motortype_name", "", "varchar(255)")]
        public virtual String MotortypeName
        {
            get { return _MotortypeName; }
            set { if (OnPropertyChanging(__.MotortypeName, value)) { _MotortypeName = value; OnPropertyChanged(__.MotortypeName); } }
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
                    case __.Description : return _Description;
                    case __.Code : return _Code;
                    case __.Regulation : return _Regulation;
                    case __.Time : return _Time;
                    case __.MotortypeName : return _MotortypeName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Regulation : _Regulation = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToInt64(value); break;
                    case __.MotortypeName : _MotortypeName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Eventkind字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field Code = FindByName(__.Code);

            ///<summary></summary>
            public static readonly Field Regulation = FindByName(__.Regulation);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field MotortypeName = FindByName(__.MotortypeName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Eventkind字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String Code = "Code";

            ///<summary></summary>
            public const String Regulation = "Regulation";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String MotortypeName = "MotortypeName";

        }
        #endregion
    }

    /// <summary>Eventkind接口</summary>
    /// <remarks></remarks>
    public partial interface IEventkind
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String Code { get; set; }

        /// <summary></summary>
        String Regulation { get; set; }

        /// <summary></summary>
        Int64 Time { get; set; }

        /// <summary></summary>
        String MotortypeName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}