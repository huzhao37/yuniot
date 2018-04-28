using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;
using Yunt.XmlProtocol.Domain.Models;
using Yunt.Common;

namespace Yunt.IDC.Helper
{
    /// <summary>
    /// 常态化
    /// </summary>
   public class Normalize
    {
        /// <summary>
        /// 根据数据精度和数据参数将数值转化为实际值
        /// </summary>
        /// <param name="accuracy">精度</param>
        /// <param name="param">参数</param>
        /// <param name="oldValue">原始值</param>
        /// <param name="motor">电机</param>
        /// <param name="forms">数据表单集合</param>
        /// <param name="values">数据值集合</param>
        /// <returns></returns>
        public static double ConvertToNormal(string accuracy, string param,int oldValue,Motor motor,List<Dataformmodel> forms,List<int> values)
        {
            if (motor == null)
                return 0;
            var accu = 1.0;
            accu = string.IsNullOrWhiteSpace(accuracy) ? 1 : Convert.ToDouble(accuracy); 
            if (param.Contains("温度"))
            {
               return (oldValue == -1)? -1: 
                    Math.Round((double)oldValue * accu * GetCurrentCTFactor(motor.MotorId), 2);
            }
            if (param.Contains("电流"))
            {
                var des = Extention.TempTemperatureTranster(oldValue);
                return (oldValue == -1)? -1
                    : Math.Round((double)des * accu, 2);
            }
            if (param.Equals("单位"))
            {
                var tempInt = (oldValue == -1) ? -1 : (int)oldValue;
                int value = tempInt;
                if (tempInt != -1)
                    value= tempInt & 7;
                return value;
            }
            if (param.Contains("称重"))
            {
                var unitForm= forms.SingleOrDefault(e => e.FieldParam.Equals("单位")&&e.MachineName.Equals(motor.Name)&&e.LineId==motor.ProductionLineId);
                unitForm.Value = ConvertToNormal(unitForm.DataPhysicalAccuracy, unitForm.FieldParam, values[unitForm.Index], motor,forms, values);

                var originalValue = (oldValue == -1) ? -1 : Math.Round((double) oldValue * accu, 2);
                return ConveyorWeightConvert(Convert.ToInt32(unitForm.Value), param, originalValue);
            }
            return (oldValue == -1)? -1
                : Math.Round((double) oldValue * accu, 2);

        }

        /// <summary>
        /// 获取设备电流CT系数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static double GetCurrentCTFactor(string id)
        {
            double ct = Container.mCtContainer.GetCTValueById(id);
            return ct;
        }


        /// <summary>
        /// 根据单位计算皮带机瞬时称重、累计称重
        /// </summary>
        /// <param name="unit">单位</param>
        /// <param name="param">称重参数</param>
        /// <param name="oldValue">称重原始值</param>
        /// <returns></returns>
        private static double ConveyorWeightConvert(int unit,string param,double oldValue)
        {
            try
            {
                switch (unit)
                {
                    case 0:
                        if(param.Equals("瞬时称重"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue / 3600, 2);
                        break;
                    case 1:
                        if (param.Equals("累计称重"))
                        {
                            oldValue = (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                            if (oldValue < -1)
                            {
                                //4294967295
                                oldValue = Math.Round((4294967295 + oldValue * 1000) / 1000, 2);
                            }
                            return oldValue;
                        }
                        if (param.Equals("瞬时称重"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue / 3.6, 2);
                        break;
                    case 2:
                        if (param.Equals("瞬时称重"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                        break;
                    case 3:
                        if (param.Equals("累计称重"))
                        {
                            oldValue = (oldValue == -1) ? -1 : Math.Round(oldValue / 1000, 2);
                            if (oldValue < -1)
                            {
                                //4294967295
                                oldValue = Math.Round((4294967295 + oldValue * 1000) / 1000, 2);
                            }
                            return oldValue;
                        }                  
                        break;
                    case 4:
                        if (param.Equals("瞬时称重"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue * 60, 2);
                        break;
                    case 5:
                        if (param.Equals("累计称重"))
                        {
                            oldValue = (oldValue == -1)? -1: Math.Round(oldValue / 1000, 2);
                            if (oldValue < -1)
                            {
                                //4294967295
                                oldValue = Math.Round((4294967295 + oldValue * 1000) / 1000, 2);
                            }
                            return oldValue;
                        }
                        if (param.Equals("瞬时称重"))
                            return (oldValue == -1) ? -1 : Math.Round(oldValue * 0.06, 2);
                        break;
                    case 6:
                        break;
                    case -1:
                        if (param.Equals("累计称重"))
                            return -1;
                        if (param.Equals("瞬时称重"))
                            return -1;
                        break;
                    default:
                        return oldValue;
                        break;
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
