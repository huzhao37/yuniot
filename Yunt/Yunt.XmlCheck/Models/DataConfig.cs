﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.XmlCheck.Models
{
    /// <summary>DataConfig</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("sqlite_master_PK_DataConfig", true, "Id")]
    [BindIndex("IX_DataConfig_DataTypeId", false, "DataTypeId")]
    //[BindRelation("DataTypeId", false, "DataType", "Id")]
    [BindTable("DataConfig", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.SQLite)]
    public partial class DataConfig : IDataConfig
    {
        #region 属性
        private Int32 _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, true, false, 8)]
        [BindColumn(1, "Id", "", null, "integer", 19, 0, false)]
        public virtual Int32 Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private int _DataTypeId;
        /// <summary></summary>
        [DisplayName("DataTypeId")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(2, "DataTypeId", "", null, "integer", 19, 0, false)]
        public virtual int DataTypeId
        {
            get { return _DataTypeId; }
            set { if (OnPropertyChanging(__.DataTypeId, value)) { _DataTypeId = value; OnPropertyChanged(__.DataTypeId); } }
        }

        private int _Count;
        /// <summary></summary>
        [DisplayName("Count")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(3, "Count", "", null, "integer", 19, 0, false)]
        public virtual int Count
        {
            get { return _Count; }
            set { if (OnPropertyChanging(__.Count, value)) { _Count = value; OnPropertyChanged(__.Count); } }
        }

        private string _DataConfigId;
        /// <summary></summary>
        [DisplayName("DataConfigId")]
        [Description("数据表单的Index")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(4, "DataConfigId", "", null, "varchar(50)", 10, 0, false)]
        public virtual string DataConfigId
        {
            get { return _DataConfigId; }
            set { if (OnPropertyChanging(__.DataConfigId, value)) { _DataConfigId = value; OnPropertyChanged(__.DataConfigId); } }
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
                    case __.DataTypeId : return _DataTypeId;
                    case __.Count : return _Count;
                    case __.DataConfigId : return _DataConfigId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.DataTypeId : _DataTypeId = Convert.ToInt32(value); break;
                    case __.Count : _Count = Convert.ToInt32(value); break;
                    case __.DataConfigId : _DataConfigId = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得DataConfig字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field DataTypeId = FindByName(__.DataTypeId);

            ///<summary></summary>
            public static readonly Field Count = FindByName(__.Count);

            ///<summary></summary>
            public static readonly Field DataConfigId = FindByName(__.DataConfigId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得DataConfig字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String DataTypeId = "DataTypeId";

            ///<summary></summary>
            public const String Count = "Count";

            ///<summary></summary>
            public const String DataConfigId = "DataConfigId";

        }
        #endregion
    }

    /// <summary>DataConfig接口</summary>
    /// <remarks></remarks>
    public partial interface IDataConfig
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        int DataTypeId { get; set; }

        /// <summary></summary>
        int Count { get; set; }

        /// <summary></summary>
        string DataConfigId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}