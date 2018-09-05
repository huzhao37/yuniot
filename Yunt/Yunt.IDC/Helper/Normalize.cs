using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Device.Domain.Model;
using Yunt.Xml.Domain.Model;
using Yunt.Common;
using Yunt.Xml.Domain.IRepository;

namespace Yunt.IDC.Helper
{
    /// <summary>
    /// 常态化
    /// </summary>
    public class Normalize
    {
        private static readonly IDataformmodelRepository DataformmodelRepository;
        static Normalize()
        {
            if(DataformmodelRepository == null)
                DataformmodelRepository = BytesToDb.DataformmodelRepository;
        }
        /// <summary>
        /// 根据数据精度和数据参数将数值转化为实际值
        /// </summary>
        /// <param name="form">数据表单集合</param>
        /// <param name="values">数据值集合</param>
        /// <returns></returns>
        public static double ConvertToNormal(Dataformmodel form, List<int> values)
        {
            //if (string.IsNullOrWhiteSpace(form?.MotorId))
            //    return 0;
            if (form.Index >= values.Count)
            {
                Logger.Warn($"[Normalize]excite values index");
                return 0;
            }
            var oldValue = values[(int)form.Index];
            var accu = 1.0;
            accu = string.IsNullOrWhiteSpace(form.DataPhysicalAccuracy) ? 1 : Convert.ToDouble(form.DataPhysicalAccuracy);

            switch (form.DataPhysicalFeature)
            {
                case "温度":
                    var des = Extention.TempTemperatureTranster(oldValue);
                    return (oldValue == -1) ? -1
                        : Math.Round(des * accu, 2);
                case "电流":
                    return (oldValue == -1) ? -1 :
                    Math.Round(oldValue * accu, 2);
                case "配置":
                    if (form.FieldParamEn.Equals("WeightUnit"))
                    {
                        var tempInt = (oldValue == -1) ? -1 : (int)oldValue;
                        int value = tempInt;
                        if (tempInt != -1)
                            value = tempInt & 7;
                        return value;
                    }
                    break;
                case "称重":
                    var unitForm= DataformmodelRepository.GetEntities(e=>e.MotorId.Equals(form.MotorId)&&e.FieldParamEn.Equals("WeightUnit"))?.ToList().FirstOrDefault();
                    if (unitForm == null)
                    {
                        Logger.Error($"{form.MotorId}:not exist WeightUnit");
                        return 0;
                    }
                    unitForm.Value = ConvertToNormal(unitForm,values);
                    var originalValue = (oldValue == -1) ? -1 : Math.Round(oldValue * accu, 2);
                    return ConveyorWeightConvert(Convert.ToInt32(unitForm.Value), form.FieldParam, originalValue);
                case "瞬时称重":
                    var unitForm2 = DataformmodelRepository.GetEntities(e => e.MotorId.Equals(form.MotorId) && e.FieldParamEn.Equals("WeightUnit"))?.ToList().FirstOrDefault();
                    if (unitForm2 == null)
                    {
                        Logger.Error($"{form.MotorId}:not exist WeightUnit");
                        return 0;
                    }
                    unitForm2.Value = ConvertToNormal(unitForm2, values);
                    var originalValue2 = (oldValue == -1) ? -1 : Math.Round(oldValue * accu, 2);
                    return ConveyorWeightConvert(Convert.ToInt32(unitForm2.Value), form.FieldParam, originalValue2);
            }
            return (oldValue == -1) ? -1
              : Math.Round(oldValue * accu, 2);
        }



        /// <summary>
        /// 根据单位计算皮带机瞬时称重、累计称重
        /// </summary>
        /// <param name="unit">单位</param>
        /// <param name="param">称重参数</param>
        /// <param name="oldValue">称重原始值</param>
        /// <returns></returns>
        private static double ConveyorWeightConvert(int unit, string param, double oldValue)
        {
            try
            {
                switch (unit)
                {
                    case 0:
                        if (param.Equals("瞬时产量"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue / 3600, 2);
                        break;
                    case 1:
                        if (param.Equals("累计产量"))
                        {
                            oldValue = (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                            if (oldValue < -1)
                                //4294967295
                                oldValue = Math.Round((4294967295 + oldValue * 1000) / 1000, 2);
                            return oldValue;
                        }
                        if (param.Equals("瞬时产量"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue / 3.6, 2);
                        break;
                    case 2:
                        if (param.Equals("瞬时产量"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                        break;
                    case 3:
                        if (param.Equals("累计产量"))
                        {
                            oldValue = (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                            if (oldValue < -1)
                                //4294967295
                                oldValue = Math.Round((4294967295 + oldValue * 1000) / 1000, 2);
                            return oldValue;
                        }
                        break;
                    case 4:
                        if (param.Equals("瞬时产量"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue * 60, 2);
                        break;
                    case 5:
                        if (param.Equals("累计产量"))
                        {
                            oldValue = (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                            if (oldValue < -1)                          
                                //4294967295
                                oldValue = Math.Round((4294967295 + oldValue * 1000) / 1000, 2);
                            return oldValue;
                        }
                        if (param.Equals("瞬时产量"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue * 0.06, 2);
                        break;
                    case 6:
                        break;
                    case -1:
                        if (param.Equals("累计产量")|| param.Equals("瞬时产量"))
                            return -1;
                        break;
                    default:
                        return oldValue;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[Normalize]报错{ex.Message}");
            }
            return oldValue;
        }
    }
}
