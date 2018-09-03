﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ExcelInfoImport.UnitoonModels
{
    /// <summary>CabinetParamter</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("CabinetParamter", Description = "", ConnName = "unitoon_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class CabinetParamter : ICabinetParamter
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

        private String _Param;
        /// <summary></summary>
        [DisplayName("Param")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Param", "", "nvarchar(-1)")]
        public virtual String Param
        {
            get { return _Param; }
            set { if (OnPropertyChanging(__.Param, value)) { _Param = value; OnPropertyChanged(__.Param); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Description", "", "nvarchar(-1)")]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private Int32 _CabinetParamterType;
        /// <summary></summary>
        [DisplayName("CabinetParamterType")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("CabinetParamterType", "", "int")]
        public virtual Int32 CabinetParamterType
        {
            get { return _CabinetParamterType; }
            set { if (OnPropertyChanging(__.CabinetParamterType, value)) { _CabinetParamterType = value; OnPropertyChanged(__.CabinetParamterType); } }
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
                    case __.Param : return _Param;
                    case __.Description : return _Description;
                    case __.CabinetParamterType : return _CabinetParamterType;
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
                    case __.Param : _Param = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.CabinetParamterType : _CabinetParamterType = Convert.ToInt32(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得CabinetParamter字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field Param = FindByName(__.Param);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field CabinetParamterType = FindByName(__.CabinetParamterType);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得CabinetParamter字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Param = "Param";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String CabinetParamterType = "CabinetParamterType";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>CabinetParamter接口</summary>
    /// <remarks></remarks>
    public partial interface ICabinetParamter
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        String Param { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        Int32 CabinetParamterType { get; set; }

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