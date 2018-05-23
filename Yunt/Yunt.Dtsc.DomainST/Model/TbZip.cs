﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>TbZip</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_zip", Description = "", ConnName = "yunt.dtsc", DbType = DatabaseType.MySql)]
    public partial class TbZip : ITbZip
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

        private Int32 _Version;
        /// <summary></summary>
        [DisplayName("Version")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("version", "", "int(11)")]
        public virtual Int32 Version
        {
            get { return _Version; }
            set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } }
        }

        private String _Zipfilename;
        /// <summary></summary>
        [DisplayName("Zipfilename")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("zipfilename", "", "VARCHAR(50)")]
        public virtual String Zipfilename
        {
            get { return _Zipfilename; }
            set { if (OnPropertyChanging(__.Zipfilename, value)) { _Zipfilename = value; OnPropertyChanged(__.Zipfilename); } }
        }

        private Byte[] _Zipfile;
        /// <summary></summary>
        [DisplayName("Zipfile")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("zipfile", "", "longblob")]
        public virtual Byte[] Zipfile
        {
            get { return _Zipfile; }
            set { if (OnPropertyChanging(__.Zipfile, value)) { _Zipfile = value; OnPropertyChanged(__.Zipfile); } }
        }

        private Int64 _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("time", "", "bigint(20)")]
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
                    case __.JobID : return _JobID;
                    case __.Version : return _Version;
                    case __.Zipfilename : return _Zipfilename;
                    case __.Zipfile : return _Zipfile;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.JobID : _JobID = Convert.ToInt32(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.Zipfilename : _Zipfilename = Convert.ToString(value); break;
                    case __.Zipfile : _Zipfile = (Byte[])value; break;
                    case __.Time : _Time = Convert.ToInt64(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbZip字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field JobID = FindByName(__.JobID);

            ///<summary></summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary></summary>
            public static readonly Field Zipfilename = FindByName(__.Zipfilename);

            ///<summary></summary>
            public static readonly Field Zipfile = FindByName(__.Zipfile);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbZip字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String JobID = "JobID";

            ///<summary></summary>
            public const String Version = "Version";

            ///<summary></summary>
            public const String Zipfilename = "Zipfilename";

            ///<summary></summary>
            public const String Zipfile = "Zipfile";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>TbZip接口</summary>
    /// <remarks></remarks>
    public partial interface ITbZip
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 JobID { get; set; }

        /// <summary></summary>
        Int32 Version { get; set; }

        /// <summary></summary>
        String Zipfilename { get; set; }

        /// <summary></summary>
        Byte[] Zipfile { get; set; }

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