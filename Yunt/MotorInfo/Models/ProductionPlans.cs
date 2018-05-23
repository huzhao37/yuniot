﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorInfo.Models
{
    /// <summary>ProductionPlans</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("production_plans", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class ProductionPlans : IProductionPlans
    {
        #region 属性
        private Int32 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("id", "", "int(11)")]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private Int32 _Start;
        /// <summary></summary>
        [DisplayName("Start")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("start", "", "int(11)")]
        public virtual Int32 Start
        {
            get { return _Start; }
            set { if (OnPropertyChanging(__.Start, value)) { _Start = value; OnPropertyChanged(__.Start); } }
        }

        private Int32 _End;
        /// <summary></summary>
        [DisplayName("End")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("end", "", "int(11)")]
        public virtual Int32 End
        {
            get { return _End; }
            set { if (OnPropertyChanging(__.End, value)) { _End = value; OnPropertyChanged(__.End); } }
        }

        private Int32 _MainCy;
        /// <summary></summary>
        [DisplayName("MainCy")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("main_cy", "", "int(11)")]
        public virtual Int32 MainCy
        {
            get { return _MainCy; }
            set { if (OnPropertyChanging(__.MainCy, value)) { _MainCy = value; OnPropertyChanged(__.MainCy); } }
        }

        private Int32 _FinishCy1;
        /// <summary></summary>
        [DisplayName("FinishCy1")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("finish_cy1", "", "int(11)")]
        public virtual Int32 FinishCy1
        {
            get { return _FinishCy1; }
            set { if (OnPropertyChanging(__.FinishCy1, value)) { _FinishCy1 = value; OnPropertyChanged(__.FinishCy1); } }
        }

        private Int32 _FinishCy2;
        /// <summary></summary>
        [DisplayName("FinishCy2")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("finish_cy2", "", "int(11)")]
        public virtual Int32 FinishCy2
        {
            get { return _FinishCy2; }
            set { if (OnPropertyChanging(__.FinishCy2, value)) { _FinishCy2 = value; OnPropertyChanged(__.FinishCy2); } }
        }

        private Int32 _FinishCy3;
        /// <summary></summary>
        [DisplayName("FinishCy3")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("finish_cy3", "", "int(11)")]
        public virtual Int32 FinishCy3
        {
            get { return _FinishCy3; }
            set { if (OnPropertyChanging(__.FinishCy3, value)) { _FinishCy3 = value; OnPropertyChanged(__.FinishCy3); } }
        }

        private Int32 _FinishCy4;
        /// <summary></summary>
        [DisplayName("FinishCy4")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("finish_cy4", "", "int(11)")]
        public virtual Int32 FinishCy4
        {
            get { return _FinishCy4; }
            set { if (OnPropertyChanging(__.FinishCy4, value)) { _FinishCy4 = value; OnPropertyChanged(__.FinishCy4); } }
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
                    case __.Start : return _Start;
                    case __.End : return _End;
                    case __.MainCy : return _MainCy;
                    case __.FinishCy1 : return _FinishCy1;
                    case __.FinishCy2 : return _FinishCy2;
                    case __.FinishCy3 : return _FinishCy3;
                    case __.FinishCy4 : return _FinishCy4;
                    case __.Time : return _Time;
                    case __.Remark : return _Remark;
                    case __.ProductionlineID : return _ProductionlineID;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Start : _Start = Convert.ToInt32(value); break;
                    case __.End : _End = Convert.ToInt32(value); break;
                    case __.MainCy : _MainCy = Convert.ToInt32(value); break;
                    case __.FinishCy1 : _FinishCy1 = Convert.ToInt32(value); break;
                    case __.FinishCy2 : _FinishCy2 = Convert.ToInt32(value); break;
                    case __.FinishCy3 : _FinishCy3 = Convert.ToInt32(value); break;
                    case __.FinishCy4 : _FinishCy4 = Convert.ToInt32(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    case __.ProductionlineID : _ProductionlineID = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得ProductionPlans字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Start = FindByName(__.Start);

            ///<summary></summary>
            public static readonly Field End = FindByName(__.End);

            ///<summary></summary>
            public static readonly Field MainCy = FindByName(__.MainCy);

            ///<summary></summary>
            public static readonly Field FinishCy1 = FindByName(__.FinishCy1);

            ///<summary></summary>
            public static readonly Field FinishCy2 = FindByName(__.FinishCy2);

            ///<summary></summary>
            public static readonly Field FinishCy3 = FindByName(__.FinishCy3);

            ///<summary></summary>
            public static readonly Field FinishCy4 = FindByName(__.FinishCy4);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field Remark = FindByName(__.Remark);

            ///<summary></summary>
            public static readonly Field ProductionlineID = FindByName(__.ProductionlineID);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得ProductionPlans字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Start = "Start";

            ///<summary></summary>
            public const String End = "End";

            ///<summary></summary>
            public const String MainCy = "MainCy";

            ///<summary></summary>
            public const String FinishCy1 = "FinishCy1";

            ///<summary></summary>
            public const String FinishCy2 = "FinishCy2";

            ///<summary></summary>
            public const String FinishCy3 = "FinishCy3";

            ///<summary></summary>
            public const String FinishCy4 = "FinishCy4";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String Remark = "Remark";

            ///<summary></summary>
            public const String ProductionlineID = "ProductionlineID";

        }
        #endregion
    }

    /// <summary>ProductionPlans接口</summary>
    /// <remarks></remarks>
    public partial interface IProductionPlans
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 Start { get; set; }

        /// <summary></summary>
        Int32 End { get; set; }

        /// <summary></summary>
        Int32 MainCy { get; set; }

        /// <summary></summary>
        Int32 FinishCy1 { get; set; }

        /// <summary></summary>
        Int32 FinishCy2 { get; set; }

        /// <summary></summary>
        Int32 FinishCy3 { get; set; }

        /// <summary></summary>
        Int32 FinishCy4 { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        String Remark { get; set; }

        /// <summary></summary>
        String ProductionlineID { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}