﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>TbJob</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_job", Description = "", ConnName = "yunt.dtsc", DbType = DatabaseType.MySql)]
    public partial class TbJob : ITbJob
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

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("name", "", "VARCHAR(50)", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private Int64 _Single;
        /// <summary></summary>
        [DisplayName("Single")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("single", "", "bit(1)")]
        public virtual Int64 Single
        {
            get { return _Single; }
            set { if (OnPropertyChanging(__.Single, value)) { _Single = value; OnPropertyChanged(__.Single); } }
        }

        private String _Datamap;
        /// <summary></summary>
        [DisplayName("Datamap")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("datamap", "", "VARCHAR(255)")]
        public virtual String Datamap
        {
            get { return _Datamap; }
            set { if (OnPropertyChanging(__.Datamap, value)) { _Datamap = value; OnPropertyChanged(__.Datamap); } }
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

        private Int32 _CategoryID;
        /// <summary></summary>
        [DisplayName("CategoryID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("category_id", "", "int(11)")]
        public virtual Int32 CategoryID
        {
            get { return _CategoryID; }
            set { if (OnPropertyChanging(__.CategoryID, value)) { _CategoryID = value; OnPropertyChanged(__.CategoryID); } }
        }

        private Int32 _UserID;
        /// <summary></summary>
        [DisplayName("UserID")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("user_id", "", "int(11)")]
        public virtual Int32 UserID
        {
            get { return _UserID; }
            set { if (OnPropertyChanging(__.UserID, value)) { _UserID = value; OnPropertyChanged(__.UserID); } }
        }

        private Int64 _State;
        /// <summary></summary>
        [DisplayName("State")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("state", "", "bit(1)")]
        public virtual Int64 State
        {
            get { return _State; }
            set { if (OnPropertyChanging(__.State, value)) { _State = value; OnPropertyChanged(__.State); } }
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

        private Int32 _Runcount;
        /// <summary></summary>
        [DisplayName("Runcount")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("runcount", "", "int(11)")]
        public virtual Int32 Runcount
        {
            get { return _Runcount; }
            set { if (OnPropertyChanging(__.Runcount, value)) { _Runcount = value; OnPropertyChanged(__.Runcount); } }
        }

        private Int64 _Createtime;
        /// <summary></summary>
        [DisplayName("Createtime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("createtime", "", "bigint(20)")]
        public virtual Int64 Createtime
        {
            get { return _Createtime; }
            set { if (OnPropertyChanging(__.Createtime, value)) { _Createtime = value; OnPropertyChanged(__.Createtime); } }
        }

        private Int64 _Lastedstart;
        /// <summary></summary>
        [DisplayName("Lastedstart")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("lastedstart", "", "bigint(20)")]
        public virtual Int64 Lastedstart
        {
            get { return _Lastedstart; }
            set { if (OnPropertyChanging(__.Lastedstart, value)) { _Lastedstart = value; OnPropertyChanged(__.Lastedstart); } }
        }

        private Int64 _Lastedend;
        /// <summary></summary>
        [DisplayName("Lastedend")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("lastedend", "", "bigint(20)")]
        public virtual Int64 Lastedend
        {
            get { return _Lastedend; }
            set { if (OnPropertyChanging(__.Lastedend, value)) { _Lastedend = value; OnPropertyChanged(__.Lastedend); } }
        }

        private Int64 _Nextstart;
        /// <summary></summary>
        [DisplayName("Nextstart")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("nextstart", "", "bigint(20)")]
        public virtual Int64 Nextstart
        {
            get { return _Nextstart; }
            set { if (OnPropertyChanging(__.Nextstart, value)) { _Nextstart = value; OnPropertyChanged(__.Nextstart); } }
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

        private String _Cron;
        /// <summary></summary>
        [DisplayName("Cron")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("cron", "", "VARCHAR(50)")]
        public virtual String Cron
        {
            get { return _Cron; }
            set { if (OnPropertyChanging(__.Cron, value)) { _Cron = value; OnPropertyChanged(__.Cron); } }
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
                    case __.Name : return _Name;
                    case __.Single : return _Single;
                    case __.Datamap : return _Datamap;
                    case __.NodeID : return _NodeID;
                    case __.CategoryID : return _CategoryID;
                    case __.UserID : return _UserID;
                    case __.State : return _State;
                    case __.Version : return _Version;
                    case __.Runcount : return _Runcount;
                    case __.Createtime : return _Createtime;
                    case __.Lastedstart : return _Lastedstart;
                    case __.Lastedend : return _Lastedend;
                    case __.Nextstart : return _Nextstart;
                    case __.Remark : return _Remark;
                    case __.Cron : return _Cron;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Single : _Single = Convert.ToInt64(value); break;
                    case __.Datamap : _Datamap = Convert.ToString(value); break;
                    case __.NodeID : _NodeID = Convert.ToInt32(value); break;
                    case __.CategoryID : _CategoryID = Convert.ToInt32(value); break;
                    case __.UserID : _UserID = Convert.ToInt32(value); break;
                    case __.State : _State = Convert.ToInt64(value); break;
                    case __.Version : _Version = Convert.ToInt32(value); break;
                    case __.Runcount : _Runcount = Convert.ToInt32(value); break;
                    case __.Createtime : _Createtime = Convert.ToInt64(value); break;
                    case __.Lastedstart : _Lastedstart = Convert.ToInt64(value); break;
                    case __.Lastedend : _Lastedend = Convert.ToInt64(value); break;
                    case __.Nextstart : _Nextstart = Convert.ToInt64(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    case __.Cron : _Cron = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbJob字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field Single = FindByName(__.Single);

            ///<summary></summary>
            public static readonly Field Datamap = FindByName(__.Datamap);

            ///<summary></summary>
            public static readonly Field NodeID = FindByName(__.NodeID);

            ///<summary></summary>
            public static readonly Field CategoryID = FindByName(__.CategoryID);

            ///<summary></summary>
            public static readonly Field UserID = FindByName(__.UserID);

            ///<summary></summary>
            public static readonly Field State = FindByName(__.State);

            ///<summary></summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary></summary>
            public static readonly Field Runcount = FindByName(__.Runcount);

            ///<summary></summary>
            public static readonly Field Createtime = FindByName(__.Createtime);

            ///<summary></summary>
            public static readonly Field Lastedstart = FindByName(__.Lastedstart);

            ///<summary></summary>
            public static readonly Field Lastedend = FindByName(__.Lastedend);

            ///<summary></summary>
            public static readonly Field Nextstart = FindByName(__.Nextstart);

            ///<summary></summary>
            public static readonly Field Remark = FindByName(__.Remark);

            ///<summary></summary>
            public static readonly Field Cron = FindByName(__.Cron);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbJob字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String Single = "Single";

            ///<summary></summary>
            public const String Datamap = "Datamap";

            ///<summary></summary>
            public const String NodeID = "NodeID";

            ///<summary></summary>
            public const String CategoryID = "CategoryID";

            ///<summary></summary>
            public const String UserID = "UserID";

            ///<summary></summary>
            public const String State = "State";

            ///<summary></summary>
            public const String Version = "Version";

            ///<summary></summary>
            public const String Runcount = "Runcount";

            ///<summary></summary>
            public const String Createtime = "Createtime";

            ///<summary></summary>
            public const String Lastedstart = "Lastedstart";

            ///<summary></summary>
            public const String Lastedend = "Lastedend";

            ///<summary></summary>
            public const String Nextstart = "Nextstart";

            ///<summary></summary>
            public const String Remark = "Remark";

            ///<summary></summary>
            public const String Cron = "Cron";

        }
        #endregion
    }

    /// <summary>TbJob接口</summary>
    /// <remarks></remarks>
    public partial interface ITbJob
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        Int64 Single { get; set; }

        /// <summary></summary>
        String Datamap { get; set; }

        /// <summary></summary>
        Int32 NodeID { get; set; }

        /// <summary></summary>
        Int32 CategoryID { get; set; }

        /// <summary></summary>
        Int32 UserID { get; set; }

        /// <summary></summary>
        Int64 State { get; set; }

        /// <summary></summary>
        Int32 Version { get; set; }

        /// <summary></summary>
        Int32 Runcount { get; set; }

        /// <summary></summary>
        Int64 Createtime { get; set; }

        /// <summary></summary>
        Int64 Lastedstart { get; set; }

        /// <summary></summary>
        Int64 Lastedend { get; set; }

        /// <summary></summary>
        Int64 Nextstart { get; set; }

        /// <summary></summary>
        String Remark { get; set; }

        /// <summary></summary>
        String Cron { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}