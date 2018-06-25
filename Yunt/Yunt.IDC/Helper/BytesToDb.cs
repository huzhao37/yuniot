using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Device.Domain.BaseModel;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Services;
using Yunt.IDC.Task;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.MiddleMap;
using Yunt.Xml.Domain.Model;

namespace Yunt.IDC.Helper
{
    public class BytesToDb
    {
        #region ctor & fields
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
        private static IMaterialFeederRepository mfRepository;
        private static IConveyorRepository cyRepository;
        private static IConeCrusherRepository ccRepository;
        private static IJawCrusherRepository jcRepository;
        private static IReverHammerCrusherRepository rhcRepository;
        private static IDoubleToothRollCrusherRepository dtrRepository;
        private static IVibrosieveRepository vibRepository;
        private static IPulverizerRepository pulRepository;
        private static IVerticalCrusherRepository vcRepository;
        private static IImpactCrusherRepository icRepository;
        private static ISimonsConeCrusherRepository sccRepository;
        private static IHVibRepository hvibRepository;
        private static IProductionLineRepository lineRepository;
        private static IOriginalBytesRepository bytesRepository;
        private static readonly ICollectdeviceRepository CollectdeviceRepository;
        public static readonly IDataformmodelRepository DataformmodelRepository;
        public static readonly IMotorEventLogRepository MotorEventLogRepository;
        public static readonly IAlarmInfoRepository alarmInfoRepository;
        
        static BytesToDb()
        {
            var wddQueue = MqDealTask.WddQueue;
            if (rabbitHelper == null)
                rabbitHelper = new RabbitMqHelper();
            if (queueHost == null) queueHost = wddQueue.Host;
            if (queuePort == 0) queuePort = wddQueue.Port;
            if (queueUserName == null) queueUserName = wddQueue.Username;
            if (queuePassword == null) queuePassword = wddQueue.Pwd;

            if (ccuri == null) ccuri = "amqp://" + queueHost + ":" + queuePort;
            if (queue == null) queue = "Wdd_OriginalBytes"; //sumin
            motorRepository = ServiceProviderServiceExtensions.GetService<IMotorRepository>(Program.Providers["Device"]);
            mfRepository = ServiceProviderServiceExtensions.GetService<IMaterialFeederRepository>(Program.Providers["Device"]);
            cyRepository = ServiceProviderServiceExtensions.GetService<IConveyorRepository>(Program.Providers["Device"]);
            ccRepository = ServiceProviderServiceExtensions.GetService<IConeCrusherRepository>(Program.Providers["Device"]);
            jcRepository = ServiceProviderServiceExtensions.GetService<IJawCrusherRepository>(Program.Providers["Device"]);
            rhcRepository = ServiceProviderServiceExtensions.GetService<IReverHammerCrusherRepository>(Program.Providers["Device"]);
            dtrRepository = ServiceProviderServiceExtensions.GetService<IDoubleToothRollCrusherRepository>(Program.Providers["Device"]);

            vibRepository = ServiceProviderServiceExtensions.GetService<IVibrosieveRepository>(Program.Providers["Device"]);
            pulRepository = ServiceProviderServiceExtensions.GetService<IPulverizerRepository>(Program.Providers["Device"]);
            vcRepository = ServiceProviderServiceExtensions.GetService<IVerticalCrusherRepository>(Program.Providers["Device"]);
            icRepository = ServiceProviderServiceExtensions.GetService<IImpactCrusherRepository>(Program.Providers["Device"]);
            sccRepository = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherRepository>(Program.Providers["Device"]);
            hvibRepository = ServiceProviderServiceExtensions.GetService<IHVibRepository>(Program.Providers["Device"]);
            lineRepository = ServiceProviderServiceExtensions.GetService<IProductionLineRepository>(Program.Providers["Device"]);
            bytesRepository= ServiceProviderServiceExtensions.GetService<IOriginalBytesRepository>(Program.Providers["Device"]);
            CollectdeviceRepository = ServiceProviderServiceExtensions.GetService<ICollectdeviceRepository>(Program.Providers["Xml"]);
            DataformmodelRepository = ServiceProviderServiceExtensions.GetService<IDataformmodelRepository>(Program.Providers["Xml"]);
            MotorEventLogRepository= ServiceProviderServiceExtensions.GetService<IMotorEventLogRepository>(Program.Providers["Analysis"]);
            alarmInfoRepository = ServiceProviderServiceExtensions.GetService<IAlarmInfoRepository>(Program.Providers["Analysis"]);

        }
        #endregion
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

                var emDevice = CollectdeviceRepository.GetEntities(e => e.Index.Equals(_model.CollectdeviceIndex)).FirstOrDefault();
                if (emDevice == null)
                {
                    Logger.Info("[BytesToDb]No Related EmbeddedDevice!");
                    return false;
                }

                var bytes = new OriginalBytes()
                {
                    Time = _model.PValues.FirstOrDefault().Key.TimeSpan(),
                    Bytes = _buffer,
                    ProductionLineId = emDevice.Productionline_Id,
                    EmbeddedDeviceId = emDevice.Id
                };
                bytesRepository.InsertAsync(bytes);
               // 写入队列中缓冲
                rabbitHelper.Write(ccuri, Extention.strToToHexByte(buffer), queue, queue, "amq.topic", queueUserName, queuePassword);


                var motors = motorRepository.GetEntities(e => e.EmbeddedDeviceId == emDevice.Id
                && e.ProductionLineId.Equals(emDevice.Productionline_Id)).ToList();

                if (motors == null || !motors.Any())
                {
                    Logger.Info("[BytesToDb]No Related Motors!");
                }

              
                if (motors != null && motors.Any())
                {
                    foreach (var pvalue in _model.PValues)
                    {
                        foreach (var motor in motors)
                        {
                            #region switch case motortype

                            switch (motor.MotorTypeId)
                            {
                                case "MF":

                                    {
                                        //给料机
                                        var mf=new MaterialFeeder();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            mfRepository.InsertAsync(obj as MaterialFeeder);
                                        break;
                                    }
                                case "JC":
                                    {
                                        //粗鄂破
                                        var jc = new JawCrusher();
                                        var obj = MotorObj(pvalue, motor, jc);
                                        if (obj != null)
                                            jcRepository.InsertAsync(obj as JawCrusher);
                                        break;
                                    }
                                case "CC":
                                    {
                                        //单杠圆锥破碎机
                                        var mf = new ConeCrusher();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            ccRepository.InsertAsync(obj as ConeCrusher);
                                        break;
                                    }
                                case "VC":
                                    {
                                        //立轴破
                                        var mf = new VerticalCrusher();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            vcRepository.InsertAsync(obj as VerticalCrusher);
                                        break;
                                    }
                                case "VB":
                                    {
                                        //振动筛
                                        var mf = new Vibrosieve();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            vibRepository.InsertAsync(obj as Vibrosieve);
                                        break;
                                    }
                                case "CY":
                                    {
                                        //皮带机
                                        var mf = new Conveyor();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            cyRepository.InsertAsync(obj as Conveyor);
                                        break;
                                    }
                                case "SCC":
                                    {
                                        //西蒙斯圆锥破碎机
                                        var mf = new SimonsConeCrusher();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            sccRepository.InsertAsync(obj as SimonsConeCrusher);
                                        break;
                                    }
                                case "PUL":
                                    {
                                        //磨粉机
                                        var mf = new Pulverizer();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            pulRepository.InsertAsync(obj as Pulverizer);
                                        break;
                                    }
                                case "IC":
                                    {
                                        //反击破
                                        var mf = new ImpactCrusher();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            icRepository.InsertAsync(obj as ImpactCrusher);
                                        break;
                                    }
                                case "HVB":
                                    {
                                        var mf = new HVib();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            hvibRepository.InsertAsync(obj as HVib);
                                        break;
                                    }
                                case "RHC":
                                    {
                                        //可逆锤破碎机
                                        var mf = new ReverHammerCrusher();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            rhcRepository.InsertAsync(obj as ReverHammerCrusher);
                                        break;
                                    }

                                case "DTRC":
                                    {
                                        //双齿辊破碎机
                                        var mf = new DoubleToothRollCrusher();
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            dtrRepository.InsertAsync(obj as DoubleToothRollCrusher);
                                        break;
                                    }
                                case "MS":
                                    {
                                        break;
                                    }
                                default:
                                    break;
                            }

                            #endregion
                        }
                        //产线级别报警
                        LineAlarms(pvalue);
                    }
                
                }
                //更新绑定产线的最新GPRS通信时间;   
                var current = _model.PValues.First().Key;
                var line =
                    lineRepository.GetEntities(e => e.ProductionLineId.Equals(emDevice.Productionline_Id)).ToList()
                        .FirstOrDefault();
                if (line != null)
                    line.Time = current.TimeSpan();
                lineRepository.UpdateEntityAsync(line);
                lineRepository.Batch();//device
                DataformmodelRepository.Batch();//xml
                alarmInfoRepository.Batch();//analysis
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("[BytesToDb]" + ex.Message);
                return false;
            }
        }

        #region private methods
        private static dynamic MotorObj(KeyValuePair<DateTime, List<int>> pvalue, Motor motor, dynamic obj)
        {
            var time = pvalue.Key.TimeSpan();
            var values = pvalue.Value;
            var type = obj.GetType();

            var forms = DataformmodelRepository.GetEntities(e => e.MotorId.Equals(motor.MotorId)).ToList();
            if (forms==null||!forms.Any())
                return null;
            for (var i = 0; i < forms.Count(); i++)
            {
                var form = forms[i];
                form.Value = Normalize.ConvertToNormal(form, values);
                DataformmodelRepository.UpdateEntityAsync(form);//更新实时数据
                if (form.BitDesc.Equals("整型模拟量"))
                {
                    //添加AI分析记录
                    MotorEventLogRepository.AddAiLogAsync(new AiLog()
                    {
                        MotorId = motor.MotorId,
                        MotorName = motor.Name,
                        ProductionLineId = motor.ProductionLineId,
                        Param = form.FieldParam,
                        Value = (float)form.Value,
                        MotorTypeId = motor.MotorTypeId,
                        Time = time
                    });

                    if (string.IsNullOrWhiteSpace(form.FieldParamEn))
                        continue;
                    var info = type.GetProperty(form.FieldParamEn);
                    if (info == null || info?.Name == "")
                        continue;
                    info?.SetValue(obj, Convert.ToSingle(Math.Round((decimal)form.Value, 2)));//保留两位小数
                }
                //数字量存储redis-3个月
                if (form.BitDesc.Equals("数字量"))
                {
                  MotorEventLogRepository.AddDiLogAsync(new DiHistory()
                    {
                        MotorId = motor.MotorId,
                        MotorName = motor.Name,                     
                        Param = form.FieldParam,
                        Value = (float)form.Value,
                        MotorTypeId = motor.MotorTypeId,
                        Time = time
                    });

                    //add-alarminfo   motor级别
                    if (form.DataPhysicalId == 21&&form.Value==1)
                    {
                        alarmInfoRepository.InsertAsync(new AlarmInfo()
                        {
                            Content = form.FieldParam,
                            MotorName = form.MachineName,
                            MotorId = motor.MotorId,
                            Time = time
                        });
                    }
                }
               
            }
            var idInfo = type.GetProperty("MotorId");
            idInfo.SetValue(obj, motor.MotorId);
            var timeInfo = type.GetProperty("Time");
            timeInfo.SetValue(obj, time);

            return obj;
        }

        private static void LineAlarms(KeyValuePair<DateTime, List<int>> pvalue)
        {
            var time = pvalue.Key.TimeSpan();
            var values = pvalue.Value;
            var forms = DataformmodelRepository.GetEntities(e => e.MotorId.IsNullOrWhiteSpace()||e.MotorId=="0")?.ToList();
            if (forms==null||!forms.Any())
                return;
            for (var i = 0; i < forms.Count(); i++)
            {
                var form = forms[i];
                form.Value = Normalize.ConvertToNormal(form, values);
                DataformmodelRepository.UpdateEntityAsync(form);//更新实时数据
                //数字量存储redis-3个月
                if (form.BitDesc.Equals("数字量"))
                {               
                    //add-alarminfo   line级别
                    if (form.DataPhysicalId == 21 && form.Value == 1)
                    {
                        alarmInfoRepository.InsertAsync(new AlarmInfo()
                        {
                            Content = form.FieldParam,
                            MotorName = form.MachineName,
                            Time = time
                        });
                    }
                }
            }
        }
        #endregion

    }
}
