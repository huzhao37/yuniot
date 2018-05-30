﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.XmlProtocol.Domain.Models
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
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("PhysicType", "名称", "VARCHAR(255)")]
        public virtual String PhysicType
        {
            get { return _PhysicType; }
            set { if (OnPropertyChanging(__.PhysicType, value)) { _PhysicType = value; OnPropertyChanged(__.PhysicType); } }
        }

        private String _Unit;
        /// <summary>单位</summary>
        [DisplayName("单位")]
        [Description("单位")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("Unit", "单位", "VARCHAR(255)")]
        public virtual String Unit
        {
            get { return _Unit; }
            set { if (OnPropertyChanging(__.Unit, value)) { _Unit = value; OnPropertyChanged(__.Unit); } }
        }

        private Int32 _Format;
        /// <summary>格式</summary>
        [DisplayName("格式")]
        [Description("格式")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Format", "格式", "int(11)")]
        public virtual Int32 Format
        {
            get { return _Format; }
            set { if (OnPropertyChanging(__.Format, value)) { _Format = value; OnPropertyChanged(__.Format); } }
        }

        private Single _Accur;
        /// <summary>精度</summary>
        [DisplayName("精度")]
        [Description("精度")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Accur", "精度", "float(8,0)")]
        public virtual Single Accur
        {
            get { return _Accur; }
            set { if (OnPropertyChanging(__.Accur, value)) { _Accur = value; OnPropertyChanged(__.Accur); } }
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
                    case __.Unit : return _Unit;
                    case __.Format : return _Format;
                    case __.Accur : return _Accur;
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
                    case __.Unit : _Unit = Convert.ToString(value); break;
                    case __.Format : _Format = Convert.ToInt32(value); break;
                    case __.Accur : _Accur = Convert.ToSingle(value); break;
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

            ///<summary>名称</summary>
            public static readonly Field PhysicType = FindByName(__.PhysicType);

            ///<summary>单位</summary>
            public static readonly Field Unit = FindByName(__.Unit);

            ///<summary>格式</summary>
            public static readonly Field Format = FindByName(__.Format);

            ///<summary>精度</summary>
            public static readonly Field Accur = FindByName(__.Accur);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Physicfeature字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary>名称</summary>
            public const String PhysicType = "PhysicType";

            ///<summary>单位</summary>
            public const String Unit = "Unit";

            ///<summary>格式</summary>
            public const String Format = "Format";

            ///<summary>精度</summary>
            public const String Accur = "Accur";

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

        /// <summary>名称</summary>
        String PhysicType { get; set; }

        /// <summary>单位</summary>
        String Unit { get; set; }

        /// <summary>格式</summary>
        Int32 Format { get; set; }

        /// <summary>精度</summary>
        Single Accur { get; set; }

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