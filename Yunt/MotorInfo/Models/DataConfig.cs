﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorInfo.Models
{
    /// <summary>Dataconfig</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("IX_DataConfig_DataTypeId", false, "datatype_id")]
    [BindTable("dataconfig", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class Dataconfig : IDataconfig
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

        private Int32 _DatatypeID;
        /// <summary></summary>
        [DisplayName("DatatypeID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("datatype_id", "", "int(11)")]
        public virtual Int32 DatatypeID
        {
            get { return _DatatypeID; }
            set { if (OnPropertyChanging(__.DatatypeID, value)) { _DatatypeID = value; OnPropertyChanged(__.DatatypeID); } }
        }

        private Int32 _Count;
        /// <summary></summary>
        [DisplayName("Count")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("count", "", "int(11)")]
        public virtual Int32 Count
        {
            get { return _Count; }
            set { if (OnPropertyChanging(__.Count, value)) { _Count = value; OnPropertyChanged(__.Count); } }
        }

        private String _CollectdeviceIndex;
        /// <summary>数据表单的Index</summary>
        [DisplayName("数据表单的Index")]
        [Description("数据表单的Index")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("collectdevice_index", "数据表单的Index", "VARCHAR(50)")]
        public virtual String CollectdeviceIndex
        {
            get { return _CollectdeviceIndex; }
            set { if (OnPropertyChanging(__.CollectdeviceIndex, value)) { _CollectdeviceIndex = value; OnPropertyChanged(__.CollectdeviceIndex); } }
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
                    case __.DatatypeID : return _DatatypeID;
                    case __.Count : return _Count;
                    case __.CollectdeviceIndex : return _CollectdeviceIndex;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.DatatypeID : _DatatypeID = Convert.ToInt32(value); break;
                    case __.Count : _Count = Convert.ToInt32(value); break;
                    case __.CollectdeviceIndex : _CollectdeviceIndex = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Dataconfig字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field DatatypeID = FindByName(__.DatatypeID);

            ///<summary></summary>
            public static readonly Field Count = FindByName(__.Count);

            ///<summary>数据表单的Index</summary>
            public static readonly Field CollectdeviceIndex = FindByName(__.CollectdeviceIndex);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Dataconfig字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String DatatypeID = "DatatypeID";

            ///<summary></summary>
            public const String Count = "Count";

            ///<summary>数据表单的Index</summary>
            public const String CollectdeviceIndex = "CollectdeviceIndex";

        }
        #endregion
    }

    /// <summary>Dataconfig接口</summary>
    /// <remarks></remarks>
    public partial interface IDataconfig
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 DatatypeID { get; set; }

        /// <summary></summary>
        Int32 Count { get; set; }

        /// <summary>数据表单的Index</summary>
        String CollectdeviceIndex { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}