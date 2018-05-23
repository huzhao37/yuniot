﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorInfo.Models
{
    /// <summary>Collectdevice</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("collectdevice", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class Collectdevice : ICollectdevice
    {
        #region 属性
        private Int32 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("id", "", "int(11)")]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Index;
        /// <summary></summary>
        [DisplayName("Index")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("index", "", "VARCHAR(50)")]
        public virtual String Index
        {
            get { return _Index; }
            set { if (OnPropertyChanging(__.Index, value)) { _Index = value; OnPropertyChanged(__.Index); } }
        }

        private String _ProductionlineID;
        /// <summary></summary>
        [DisplayName("ProductionlineID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("productionline_id", "", "VARCHAR(50)")]
        public virtual String ProductionlineID
        {
            get { return _ProductionlineID; }
            set { if (OnPropertyChanging(__.ProductionlineID, value)) { _ProductionlineID = value; OnPropertyChanged(__.ProductionlineID); } }
        }

        private DateTime _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("time", "", "datetime")]
        public virtual DateTime Time
        {
            get { return _Time; }
            set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } }
        }

        private String _Remark;
        /// <summary></summary>
        [DisplayName("Remark")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("remark", "", "VARCHAR(255)")]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
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
                    case __.Index : return _Index;
                    case __.ProductionlineID : return _ProductionlineID;
                    case __.Time : return _Time;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Index : _Index = Convert.ToString(value); break;
                    case __.ProductionlineID : _ProductionlineID = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Collectdevice字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Index = FindByName(__.Index);

            ///<summary></summary>
            public static readonly Field ProductionlineID = FindByName(__.ProductionlineID);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Collectdevice字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Index = "Index";

            ///<summary></summary>
            public const String ProductionlineID = "ProductionlineID";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>Collectdevice接口</summary>
    /// <remarks></remarks>
    public partial interface ICollectdevice
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String Index { get; set; }

        /// <summary></summary>
        String ProductionlineID { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}