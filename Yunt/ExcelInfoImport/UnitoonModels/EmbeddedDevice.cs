﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ExcelInfoImport.UnitoonModels
{
    /// <summary>EmbeddedDevice</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("EmbeddedDevice", Description = "", ConnName = "unitoon_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class EmbeddedDevice : IEmbeddedDevice
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

        private String _Index;
        /// <summary></summary>
        [DisplayName("Index")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Index", "", "nvarchar(-1)")]
        public virtual String Index
        {
            get { return _Index; }
            set { if (OnPropertyChanging(__.Index, value)) { _Index = value; OnPropertyChanged(__.Index); } }
        }

        private String _WorkNumber;
        /// <summary></summary>
        [DisplayName("WorkNumber")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("WorkNumber", "", "nvarchar(-1)")]
        public virtual String WorkNumber
        {
            get { return _WorkNumber; }
            set { if (OnPropertyChanging(__.WorkNumber, value)) { _WorkNumber = value; OnPropertyChanged(__.WorkNumber); } }
        }

        private String _OperationNumber;
        /// <summary></summary>
        [DisplayName("OperationNumber")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("OperationNumber", "", "nvarchar(-1)")]
        public virtual String OperationNumber
        {
            get { return _OperationNumber; }
            set { if (OnPropertyChanging(__.OperationNumber, value)) { _OperationNumber = value; OnPropertyChanged(__.OperationNumber); } }
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

        private Int32 _EmbeddedDataFromId;
        /// <summary></summary>
        [DisplayName("EmbeddedDataFromId")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("EmbeddedDataFromId", "", "int")]
        public virtual Int32 EmbeddedDataFromId
        {
            get { return _EmbeddedDataFromId; }
            set { if (OnPropertyChanging(__.EmbeddedDataFromId, value)) { _EmbeddedDataFromId = value; OnPropertyChanged(__.EmbeddedDataFromId); } }
        }

        private Int32 _ProductionLineId;
        /// <summary></summary>
        [DisplayName("ProductionLineId")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("ProductionLineId", "", "int")]
        public virtual Int32 ProductionLineId
        {
            get { return _ProductionLineId; }
            set { if (OnPropertyChanging(__.ProductionLineId, value)) { _ProductionLineId = value; OnPropertyChanged(__.ProductionLineId); } }
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
                    case __.WorkNumber : return _WorkNumber;
                    case __.OperationNumber : return _OperationNumber;
                    case __.Name : return _Name;
                    case __.IsDeleted : return _IsDeleted;
                    case __.Time : return _Time;
                    case __.EmbeddedDataFromId : return _EmbeddedDataFromId;
                    case __.ProductionLineId : return _ProductionLineId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.Index : _Index = Convert.ToString(value); break;
                    case __.WorkNumber : _WorkNumber = Convert.ToString(value); break;
                    case __.OperationNumber : _OperationNumber = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.EmbeddedDataFromId : _EmbeddedDataFromId = Convert.ToInt32(value); break;
                    case __.ProductionLineId : _ProductionLineId = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得EmbeddedDevice字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field Index = FindByName(__.Index);

            ///<summary></summary>
            public static readonly Field WorkNumber = FindByName(__.WorkNumber);

            ///<summary></summary>
            public static readonly Field OperationNumber = FindByName(__.OperationNumber);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field EmbeddedDataFromId = FindByName(__.EmbeddedDataFromId);

            ///<summary></summary>
            public static readonly Field ProductionLineId = FindByName(__.ProductionLineId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得EmbeddedDevice字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Index = "Index";

            ///<summary></summary>
            public const String WorkNumber = "WorkNumber";

            ///<summary></summary>
            public const String OperationNumber = "OperationNumber";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String EmbeddedDataFromId = "EmbeddedDataFromId";

            ///<summary></summary>
            public const String ProductionLineId = "ProductionLineId";

        }
        #endregion
    }

    /// <summary>EmbeddedDevice接口</summary>
    /// <remarks></remarks>
    public partial interface IEmbeddedDevice
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        String Index { get; set; }

        /// <summary></summary>
        String WorkNumber { get; set; }

        /// <summary></summary>
        String OperationNumber { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        Boolean IsDeleted { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        Int32 EmbeddedDataFromId { get; set; }

        /// <summary></summary>
        Int32 ProductionLineId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}