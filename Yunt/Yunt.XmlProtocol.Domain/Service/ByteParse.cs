using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunt.XmlProtocol.Domain.MiddleMap;
using Yunt.Common;
using Yunt.XmlProtocol.Domain.Models;

namespace Yunt.XmlProtocol.Domain.Service
{
    /// <summary>
    /// 队列Bytes解析
    /// </summary>
    public class ByteParse
    {
        private readonly byte[] _buffer;
      //  private long x = DataType.FindCount();
        private readonly IEnumerable<Datatype> _types = Datatype.FindAll(null,"Id",null,0,0);

        private readonly IEnumerable<Dataconfig> _configs = Dataconfig.FindAll(null, "Id", null, 0, 0);

        public ByteParse(byte[] buffer)
        {
            this._buffer = buffer;
        }

        public DataGramModel Parser()
        {
            if (_types == null || _configs == null)
            {
                Logger.Error("[MqHandler]Error: Cannot find config Info in SqlDb.");
                return null;
            }

            var bufferIndex = 0;
            var dataGram = new DataGramModel();
            dataGram.DeviceId = Extention.ByteArrayToHexString(Extention.ByteCapture(_buffer, ref bufferIndex, 0, 6));
            dataGram.CollectdeviceIndex = Extention.ByteArrayToHexString(Extention.ByteCapture(_buffer, ref bufferIndex, 0, 2));
            dataGram.Count = Extention.byteToInt(Extention.ByteCapture(_buffer, ref bufferIndex, 0, 1));

            Logger.Info(dataGram.DeviceId);
            Logger.Info(dataGram.CollectdeviceIndex.ToString());

            var configModels = _configs.Where(m => m.CollectdeviceIndex == dataGram.CollectdeviceIndex).OrderBy(e => e.DatatypeID)
                .ToList();
            if (!configModels.Any())
            {
                Logger.Error("[MqHandler]Error: 没有对应的数据表单;");
                return null;
            }

            for (var i = 0; i < dataGram.Count; i++)
            {
                var parmValues = new List<int>();
                var time = DateTime.Now;

                //List<DataModel> datas = configModels.datas;
                foreach (var config in configModels)
                {
                    //在此处与协议规定的以大数优先
                    var typeid = config.DatatypeID;
                    var typeModel = _types.FirstOrDefault(t => t.Id == typeid);
                    if (typeModel == null) return dataGram;
                    switch (typeid)
                    {
                        case 4:
                            {
                                //4个字节(记录ID);
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToUInt(temp);
                                    var resultValue = (value == 4294967295) ? -1 : (int)value;
                                    parmValues.Add(resultValue);
                                    Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 5:
                            {
                                //4个字节(时间);
                                var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                var value = Extention.byteToTime(temp);
                                time = Unix.ConvertIntDateTime(value);
                                Logger.Info(time.ToString("yyyy-MM-dd HH:mm:ss"));
                                break;
                            }
                        case 7:
                            {
                                //4个字节(整形模拟量)        FFFFFFFF - 4294967295 (uint)
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToUInt(temp);
                                    var resultValue = (value == 4294967295) ? -1 : (int)value;
                                    parmValues.Add(resultValue);
                                    Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 9:
                            {
                                //4个字节
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToUInt(temp);
                                    var resultValue = (value == 4294967295) ? -1 : (int)value;
                                    parmValues.Add(resultValue);
                                    Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 11:
                            {
                                //2个字节(整形模拟量)   FFFF - 65535       
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToInt(temp);
                                    var resultValue = (value == 65535) ? -1 : value;
                                    parmValues.Add(resultValue);
                                    Logger.Info(resultValue.ToString());
                                }
                                break;
                            }

                        case 12:
                            {
                                //12位，每次读取3个字节，2个数据;  FFF - 4095
                                var total = (config.Count % 2 == 0) ? config.Count / 2 : config.Count / 2 + 1;
                                var tempCount = 0;
                                for (var j = 0; j < total; j++)
                                {
                                    var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var values = Extention.byteToInt12(temp);
                                    foreach (var value in values)
                                    {
                                        var tempValue = (value == 4095) ? -1 : value;
                                        tempCount++;
                                        if (tempCount > config.Count)
                                            continue;
                                        parmValues.Add(tempValue);
                                        Logger.Info(tempValue.ToString());
                                    }
                                }
                                break;
                            }

                        case 13:
                            {
                                //1个字节(整形模拟量)    FF - 255                 
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToInt(temp);
                                    var resultValue = (value == 255) ? -1 : value;
                                    parmValues.Add(resultValue);
                                    Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 14:
                            {
                                /*一个8个字节，7*8+1=57 57个参数(最后一个字节只有一位有效位)*/
                                //1位，8个一位组成一个字节,每次读取一个字节，返回8个数据;
                                var total = (config.Count % 8 == 0) ? config.Count / 8 : config.Count / 8 + 1;
                                var tempCount = 0;
                                for (var j = 0; j < total; j++)
                                {
                                    var temp =
                                    Extention.ByteCapture(_buffer, ref bufferIndex, 0, typeModel.InByte).FirstOrDefault();
                                    var values = Extention.byteToInt1(temp);
                                    foreach (var value in values)
                                    {
                                        tempCount++;
                                        if (tempCount > config.Count)
                                            continue;
                                        parmValues.Add(value);
                                        Logger.Info(value.ToString());
                                    }
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                dataGram.PValues.Add(time, parmValues);
            }

            return dataGram;
        }
    }
}
