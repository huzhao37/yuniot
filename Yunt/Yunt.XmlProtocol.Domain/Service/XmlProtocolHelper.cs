using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NewLife.Log;
using Yunt.XmlProtocol.Domain.Models;
using Yunt.XmlProtocol.Domain.XmlMap;

namespace Yunt.XmlProtocol.Domain.Service
{
    public class XmlProtocolHelper
    {

        /// <summary>
        /// 保存DataConfig信息（替代原来的手写xml）
        /// </summary>
        /// <param name="dataList"></param>
        public void SaveToDataConfigInfo(List<Dataformmodel> dataList)
        {
            if (!dataList.Any()) return;
            var index = dataList[0]?.CollectdeviceIndex ?? "";

            //按照数据类型划分组
            var formatList = dataList.OrderBy(e => e.Index).GroupBy(e => e.DataType).ToList();
            foreach (var item in formatList)
            {
                var format = item.Key;
                var datas = item.ToList();

                //这种组成方式。。。（需要规范）
                var bitDescStr = format.Split("bit");
                var bit = 0;
                var desc = "";

                var bitStr = bitDescStr[0];
                if (bitDescStr.Length == 2)
                    desc = bitDescStr[1];

                if (bitStr.Equals("LOGID"))
                {
                    bit = 32;
                    desc = "LOGID";
                }
                else if (bitStr.Equals("时间"))
                {
                    bit = 32;
                    desc = "时间";
                }
                else
                {
                    bit = Convert.ToInt32(bitStr);
                }
                var dataType = Datatype.FindByBitAndDesc(bit, desc);
                var dataConfig = new Dataconfig()
                {
                    Count = datas.Count,
                    DatatypeID = dataType.Id,
                    CollectdeviceIndex = index,
                };
                dataConfig.Insert();

            }
        }

        #region DataConfig及DataType  Xml文件相关(一次性产品)
        /// <summary>
        /// 其他xml文件持久化
        /// </summary>
        public static void OtherXmlPersist()
        {
            var dataTuple = GetDataTypeInfo();
            var result = SaveToDb(dataTuple);
            XTrace.Log.Info($"[OtherXmlPersist]持久化至SQLDb成功：{result}");
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        public static Tuple<DataTypeModel, DataConfigModel> GetDataTypeInfo()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\DataType.xml";
            if (!File.Exists(path)) return new Tuple<DataTypeModel, DataConfigModel>(new DataTypeModel(), new DataConfigModel());
            var fs = File.Open(path, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs);
            var text = sr.ReadToEnd();
            var dataType = Common.Xml.XmlHelper.XmlSerializeHelper.DeSerialize<DataTypeModel>(text, Encoding.UTF8);


            var path2 = AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\DataConfig.xml";
            if (!File.Exists(path2)) return new Tuple<DataTypeModel, DataConfigModel>(new DataTypeModel(), new DataConfigModel());
            var fs2 = File.Open(path2, FileMode.Open, FileAccess.Read);
            var sr2 = new StreamReader(fs2);
            var text2 = sr2.ReadToEnd();
            var dataConfig = Common.Xml.XmlHelper.XmlSerializeHelper.DeSerialize<DataConfigModel>(text2, Encoding.UTF8);
            return new Tuple<DataTypeModel, DataConfigModel>(dataType, dataConfig);
        }


        public static bool SaveToDb(Tuple<DataTypeModel, DataConfigModel> dataTuple)
        {
            var result = false;
            try
            {
                if (dataTuple.Item1.Types.Any())
                {
                    dataTuple.Item1.Types.ForEach(
                        type =>
                        {
                            var dataType = new Datatype()
                            {
                                Id = type.Id,
                                Description = type.Description,
                                Bit = type.Bit,
                                InByte = type.InByte,
                                OutIntArray = type.OutIntArray
                            };
                            dataType.Insert();
                        });
                }

                if (dataTuple.Item2.Configs.Any())
                {
                    dataTuple.Item2.Configs.ForEach(
                        config =>
                        {
                            if (config.Datas.Any())
                            {
                                config.Datas.ForEach(data =>
                                {
                                    var dataConfig = new Dataconfig()
                                    {
                                        //DataConfigId = config.Id,
                                        DatatypeID = data.TypeId,
                                        Count = data.Count
                                    };
                                    dataConfig.Insert();
                                });
                            }

                        });
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                XTrace.Log.Error($"[OtherXmlPersist]SaveToDb出错{e.Message}");
            }

            return result;
        }
        #endregion
    }
}
