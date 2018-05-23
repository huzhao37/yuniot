﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorEvent.EmailPush.Models
{
    /// <summary>Motor</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("Motor", Description = "", ConnName = "unitooniot_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class Motor : IMotor
    {
        #region 属性
        private Int32 _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "", "int")]
        public virtual Int32 Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, false, 120)]
        [BindColumn("Name", "", "nvarchar(120)", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _SerialNumber;
        /// <summary></summary>
        [DisplayName("SerialNumber")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("SerialNumber", "", "nvarchar(-1)")]
        public virtual String SerialNumber
        {
            get { return _SerialNumber; }
            set { if (OnPropertyChanging(__.SerialNumber, value)) { _SerialNumber = value; OnPropertyChanged(__.SerialNumber); } }
        }

        private DateTime _BuildTime;
        /// <summary></summary>
        [DisplayName("BuildTime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BuildTime", "", "datetime")]
        public virtual DateTime BuildTime
        {
            get { return _BuildTime; }
            set { if (OnPropertyChanging(__.BuildTime, value)) { _BuildTime = value; OnPropertyChanged(__.BuildTime); } }
        }

        private DateTime _LatestMaintainTime;
        /// <summary></summary>
        [DisplayName("LatestMaintainTime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LatestMaintainTime", "", "datetime")]
        public virtual DateTime LatestMaintainTime
        {
            get { return _LatestMaintainTime; }
            set { if (OnPropertyChanging(__.LatestMaintainTime, value)) { _LatestMaintainTime = value; OnPropertyChanged(__.LatestMaintainTime); } }
        }

        private DateTime _LatestDataTime;
        /// <summary></summary>
        [DisplayName("LatestDataTime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LatestDataTime", "", "datetime")]
        public virtual DateTime LatestDataTime
        {
            get { return _LatestDataTime; }
            set { if (OnPropertyChanging(__.LatestDataTime, value)) { _LatestDataTime = value; OnPropertyChanged(__.LatestDataTime); } }
        }

        private Int32 _Status;
        /// <summary></summary>
        [DisplayName("Status")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Status", "", "int")]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }

        private Int32 _ProductionLineId;
        /// <summary></summary>
        [DisplayName("ProductionLineId")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ProductionLineId", "", "int")]
        public virtual Int32 ProductionLineId
        {
            get { return _ProductionLineId; }
            set { if (OnPropertyChanging(__.ProductionLineId, value)) { _ProductionLineId = value; OnPropertyChanged(__.ProductionLineId); } }
        }

        private Int32 _MotorTypeId;
        /// <summary></summary>
        [DisplayName("MotorTypeId")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MotorTypeId", "", "int")]
        public virtual Int32 MotorTypeId
        {
            get { return _MotorTypeId; }
            set { if (OnPropertyChanging(__.MotorTypeId, value)) { _MotorTypeId = value; OnPropertyChanged(__.MotorTypeId); } }
        }

        private Double _Capicity;
        /// <summary></summary>
        [DisplayName("Capicity")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Capicity", "", "float")]
        public virtual Double Capicity
        {
            get { return _Capicity; }
            set { if (OnPropertyChanging(__.Capicity, value)) { _Capicity = value; OnPropertyChanged(__.Capicity); } }
        }

        private Double _FeedSize;
        /// <summary></summary>
        [DisplayName("FeedSize")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("FeedSize", "", "float")]
        public virtual Double FeedSize
        {
            get { return _FeedSize; }
            set { if (OnPropertyChanging(__.FeedSize, value)) { _FeedSize = value; OnPropertyChanged(__.FeedSize); } }
        }

        private Double _MotorPower;
        /// <summary></summary>
        [DisplayName("MotorPower")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MotorPower", "", "float")]
        public virtual Double MotorPower
        {
            get { return _MotorPower; }
            set { if (OnPropertyChanging(__.MotorPower, value)) { _MotorPower = value; OnPropertyChanged(__.MotorPower); } }
        }

        private String _Image;
        /// <summary></summary>
        [DisplayName("Image")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Image", "", "nvarchar(-1)")]
        public virtual String Image
        {
            get { return _Image; }
            set { if (OnPropertyChanging(__.Image, value)) { _Image = value; OnPropertyChanged(__.Image); } }
        }

        private String _FinalSize;
        /// <summary></summary>
        [DisplayName("FinalSize")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("FinalSize", "", "nvarchar(-1)")]
        public virtual String FinalSize
        {
            get { return _FinalSize; }
            set { if (OnPropertyChanging(__.FinalSize, value)) { _FinalSize = value; OnPropertyChanged(__.FinalSize); } }
        }

        private Boolean _IsBeltWeight;
        /// <summary></summary>
        [DisplayName("IsBeltWeight")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsBeltWeight", "", "bit")]
        public virtual Boolean IsBeltWeight
        {
            get { return _IsBeltWeight; }
            set { if (OnPropertyChanging(__.IsBeltWeight, value)) { _IsBeltWeight = value; OnPropertyChanged(__.IsBeltWeight); } }
        }

        private Boolean _IsDisplay;
        /// <summary></summary>
        [DisplayName("IsDisplay")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisplay", "", "bit")]
        public virtual Boolean IsDisplay
        {
            get { return _IsDisplay; }
            set { if (OnPropertyChanging(__.IsDisplay, value)) { _IsDisplay = value; OnPropertyChanged(__.IsDisplay); } }
        }

        private Boolean _IsMainBeltWeight;
        /// <summary></summary>
        [DisplayName("IsMainBeltWeight")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsMainBeltWeight", "", "bit")]
        public virtual Boolean IsMainBeltWeight
        {
            get { return _IsMainBeltWeight; }
            set { if (OnPropertyChanging(__.IsMainBeltWeight, value)) { _IsMainBeltWeight = value; OnPropertyChanged(__.IsMainBeltWeight); } }
        }

        private String _ControlDeviceId;
        /// <summary></summary>
        [DisplayName("ControlDeviceId")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ControlDeviceId", "", "nvarchar(-1)")]
        public virtual String ControlDeviceId
        {
            get { return _ControlDeviceId; }
            set { if (OnPropertyChanging(__.ControlDeviceId, value)) { _ControlDeviceId = value; OnPropertyChanged(__.ControlDeviceId); } }
        }

        private Boolean _IsOutConveyor;
        /// <summary></summary>
        [DisplayName("IsOutConveyor")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOutConveyor", "", "bit")]
        public virtual Boolean IsOutConveyor
        {
            get { return _IsOutConveyor; }
            set { if (OnPropertyChanging(__.IsOutConveyor, value)) { _IsOutConveyor = value; OnPropertyChanged(__.IsOutConveyor); } }
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
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Time", "", "datetime2(7)")]
        public virtual DateTime Time
        {
            get { return _Time; }
            set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } }
        }

        private Int32 _EmbeddedDeviceId;
        /// <summary></summary>
        [DisplayName("EmbeddedDeviceId")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EmbeddedDeviceId", "", "int")]
        public virtual Int32 EmbeddedDeviceId
        {
            get { return _EmbeddedDeviceId; }
            set { if (OnPropertyChanging(__.EmbeddedDeviceId, value)) { _EmbeddedDeviceId = value; OnPropertyChanged(__.EmbeddedDeviceId); } }
        }

        private Int32 _StorageSiloId;
        /// <summary></summary>
        [DisplayName("StorageSiloId")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("StorageSiloId", "", "int")]
        public virtual Int32 StorageSiloId
        {
            get { return _StorageSiloId; }
            set { if (OnPropertyChanging(__.StorageSiloId, value)) { _StorageSiloId = value; OnPropertyChanged(__.StorageSiloId); } }
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
                    case __.Name : return _Name;
                    case __.SerialNumber : return _SerialNumber;
                    case __.BuildTime : return _BuildTime;
                    case __.LatestMaintainTime : return _LatestMaintainTime;
                    case __.LatestDataTime : return _LatestDataTime;
                    case __.Status : return _Status;
                    case __.ProductionLineId : return _ProductionLineId;
                    case __.MotorTypeId : return _MotorTypeId;
                    case __.Capicity : return _Capicity;
                    case __.FeedSize : return _FeedSize;
                    case __.MotorPower : return _MotorPower;
                    case __.Image : return _Image;
                    case __.FinalSize : return _FinalSize;
                    case __.IsBeltWeight : return _IsBeltWeight;
                    case __.IsDisplay : return _IsDisplay;
                    case __.IsMainBeltWeight : return _IsMainBeltWeight;
                    case __.ControlDeviceId : return _ControlDeviceId;
                    case __.IsOutConveyor : return _IsOutConveyor;
                    case __.IsDeleted : return _IsDeleted;
                    case __.Time : return _Time;
                    case __.EmbeddedDeviceId : return _EmbeddedDeviceId;
                    case __.StorageSiloId : return _StorageSiloId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.SerialNumber : _SerialNumber = Convert.ToString(value); break;
                    case __.BuildTime : _BuildTime = Convert.ToDateTime(value); break;
                    case __.LatestMaintainTime : _LatestMaintainTime = Convert.ToDateTime(value); break;
                    case __.LatestDataTime : _LatestDataTime = Convert.ToDateTime(value); break;
                    case __.Status : _Status = Convert.ToInt32(value); break;
                    case __.ProductionLineId : _ProductionLineId = Convert.ToInt32(value); break;
                    case __.MotorTypeId : _MotorTypeId = Convert.ToInt32(value); break;
                    case __.Capicity : _Capicity = Convert.ToDouble(value); break;
                    case __.FeedSize : _FeedSize = Convert.ToDouble(value); break;
                    case __.MotorPower : _MotorPower = Convert.ToDouble(value); break;
                    case __.Image : _Image = Convert.ToString(value); break;
                    case __.FinalSize : _FinalSize = Convert.ToString(value); break;
                    case __.IsBeltWeight : _IsBeltWeight = Convert.ToBoolean(value); break;
                    case __.IsDisplay : _IsDisplay = Convert.ToBoolean(value); break;
                    case __.IsMainBeltWeight : _IsMainBeltWeight = Convert.ToBoolean(value); break;
                    case __.ControlDeviceId : _ControlDeviceId = Convert.ToString(value); break;
                    case __.IsOutConveyor : _IsOutConveyor = Convert.ToBoolean(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.EmbeddedDeviceId : _EmbeddedDeviceId = Convert.ToInt32(value); break;
                    case __.StorageSiloId : _StorageSiloId = Convert.ToInt32(value); break;
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
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field SerialNumber = FindByName(__.SerialNumber);

            ///<summary></summary>
            public static readonly Field BuildTime = FindByName(__.BuildTime);

            ///<summary></summary>
            public static readonly Field LatestMaintainTime = FindByName(__.LatestMaintainTime);

            ///<summary></summary>
            public static readonly Field LatestDataTime = FindByName(__.LatestDataTime);

            ///<summary></summary>
            public static readonly Field Status = FindByName(__.Status);

            ///<summary></summary>
            public static readonly Field ProductionLineId = FindByName(__.ProductionLineId);

            ///<summary></summary>
            public static readonly Field MotorTypeId = FindByName(__.MotorTypeId);

            ///<summary></summary>
            public static readonly Field Capicity = FindByName(__.Capicity);

            ///<summary></summary>
            public static readonly Field FeedSize = FindByName(__.FeedSize);

            ///<summary></summary>
            public static readonly Field MotorPower = FindByName(__.MotorPower);

            ///<summary></summary>
            public static readonly Field Image = FindByName(__.Image);

            ///<summary></summary>
            public static readonly Field FinalSize = FindByName(__.FinalSize);

            ///<summary></summary>
            public static readonly Field IsBeltWeight = FindByName(__.IsBeltWeight);

            ///<summary></summary>
            public static readonly Field IsDisplay = FindByName(__.IsDisplay);

            ///<summary></summary>
            public static readonly Field IsMainBeltWeight = FindByName(__.IsMainBeltWeight);

            ///<summary></summary>
            public static readonly Field ControlDeviceId = FindByName(__.ControlDeviceId);

            ///<summary></summary>
            public static readonly Field IsOutConveyor = FindByName(__.IsOutConveyor);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field EmbeddedDeviceId = FindByName(__.EmbeddedDeviceId);

            ///<summary></summary>
            public static readonly Field StorageSiloId = FindByName(__.StorageSiloId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Motor字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String SerialNumber = "SerialNumber";

            ///<summary></summary>
            public const String BuildTime = "BuildTime";

            ///<summary></summary>
            public const String LatestMaintainTime = "LatestMaintainTime";

            ///<summary></summary>
            public const String LatestDataTime = "LatestDataTime";

            ///<summary></summary>
            public const String Status = "Status";

            ///<summary></summary>
            public const String ProductionLineId = "ProductionLineId";

            ///<summary></summary>
            public const String MotorTypeId = "MotorTypeId";

            ///<summary></summary>
            public const String Capicity = "Capicity";

            ///<summary></summary>
            public const String FeedSize = "FeedSize";

            ///<summary></summary>
            public const String MotorPower = "MotorPower";

            ///<summary></summary>
            public const String Image = "Image";

            ///<summary></summary>
            public const String FinalSize = "FinalSize";

            ///<summary></summary>
            public const String IsBeltWeight = "IsBeltWeight";

            ///<summary></summary>
            public const String IsDisplay = "IsDisplay";

            ///<summary></summary>
            public const String IsMainBeltWeight = "IsMainBeltWeight";

            ///<summary></summary>
            public const String ControlDeviceId = "ControlDeviceId";

            ///<summary></summary>
            public const String IsOutConveyor = "IsOutConveyor";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String EmbeddedDeviceId = "EmbeddedDeviceId";

            ///<summary></summary>
            public const String StorageSiloId = "StorageSiloId";

        }
        #endregion
    }

    /// <summary>Motor接口</summary>
    /// <remarks></remarks>
    public partial interface IMotor
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        String SerialNumber { get; set; }

        /// <summary></summary>
        DateTime BuildTime { get; set; }

        /// <summary></summary>
        DateTime LatestMaintainTime { get; set; }

        /// <summary></summary>
        DateTime LatestDataTime { get; set; }

        /// <summary></summary>
        Int32 Status { get; set; }

        /// <summary></summary>
        Int32 ProductionLineId { get; set; }

        /// <summary></summary>
        Int32 MotorTypeId { get; set; }

        /// <summary></summary>
        Double Capicity { get; set; }

        /// <summary></summary>
        Double FeedSize { get; set; }

        /// <summary></summary>
        Double MotorPower { get; set; }

        /// <summary></summary>
        String Image { get; set; }

        /// <summary></summary>
        String FinalSize { get; set; }

        /// <summary></summary>
        Boolean IsBeltWeight { get; set; }

        /// <summary></summary>
        Boolean IsDisplay { get; set; }

        /// <summary></summary>
        Boolean IsMainBeltWeight { get; set; }

        /// <summary></summary>
        String ControlDeviceId { get; set; }

        /// <summary></summary>
        Boolean IsOutConveyor { get; set; }

        /// <summary></summary>
        Boolean IsDeleted { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        Int32 EmbeddedDeviceId { get; set; }

        /// <summary></summary>
        Int32 StorageSiloId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}