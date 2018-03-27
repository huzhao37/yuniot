using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Common
{
    /// <summary>
    /// 标识符工厂
    /// </summary>
    public static class IdFactory
    {
        /// <summary>
        /// 生成新编码
        /// </summary>
        /// <returns></returns>
        public static string NewMotor<T>(this T t, string lineId, string MotorType, int motorIndex)
        {
            try
            {
                return lineId + "-" + MotorType + motorIndex.ToString().PadLeft(6, '0');
                ; //保证6位数，不足补0
            }
            catch (Exception ex)
            {
                Logger.Error("[IdFactory]标识符生成出错！！！");
                Logger.Exception(ex);
            }
            Environment.Exit(1);
            return null;
        }

        /// <summary>
        /// 生成新编码
        /// </summary>
        /// <returns></returns>
        public static string NewSpareParts<T>(this T t, string sparePartType, int sparePartIndex)
        {
            try
            {
                return sparePartType + sparePartIndex.ToString().PadLeft(6, '0');
                ; //保证6位数，不足补0
            }
            catch (Exception ex)
            {
                Logger.Error("[IdFactory]标识符生成出错！！！");
                Logger.Exception(ex);
            }
            Environment.Exit(1);
            return null;
        }

        /// <summary>
        /// 生成新编码
        /// </summary>
        /// <returns></returns>
        public static string NewUser<T>(this T t, int sparePartIndex)
        {
            try
            {
                return "U" + sparePartIndex.ToString().PadLeft(6, '0');
                ; //保证6位数，不足补0
            }
            catch (Exception ex)
            {
                Logger.Error("[IdFactory]标识符生成出错！！！");
                Logger.Exception(ex);
            }
            Environment.Exit(1);
            return null;
        }
    }
}
