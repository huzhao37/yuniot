﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DeviceManager.Model
{
    /// <summary>Motorparams</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("motorparams", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class Motorparams : IMotorparams
    {
        #region 属性
        private Int32 _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "", "int(11)")]
        public virtual Int32 Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private String _Param;
        /// <summary></summary>
        [DisplayName("Param")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("Param", "", "VARCHAR(255)")]
        public virtual String Param
        {
            get { return _Param; }
            set { if (OnPropertyChanging(__.Param, value)) { _Param = value; OnPropertyChanged(__.Param); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("Description", "", "VARCHAR(255)")]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _MotorTypeId;
        /// <summary></summary>
        [DisplayName("MotorTypeId")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("MotorTypeId", "", "VARCHAR(255)")]
        public virtual String MotorTypeId
        {
            get { return _MotorTypeId; }
            set { if (OnPropertyChanging(__.MotorTypeId, value)) { _MotorTypeId = value; OnPropertyChanged(__.MotorTypeId); } }
        }

        private Int32 _PhysicId;
        /// <summary></summary>
        [DisplayName("PhysicId")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("PhysicId", "", "int(11)")]
        public virtual Int32 PhysicId
        {
            get { return _PhysicId; }
            set { if (OnPropertyChanging(__.PhysicId, value)) { _PhysicId = value; OnPropertyChanged(__.PhysicId); } }
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
                    case __.MotorTypeId : return _MotorTypeId;
                    case __.PhysicId : return _PhysicId;
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
                    case __.MotorTypeId : _MotorTypeId = Convert.ToString(value); break;
                    case __.PhysicId : _PhysicId = Convert.ToInt32(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Motorparams字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field Param = FindByName(__.Param);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field MotorTypeId = FindByName(__.MotorTypeId);

            ///<summary></summary>
            public static readonly Field PhysicId = FindByName(__.PhysicId);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Motorparams字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String Param = "Param";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String MotorTypeId = "MotorTypeId";

            ///<summary></summary>
            public const String PhysicId = "PhysicId";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>Motorparams接口</summary>
    /// <remarks></remarks>
    public partial interface IMotorparams
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        String Param { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String MotorTypeId { get; set; }

        /// <summary></summary>
        Int32 PhysicId { get; set; }

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