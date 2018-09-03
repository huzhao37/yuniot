﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ExcelInfoImport.UnitoonModels
{
    /// <summary>EmbeddedDataForm</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("EmbeddedDataForm", Description = "", ConnName = "unitoon_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class EmbeddedDataForm : IEmbeddedDataForm
    {
        #region 属性
        private Int32 _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn("Id", "", "int")]
        public virtual Int32 Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private Int32 _Index;
        /// <summary></summary>
        [DisplayName("Index")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("Index", "", "int")]
        public virtual Int32 Index
        {
            get { return _Index; }
            set { if (OnPropertyChanging(__.Index, value)) { _Index = value; OnPropertyChanged(__.Index); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Name", "", "nvarchar(-1)", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private Boolean _IsDeleted;
        /// <summary></summary>
        [DisplayName("IsDeleted")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDeleted", "", "bit")]
        public virtual Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { if (OnPropertyChanging(__.IsDeleted, value)) { _IsDeleted = value; OnPropertyChanged(__.IsDeleted); } }
        }

        private DateTime _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 7)]
        [BindColumn("Time", "", "datetime2(7)")]
        public virtual DateTime Time
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
                    case __.Id : return _Id;
                    case __.Index : return _Index;
                    case __.Name : return _Name;
                    case __.IsDeleted : return _IsDeleted;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.Index : _Index = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得EmbeddedDataForm字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field Index = FindByName(__.Index);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得EmbeddedDataForm字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Index = "Index";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>EmbeddedDataForm接口</summary>
    /// <remarks></remarks>
    public partial interface IEmbeddedDataForm
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        Int32 Index { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        Boolean IsDeleted { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}