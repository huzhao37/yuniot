﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.XmlProtocol.Domain.Models
{
    /// <summary>本地数据库</summary>
    [Serializable]
    [DataObject]
    [Description("本地数据库")]
    [BindTable("dataformmodel", Description = "本地数据库", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class Dataformmodel : IDataformmodel
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "int(11)")]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _MachineName;
        /// <summary></summary>
        [DisplayName("MachineName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MachineName", "", "VARCHAR(50)")]
        public virtual String MachineName
        {
            get { return _MachineName; }
            set { if (OnPropertyChanging(__.MachineName, value)) { _MachineName = value; OnPropertyChanged(__.MachineName); } }
        }

        private Int32 _Index;
        /// <summary></summary>
        [DisplayName("Index")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Index", "", "int(11)")]
        public virtual Int32 Index
        {
            get { return _Index; }
            set { if (OnPropertyChanging(__.Index, value)) { _Index = value; OnPropertyChanged(__.Index); } }
        }

        private String _FieldParam;
        /// <summary></summary>
        [DisplayName("FieldParam")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FieldParam", "", "VARCHAR(50)")]
        public virtual String FieldParam
        {
            get { return _FieldParam; }
            set { if (OnPropertyChanging(__.FieldParam, value)) { _FieldParam = value; OnPropertyChanged(__.FieldParam); } }
        }

        private String _FieldParamEn;
        /// <summary></summary>
        [DisplayName("FieldParamEn")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FieldParamEn", "", "VARCHAR(50)")]
        public virtual String FieldParamEn
        {
            get { return _FieldParamEn; }
            set { if (OnPropertyChanging(__.FieldParamEn, value)) { _FieldParamEn = value; OnPropertyChanged(__.FieldParamEn); } }
        }

        private String _MotorTypeName;
        /// <summary></summary>
        [DisplayName("MotorTypeName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MotorTypeName", "", "VARCHAR(50)")]
        public virtual String MotorTypeName
        {
            get { return _MotorTypeName; }
            set { if (OnPropertyChanging(__.MotorTypeName, value)) { _MotorTypeName = value; OnPropertyChanged(__.MotorTypeName); } }
        }

        private String _Unit;
        /// <summary></summary>
        [DisplayName("Unit")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Unit", "", "VARCHAR(50)")]
        public virtual String Unit
        {
            get { return _Unit; }
            set { if (OnPropertyChanging(__.Unit, value)) { _Unit = value; OnPropertyChanged(__.Unit); } }
        }

        private String _DataType;
        /// <summary></summary>
        [DisplayName("DataType")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DataType", "", "VARCHAR(50)")]
        public virtual String DataType
        {
            get { return _DataType; }
            set { if (OnPropertyChanging(__.DataType, value)) { _DataType = value; OnPropertyChanged(__.DataType); } }
        }

        private String _DataPhysicalFeature;
        /// <summary></summary>
        [DisplayName("DataPhysicalFeature")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DataPhysicalFeature", "", "VARCHAR(50)")]
        public virtual String DataPhysicalFeature
        {
            get { return _DataPhysicalFeature; }
            set { if (OnPropertyChanging(__.DataPhysicalFeature, value)) { _DataPhysicalFeature = value; OnPropertyChanged(__.DataPhysicalFeature); } }
        }

        private String _DataPhysicalAccuracy;
        /// <summary></summary>
        [DisplayName("DataPhysicalAccuracy")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DataPhysicalAccuracy", "", "VARCHAR(50)")]
        public virtual String DataPhysicalAccuracy
        {
            get { return _DataPhysicalAccuracy; }
            set { if (OnPropertyChanging(__.DataPhysicalAccuracy, value)) { _DataPhysicalAccuracy = value; OnPropertyChanged(__.DataPhysicalAccuracy); } }
        }

        private String _MachineId;
        /// <summary></summary>
        [DisplayName("MachineId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MachineId", "", "VARCHAR(50)")]
        public virtual String MachineId
        {
            get { return _MachineId; }
            set { if (OnPropertyChanging(__.MachineId, value)) { _MachineId = value; OnPropertyChanged(__.MachineId); } }
        }

        private String _DeviceId;
        /// <summary></summary>
        [DisplayName("DeviceId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeviceId", "", "VARCHAR(50)")]
        public virtual String DeviceId
        {
            get { return _DeviceId; }
            set { if (OnPropertyChanging(__.DeviceId, value)) { _DeviceId = value; OnPropertyChanged(__.DeviceId); } }
        }

        private DateTime _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Time", "", "datetime")]
        public virtual DateTime Time
        {
            get { return _Time; }
            set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } }
        }

        private Double _Value;
        /// <summary></summary>
        [DisplayName("Value")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Value", "", "double")]
        public virtual Double Value
        {
            get { return _Value; }
            set { if (OnPropertyChanging(__.Value, value)) { _Value = value; OnPropertyChanged(__.Value); } }
        }

        private Int32 _DIValue;
        /// <summary></summary>
        [DisplayName("DIValue")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("DIValue", "", "int(11)")]
        public virtual Int32 DIValue
        {
            get { return _DIValue; }
            set { if (OnPropertyChanging(__.DIValue, value)) { _DIValue = value; OnPropertyChanged(__.DIValue); } }
        }

        private Int32 _DOValue;
        /// <summary></summary>
        [DisplayName("DOValue")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("DOValue", "", "int(11)")]
        public virtual Int32 DOValue
        {
            get { return _DOValue; }
            set { if (OnPropertyChanging(__.DOValue, value)) { _DOValue = value; OnPropertyChanged(__.DOValue); } }
        }

        private Int32 _DebugValue;
        /// <summary></summary>
        [DisplayName("DebugValue")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("DebugValue", "", "int(11)")]
        public virtual Int32 DebugValue
        {
            get { return _DebugValue; }
            set { if (OnPropertyChanging(__.DebugValue, value)) { _DebugValue = value; OnPropertyChanged(__.DebugValue); } }
        }

        private Int32 _WarnValue;
        /// <summary></summary>
        [DisplayName("WarnValue")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("WarnValue", "", "int(11)")]
        public virtual Int32 WarnValue
        {
            get { return _WarnValue; }
            set { if (OnPropertyChanging(__.WarnValue, value)) { _WarnValue = value; OnPropertyChanged(__.WarnValue); } }
        }

        private Int32 _Bit;
        /// <summary></summary>
        [DisplayName("Bit")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Bit", "", "int(11)")]
        public virtual Int32 Bit
        {
            get { return _Bit; }
            set { if (OnPropertyChanging(__.Bit, value)) { _Bit = value; OnPropertyChanged(__.Bit); } }
        }

        private String _BitDesc;
        /// <summary></summary>
        [DisplayName("BitDesc")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BitDesc", "", "VARCHAR(50)")]
        public virtual String BitDesc
        {
            get { return _BitDesc; }
            set { if (OnPropertyChanging(__.BitDesc, value)) { _BitDesc = value; OnPropertyChanged(__.BitDesc); } }
        }

        private string _LineId;
        /// <summary></summary>
        [DisplayName("LineId")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LineId", "", "VARCHAR(50)")]
        public virtual string LineId
        {
            get { return _LineId; }
            set { if (OnPropertyChanging(__.LineId, value)) { _LineId = value; OnPropertyChanged(__.LineId); } }
        }

        private String _CollectdeviceIndex;
        /// <summary></summary>
        [DisplayName("CollectdeviceIndex")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("collectdevice_index", "", "VARCHAR(50)")]
        public virtual String CollectdeviceIndex
        {
            get { return _CollectdeviceIndex; }
            set { if (OnPropertyChanging(__.CollectdeviceIndex, value)) { _CollectdeviceIndex = value; OnPropertyChanged(__.CollectdeviceIndex); } }
        }

        private string _MotorId;
        /// <summary></summary>
        [DisplayName("MotorId")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("MotorId", "", "vachar(15)")]
        public virtual string MotorId
        {
            get { return _MotorId; }
            set { if (OnPropertyChanging(__.MotorId, value)) { _MotorId = value; OnPropertyChanged(__.MotorId); } }
        }

        private Int32 _DataPhysicalId;
        /// <summary></summary>
        [DisplayName("DataPhysicalId")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("DataPhysicalId", "", "int(11)")]
        public virtual Int32 DataPhysicalId
        {
            get { return _DataPhysicalId; }
            set { if (OnPropertyChanging(__.DataPhysicalId, value)) { _DataPhysicalId = value; OnPropertyChanged(__.DataPhysicalId); } }
        }

        private Int32 _FormatId;
        /// <summary></summary>
        [DisplayName("FormatId")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("FormatId", "", "int(11)")]
        public virtual Int32 FormatId
        {
            get { return _FormatId; }
            set { if (OnPropertyChanging(__.FormatId, value)) { _FormatId = value; OnPropertyChanged(__.FormatId); } }
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
                    case __.MachineName : return _MachineName;
                    case __.Index : return _Index;
                    case __.FieldParam : return _FieldParam;
                    case __.FieldParamEn : return _FieldParamEn;
                    case __.MotorTypeName : return _MotorTypeName;
                    case __.Unit : return _Unit;
                    case __.DataType : return _DataType;
                    case __.DataPhysicalFeature : return _DataPhysicalFeature;
                    case __.DataPhysicalAccuracy : return _DataPhysicalAccuracy;
                    case __.MachineId : return _MachineId;
                    case __.DeviceId : return _DeviceId;
                    case __.Time : return _Time;
                    case __.Value : return _Value;
                    case __.DIValue : return _DIValue;
                    case __.DOValue : return _DOValue;
                    case __.DebugValue : return _DebugValue;
                    case __.WarnValue : return _WarnValue;
                    case __.Bit : return _Bit;
                    case __.BitDesc : return _BitDesc;
                    case __.LineId : return _LineId;
                    case __.CollectdeviceIndex : return _CollectdeviceIndex;
                    case __.MotorId : return _MotorId;
                    case __.DataPhysicalId : return _DataPhysicalId;
                    case __.FormatId : return _FormatId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.MachineName : _MachineName = Convert.ToString(value); break;
                    case __.Index : _Index = Convert.ToInt32(value); break;
                    case __.FieldParam : _FieldParam = Convert.ToString(value); break;
                    case __.FieldParamEn : _FieldParamEn = Convert.ToString(value); break;
                    case __.MotorTypeName : _MotorTypeName = Convert.ToString(value); break;
                    case __.Unit : _Unit = Convert.ToString(value); break;
                    case __.DataType : _DataType = Convert.ToString(value); break;
                    case __.DataPhysicalFeature : _DataPhysicalFeature = Convert.ToString(value); break;
                    case __.DataPhysicalAccuracy : _DataPhysicalAccuracy = Convert.ToString(value); break;
                    case __.MachineId : _MachineId = Convert.ToString(value); break;
                    case __.DeviceId : _DeviceId = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.Value : _Value = Convert.ToDouble(value); break;
                    case __.DIValue : _DIValue = Convert.ToInt32(value); break;
                    case __.DOValue : _DOValue = Convert.ToInt32(value); break;
                    case __.DebugValue : _DebugValue = Convert.ToInt32(value); break;
                    case __.WarnValue : _WarnValue = Convert.ToInt32(value); break;
                    case __.Bit : _Bit = Convert.ToInt32(value); break;
                    case __.BitDesc : _BitDesc = Convert.ToString(value); break;
                    case __.LineId : _LineId = Convert.ToString(value); break;
                    case __.CollectdeviceIndex : _CollectdeviceIndex = Convert.ToString(value); break;
                    case __.MotorId : _MotorId = Convert.ToString(value); break;
                    case __.DataPhysicalId : _DataPhysicalId = Convert.ToInt32(value); break;
                    case __.FormatId : _FormatId = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得本地数据库字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field MachineName = FindByName(__.MachineName);

            ///<summary></summary>
            public static readonly Field Index = FindByName(__.Index);

            ///<summary></summary>
            public static readonly Field FieldParam = FindByName(__.FieldParam);

            ///<summary></summary>
            public static readonly Field FieldParamEn = FindByName(__.FieldParamEn);

            ///<summary></summary>
            public static readonly Field MotorTypeName = FindByName(__.MotorTypeName);

            ///<summary></summary>
            public static readonly Field Unit = FindByName(__.Unit);

            ///<summary></summary>
            public static readonly Field DataType = FindByName(__.DataType);

            ///<summary></summary>
            public static readonly Field DataPhysicalFeature = FindByName(__.DataPhysicalFeature);

            ///<summary></summary>
            public static readonly Field DataPhysicalAccuracy = FindByName(__.DataPhysicalAccuracy);

            ///<summary></summary>
            public static readonly Field MachineId = FindByName(__.MachineId);

            ///<summary></summary>
            public static readonly Field DeviceId = FindByName(__.DeviceId);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field Value = FindByName(__.Value);

            ///<summary></summary>
            public static readonly Field DIValue = FindByName(__.DIValue);

            ///<summary></summary>
            public static readonly Field DOValue = FindByName(__.DOValue);

            ///<summary></summary>
            public static readonly Field DebugValue = FindByName(__.DebugValue);

            ///<summary></summary>
            public static readonly Field WarnValue = FindByName(__.WarnValue);

            ///<summary></summary>
            public static readonly Field Bit = FindByName(__.Bit);

            ///<summary></summary>
            public static readonly Field BitDesc = FindByName(__.BitDesc);

            ///<summary></summary>
            public static readonly Field LineId = FindByName(__.LineId);

            ///<summary></summary>
            public static readonly Field CollectdeviceIndex = FindByName(__.CollectdeviceIndex);

            ///<summary></summary>
            public static readonly Field MotorId = FindByName(__.MotorId);

            ///<summary></summary>
            public static readonly Field DataPhysicalId = FindByName(__.DataPhysicalId);

            ///<summary></summary>
            public static readonly Field FormatId = FindByName(__.FormatId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得本地数据库字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String MachineName = "MachineName";

            ///<summary></summary>
            public const String Index = "Index";

            ///<summary></summary>
            public const String FieldParam = "FieldParam";

            ///<summary></summary>
            public const String FieldParamEn = "FieldParamEn";

            ///<summary></summary>
            public const String MotorTypeName = "MotorTypeName";

            ///<summary></summary>
            public const String Unit = "Unit";

            ///<summary></summary>
            public const String DataType = "DataType";

            ///<summary></summary>
            public const String DataPhysicalFeature = "DataPhysicalFeature";

            ///<summary></summary>
            public const String DataPhysicalAccuracy = "DataPhysicalAccuracy";

            ///<summary></summary>
            public const String MachineId = "MachineId";

            ///<summary></summary>
            public const String DeviceId = "DeviceId";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String Value = "Value";

            ///<summary></summary>
            public const String DIValue = "DIValue";

            ///<summary></summary>
            public const String DOValue = "DOValue";

            ///<summary></summary>
            public const String DebugValue = "DebugValue";

            ///<summary></summary>
            public const String WarnValue = "WarnValue";

            ///<summary></summary>
            public const String Bit = "Bit";

            ///<summary></summary>
            public const String BitDesc = "BitDesc";

            ///<summary></summary>
            public const String LineId = "LineId";

            ///<summary></summary>
            public const String CollectdeviceIndex = "CollectdeviceIndex";

            ///<summary></summary>
            public const String MotorId = "MotorId";

            ///<summary></summary>
            public const String DataPhysicalId = "DataPhysicalId";

            ///<summary></summary>
            public const String FormatId = "FormatId";

        }
        #endregion
    }

    /// <summary>本地数据库接口</summary>
    public partial interface IDataformmodel
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String MachineName { get; set; }

        /// <summary></summary>
        Int32 Index { get; set; }

        /// <summary></summary>
        String FieldParam { get; set; }

        /// <summary></summary>
        String FieldParamEn { get; set; }

        /// <summary></summary>
        String MotorTypeName { get; set; }

        /// <summary></summary>
        String Unit { get; set; }

        /// <summary></summary>
        String DataType { get; set; }

        /// <summary></summary>
        String DataPhysicalFeature { get; set; }

        /// <summary></summary>
        String DataPhysicalAccuracy { get; set; }

        /// <summary></summary>
        String MachineId { get; set; }

        /// <summary></summary>
        String DeviceId { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        Double Value { get; set; }

        /// <summary></summary>
        Int32 DIValue { get; set; }

        /// <summary></summary>
        Int32 DOValue { get; set; }

        /// <summary></summary>
        Int32 DebugValue { get; set; }

        /// <summary></summary>
        Int32 WarnValue { get; set; }

        /// <summary></summary>
        Int32 Bit { get; set; }

        /// <summary></summary>
        String BitDesc { get; set; }

        /// <summary></summary>
        string LineId { get; set; }

        /// <summary></summary>
        String CollectdeviceIndex { get; set; }

        /// <summary></summary>
        string MotorId { get; set; }

        /// <summary></summary>
        Int32 DataPhysicalId { get; set; }

        /// <summary></summary>
        Int32 FormatId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}