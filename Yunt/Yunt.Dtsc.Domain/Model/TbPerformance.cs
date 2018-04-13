﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>TbPerformance</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_performance", Description = "", ConnName = "yunt.dtsc", DbType = DatabaseType.MySql)]
    public partial class TbPerformance : ITbPerformance
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

        private Int32 _JobID;
        /// <summary></summary>
        [DisplayName("JobID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("job_id", "", "int(11)")]
        public virtual Int32 JobID
        {
            get { return _JobID; }
            set { if (OnPropertyChanging(__.JobID, value)) { _JobID = value; OnPropertyChanged(__.JobID); } }
        }

        private Int32 _NodeID;
        /// <summary></summary>
        [DisplayName("NodeID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("node_id", "", "int(11)")]
        public virtual Int32 NodeID
        {
            get { return _NodeID; }
            set { if (OnPropertyChanging(__.NodeID, value)) { _NodeID = value; OnPropertyChanged(__.NodeID); } }
        }

        private Single _Cpu;
        /// <summary></summary>
        [DisplayName("Cpu")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("cpu", "", "float")]
        public virtual Single Cpu
        {
            get { return _Cpu; }
            set { if (OnPropertyChanging(__.Cpu, value)) { _Cpu = value; OnPropertyChanged(__.Cpu); } }
        }

        private Single _Memory;
        /// <summary></summary>
        [DisplayName("Memory")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("memory", "", "float")]
        public virtual Single Memory
        {
            get { return _Memory; }
            set { if (OnPropertyChanging(__.Memory, value)) { _Memory = value; OnPropertyChanged(__.Memory); } }
        }

        private Single _Installdirsize;
        /// <summary></summary>
        [DisplayName("Installdirsize")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("installdirsize", "", "float")]
        public virtual Single Installdirsize
        {
            get { return _Installdirsize; }
            set { if (OnPropertyChanging(__.Installdirsize, value)) { _Installdirsize = value; OnPropertyChanged(__.Installdirsize); } }
        }

        private Int64 _Updatetime;
        /// <summary></summary>
        [DisplayName("Updatetime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("updatetime", "", "bigint(20)")]
        public virtual Int64 Updatetime
        {
            get { return _Updatetime; }
            set { if (OnPropertyChanging(__.Updatetime, value)) { _Updatetime = value; OnPropertyChanged(__.Updatetime); } }
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
                    case __.JobID : return _JobID;
                    case __.NodeID : return _NodeID;
                    case __.Cpu : return _Cpu;
                    case __.Memory : return _Memory;
                    case __.Installdirsize : return _Installdirsize;
                    case __.Updatetime : return _Updatetime;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.JobID : _JobID = Convert.ToInt32(value); break;
                    case __.NodeID : _NodeID = Convert.ToInt32(value); break;
                    case __.Cpu : _Cpu = Convert.ToSingle(value); break;
                    case __.Memory : _Memory = Convert.ToSingle(value); break;
                    case __.Installdirsize : _Installdirsize = Convert.ToSingle(value); break;
                    case __.Updatetime : _Updatetime = Convert.ToInt64(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbPerformance字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field JobID = FindByName(__.JobID);

            ///<summary></summary>
            public static readonly Field NodeID = FindByName(__.NodeID);

            ///<summary></summary>
            public static readonly Field Cpu = FindByName(__.Cpu);

            ///<summary></summary>
            public static readonly Field Memory = FindByName(__.Memory);

            ///<summary></summary>
            public static readonly Field Installdirsize = FindByName(__.Installdirsize);

            ///<summary></summary>
            public static readonly Field Updatetime = FindByName(__.Updatetime);

            ///<summary></summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbPerformance字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String JobID = "JobID";

            ///<summary></summary>
            public const String NodeID = "NodeID";

            ///<summary></summary>
            public const String Cpu = "Cpu";

            ///<summary></summary>
            public const String Memory = "Memory";

            ///<summary></summary>
            public const String Installdirsize = "Installdirsize";

            ///<summary></summary>
            public const String Updatetime = "Updatetime";

            ///<summary></summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>TbPerformance接口</summary>
    /// <remarks></remarks>
    public partial interface ITbPerformance
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 JobID { get; set; }

        /// <summary></summary>
        Int32 NodeID { get; set; }

        /// <summary></summary>
        Single Cpu { get; set; }

        /// <summary></summary>
        Single Memory { get; set; }

        /// <summary></summary>
        Single Installdirsize { get; set; }

        /// <summary></summary>
        Int64 Updatetime { get; set; }

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