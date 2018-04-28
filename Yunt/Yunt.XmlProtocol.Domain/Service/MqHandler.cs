using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunt.XmlProtocol.Domain.MiddleMap;
using Yunt.Common;

namespace Yunt.XmlProtocol.Domain.Service
{
   public class MqHandler
    { 
        /// <summary>
      /// 队列数据解析并操作;
      /// </summary>
      /// <param name="buffer">数据</param>
      ///  <param name="typeString">数据类型</param>
      /// <param name="operation">匿名委托方法</param>
        public static bool UniversalParser(byte[] buffer, string typeString, Func<DataGramModel, string, bool> operation)
        {
            try
            {
                Logger.Info(typeString);
                var data = Extention.ByteArrayToHexString(buffer);
                Logger.Info(data);

                var analyze = new ByteParse(buffer);
                var model = analyze.Parser();

                Logger.Info("[MqHandler]Analyze End...");
                var result = false;
                if (model != null)
                {
                     result = operation(model, data);
                }

                //回复确认;
                //TODO: true则回复确认，false则不回复;
                if (!result)
                    Logger.Error("[MqHandler]Error in Write to database...");
                else
                    Logger.Info("[MqHandler]wait for next buffer...");

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("[MqHandler]Error in Write to database: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 队列数据解析;
        /// </summary>
        /// <param name="buffer">数据</param>
        public static DataGramModel UniversalParser(byte[] buffer)
        {
            try
            {
                var data = Extention.ByteArrayToHexString(buffer);
                Logger.Info(data);

                var analyze = new ByteParse(buffer);
                var model = analyze.Parser();

                Logger.Info("[MqHandler]Analyze End...");
                if (model != null)
                {
                   return model;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[MqHandler]Error: " + ex.Message);
               
            }
            return new DataGramModel();
        }

        /// <summary>
        /// 远程锁死应答数据解析;
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="typeString"></param>
        /// <returns></returns>
        public static bool Locker(byte[] buffer, string typeString)
        {
            Logger.Info(typeString);
            return true;
        }
    }
}
