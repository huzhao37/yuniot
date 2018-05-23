﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace MotorEvent.EmailPush.Models
{
    /// <summary>ProductionLine</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("ProductionLine", Description = "", ConnName = "unitooniot_configmanager", DbType = DatabaseType.SqlServer)]
    public partial class ProductionLine : IProductionLine
    {
        #region 属性
        private Int32 _Id;
        /// <summary></summary>
        [DisplayName("Id")]
        [Description("")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "", "int")]
        public virtual Int32 Id
        {
            get { return _Id; }
            set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } }
        }

        private DateTime _BuildTime;
        /// <summary></summary>
        [DisplayName("BuildTime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BuildTime", "", "datetime")]
        public virtual DateTime BuildTime
        {
            get { return _BuildTime; }
            set { if (OnPropertyChanging(__.BuildTime, value)) { _BuildTime = value; OnPropertyChanged(__.BuildTime); } }
        }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [Description("")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn("Name", "", "nvarchar(200)", Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private Double _Output;
        /// <summary></summary>
        [DisplayName("Output")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Output", "", "float")]
        public virtual Double Output
        {
            get { return _Output; }
            set { if (OnPropertyChanging(__.Output, value)) { _Output = value; OnPropertyChanged(__.Output); } }
        }

        private String _Location;
        /// <summary></summary>
        [DisplayName("Location")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Location", "", "nvarchar(-1)")]
        public virtual String Location
        {
            get { return _Location; }
            set { if (OnPropertyChanging(__.Location, value)) { _Location = value; OnPropertyChanged(__.Location); } }
        }

        private String _PropertyRightPerson;
        /// <summary></summary>
        [DisplayName("PropertyRightPerson")]
        [Description("")]
        [DataObjectField(false, false, true, 120)]
        [BindColumn("PropertyRightPerson", "", "nvarchar(120)")]
        public virtual String PropertyRightPerson
        {
            get { return _PropertyRightPerson; }
            set { if (OnPropertyChanging(__.PropertyRightPerson, value)) { _PropertyRightPerson = value; OnPropertyChanged(__.PropertyRightPerson); } }
        }

        private String _ResponsiblePerson;
        /// <summary></summary>
        [DisplayName("ResponsiblePerson")]
        [Description("")]
        [DataObjectField(false, false, true, 120)]
        [BindColumn("ResponsiblePerson", "", "nvarchar(120)")]
        public virtual String ResponsiblePerson
        {
            get { return _ResponsiblePerson; }
            set { if (OnPropertyChanging(__.ResponsiblePerson, value)) { _ResponsiblePerson = value; OnPropertyChanged(__.ResponsiblePerson); } }
        }

        private String _ResponsiblePersonContact;
        /// <summary></summary>
        [DisplayName("ResponsiblePersonContact")]
        [Description("")]
        [DataObjectField(false, false, true, 120)]
        [BindColumn("ResponsiblePersonContact", "", "nvarchar(120)")]
        public virtual String ResponsiblePersonContact
        {
            get { return _ResponsiblePersonContact; }
            set { if (OnPropertyChanging(__.ResponsiblePersonContact, value)) { _ResponsiblePersonContact = value; OnPropertyChanged(__.ResponsiblePersonContact); } }
        }

        private Int32 _Status;
        /// <summary></summary>
        [DisplayName("Status")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Status", "", "int")]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }

        private Double _Longitude;
        /// <summary></summary>
        [DisplayName("Longitude")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Longitude", "", "float")]
        public virtual Double Longitude
        {
            get { return _Longitude; }
            set { if (OnPropertyChanging(__.Longitude, value)) { _Longitude = value; OnPropertyChanged(__.Longitude); } }
        }

        private Double _Latitude;
        /// <summary></summary>
        [DisplayName("Latitude")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Latitude", "", "float")]
        public virtual Double Latitude
        {
            get { return _Latitude; }
            set { if (OnPropertyChanging(__.Latitude, value)) { _Latitude = value; OnPropertyChanged(__.Latitude); } }
        }

        private String _OreType;
        /// <summary></summary>
        [DisplayName("OreType")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("OreType", "", "nvarchar(-1)")]
        public virtual String OreType
        {
            get { return _OreType; }
            set { if (OnPropertyChanging(__.OreType, value)) { _OreType = value; OnPropertyChanged(__.OreType); } }
        }

        private DateTime _ExpiredTime;
        /// <summary></summary>
        [DisplayName("ExpiredTime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExpiredTime", "", "datetime")]
        public virtual DateTime ExpiredTime
        {
            get { return _ExpiredTime; }
            set { if (OnPropertyChanging(__.ExpiredTime, value)) { _ExpiredTime = value; OnPropertyChanged(__.ExpiredTime); } }
        }

        private String _Production;
        /// <summary></summary>
        [DisplayName("Production")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Production", "", "nvarchar(-1)")]
        public virtual String Production
        {
            get { return _Production; }
            set { if (OnPropertyChanging(__.Production, value)) { _Production = value; OnPropertyChanged(__.Production); } }
        }

        private DateTime _LatestDataTime;
        /// <summary></summary>
        [DisplayName("LatestDataTime")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LatestDataTime", "", "datetime")]
        public virtual DateTime LatestDataTime
        {
            get { return _LatestDataTime; }
            set { if (OnPropertyChanging(__.LatestDataTime, value)) { _LatestDataTime = value; OnPropertyChanged(__.LatestDataTime); } }
        }

        private Boolean _IsDeleted;
        /// <summary></summary>
        [DisplayName("IsDeleted")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDeleted", "", "bit")]
        public virtual Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { if (OnPropertyChanging(__.IsDeleted, value)) { _IsDeleted = value; OnPropertyChanged(__.IsDeleted); } }
        }

        private DateTime _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Time", "", "datetime2(7)")]
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
                    case __.BuildTime : return _BuildTime;
                    case __.Name : return _Name;
                    case __.Output : return _Output;
                    case __.Location : return _Location;
                    case __.PropertyRightPerson : return _PropertyRightPerson;
                    case __.ResponsiblePerson : return _ResponsiblePerson;
                    case __.ResponsiblePersonContact : return _ResponsiblePersonContact;
                    case __.Status : return _Status;
                    case __.Longitude : return _Longitude;
                    case __.Latitude : return _Latitude;
                    case __.OreType : return _OreType;
                    case __.ExpiredTime : return _ExpiredTime;
                    case __.Production : return _Production;
                    case __.LatestDataTime : return _LatestDataTime;
                    case __.IsDeleted : return _IsDeleted;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.BuildTime : _BuildTime = Convert.ToDateTime(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Output : _Output = Convert.ToDouble(value); break;
                    case __.Location : _Location = Convert.ToString(value); break;
                    case __.PropertyRightPerson : _PropertyRightPerson = Convert.ToString(value); break;
                    case __.ResponsiblePerson : _ResponsiblePerson = Convert.ToString(value); break;
                    case __.ResponsiblePersonContact : _ResponsiblePersonContact = Convert.ToString(value); break;
                    case __.Status : _Status = Convert.ToInt32(value); break;
                    case __.Longitude : _Longitude = Convert.ToDouble(value); break;
                    case __.Latitude : _Latitude = Convert.ToDouble(value); break;
                    case __.OreType : _OreType = Convert.ToString(value); break;
                    case __.ExpiredTime : _ExpiredTime = Convert.ToDateTime(value); break;
                    case __.Production : _Production = Convert.ToString(value); break;
                    case __.LatestDataTime : _LatestDataTime = Convert.ToDateTime(value); break;
                    case __.IsDeleted : _IsDeleted = Convert.ToBoolean(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得ProductionLine字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field Id = FindByName(__.Id);

            ///<summary></summary>
            public static readonly Field BuildTime = FindByName(__.BuildTime);

            ///<summary></summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary></summary>
            public static readonly Field Output = FindByName(__.Output);

            ///<summary></summary>
            public static readonly Field Location = FindByName(__.Location);

            ///<summary></summary>
            public static readonly Field PropertyRightPerson = FindByName(__.PropertyRightPerson);

            ///<summary></summary>
            public static readonly Field ResponsiblePerson = FindByName(__.ResponsiblePerson);

            ///<summary></summary>
            public static readonly Field ResponsiblePersonContact = FindByName(__.ResponsiblePersonContact);

            ///<summary></summary>
            public static readonly Field Status = FindByName(__.Status);

            ///<summary></summary>
            public static readonly Field Longitude = FindByName(__.Longitude);

            ///<summary></summary>
            public static readonly Field Latitude = FindByName(__.Latitude);

            ///<summary></summary>
            public static readonly Field OreType = FindByName(__.OreType);

            ///<summary></summary>
            public static readonly Field ExpiredTime = FindByName(__.ExpiredTime);

            ///<summary></summary>
            public static readonly Field Production = FindByName(__.Production);

            ///<summary></summary>
            public static readonly Field LatestDataTime = FindByName(__.LatestDataTime);

            ///<summary></summary>
            public static readonly Field IsDeleted = FindByName(__.IsDeleted);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得ProductionLine字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String Id = "Id";

            ///<summary></summary>
            public const String BuildTime = "BuildTime";

            ///<summary></summary>
            public const String Name = "Name";

            ///<summary></summary>
            public const String Output = "Output";

            ///<summary></summary>
            public const String Location = "Location";

            ///<summary></summary>
            public const String PropertyRightPerson = "PropertyRightPerson";

            ///<summary></summary>
            public const String ResponsiblePerson = "ResponsiblePerson";

            ///<summary></summary>
            public const String ResponsiblePersonContact = "ResponsiblePersonContact";

            ///<summary></summary>
            public const String Status = "Status";

            ///<summary></summary>
            public const String Longitude = "Longitude";

            ///<summary></summary>
            public const String Latitude = "Latitude";

            ///<summary></summary>
            public const String OreType = "OreType";

            ///<summary></summary>
            public const String ExpiredTime = "ExpiredTime";

            ///<summary></summary>
            public const String Production = "Production";

            ///<summary></summary>
            public const String LatestDataTime = "LatestDataTime";

            ///<summary></summary>
            public const String IsDeleted = "IsDeleted";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>ProductionLine接口</summary>
    /// <remarks></remarks>
    public partial interface IProductionLine
    {
        #region 属性
        /// <summary></summary>
        Int32 Id { get; set; }

        /// <summary></summary>
        DateTime BuildTime { get; set; }

        /// <summary></summary>
        String Name { get; set; }

        /// <summary></summary>
        Double Output { get; set; }

        /// <summary></summary>
        String Location { get; set; }

        /// <summary></summary>
        String PropertyRightPerson { get; set; }

        /// <summary></summary>
        String ResponsiblePerson { get; set; }

        /// <summary></summary>
        String ResponsiblePersonContact { get; set; }

        /// <summary></summary>
        Int32 Status { get; set; }

        /// <summary></summary>
        Double Longitude { get; set; }

        /// <summary></summary>
        Double Latitude { get; set; }

        /// <summary></summary>
        String OreType { get; set; }

        /// <summary></summary>
        DateTime ExpiredTime { get; set; }

        /// <summary></summary>
        String Production { get; set; }

        /// <summary></summary>
        DateTime LatestDataTime { get; set; }

        /// <summary></summary>
        Boolean IsDeleted { get; set; }

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