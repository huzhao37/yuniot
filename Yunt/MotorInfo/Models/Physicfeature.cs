﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DeviceManager.Model
{
    /// <summary>Physicfeature</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("physicfeature", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class Physicfeature : IPhysicfeature
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

        private String _PhysicType;
        /// <summary></summary>
        [DisplayName("PhysicType")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("PhysicType", "", "VARCHAR(255)")]
        public virtual String PhysicType
        {
            get { return _PhysicType; }
            set { if (OnPropertyChanging(__.PhysicType, value)) { _PhysicType = value; OnPropertyChanged(__.PhysicType); } }
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
                    case __.PhysicType : return _PhysicType;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.PhysicType : _PhysicType = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Physicfeature字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field PhysicType = FindByName(__.PhysicType);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Physicfeature字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String PhysicType = "PhysicType";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>Physicfeature接口</summary>
    /// <remarks></remarks>
    public partial interface IPhysicfeature
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        String PhysicType { get; set; }

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