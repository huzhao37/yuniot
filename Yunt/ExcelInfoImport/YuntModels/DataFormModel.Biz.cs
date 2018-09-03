﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Log;
using NewLife.Web;
﻿using NewLife.Data;
using XCode;
using XCode.Configuration;
using XCode.Membership;
using System.IO;
using System.Linq;

namespace ExcelInfoImport.YuntModels
{
    /// <summary>本地数据库</summary>
    public partial class Dataformmodel : Entity<Dataformmodel>
    {
        #region 对象操作
            ﻿

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
			// 如果没有脏数据，则不需要进行任何处理
			if (!HasDirty) return;

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);
            
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Dataformmodel).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new Dataformmodel();
        //    entity.MachineName = "abc";
        //    entity.Index = 0;
        //    entity.FieldParam = "abc";
        //    entity.FieldParamEn = "abc";
        //    entity.MotorTypeName = "abc";
        //    entity.Unit = "abc";
        //    entity.DataType = "abc";
        //    entity.DataPhysicalFeature = "abc";
        //    entity.DataPhysicalAccuracy = "abc";
        //    entity.MachineId = "abc";
        //    entity.DeviceId = "abc";
        //    entity.Time = DateTime.Now;
        //    entity.Value = 0;
        //    entity.DIValue = 0;
        //    entity.DOValue = 0;
        //    entity.DebugValue = 0;
        //    entity.WarnValue = 0;
        //    entity.Bit = 0;
        //    entity.BitDesc = "abc";
        //    entity.LineId = 0;
        //    entity.DataConfigId = 0;
        //    entity.MotorId = 0;
        //    entity.DataPhysicalId = 0;
        //    entity.FormatId = 0;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Dataformmodel).Name, Meta.Table.DataTable.DisplayName);
        //}

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        /// <summary>根据ID查找</summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Entity<Dataformmodel> FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(__.ID, id);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.ID, id);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }

        /// <summary>根据Index查找</summary>
        /// <param name="index">Index</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Entity<Dataformmodel> FindByIndex(Int32 index)
        {
            if (Meta.Count >= 1000)
                return Find(__.Index, index);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Index, index);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }

        /// <summary>根据ProLineId和表单ID查找</summary>
        /// <param name="pId">ProLineId</param>
        /// <param name="configId">DataConfigId</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<Dataformmodel> FindByProLineId(string pId, string configId)
        {
            if (Meta.Count >= 1000)
                return FindAll(new[] { __.LineId, __.CollectdeviceIndex },
                    new object[] { pId, configId });
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(e => e.LineId.EqualIgnoreCase(pId) && e.CollectdeviceIndex.EqualIgnoreCase(configId));
            // 单对象缓存
            //return Meta.SingleCache[id];
        }

        /// <summary>根据ProLineId和电机名称查找</summary>
        /// <param name="pId">ProLineId</param>
        /// <param name="motorName">MachineName</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<Dataformmodel> FindByLineIdAndMachineName(string pId, string motorName)
        {
            if (Meta.Count >= 1000)
                return FindAll(new string[] { __.LineId, __.MachineName },
                    new object[] { pId, motorName });
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(e => e.LineId.EqualIgnoreCase(pId) && e.MachineName.Equals(motorName));
            // 单对象缓存
            //return Meta.SingleCache[id];
        }

        /// <summary>根据ProLineId和电机名称、数据类型查找</summary>
        /// <param name="pId">ProLineId</param>
        /// <param name="motorName">MachineName</param>
        /// <param name="dataType">MachineName</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<Dataformmodel> FindByLineIdAndMachineName(string pId, string motorName, string dataType)
        {
            //九江新产线反击破需要
            if (motorName.Contains("反击破") && pId.EqualIgnoreCase("189"))
                dataType = "模拟量";
            var where = $"LineId=" + pId + " and MachineName=" + "'" + motorName + $"' and DataType like" + "'%" + dataType + "%'";

            if (Meta.Count >= 1000)
                return FindAll(where, null, null, 0, 0);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(e => e.LineId.EqualIgnoreCase(pId) && e.MachineName.Equals(motorName) && e.DataType.Contains(dataType));

            //if (Meta.Count >= 1000)
            //    return FindAll(new string[] { __.LineId, __.MachineName },
            //        new object[] { pId, motorName });
            //else // 实体缓存
            //    return Meta.Cache.Entities.FindAll(e => e.LineId == pId && e.MachineName.Equals(motorName));
            // 单对象缓存
            //return Meta.SingleCache[id];
        }

        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="userid">用户编号</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static IList<Dataformmodel> Search(Int32 userid, DateTime start, DateTime end, String key, PageParameter param)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
            var exp = SearchWhereByKeys(key, null, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //exp &= _.OccurTime.Between(start, end); // 大于等于start，小于end，当start/end大于MinValue时有效

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        public static void FromExcel(string  pId, int configId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NewLife.Runtime.IsWeb ? "Files" : "Files", $"{configId}.txt");
            var motorParams = UnitoonModels.MotorParamters.FindAll($"SELECT * from MotorParamters WHERE  EmbeddedDeviceId={configId} ORDER BY [INDEX] ASC")?.ToList();//
            if (File.Exists(path))
            {
                XTrace.WriteLine("从文件导入数据表单基本数据");
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    sr.ReadLine(); //跳过第一行
                    //sr.ReadLine(); //跳过第二行
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (line.IsNullOrWhiteSpace()) continue;

                        var arr = line.Split(new char[] { ' ', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (configId== 50)
                        {

                            AddEntity(Convert.ToInt32(arr[0]),
                                arr[10], arr[17], arr[18], arr[19], pId, "010203040546",//采集设备Index/"010203040538"
                                 arr[3], motorParams);


                        }else{
                            AddEntity(Convert.ToInt32(arr[0]),
                                arr[10], arr[17], arr[18], arr[19], pId, "010203040538",//采集设备Index/"010203040546"
                                 arr[3], motorParams);
                        }


                    }
                }
            }
        }


        private static void AddEntity(int index,  string unit,
            string dataType, string dataPhysicalFeature, string dataPhysicalAccuracy, string pId,string collectDeviceId, 
           string remark, List<UnitoonModels.MotorParamters> motorParams)
        {

            var motorParam=motorParams.Find(e => e.Index == index);        
            var dataPhysical = Physicfeature.Find("PhysicType", dataPhysicalFeature);
            //这种组成方式。。。（需要规范）
            var bitDescStr = dataType.Split("bit");
            var bit = 0;
            var bitDesc = "";

            bit = Convert.ToInt32(bitDescStr[0]);
            if (bitDescStr.Length == 2)
                bitDesc = bitDescStr[1];
            if (bit.Equals("LOGID"))
            {
                bit = 32;
                bitDesc = "整型模拟量";
            }
            if (bit.Equals("时间"))
            {
                bit = 32;
                bitDesc = "时间";
            }
            UnitoonModels.Motor motor = null;
            UnitoonModels.CabinetParamter cabinetParamter = null;
            UnitoonModels.AlarmType alarmType = null;
            var fieldParam = "";
            var fieldParamEn = "";
            if (motorParam != null)
            {
                motor = UnitoonModels.Motor.Find("Id", motorParam.MotorId);
                if (motorParam.AlarmTypeId == 0)
                {
                    cabinetParamter = UnitoonModels.CabinetParamter.Find("Id", motorParam.CabinetParamterId);
                    fieldParam = cabinetParamter.Description;
                    fieldParamEn = cabinetParamter.Param;
                }
                else
                {
                    alarmType = UnitoonModels.AlarmType.Find("Id", motorParam.AlarmTypeId);
                    fieldParam = alarmType.Description;
                    //fieldParamEn = alarmType.Description;
                }
            }
            else
            {
                return;
            }
            var oldMotorType = UnitoonModels.MotorType.Find("Id", (motor?.MotorTypeId??0));
            var motorType = Motortype.Find("MotorTypeName", oldMotorType?.MachineType??"");
            if (motorType == null)
            {
                var entity = new Dataformmodel
                {
                    Index = index - 1,
                    FieldParam = fieldParam,
                    FieldParamEn = fieldParamEn,
                    MotorTypeName = motorType?.MotorTypeId??"Sim",
                    MachineName = motor?.Name,
                    Unit = dataPhysical?.Unit ?? "",
                    DataType = dataType,
                    DataPhysicalFeature = dataPhysicalFeature,
                    DataPhysicalAccuracy = dataPhysicalAccuracy,
                    LineId = pId,
                    Time = DateTime.Now,
                    CollectdeviceIndex = collectDeviceId,
                    DataPhysicalId = dataPhysical.Id,
                    Bit = bit,
                    BitDesc = bitDesc,
                    MotorId = pId + "-" + "Sim" + motorParam?.CardId.ToString() ?? "UnKnown",//
                    Remark = remark
                };
                entity.Insert();
            }
            else
            {
                var entity = new Dataformmodel
                {
                    Index = index - 1,
                    FieldParam = fieldParam,
                    FieldParamEn = fieldParamEn,
                    MotorTypeName = motorType.MotorTypeId,
                    MachineName = motor?.Name,
                    Unit = dataPhysical?.Unit ?? "",
                    DataType = dataType,
                    DataPhysicalFeature = dataPhysicalFeature,
                    DataPhysicalAccuracy = dataPhysicalAccuracy,
                    LineId = pId,
                    Time = DateTime.Now,
                    CollectdeviceIndex = collectDeviceId,
                    DataPhysicalId = dataPhysical.Id,
                    Bit = bit,
                    BitDesc = bitDesc,
                    MotorId = pId + "-" + motorType.MotorTypeId + motorParam?.MotorId.ToString() ?? "UnKnown",//
                    Remark = remark
                };

                entity.Insert();
            }   
        }
        #endregion

        #region 业务

        /// <summary>
        /// 命名规范，望以后改正，在初始化使用，耗时
        /// </summary>
        private static string NameRegular(string name)
        {
            if (name.Contains("给料"))
                name = "给料机";
            if (name.Contains("鄂式"))
                name = "颚式破碎机";
            if (name.Contains("单缸"))
                name = "单缸圆锥破碎机";
            if (name.Contains("立轴"))
                name = "立轴破碎机";
            if (name.Contains("震动") || name.Contains("振动"))
                name = "振动筛";
            if (name.Contains("皮带"))
                name = "皮带机";
            if (name.Contains("西蒙斯"))
                name = "西蒙斯圆锥破碎机";
            if (name.Contains("磨粉"))
                name = "磨粉机";
            if (name.Contains("反击"))
                name = "反击破碎机";
            return name;

        }

        public static void MotorParams()
        {
            var dataForms = Dataformmodel.FindAll();
            var entities = new List<Motorparams>();
            if (dataForms != null && dataForms.Any())
                dataForms.ToList().ForEach(d =>
                {
                    if (d.FieldParamEn.IsNullOrWhiteSpace())
                        return;
                   var entity=new Motorparams()
                    {
                        MotorTypeId=d.MotorTypeName,
                        PhysicId=d.DataPhysicalId,
                        Param=d.FieldParamEn,
                        Description=d.FieldParam,
                        Time=DateTime.Now

                    };
                    var exist = Motorparams.Find(new string[] { "Param", "MotorTypeId" }, new object[] { d.FieldParamEn , d.MotorTypeName });
                    if (exist != null)
                        return;
                    entity.Save();
                });
           
        
        }

        public static void Motors()
        {
            var motors = UnitoonModels.Motor.FindAll().Where(e=>e.ProductionLineId==23)?.ToList();
            var entities = new List<Motor>();
            if (motors != null && motors.Any())
                motors.ToList().ForEach(m =>
                {
                    var embedeviceIndex =  m.EmbeddedDeviceId == 50?"010203040546": "010203040538";
                    var embedevice = Collectdevice.Find("Index", embedeviceIndex);
                    var oldMotorType = UnitoonModels.MotorType.Find("Id", (m?.MotorTypeId ?? 0));
                    var motorType = Motortype.Find("MotorTypeName", oldMotorType?.MachineType ?? "");
                    float standValue = 0;
                    if (motorType == null)
                        return;
                    switch (motorType.MotorTypeId)
                    {
                        case "MF":
                            standValue =Convert.ToSingle(UnitoonModels.StandParamValues.FindAll(new string[] { "MotorId" , "Parameter" },new object[] { m.Id, "Frequency" }).FirstOrDefault()?.Value??0);
                            break;

                        case "CY":
                            standValue = Convert.ToSingle(UnitoonModels.StandParamValues.FindAll(new string[] { "MotorId", "Parameter" }, new object[] { m.Id, "InstantWeight" }).FirstOrDefault()?.Value ?? 0);
                            break;
                        default:
                            standValue = Convert.ToSingle(UnitoonModels.StandParamValues.FindAll(new string[] { "MotorId", "Parameter" }, new object[] { m.Id, "Current" }).FirstOrDefault()?.Value ?? 0);
                            break;
                    }              
                    var entity = new Motor()
                    {
                       ProductionLineId= "JXD001",
                       Time =TimeSpan(DateTime.Now),
                       EmbeddedDeviceId= embedevice?.ID??0,
                       FeedSize=Convert.ToSingle(m.FeedSize),
                       FinalSize= Convert.ToSingle(m.FinalSize),
                       MotorId = "JXD001" + "-" + motorType.MotorTypeId + m.Id.ToString() ?? "UnKnown",
                       MotorPower = Convert.ToSingle(m.MotorPower),
                       MotorTypeId= motorType.MotorTypeId,
                       Name=m.Name,
                       ProductSpecification="",
                       SerialNumber=m.SerialNumber,
                       StandValue= standValue,
                       IsBeltWeight=m.IsBeltWeight?1:0,
                       IsMainBeltWeight=m.IsMainBeltWeight ? 1 : 0,
                       OffSet=m.OffSet,
                       Slope=m.Slope,
                       UseCalc=m.UseCalc?1:0,
                    };
                    var exist = Motor.Find(new string[] { "MotorId"}, new object[] { entity.MotorId });
                    if (exist != null)
                        return;
                    entity.Save();
                });


        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int TimeSpan(DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                return 0;
            }
            else
            {
                //  DateTime startTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local);//等价的建议写法
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (int)(time - startTime).TotalSeconds;
            }
        }

        #endregion
    }
}