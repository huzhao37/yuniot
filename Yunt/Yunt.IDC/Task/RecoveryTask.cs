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
using Yunt.IDC.Helper;
using Yunt.IDC.Task;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.MiddleMap;
using Yunt.Xml.Domain.Model;
using Yunt.Xml.Domain.Services;

namespace Yunt.IDC.Task
{
    public class RecoveryTask
    {
        #region ctor & fields
        private static IMotorRepository motorRepository;
        private static IOriginalBytesRepository originalBytesRepository;
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
        private static IOriginalBytesRepository bytesRepository;
        public static readonly IDataformmodelRepository DataformmodelRepository;
        private static readonly IBytesParseRepository BytesParseRepository;
        private static List<Motor> _motors;
        static RecoveryTask()
        {
            originalBytesRepository = ServiceProviderServiceExtensions.GetService<IOriginalBytesRepository>(Program.Providers["Device"]);
            BytesParseRepository = ServiceProviderServiceExtensions.GetService<IBytesParseRepository>(Program.Providers["Xml"]);
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
            bytesRepository = ServiceProviderServiceExtensions.GetService<IOriginalBytesRepository>(Program.Providers["Device"]);

            DataformmodelRepository = ServiceProviderServiceExtensions.GetService<IDataformmodelRepository>(Program.Providers["Xml"]);

            _motors = motorRepository.GetEntities(e => e.ProductionLineId.Equals("WDD-P001") && e.MotorTypeId.Equals("CY") &&
                       (//e.Name.Equals("J38(5~20mm)") || e.Name.Equals("J5(40~80mm)") || e.Name.Equals("J42(成品砂)")|| 
                        e.Name.Equals("皮带机A1") || e.Name.Equals("皮带机C1") //|| e.Name.Equals("皮带机J22") || e.Name.Equals("皮带机J30") 
                       || e.Name.Equals("皮带机J4") || e.Name.Equals("皮带机J6")))?.ToList();

        }
        #endregion


        #region recovery
        public static void Start()
        {
            var datas = originalBytesRepository.GetEntities(e => e.Time >= 1529456400 && e.Time <= 1532055000 //7.1 0:00~7.20 10:50(1530374400,1532055000)
                          && e.ProductionLineId.Equals("WDD-P001"))?.OrderBy(e => e.Time)?.ToList();//6.20 9:00~6.30 23:59 (1529456400,1530374340)
            if (datas != null && datas.Any())
                datas.ForEach(d =>
                {
                    try
                    {
                        var bytes = Extention.strToToHexByte(d.Bytes);
                        var model = BytesParseRepository.Parser(bytes);
                        var result = Saving(model);
                        if (!result)
                        {
                            Logger.Error($"{d.Time.Time()}:数据报错！");
                            // Console.ReadKey();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{d.Time.Time()}数据报错:{ex.Message}！");
                        //Console.ReadKey();
                    }

                });
            Logger.Error($"6月份原始DB数据恢复完毕！");
        }
        private static bool Saving(DataGramModel model)
        {
            try
            {
                if (model == null)
                {
                    Logger.Info("[RecoveryTask]Model Parser is NULL!");
                    return false;
                }
                if (_motors == null || !_motors.Any())
                {
                    Logger.Info("[RecoveryTask]No Related Motors!");
                    return false;
                }


                if (_motors != null && _motors.Any())
                {
                    foreach (var pvalue in model.PValues)
                    {
                        var time = pvalue.Key.TimeSpan();
                        foreach (var motor in _motors)
                        {
                            #region switch case motortype

                            switch (motor.MotorTypeId)
                            {
                                case "MF":

                                    {
                                        //给料机
                                        var mf = mfRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (mf == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, mf);
                                        if (obj != null)
                                            mfRepository.UpdateAsync(obj as MaterialFeeder);
                                        break;
                                    }
                                case "JC":
                                    {
                                        //粗鄂破
                                        var jc = jcRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (jc == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, jc);
                                        if (obj != null)
                                            jcRepository.UpdateAsync(obj as JawCrusher);
                                        break;
                                    }
                                case "CC":
                                    {
                                        //单杠圆锥破碎机
                                        var cc = ccRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (cc == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, cc);
                                        if (obj != null)
                                            ccRepository.UpdateAsync(obj as ConeCrusher);
                                        break;
                                    }
                                case "VC":
                                    {
                                        //立轴破
                                        var vc = vcRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (vc == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, vc);
                                        if (obj != null)
                                            vcRepository.UpdateAsync(obj as VerticalCrusher);
                                        break;
                                    }
                                case "VB":
                                    {
                                        //振动筛
                                        var vib = vibRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (vib == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, vib);
                                        if (obj != null)
                                            vibRepository.UpdateAsync(obj as Vibrosieve);
                                        break;
                                    }
                                case "CY":
                                    {
                                        //皮带机
                                        //var cy2 = cyRepository.GetFromSqlDb(e => e.Time == time)?.FirstOrDefault();
                                        var cy = cyRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (cy == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, cy);
                                        if (obj != null)
                                            cyRepository.UpdateAsync(obj as Conveyor);
                                        break;
                                    }
                                case "SCC":
                                    {
                                        //西蒙斯圆锥破碎机
                                        var scc = sccRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (scc == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, scc);
                                        if (obj != null)
                                            sccRepository.UpdateAsync(obj as SimonsConeCrusher);
                                        break;
                                    }
                                case "PUL":
                                    {
                                        //磨粉机
                                        var pul = pulRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (pul == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, pul);
                                        if (obj != null)
                                            pulRepository.UpdateAsync(obj as Pulverizer);
                                        break;
                                    }
                                case "IC":
                                    {
                                        //反击破
                                        var ic = icRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (ic == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, ic);
                                        if (obj != null)
                                            icRepository.UpdateAsync(obj as ImpactCrusher);
                                        break;
                                    }
                                case "HVB":
                                    {
                                        var hvb = hvibRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (hvb == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, hvb);
                                        if (obj != null)
                                            hvibRepository.UpdateAsync(obj as HVib);
                                        break;
                                    }
                                case "RHC":
                                    {
                                        //可逆锤破碎机
                                        var rhc = rhcRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (rhc == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, rhc);
                                        if (obj != null)
                                            rhcRepository.UpdateAsync(obj as ReverHammerCrusher);
                                        break;
                                    }

                                case "DTRC":
                                    {
                                        //双齿辊破碎机
                                        var dtrc = dtrRepository.GetFromSqlDb(e => e.Time == time && e.MotorId.Equals(motor.MotorId))?.FirstOrDefault();
                                        if (dtrc == null)
                                            break;
                                        var obj = MotorObj(pvalue, motor, dtrc);
                                        if (obj != null)
                                            dtrRepository.UpdateAsync(obj as DoubleToothRollCrusher);
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
                    }

                }
                originalBytesRepository.Batch();//device
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("[RecoveryTask]" + ex.Message);
                return false;
            }
        }
        private static dynamic MotorObj(KeyValuePair<DateTime, List<int>> pvalue, Motor motor, dynamic obj)
        {
            var time = pvalue.Key.TimeSpan();
            var values = pvalue.Value;
            var type = obj.GetType();
            //电量，电流，电压数据恢复
            var forms = DataformmodelRepository.GetEntities(e => e.MotorId.Equals(motor.MotorId) &&
                        (e.DataPhysicalId == 17 || e.DataPhysicalId == 2 || e.DataPhysicalId == 3)).ToList();
            if (forms == null || !forms.Any())
                return null;
            for (var i = 0; i < forms.Count(); i++)
            {
                var form = forms[i];
                form.Value = OriginalNormalize.ConvertToNormal(form, values);
                if (form.BitDesc.Equals("整型模拟量"))
                {
                    if (string.IsNullOrWhiteSpace(form.FieldParamEn))
                        continue;
                    var info = type.GetProperty(form.FieldParamEn);
                    if (info == null || info?.Name == "")
                        continue;
                    info?.SetValue(obj, MathF.Round((float)form.Value, 2));//保留两位小数
                }

            }
            return obj;
        }
        #endregion

        #region private methods
        public static bool Update()
        {
            try
            {
                if (_motors == null || !_motors.Any())
                {
                    Logger.Info("[RecoveryTask]No Related Motors!");
                    return false;
                }

                if (_motors != null && _motors.Any())
                {
                    foreach (var motor in _motors)
                    {
                        //皮带机
                        var cys = cyRepository.GetFromSqlDb(e => e.Time >= 1529456400 && e.Time <= 1532055000 && e.MotorId.Equals(motor.MotorId))?.ToList();
                        if (cys == null||!cys.Any())
                            continue;
                        var updates = new List<Conveyor>();
                        cys.ForEach(cy => {
                            var update = cy;
                            update.ActivePower = 0f;
                            update.Current_A = 0f;
                            update.Current_B = 0f;
                            update.Current_C = 0f;
                            update.Voltage_A = 0f;
                            update.Voltage_B = 0f;
                            update.Voltage_C = 0f;
                            updates.Add(update);
                          
                        });
                        //test 请用完注释
                        cyRepository.UpdateEntityAsync(updates);
                        Logger.Warn($"{motor.Name}:更新{updates.Count}");
                    }

                }
                cyRepository.Batch();//device
                Logger.Warn($"更新完毕！");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("[RecoveryTask]" + ex.Message);
                return false;
            }
        }
        #endregion

    }
}
