﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.XmlCheck.Models
{
    /// <summary>DataType</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("PK_DataType_Id", false, "Id")]
    //[BindRelation("Id", false, "_DataConfig_old_20170613", "DataTypeId")]
    //[BindRelation("Id", false, "DataConfig", "DataTypeId")]
    [BindTable("DataType", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.SQLite)]
    public partial class DataType : IDataType
    {
        #region 属性
        private int _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, false, false, 8)]
        [BindColumn(1, "Id", "", null, "integer", 19, 0, false)]
        public virtual int Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(2, "Description", "", null, "text(50)", 0, 0, false)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private int _Bit;
        /// <summary></summary>
        [DisplayName("Bit")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(3, "Bit", "", null, "integer", 19, 0, false)]
        public virtual int Bit
        {
            get { return _Bit; }
            set { if (OnPropertyChanging(__.Bit, value)) { _Bit = value; OnPropertyChanged(__.Bit); } }
        }

        private int _InByte;
        /// <summary></summary>
        [DisplayName("InByte")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(4, "InByte", "", null, "integer", 19, 0, false)]
        public virtual int InByte
        {
            get { return _InByte; }
            set { if (OnPropertyChanging(__.InByte, value)) { _InByte = value; OnPropertyChanged(__.InByte); } }
        }

        private int _OutIntArray;
        /// <summary></summary>
        [DisplayName("OutIntArray")]
        [Description("")]
        [DataObjectField(false, false, false, 8)]
        [BindColumn(5, "OutIntArray", "", null, "integer", 19, 0, false)]
        public virtual int OutIntArray
        {
            get { return _OutIntArray; }
            set { if (OnPropertyChanging(__.OutIntArray, value)) { _OutIntArray = value; OnPropertyChanged(__.OutIntArray); } }
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
                    case __.Description : return _Description;
                    case __.Bit : return _Bit;
                    case __.InByte : return _InByte;
                    case __.OutIntArray : return _OutIntArray;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.Bit : _Bit = Convert.ToInt32(value); break;
                    case __.InByte : _InByte = Convert.ToInt32(value); break;
                    case __.OutIntArray : _OutIntArray = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得DataType字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field Bit = FindByName(__.Bit);

            ///<summary></summary>
            public static readonly Field InByte = FindByName(__.InByte);

            ///<summary></summary>
            public static readonly Field OutIntArray = FindByName(__.OutIntArray);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得DataType字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String Bit = "Bit";

            ///<summary></summary>
            public const String InByte = "InByte";

            ///<summary></summary>
            public const String OutIntArray = "OutIntArray";

        }
        #endregion
    }

    /// <summary>DataType接口</summary>
    /// <remarks></remarks>
    public partial interface IDataType
    {
        #region 属性
        /// <summary></summary>
        int Id { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        int Bit { get; set; }

        /// <summary></summary>
        int InByte { get; set; }

        /// <summary></summary>
        int OutIntArray { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}