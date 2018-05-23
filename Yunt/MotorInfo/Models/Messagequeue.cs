﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorInfo.Models
{
    /// <summary>Messagequeue</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("messagequeue", Description = "", ConnName = "yunt_xml", DbType = DatabaseType.MySql)]
    public partial class Messagequeue : IMessagequeue
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

        private String _Host;
        /// <summary></summary>
        [DisplayName("Host")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("host", "", "VARCHAR(50)")]
        public virtual String Host
        {
            get { return _Host; }
            set { if (OnPropertyChanging(__.Host, value)) { _Host = value; OnPropertyChanged(__.Host); } }
        }

        private Int32 _Port;
        /// <summary></summary>
        [DisplayName("Port")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("port", "", "int(11)")]
        public virtual Int32 Port
        {
            get { return _Port; }
            set { if (OnPropertyChanging(__.Port, value)) { _Port = value; OnPropertyChanged(__.Port); } }
        }

        private String _RouteKey;
        /// <summary></summary>
        [DisplayName("RouteKey")]
        [Description("")]
        [DataObjectField(false, false, false, 20)]
        [BindColumn("route_key", "", "VARCHAR(20)")]
        public virtual String RouteKey
        {
            get { return _RouteKey; }
            set { if (OnPropertyChanging(__.RouteKey, value)) { _RouteKey = value; OnPropertyChanged(__.RouteKey); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, false, 20)]
        [BindColumn("name", "", "VARCHAR(20)", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private Int32 _Timer;
        /// <summary></summary>
        [DisplayName("Timer")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("timer", "", "int(11)")]
        public virtual Int32 Timer
        {
            get { return _Timer; }
            set { if (OnPropertyChanging(__.Timer, value)) { _Timer = value; OnPropertyChanged(__.Timer); } }
        }

        private String _CollectdeviceIndex;
        /// <summary></summary>
        [DisplayName("CollectdeviceIndex")]
        [Description("")]
        [DataObjectField(false, false, false, 20)]
        [BindColumn("collectdevice_index", "", "VARCHAR(20)")]
        public virtual String CollectdeviceIndex
        {
            get { return _CollectdeviceIndex; }
            set { if (OnPropertyChanging(__.CollectdeviceIndex, value)) { _CollectdeviceIndex = value; OnPropertyChanged(__.CollectdeviceIndex); } }
        }

        private Int32 _WriteRead;
        /// <summary></summary>
        [DisplayName("WriteRead")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("write_read", "", "int(10)")]
        public virtual Int32 WriteRead
        {
            get { return _WriteRead; }
            set { if (OnPropertyChanging(__.WriteRead, value)) { _WriteRead = value; OnPropertyChanged(__.WriteRead); } }
        }

        private String _ComType;
        /// <summary></summary>
        [DisplayName("ComType")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("com_type", "", "VARCHAR(50)")]
        public virtual String ComType
        {
            get { return _ComType; }
            set { if (OnPropertyChanging(__.ComType, value)) { _ComType = value; OnPropertyChanged(__.ComType); } }
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

        private String _Username;
        /// <summary></summary>
        [DisplayName("Username")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("username", "", "VARCHAR(255)")]
        public virtual String Username
        {
            get { return _Username; }
            set { if (OnPropertyChanging(__.Username, value)) { _Username = value; OnPropertyChanged(__.Username); } }
        }

        private String _Pwd;
        /// <summary></summary>
        [DisplayName("Pwd")]
        [Description("")]
        [DataObjectField(false, false, false, 255)]
        [BindColumn("pwd", "", "VARCHAR(255)")]
        public virtual String Pwd
        {
            get { return _Pwd; }
            set { if (OnPropertyChanging(__.Pwd, value)) { _Pwd = value; OnPropertyChanged(__.Pwd); } }
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
                    case __.Host : return _Host;
                    case __.Port : return _Port;
                    case __.RouteKey : return _RouteKey;
                    case __.Name : return _Name;
                    case __.Timer : return _Timer;
                    case __.CollectdeviceIndex : return _CollectdeviceIndex;
                    case __.WriteRead : return _WriteRead;
                    case __.ComType : return _ComType;
                    case __.Time : return _Time;
                    case __.Remark : return _Remark;
                    case __.Username : return _Username;
                    case __.Pwd : return _Pwd;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Host : _Host = Convert.ToString(value); break;
                    case __.Port : _Port = Convert.ToInt32(value); break;
                    case __.RouteKey : _RouteKey = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Timer : _Timer = Convert.ToInt32(value); break;
                    case __.CollectdeviceIndex : _CollectdeviceIndex = Convert.ToString(value); break;
                    case __.WriteRead : _WriteRead = Convert.ToInt32(value); break;
                    case __.ComType : _ComType = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    case __.Username : _Username = Convert.ToString(value); break;
                    case __.Pwd : _Pwd = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Messagequeue字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Host = FindByName(__.Host);

            ///<summary></summary>
            public static readonly Field Port = FindByName(__.Port);

            ///<summary></summary>
            public static readonly Field RouteKey = FindByName(__.RouteKey);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field Timer = FindByName(__.Timer);

            ///<summary></summary>
            public static readonly Field CollectdeviceIndex = FindByName(__.CollectdeviceIndex);

            ///<summary></summary>
            public static readonly Field WriteRead = FindByName(__.WriteRead);

            ///<summary></summary>
            public static readonly Field ComType = FindByName(__.ComType);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            ///<summary></summary>
            public static readonly Field Remark = FindByName(__.Remark);

            ///<summary></summary>
            public static readonly Field Username = FindByName(__.Username);

            ///<summary></summary>
            public static readonly Field Pwd = FindByName(__.Pwd);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得Messagequeue字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Host = "Host";

            ///<summary></summary>
            public const String Port = "Port";

            ///<summary></summary>
            public const String RouteKey = "RouteKey";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String Timer = "Timer";

            ///<summary></summary>
            public const String CollectdeviceIndex = "CollectdeviceIndex";

            ///<summary></summary>
            public const String WriteRead = "WriteRead";

            ///<summary></summary>
            public const String ComType = "ComType";

            ///<summary></summary>
            public const String Time = "Time";

            ///<summary></summary>
            public const String Remark = "Remark";

            ///<summary></summary>
            public const String Username = "Username";

            ///<summary></summary>
            public const String Pwd = "Pwd";

        }
        #endregion
    }

    /// <summary>Messagequeue接口</summary>
    /// <remarks></remarks>
    public partial interface IMessagequeue
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String Host { get; set; }

        /// <summary></summary>
        Int32 Port { get; set; }

        /// <summary></summary>
        String RouteKey { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        Int32 Timer { get; set; }

        /// <summary></summary>
        String CollectdeviceIndex { get; set; }

        /// <summary></summary>
        Int32 WriteRead { get; set; }

        /// <summary></summary>
        String ComType { get; set; }

        /// <summary></summary>
        DateTime Time { get; set; }

        /// <summary></summary>
        String Remark { get; set; }

        /// <summary></summary>
        String Username { get; set; }

        /// <summary></summary>
        String Pwd { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}