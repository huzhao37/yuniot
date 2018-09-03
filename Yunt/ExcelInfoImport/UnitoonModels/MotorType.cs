﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ExcelInfoImport.UnitoonModels
{
    /// <summary>MotorType</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("MotorType", Description = "", ConnName = "unitoon_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class MotorType : IMotorType
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

        private String _MachineType;
        /// <summary></summary>
        [DisplayName("MachineType")]
        [Description("")]
        [DataObjectField(false, false, false, 120)]
        [BindColumn("MachineType", "", "nvarchar(120)")]
        public virtual String MachineType
        {
            get { return _MachineType; }
            set { if (OnPropertyChanging(__.MachineType, value)) { _MachineType = value; OnPropertyChanged(__.MachineType); } }
        }

        private String _Code;
        /// <summary></summary>
        [DisplayName("Code")]
        [Description("")]
        [DataObjectField(false, false, false, 120)]
        [BindColumn("Code", "", "nvarchar(120)")]
        public virtual String Code
        {
            get { return _Code; }
            set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } }
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

        private String _MaintenancePeriod;
        /// <summary></summary>
        [DisplayName("MaintenancePeriod")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("MaintenancePeriod", "", "nvarchar(-1)")]
        public virtual String MaintenancePeriod
        {
            get { return _MaintenancePeriod; }
            set { if (OnPropertyChanging(__.MaintenancePeriod, value)) { _MaintenancePeriod = value; OnPropertyChanged(__.MaintenancePeriod); } }
        }

        private Double _Capacity;
        /// <summary></summary>
        [DisplayName("Capacity")]
        [Description("")]
        [DataObjectField(false, false, false, 53)]
        [BindColumn("Capacity", "", "float")]
        public virtual Double Capacity
        {
            get { return _Capacity; }
            set { if (OnPropertyChanging(__.Capacity, value)) { _Capacity = value; OnPropertyChanged(__.Capacity); } }
        }

        private Double _FeedSize;
        /// <summary></summary>
        [DisplayName("FeedSize")]
        [Description("")]
        [DataObjectField(false, false, false, 53)]
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
        [DataObjectField(false, false, false, 53)]
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
                    case __.MachineType : return _MachineType;
                    case __.Code : return _Code;
                    case __.Description : return _Description;
                    case __.MaintenancePeriod : return _MaintenancePeriod;
                    case __.Capacity : return _Capacity;
                    case __.FeedSize : return _FeedSize;
                    case __.MotorPower : return _MotorPower;
                    case __.Image : return _Image;
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
                    case __.MachineType : _MachineType = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.MaintenancePeriod : _MaintenancePeriod = Convert.ToString(value); break;
                    case __.Capacity : _Capacity = Convert.ToDouble(value); break;
                    case __.FeedSize : _FeedSize = Convert.ToDouble(value); break;
                    case __.MotorPower : _MotorPower = Convert.ToDouble(value); break;
                    case __.Image : _Image = Convert.ToString(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得MotorType字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field MachineType = FindByName(__.MachineType);

            ///<summary></summary>
            public static readonly Field Code = FindByName(__.Code);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field MaintenancePeriod = FindByName(__.MaintenancePeriod);

            ///<summary></summary>
            public static readonly Field Capacity = FindByName(__.Capacity);

            ///<summary></summary>
            public static readonly Field FeedSize = FindByName(__.FeedSize);

            ///<summary></summary>
            public static readonly Field MotorPower = FindByName(__.MotorPower);

            ///<summary></summary>
            public static readonly Field Image = FindByName(__.Image);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得MotorType字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String MachineType = "MachineType";

            ///<summary></summary>
            public const String Code = "Code";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String MaintenancePeriod = "MaintenancePeriod";

            ///<summary></summary>
            public const String Capacity = "Capacity";

            ///<summary></summary>
            public const String FeedSize = "FeedSize";

            ///<summary></summary>
            public const String MotorPower = "MotorPower";

            ///<summary></summary>
            public const String Image = "Image";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>MotorType接口</summary>
    /// <remarks></remarks>
    public partial interface IMotorType
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        String MachineType { get; set; }

        /// <summary></summary>
        String Code { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String MaintenancePeriod { get; set; }

        /// <summary></summary>
        Double Capacity { get; set; }

        /// <summary></summary>
        Double FeedSize { get; set; }

        /// <summary></summary>
        Double MotorPower { get; set; }

        /// <summary></summary>
        String Image { get; set; }

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