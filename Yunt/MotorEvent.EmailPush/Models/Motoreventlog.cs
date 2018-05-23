﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorEvent.EmailPush.Models
{
    /// <summary>Motoreventlog</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("motoreventlog", Description = "", ConnName = "unitooniot_analysis", DbType = DatabaseType.SqlServer)]
    public partial class Motoreventlog : IMotoreventlog
    {
        #region 属性
        private Int64 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("id", "", "bigint")]
        public virtual Int64 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _EventCode;
        /// <summary></summary>
        [DisplayName("EventCode")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("event_code", "", "varchar(255)")]
        public virtual String Event_Code
        {
            get { return _EventCode; }
            set { if (OnPropertyChanging(__.EventCode, value)) { _EventCode = value; OnPropertyChanged(__.EventCode); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("description", "", "nvarchar(255)")]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private Int32 _MotorID;
        /// <summary></summary>
        [DisplayName("MotorID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("motor_id", "", "int")]
        public virtual Int32 Motor_ID
        {
            get { return _MotorID; }
            set { if (OnPropertyChanging(__.MotorID, value)) { _MotorID = value; OnPropertyChanged(__.MotorID); } }
        }

        private String _Motorname;
        /// <summary></summary>
        [DisplayName("Motorname")]
        [Description("")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("motorname", "", "varchar(255)")]
        public virtual String Motorname
        {
            get { return _Motorname; }
            set { if (OnPropertyChanging(__.Motorname, value)) { _Motorname = value; OnPropertyChanged(__.Motorname); } }
        }

        private Int32 _ProductionlineID;
        /// <summary></summary>
        [DisplayName("ProductionlineID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("productionline_id", "", "int")]
        public virtual Int32 Productionline_ID
        {
            get { return _ProductionlineID; }
            set { if (OnPropertyChanging(__.ProductionlineID, value)) { _ProductionlineID = value; OnPropertyChanged(__.ProductionlineID); } }
        }

        private Int64 _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("time", "", "bigint")]
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
                    case __.ID : return _ID;
                    case __.EventCode : return _EventCode;
                    case __.Description : return _Description;
                    case __.MotorID : return _MotorID;
                    case __.Motorname : return _Motorname;
                    case __.ProductionlineID : return _ProductionlineID;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.EventCode : _EventCode = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.MotorID : _MotorID = Convert.ToInt32(value); break;
                    case __.Motorname : _Motorname = Convert.ToString(value); break;
                    case __.ProductionlineID : _ProductionlineID = Convert.ToInt32(value); break;
                    case __.Time : _Time = Convert.ToInt64(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Motoreventlog字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field EventCode = FindByName(__.EventCode);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field MotorID = FindByName(__.MotorID);

            ///<summary></summary>
            public static readonly Field Motorname = FindByName(__.Motorname);

            ///<summary></summary>
            public static readonly Field ProductionlineID = FindByName(__.ProductionlineID);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Motoreventlog字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String EventCode = "EventCode";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String MotorID = "MotorID";

            ///<summary></summary>
            public const String Motorname = "Motorname";

            ///<summary></summary>
            public const String ProductionlineID = "ProductionlineID";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>Motoreventlog接口</summary>
    /// <remarks></remarks>
    public partial interface IMotoreventlog
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String Event_Code { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        Int32 Motor_ID { get; set; }

        /// <summary></summary>
        String Motorname { get; set; }

        /// <summary></summary>
        Int32 Productionline_ID { get; set; }

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