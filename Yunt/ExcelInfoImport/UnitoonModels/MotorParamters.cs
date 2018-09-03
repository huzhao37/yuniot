﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ExcelInfoImport.UnitoonModels
{
    /// <summary>MotorParamters</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("MotorParamters", Description = "", ConnName = "unitoon_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class MotorParamters : IMotorParamters
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

        private Int32 _MotorId;
        /// <summary></summary>
        [DisplayName("MotorId")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("MotorId", "", "int")]
        public virtual Int32 MotorId
        {
            get { return _MotorId; }
            set { if (OnPropertyChanging(__.MotorId, value)) { _MotorId = value; OnPropertyChanged(__.MotorId); } }
        }

        private Int32 _CabinetParamterId;
        /// <summary></summary>
        [DisplayName("CabinetParamterId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("CabinetParamterId", "", "int")]
        public virtual Int32 CabinetParamterId
        {
            get { return _CabinetParamterId; }
            set { if (OnPropertyChanging(__.CabinetParamterId, value)) { _CabinetParamterId = value; OnPropertyChanged(__.CabinetParamterId); } }
        }

        private Int32 _EmbeddedDeviceId;
        /// <summary></summary>
        [DisplayName("EmbeddedDeviceId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("EmbeddedDeviceId", "", "int")]
        public virtual Int32 EmbeddedDeviceId
        {
            get { return _EmbeddedDeviceId; }
            set { if (OnPropertyChanging(__.EmbeddedDeviceId, value)) { _EmbeddedDeviceId = value; OnPropertyChanged(__.EmbeddedDeviceId); } }
        }

        private Double _DataAccruacy;
        /// <summary></summary>
        [DisplayName("DataAccruacy")]
        [Description("")]
        [DataObjectField(false, false, false, 53)]
        [BindColumn("DataAccruacy", "", "float")]
        public virtual Double DataAccruacy
        {
            get { return _DataAccruacy; }
            set { if (OnPropertyChanging(__.DataAccruacy, value)) { _DataAccruacy = value; OnPropertyChanged(__.DataAccruacy); } }
        }

        private Int32 _CardId;
        /// <summary></summary>
        [DisplayName("CardId")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("CardId", "", "int")]
        public virtual Int32 CardId
        {
            get { return _CardId; }
            set { if (OnPropertyChanging(__.CardId, value)) { _CardId = value; OnPropertyChanged(__.CardId); } }
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

        private Int32 _AlarmTypeId;
        /// <summary></summary>
        [DisplayName("AlarmTypeId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("AlarmTypeId", "", "int")]
        public virtual Int32 AlarmTypeId
        {
            get { return _AlarmTypeId; }
            set { if (OnPropertyChanging(__.AlarmTypeId, value)) { _AlarmTypeId = value; OnPropertyChanged(__.AlarmTypeId); } }
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
                    case __.MotorId : return _MotorId;
                    case __.CabinetParamterId : return _CabinetParamterId;
                    case __.EmbeddedDeviceId : return _EmbeddedDeviceId;
                    case __.DataAccruacy : return _DataAccruacy;
                    case __.CardId : return _CardId;
                    case __.IsDeleted : return _IsDeleted;
                    case __.Time : return _Time;
                    case __.AlarmTypeId : return _AlarmTypeId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.Index : _Index = Convert.ToInt32(value); break;
                    case __.MotorId : _MotorId = Convert.ToInt32(value); break;
                    case __.CabinetParamterId : _CabinetParamterId = Convert.ToInt32(value); break;
                    case __.EmbeddedDeviceId : _EmbeddedDeviceId = Convert.ToInt32(value); break;
                    case __.DataAccruacy : _DataAccruacy = Convert.ToDouble(value); break;
                    case __.CardId : _CardId = Convert.ToInt32(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.AlarmTypeId : _AlarmTypeId = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得MotorParamters字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field Index = FindByName(__.Index);

            ///<summary></summary>
            public static readonly Field MotorId = FindByName(__.MotorId);

            ///<summary></summary>
            public static readonly Field CabinetParamterId = FindByName(__.CabinetParamterId);

            ///<summary></summary>
            public static readonly Field EmbeddedDeviceId = FindByName(__.EmbeddedDeviceId);

            ///<summary></summary>
            public static readonly Field DataAccruacy = FindByName(__.DataAccruacy);

            ///<summary></summary>
            public static readonly Field CardId = FindByName(__.CardId);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field AlarmTypeId = FindByName(__.AlarmTypeId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得MotorParamters字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Index = "Index";

            ///<summary></summary>
            public const String MotorId = "MotorId";

            ///<summary></summary>
            public const String CabinetParamterId = "CabinetParamterId";

            ///<summary></summary>
            public const String EmbeddedDeviceId = "EmbeddedDeviceId";

            ///<summary></summary>
            public const String DataAccruacy = "DataAccruacy";

            ///<summary></summary>
            public const String CardId = "CardId";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String AlarmTypeId = "AlarmTypeId";

        }
        #endregion
    }

    /// <summary>MotorParamters接口</summary>
    /// <remarks></remarks>
    public partial interface IMotorParamters
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        Int32 Index { get; set; }

        /// <summary></summary>
        Int32 MotorId { get; set; }

        /// <summary></summary>
        Int32 CabinetParamterId { get; set; }

        /// <summary></summary>
        Int32 EmbeddedDeviceId { get; set; }

        /// <summary></summary>
        Double DataAccruacy { get; set; }

        /// <summary></summary>
        Int32 CardId { get; set; }

        /// <summary></summary>
        Boolean IsDeleted { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        Int32 AlarmTypeId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}