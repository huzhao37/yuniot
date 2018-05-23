﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorInfo.Models
{
    /// <summary>Motoridfactories</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("IX_MotorIdFactories_MotorIndex_MotorTypeId_ProductionLineId", false, "MotorIndex,MotorTypeId,ProductionLineId")]
    [BindTable("motoridfactories", Description = "", ConnName = "yunt_device", DbType = DatabaseType.MySql)]
    public partial class Motoridfactories : IMotoridfactories
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

        private Int32 _MotorIndex;
        /// <summary></summary>
        [DisplayName("MotorIndex")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MotorIndex", "", "int(11)")]
        public virtual Int32 MotorIndex
        {
            get { return _MotorIndex; }
            set { if (OnPropertyChanging(__.MotorIndex, value)) { _MotorIndex = value; OnPropertyChanged(__.MotorIndex); } }
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

        private String _ProductionLineId;
        /// <summary></summary>
        [DisplayName("ProductionLineId")]
        [Description("")]
        [DataObjectField(false, false, false, 15)]
        [BindColumn("ProductionLineId", "", "VARCHAR(15)")]
        public virtual String ProductionLineId
        {
            get { return _ProductionLineId; }
            set { if (OnPropertyChanging(__.ProductionLineId, value)) { _ProductionLineId = value; OnPropertyChanged(__.ProductionLineId); } }
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
                    case __.MotorIndex : return _MotorIndex;
                    case __.MotorTypeId : return _MotorTypeId;
                    case __.ProductionLineId : return _ProductionLineId;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt64(value); break;
                    case __.MotorIndex : _MotorIndex = Convert.ToInt32(value); break;
                    case __.MotorTypeId : _MotorTypeId = Convert.ToString(value); break;
                    case __.ProductionLineId : _ProductionLineId = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToInt64(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Motoridfactories字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field MotorIndex = FindByName(__.MotorIndex);

            ///<summary></summary>
            public static readonly Field MotorTypeId = FindByName(__.MotorTypeId);

            ///<summary></summary>
            public static readonly Field ProductionLineId = FindByName(__.ProductionLineId);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Motoridfactories字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String MotorIndex = "MotorIndex";

            ///<summary></summary>
            public const String MotorTypeId = "MotorTypeId";

            ///<summary></summary>
            public const String ProductionLineId = "ProductionLineId";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>Motoridfactories接口</summary>
    /// <remarks></remarks>
    public partial interface IMotoridfactories
    {
        #region 属性
        /// <summary></summary>
        Int64 Id { get; set; }

        /// <summary></summary>
        Int32 MotorIndex { get; set; }

        /// <summary></summary>
        String MotorTypeId { get; set; }

        /// <summary></summary>
        String ProductionLineId { get; set; }

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