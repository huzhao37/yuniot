using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.MQ;
using Yunt.XmlProtocol.Domain.MiddleMap;
using Yunt.XmlProtocol.Domain.Models;

namespace Yunt.IDC.Helper
{
    public class BytesToDb
    {
        private static DataGramModel _model;
        private readonly static RabbitMqHelper rabbitHelper;
        private readonly static string queueHost;
        private readonly static int queuePort;
        //private readonly static string queueName;
        private readonly static string queueUserName;
        private readonly static string queuePassword;
        private readonly static string ccuri;
        private readonly static string queue;
        private static string _buffer;
        private static List<int> _normalIds;
        private static IMotorRepository motorRepository;
        static BytesToDb()
        {
            var wddQueue =
                 Messagequeue.FindAll()
                     .Where(e => e.WriteRead.Equals(WriteOrRead.Read) && !e.RouteKey.EqualIgnoreCase("STATUS")).FirstOrDefault();
            if (rabbitHelper == null)
                rabbitHelper = new RabbitMqHelper();
            if (queueHost == null) queueHost = wddQueue.Host;
            if (queuePort == 0) queuePort = wddQueue.Port;
            if (queueUserName == null) queueUserName = wddQueue.Username;
            if (queuePassword == null) queuePassword = wddQueue.Pwd;

            if (ccuri == null) ccuri = "amqp://" + queueHost + ":" + queuePort;
            if (queue == null) queue = "OriginalBytes"; //sumin
            motorRepository = ServiceProviderServiceExtensions.GetService<IMotorRepository>(Program.Providers["Device"]);
        }

        public static bool Saving(DataGramModel model, string buffer)
        {
        
            _model = model;
            _buffer = buffer;
            try
            {
                if (_model == null)
                {
                    Logger.Info("[BytesToDb]Model Parser is NULL!");
                    return false;
                }

                var emDevice = Collectdevice.Find("Index",_model.DeviceId);
                if (emDevice == null)
                {
                    Logger.Info("[BytesToDb]No Related EmbeddedDevice!");
                    return false;
                }
                
                //var bytes = new OriginalBytes()
                //{
                //    Time = _model.PValues.FirstOrDefault().Key, // DateTime.Now,
                //    Bytes = _buffer,
                //    ProductionLineId = productionLine.Id,
                //    EmbeddedDeviceId = emDevice.Id
                //};
                //Container.oBytesContainer.InsertSqlAsync(bytes);
                //写入队列中缓冲
                rabbitHelper.Write(ccuri, Extention.strToToHexByte(buffer), queue, queueUserName, queuePassword);

            
                var motors = motorRepository.GetEntities(e => e.EmbeddedDeviceId == emDevice.ID&& e.Id != 0 && e.ProductionLineId.EqualIgnoreCase(emDevice.ProductionlineID));
           
                if (motors == null || !motors.Any())
                {
                    Logger.Info("[BytesToDb]No Related Motors!");
                }
                if (motors != null && motors.Any())
                {
                    foreach (var motor in motors)
                    {
                        //包含为设置显示的电机设备---2017-3-17
                        //bool isDisplay = motor.IsDisplay;
                        //if (!isDisplay)
                        //    continue;

                        #region switch case motortype

                        switch (motor.MotorTypeId)
                        {
                            case "MF":

                                {
                                    //给料机
                                    GetMeterialFeederDataEntity(motor.MotorId);
                                    break;
                                }
                            case "JC":
                                {
                                    //粗鄂破
                                    GetJawCrusherDataEntity(motor.MotorId);
                                    break;
                                }
                            case 3:
                                {
                                    //单杠圆锥破碎机
                                    GetConeCrusherDataEntity(motor.MotorId);
                                    break;
                                }
                            case 4:
                                {
                                    //立轴破
                                    GetVerticalCrusherDataEntity(motor.MotorId);
                                    break;
                                }
                            case 6:
                                {
                                    //振动筛
                                    GetVibrosieveDataEntity(motor.MotorId);
                                    break;
                                }
                            case 7:
                                {
                                    //皮带机
                                    GetConveyorDataEntity(motor.MotorId);
                                    break;
                                }
                            case 10:
                                {
                                    //西蒙斯圆锥破碎机
                                    GetSimonsDataEntity(motor.MotorId);
                                    break;
                                }
                            case 11:
                                {
                                    //磨粉机
                                    GetPulverizerDataEntity(motor.MotorId);
                                    break;
                                }
                            case 14:
                                {
                                    //反击破
                                    GetImpactCrusherDataEntity(motor.MotorId);
                                    break;
                                }
                            case 22:
                                {
                                    //PLC
                                    //GetPLCDataEntity(motor.Id);
                                    break;
                                }
                            case 26:
                                {
                                    //可逆锤破碎机
                                    GetReverHammerCrusherDataEntity(motor.MotorId);
                                    break;
                                }

                            case 27:
                                {
                                    //双齿辊破碎机
                                    GetDoubleToothRollCrusherDataEntity(motor.MotorId);
                                    break;
                                }
                            case 28:
                                {
                                    //中间料仓
                                    GetMidSiloDataEntity(motor.MotorId);
                                    break;
                                }
                            default:
                                break;
                        }

                        #endregion
                    }
                }
                //更新绑定产线的最新GPRS通信时间;   
                var current = _model.PValues.First().Key;
                Container.lineContainer.UpdateProductionLineLatestDataTime(productionLine.Id, current);


                return true;
            }
            catch (Exception ex)
            {
                Container.Error("[BytesToDb]" + ex.Message);
                return false;
            }
        }



        #region Motor Data
        /// <summary>
        /// 中间料仓
        /// </summary>
        /// <param name="id"></param>
        private static void GetMidSiloDataEntity(int id)
        {
            //todo
        }
        /// <summary>
        /// 给料机
        /// </summary>
        /// <param name="id"></param>
        private static void GetMeterialFeederDataEntity(string id)
        {
            //var alarmArray = new List<AlarmRealRecord>();
            var mfArray = new List<MaterialFeeder>();
            var mParms = Dataformmodel.FindAll(new string[]{ "MotorId" },new object[]{id});
           


            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key.TimeSpan();
                var values = Pvalue.Value;
                for (var i = 0; i < mParms.Count(); i++)
                {
                    var form = mParms[i];
                    form.Value = Normalize.ConvertToNormal(form.DataPhysicalAccuracy, form.FieldParam, values[form.Index], motor, mParms, values);
                    form.SaveAsync();//更新实时数据
                }

                var mf = new MaterialFeeder() { MotorId = id, Time = time };
                var mfType = mf.GetType();

                #region Normal

                IOrderedEnumerable<MotorParamters> sccParms = null;

                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index);
                if (sccParms != null && sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = mfType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;
                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(mf, tempDouble);
                                break;
                            default:
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[sccparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(mf, tempDouble);
                                break;
                        }
                    }
                }

                #endregion   

                #region Warning
                //if (mf.Frequency > 0)
                //{
                //    var mfWarningParms = mParms.Where(c => c.AlarmTypeId > 0).OrderBy(c => c.Index);
                //    alarmArray.AddRange(from mfWarningparm in mfWarningParms
                //                        where values[mfWarningparm.Index - 1] > 0
                //                        //where mfWarningparm.AlarmTypeId > 0
                //                        select new AlarmRealRecord()
                //                        {
                //                            AlarmTypeId = (int)mfWarningparm.AlarmTypeId,
                //                            IsDeleted = false,
                //                            Time = time,
                //                            MotorId = id
                //                        });
                //}


                #endregion

                mfArray.Add(mf);

            }

            // Bulk Insert
            Container.mfContainer.BulkInsertRedis(mfArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 单杠圆锥破碎机
        /// </summary>
        /// <param name="id"></param>
        private static void GetConeCrusherDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var coneArray = new List<ConeCrusher>();

            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var cone = new ConeCrusher() { MotorId = id, Time = time };

                var coneType = cone.GetType();

                #region Normal

                var coneParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    coneParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (coneParms.Any())
                {
                    foreach (var coneparm in coneParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)coneparm.CabinetParamterId).Param;
                        var info = coneType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = coneparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int des;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[coneparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[coneparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(cone, tempDouble);
                                break;
                            case "TankTemperature":
                                des = Extention.TempTemperatureTranster(values[coneparm.Index - 1]);
                                tempDouble = (values[coneparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(cone, tempDouble);
                                break;
                            case "OilFeedTempreature":
                                des = Extention.TempTemperatureTranster(values[coneparm.Index - 1]);
                                tempDouble = (values[coneparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(cone, tempDouble);
                                break;
                            case "OilReturnTempreature":
                                des = Extention.TempTemperatureTranster(values[coneparm.Index - 1]);
                                tempDouble = (values[coneparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(cone, tempDouble);
                                break;

                            default:
                                tempDouble = (values[coneparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[coneparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(cone, tempDouble);
                                break;
                        }
                    }
                }

                #endregion

                #region Warning

                if (cone.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId > 0
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                #endregion

                coneArray.Add(cone);
            }

            Container.ccContainer.BulkInsertRedis(coneArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 磨粉机
        /// </summary>
        /// <param name="id"></param>
        private static void GetPulverizerDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var pulArray = new List<Pulverizer>();

            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var pul = new Pulverizer() { MotorId = id, Time = time };

                var pulType = pul.GetType();


                var pulParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    pulParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (pulParms.Any())
                {
                    foreach (var pulparm in pulParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)pulparm.CabinetParamterId).Param;
                        var info = pulType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = pulparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[pulparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(pul, tempDouble);
                                break;
                            default:
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(pul, tempDouble);
                                break;
                        }
                    }
                }
                if (pul.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId != null
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                pulArray.Add(pul);
            }
            Container.pulContainer.BulkInsertRedis(pulArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 西蒙斯圆锥破碎机
        /// </summary>
        /// <param name="id"></param>
        private static void GetSimonsDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var sccArray = new List<SimonsConeCrusher>();

            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var scc = new SimonsConeCrusher() { MotorId = id, Time = time };

                var sccType = scc.GetType();
                var sccParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = sccType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int des;
                        switch (propertyName)
                        {
                            case "TankTemperature":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(scc, tempDouble);
                                break;
                            case "OilFeedTempreature":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(scc, tempDouble);
                                break;
                            case "OilReturnTempreature":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(scc, tempDouble);
                                break;
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(scc, tempDouble);
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (scc.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId != null
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                sccArray.Add(scc);
            }
            // Bulk Insert
            Container.sccContainer.BulkInsertRedis(sccArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 立轴破
        /// 数据精度仍然是硬编码，不知道具体精度是多少到目前。@2015-11-06
        /// </summary>
        /// <param name="id"></param>
        private static void GetVerticalCrusherDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var vcArray = new List<VerticalCrusher>();

            IEnumerable<MotorParamters> mParms = null;
            try
            {
                mParms = Container.mParmContainer.GetParmsByMotor(id);
            }
            catch (Exception ex)
            {
                Container.Error(ex.Message);
            }

            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var vc = new VerticalCrusher() { MotorId = id, Time = time };

                var vcType = vc.GetType();

                #region Normal

                var vcParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    try
                    {
                        vcParms =
                            mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                                .OrderBy(c => c.Index)
                                .ToList();
                    }
                    catch (Exception ex)
                    {
                        Container.Error(ex.Message);
                    }

                if (vcParms.Any())
                {
                    foreach (var pulparm in vcParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)pulparm.CabinetParamterId).Param;
                        var info = vcType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = pulparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int des;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[pulparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "Voltage":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "PowerFactor":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "ReactivePower":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "TotalPower":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "Current2":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "LubricatingOilPressure":
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[pulparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "OilReturnTempreature":
                                des = Extention.TempTemperatureTranster(values[pulparm.Index - 1]);
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "TankTemperature":
                                des = Extention.TempTemperatureTranster(values[pulparm.Index - 1]);
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            case "BearingTempreature":
                                des = Extention.TempTemperatureTranster(values[pulparm.Index - 1]);
                                tempDouble = (values[pulparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(vc, tempDouble);
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region Warning

                if (vc.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId != null
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                #endregion

                vcArray.Add(vc);
            }
            //  Bulk Insert
            Container.vcContainer.BulkInsertRedis(vcArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 反击破碎机
        /// </summary>
        /// <param name="id"></param>
        private static void GetImpactCrusherDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var icArray = new List<ImpactCrusher>();

            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var ic = new ImpactCrusher() { MotorId = id, Time = time };

                var icType = ic.GetType();

                #region Normal

                var sccParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = icType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int des;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(ic, tempDouble);
                                break;
                            case "Current2":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(ic, tempDouble);
                                break;
                            case "SpindleTemperature1":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(ic, tempDouble);
                                break;
                            case "SpindleTemperature2":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(ic, tempDouble);
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region Warning

                if (ic.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId != null
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                #endregion

                icArray.Add(ic);
            }
            // Bulk Insert
            //Container.icContainer.BulkInsertSql(icArray);
            Container.icContainer.BulkInsertRedis(icArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 皮带机
        /// </summary>
        /// <param name="id"></param>
        private static void GetConveyorDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var cyArray = new List<Conveyor>();
            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var cy = new Conveyor() { MotorId = id, Time = time };

                var cyType = cy.GetType();

                #region Normal

                var cyParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    cyParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (cyParms.Any())
                {
                    foreach (var cyparm in cyParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)cyparm.CabinetParamterId).Param;
                        var info = cyType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = cyparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int tempInt;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[cyparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[cyparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id),
                                        2);
                                info.SetValue(cy, tempDouble);
                                break;
                            case "Unit":
                                tempInt = (values[cyparm.Index - 1] == -1) ? -1 : (int)values[cyparm.Index - 1];
                                int value = tempInt;
                                if (tempInt != -1)
                                    value = tempInt & 7;
                                info.SetValue(cy, value);
                                break;
                            case "BootFlagBit":
                                tempInt = (values[cyparm.Index - 1] == -1) ? -1 : (int)values[cyparm.Index - 1];
                                info.SetValue(cy, tempInt);
                                break;
                            case "ZeroCalibration":
                                tempInt = (values[cyparm.Index - 1] == -1) ? -1 : (int)values[cyparm.Index - 1];
                                info.SetValue(cy, tempInt);
                                break;
                            default:
                                tempDouble = (values[cyparm.Index - 1] == -1) ? -1 : (int)values[cyparm.Index - 1];
                                info.SetValue(cy, tempDouble);
                                break;
                        }
                    }
                }

                //称重单位换算;
                ConveyorWeightConvert(ref cy, cy.Unit);

                #endregion


                #region Warning
                if (cy.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId != null
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                #endregion

                cyArray.Add(cy);
            }
            //Bulk Insert
            //Container.cyContainer.BulkInsertSql(cyArray);
            Container.cyContainer.BulkInsertRedis(cyArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 振动筛
        /// </summary>
        /// <param name="id"></param>
        private static void GetVibrosieveDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var vbArray = new List<Vibrosieve>();
            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;
                var vb = new Vibrosieve() { MotorId = id, Time = time };
                var vbType = vb.GetType();

                #region Normal

                var sccParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = vbType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)

                            DataAccuracy = 1;
                        var propertyName = info.Name;
                        double tempDouble;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(vb, tempDouble);
                                break;

                            default:
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[sccparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(vb, tempDouble);
                                break;
                        }
                    }
                }

                #endregion

                #region Warning
                if (vb.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                                        where values[ccWarningparm.Index - 1] > 0
                                        where ccWarningparm.AlarmTypeId != null
                                        select new AlarmRealRecord()
                                        {
                                            AlarmTypeId = (int)ccWarningparm.AlarmTypeId,
                                            IsDeleted = false,
                                            Time = time,
                                            MotorId = id
                                        });
                }

                #endregion

                vbArray.Add(vb);
            }
            //Bulk Insert
            //Container.vbContainer.BulkInsertSql(vbArray);
            Container.vbContainer.BulkInsertRedis(vbArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 粗鄂破
        /// </summary>
        /// <param name="id"></param>
        private static void GetJawCrusherDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var jcArray = new List<JawCrusher>();
            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;
                var jc = new JawCrusher() { MotorId = id, Time = time };
                var jcType = jc.GetType();

                #region Normal

                var sccParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();


                if (sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = jcType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int des;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(jc, tempDouble);
                                break;
                            case "RackSpindleTemperature1":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(jc, tempDouble);
                                break;
                            case "RackSpindleTemperature2":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(jc, tempDouble);
                                break;
                            case "MotiveSpindleTemperature1":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(jc, tempDouble);
                                break;
                            case "MotiveSpindleTemperature2":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(jc, tempDouble);
                                break;
                            default:
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[sccparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(jc, tempDouble);
                                break;
                        }
                    }
                }

                #endregion

                #region Warning

                if (jc.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                        where values[ccWarningparm.Index - 1] > 0
                        where ccWarningparm.AlarmTypeId != null
                        select new AlarmRealRecord()
                        {
                            AlarmTypeId = (int) ccWarningparm.AlarmTypeId, IsDeleted = false, Time = time, MotorId = id
                        });
                }

                #endregion

                jcArray.Add(jc);
            }
            //  Bulk Insert
            //Container.jcContainer.BulkInsertSql(jcArray);
            Container.jcContainer.BulkInsertRedis(jcArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 可逆锤破
        /// </summary>
        /// <param name="id"></param>
        private static void GetReverHammerCrusherDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var rhcArray = new List<ReverHammerCrusher>();

            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var rhc = new ReverHammerCrusher() { MotorId = id, Time = time };

                var rhcType = rhc.GetType();

                #region Normal

                var sccParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = rhcType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        int des;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(rhc, tempDouble);
                                break;
                            case "SpindleTemperature1":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(rhc, tempDouble);
                                break;
                            case "SpindleTemperature2":
                                des = Extention.TempTemperatureTranster(values[sccparm.Index - 1]);
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)des * DataAccuracy, 2);
                                info.SetValue(rhc, tempDouble);
                                break;
                            case "BearingSpeed":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round((double)values[sccparm.Index - 1] * DataAccuracy, 2);
                                info.SetValue(rhc, tempDouble);
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region Warning
                if (rhc.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                        where values[ccWarningparm.Index - 1] > 0
                        where ccWarningparm.AlarmTypeId != null
                        select new AlarmRealRecord()
                        {
                            AlarmTypeId = (int) ccWarningparm.AlarmTypeId, IsDeleted = false, Time = time, MotorId = id
                        });
                }

                #endregion

                rhcArray.Add(rhc);
            }
            //Bulk Insert
            //Container.rhcContainer.BulkInsertSql(rhcArray);
            Container.rhcContainer.BulkInsertRedis(rhcArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        /// <summary>
        /// 双齿辊破碎机
        /// </summary>
        /// <param name="id"></param>
        private static void GetDoubleToothRollCrusherDataEntity(int id)
        {
            var alarmArray = new List<AlarmRealRecord>();
            var dtrArray = new List<DoubleToothRollCrusher>();

            var mParms = Container.mParmContainer.GetParmsByMotor(id);
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;

                var dtr = new DoubleToothRollCrusher() { MotorId = id, Time = time };

                var dtrType = dtr.GetType();

                #region Normal

                var sccParms = new List<MotorParamters>();
                if (mParms != null && mParms.Any())
                    sccParms =
                        mParms.Where(c => _normalIds.Contains((int)c.CabinetParamterId))
                            .OrderBy(c => c.Index)
                            .ToList();
                if (sccParms.Any())
                {
                    foreach (var sccparm in sccParms)
                    {
                        var param = Container.cabinetParamterContainer.GetEntityById((int)sccparm.CabinetParamterId).Param;
                        var info = dtrType.GetProperty(param);
                        if (info == null || info.Name == "")
                            continue;

                        //数据精度 exception: 0剔除;
                        var DataAccuracy = sccparm.DataAccruacy;
                        if (DataAccuracy == 0)
                            DataAccuracy = 1;

                        var propertyName = info.Name;
                        double tempDouble;
                        switch (propertyName)
                        {
                            case "Current":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(dtr, tempDouble);
                                break;
                            case "Current2":
                                tempDouble = (values[sccparm.Index - 1] == -1)
                                    ? -1
                                    : Math.Round(
                                        (double)values[sccparm.Index - 1] * DataAccuracy * GetCurrentCTFactor(id), 2);
                                info.SetValue(dtr, tempDouble);
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region Warning

                if (dtr.Current > 0)
                {
                    var ccWarningParms =
                        mParms.Where(c => c.AlarmTypeId > 0)
                            .OrderBy(c => c.Index);
                    alarmArray.AddRange(from ccWarningparm in ccWarningParms
                        where values[ccWarningparm.Index - 1] > 0
                        where ccWarningparm.AlarmTypeId != null
                        select new AlarmRealRecord()
                        {
                            AlarmTypeId = (int) ccWarningparm.AlarmTypeId, IsDeleted = false, Time = time, MotorId = id
                        });
                }

                #endregion

                dtrArray.Add(dtr);
            }
            //  Bulk Insert
            //Container.dtrContainer.BulkInsertSql(dtrArray);
            Container.dtrContainer.BulkInsertRedis(dtrArray);
            Container.aRealRecordContainer.BulkInsertRedis(alarmArray);
        }

        #endregion

        #region private methods



        /// <summary>
        /// 根据单位计算皮带机瞬时称重、累计称重
        /// </summary>
        /// <param name="cy"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        private static bool ConveyorWeightConvert(ref Conveyor cy, int unit)
        {
            try
            {
                switch (unit)
                {
                    case 0:
                        cy.InstantWeight = (cy.InstantWeight == -1) ? -1 : Math.Round(cy.InstantWeight / 3600, 2);
                        break;
                    case 1:
                        cy.AccumulativeWeight = (cy.AccumulativeWeight == -1)
                            ? -1
                            : Math.Round(cy.AccumulativeWeight / 1000, 2);
                        if (cy.AccumulativeWeight < -1)
                        {
                            //4294967295
                            cy.AccumulativeWeight = Math.Round((4294967295 + cy.AccumulativeWeight * 1000) / 1000, 2);
                        }
                        cy.InstantWeight = (cy.InstantWeight == -1) ? -1 : Math.Round(cy.InstantWeight / 3.6, 2);
                        break;
                    case 2:
                        cy.InstantWeight = (cy.InstantWeight == -1) ? -1 : Math.Round(cy.InstantWeight / 1000, 2);
                        break;
                    case 3:
                        cy.AccumulativeWeight = (cy.AccumulativeWeight == -1)
                            ? -1
                            : Math.Round(cy.AccumulativeWeight / 1000, 2);
                        if (cy.AccumulativeWeight < -1)
                        {
                            //4294967295
                            cy.AccumulativeWeight = Math.Round((4294967295 + cy.AccumulativeWeight * 1000) / 1000, 2);
                        }
                        break;
                    case 4:
                        cy.InstantWeight = (cy.InstantWeight == -1) ? -1 : Math.Round(cy.InstantWeight * 60, 2);
                        break;
                    case 5:
                        cy.AccumulativeWeight = (cy.AccumulativeWeight == -1)
                            ? -1
                            : Math.Round(cy.AccumulativeWeight / 1000, 2);
                        if (cy.AccumulativeWeight < -1)
                        {
                            //4294967295
                            cy.AccumulativeWeight = Math.Round((4294967295 + cy.AccumulativeWeight * 1000) / 1000, 2);
                        }
                        cy.InstantWeight = (cy.InstantWeight == -1) ? -1 : Math.Round(cy.InstantWeight * 0.06, 2);
                        break;
                    case 6:
                        break;
                    case -1:
                        cy.AccumulativeWeight = -1;
                        cy.InstantWeight = -1;
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取设备电流CT系数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static double GetCurrentCTFactor(int id)
        {
            var ct = Container.mCtContainer.GetCTValueById(id);
            return ct;
        }

        #endregion

        #region 自动装卸载

        /// <summary>
        /// 处理自动装卸sim卡数据
        /// </summary>
        /// <param name="id">sim卡ID</param>
        private static void GetAutoDumpDataEntity(int id)
        {
            var adArray = new List<AutoDump>();
            var parm = Container.mParmContainer.GetEntities(m => m.CardId==id).FirstOrDefault();
            foreach (var Pvalue in _model.PValues)
            {
                var time = Pvalue.Key;
                var values = Pvalue.Value;
                var ad = new AutoDump() { SimCardId = id, Time = time };
                var adType = ad.GetType();
                var param = Container.cabinetParamterContainer.GetEntityById((int)parm.CabinetParamterId).Param;
                var info = adType.GetProperty(param);
                if (info == null || info.Name == "")
                    continue;

                var propertyName = info.Name;
                double countDouble;
                switch (propertyName)
                {
                    case "Count":
                        countDouble = (values[parm.Index - 1] == -1)
                            ? -1
                            : Math.Round((double)values[parm.Index - 1], 2);
                        info.SetValue(ad, (int)countDouble);
                        break;
                    default:
                        countDouble = (values[parm.Index - 1] == -1)
                            ? -1
                            : Math.Round((double)values[parm.Index - 1], 2);
                        info.SetValue(ad, (int)countDouble);
                        break;
                }

                adArray.Add(ad);
            }
            //Bulk Insert
            Container.autoDumpContainer.BulkInsert(adArray);
        }

        #endregion
    }
}
