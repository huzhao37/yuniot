﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorInfo.Models
{
    /// <summary>Motor</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("IX_Motor_MotorId_MotorTypeId_EmbeddedDeviceId", false, "MotorId,MotorTypeId,EmbeddedDeviceId")]
    [BindTable("motor", Description = "", ConnName = "yunt_device", DbType = DatabaseType.MySql)]
    public partial class Motor : IMotor
    {
        #region 属性
        private Int64 _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "", "bigint(20)")]
        public virtual Int64 Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private Int32 _EmbeddedDeviceId;
        /// <summary></summary>
        [DisplayName("EmbeddedDeviceId")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EmbeddedDeviceId", "", "int(11)")]
        public virtual Int32 EmbeddedDeviceId
        {
            get { return _EmbeddedDeviceId; }
            set { if (OnPropertyChanging(__.EmbeddedDeviceId, value)) { _EmbeddedDeviceId = value; OnPropertyChanged(__.EmbeddedDeviceId); } }
        }

        private Single _FeedSize;
        /// <summary></summary>
        [DisplayName("FeedSize")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("FeedSize", "", "float")]
        public virtual Single FeedSize
        {
            get { return _FeedSize; }
            set { if (OnPropertyChanging(__.FeedSize, value)) { _FeedSize = value; OnPropertyChanged(__.FeedSize); } }
        }

        private Single _FinalSize;
        /// <summary></summary>
        [DisplayName("FinalSize")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("FinalSize", "", "float")]
        public virtual Single FinalSize
        {
            get { return _FinalSize; }
            set { if (OnPropertyChanging(__.FinalSize, value)) { _FinalSize = value; OnPropertyChanged(__.FinalSize); } }
        }

        private String _MotorId;
        /// <summary></summary>
        [DisplayName("MotorId")]
        [Description("")]
        [DataObjectField(false, false, false, 20)]
        [BindColumn("MotorId", "", "VARCHAR(20)")]
        public virtual String MotorId
        {
            get { return _MotorId; }
            set { if (OnPropertyChanging(__.MotorId, value)) { _MotorId = value; OnPropertyChanged(__.MotorId); } }
        }

        private Single _MotorPower;
        /// <summary></summary>
        [DisplayName("MotorPower")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MotorPower", "", "float")]
        public virtual Single MotorPower
        {
            get { return _MotorPower; }
            set { if (OnPropertyChanging(__.MotorPower, value)) { _MotorPower = value; OnPropertyChanged(__.MotorPower); } }
        }

        private String _MotorTypeId;
        /// <summary></summary>
        [DisplayName("MotorTypeId")]
        [Description("")]
        [DataObjectField(false, false, false, 4)]
        [BindColumn("MotorTypeId", "", "VARCHAR(4)")]
        public virtual String MotorTypeId
        {
            get { return _MotorTypeId; }
            set { if (OnPropertyChanging(__.MotorTypeId, value)) { _MotorTypeId = value; OnPropertyChanged(__.MotorTypeId); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Name", "", "longtext", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _ProductSpecification;
        /// <summary></summary>
        [DisplayName("ProductSpecification")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ProductSpecification", "", "longtext")]
        public virtual String ProductSpecification
        {
            get { return _ProductSpecification; }
            set { if (OnPropertyChanging(__.ProductSpecification, value)) { _ProductSpecification = value; OnPropertyChanged(__.ProductSpecification); } }
        }

        private String _ProductionLineId;
        /// <summary></summary>
        [DisplayName("ProductionLineId")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ProductionLineId", "", "longtext")]
        public virtual String ProductionLineId
        {
            get { return _ProductionLineId; }
            set { if (OnPropertyChanging(__.ProductionLineId, value)) { _ProductionLineId = value; OnPropertyChanged(__.ProductionLineId); } }
        }

        private String _SerialNumber;
        /// <summary></summary>
        [DisplayName("SerialNumber")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("SerialNumber", "", "longtext")]
        public virtual String SerialNumber
        {
            get { return _SerialNumber; }
            set { if (OnPropertyChanging(__.SerialNumber, value)) { _SerialNumber = value; OnPropertyChanged(__.SerialNumber); } }
        }

        private Single _StandValue;
        /// <summary></summary>
        [DisplayName("StandValue")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("StandValue", "", "float")]
        public virtual Single StandValue
        {
            get { return _StandValue; }
            set { if (OnPropertyChanging(__.StandValue, value)) { _StandValue = value; OnPropertyChanged(__.StandValue); } }
        }

        private Int64 _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Time", "", "bigint(20)")]
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
                    case __.Id : return _Id;
                    case __.EmbeddedDeviceId : return _EmbeddedDeviceId;
                    case __.FeedSize : return _FeedSize;
                    case __.FinalSize : return _FinalSize;
                    case __.MotorId : return _MotorId;
                    case __.MotorPower : return _MotorPower;
                    case __.MotorTypeId : return _MotorTypeId;
                    case __.Name : return _Name;
                    case __.ProductSpecification : return _ProductSpecification;
                    case __.ProductionLineId : return _ProductionLineId;
                    case __.SerialNumber : return _SerialNumber;
                    case __.StandValue : return _StandValue;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt64(value); break;
                    case __.EmbeddedDeviceId : _EmbeddedDeviceId = Convert.ToInt32(value); break;
                    case __.FeedSize : _FeedSize = Convert.ToSingle(value); break;
                    case __.FinalSize : _FinalSize = Convert.ToSingle(value); break;
                    case __.MotorId : _MotorId = Convert.ToString(value); break;
                    case __.MotorPower : _MotorPower = Convert.ToSingle(value); break;
                    case __.MotorTypeId : _MotorTypeId = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.ProductSpecification : _ProductSpecification = Convert.ToString(value); break;
                    case __.ProductionLineId : _ProductionLineId = Convert.ToString(value); break;
                    case __.SerialNumber : _SerialNumber = Convert.ToString(value); break;
                    case __.StandValue : _StandValue = Convert.ToSingle(value); break;
                    case __.Time : _Time = Convert.ToInt64(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Motor字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field EmbeddedDeviceId = FindByName(__.EmbeddedDeviceId);

            ///<summary></summary>
            public static readonly Field FeedSize = FindByName(__.FeedSize);

            ///<summary></summary>
            public static readonly Field FinalSize = FindByName(__.FinalSize);

            ///<summary></summary>
            public static readonly Field MotorId = FindByName(__.MotorId);

            ///<summary></summary>
            public static readonly Field MotorPower = FindByName(__.MotorPower);

            ///<summary></summary>
            public static readonly Field MotorTypeId = FindByName(__.MotorTypeId);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field ProductSpecification = FindByName(__.ProductSpecification);

            ///<summary></summary>
            public static readonly Field ProductionLineId = FindByName(__.ProductionLineId);

            ///<summary></summary>
            public static readonly Field SerialNumber = FindByName(__.SerialNumber);

            ///<summary></summary>
            public static readonly Field StandValue = FindByName(__.StandValue);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Motor字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String EmbeddedDeviceId = "EmbeddedDeviceId";

            ///<summary></summary>
            public const String FeedSize = "FeedSize";

            ///<summary></summary>
            public const String FinalSize = "FinalSize";

            ///<summary></summary>
            public const String MotorId = "MotorId";

            ///<summary></summary>
            public const String MotorPower = "MotorPower";

            ///<summary></summary>
            public const String MotorTypeId = "MotorTypeId";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String ProductSpecification = "ProductSpecification";

            ///<summary></summary>
            public const String ProductionLineId = "ProductionLineId";

            ///<summary></summary>
            public const String SerialNumber = "SerialNumber";

            ///<summary></summary>
            public const String StandValue = "StandValue";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>Motor接口</summary>
    /// <remarks></remarks>
    public partial interface IMotor
    {
        #region 属性
        /// <summary></summary>
        Int64 Id { get; set; }

        /// <summary></summary>
        Int32 EmbeddedDeviceId { get; set; }

        /// <summary></summary>
        Single FeedSize { get; set; }

        /// <summary></summary>
        Single FinalSize { get; set; }

        /// <summary></summary>
        String MotorId { get; set; }

        /// <summary></summary>
        Single MotorPower { get; set; }

        /// <summary></summary>
        String MotorTypeId { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        String ProductSpecification { get; set; }

        /// <summary></summary>
        String ProductionLineId { get; set; }

        /// <summary></summary>
        String SerialNumber { get; set; }

        /// <summary></summary>
        Single StandValue { get; set; }

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