using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
using Yunt.Xml.Domain.Services;
using Yunt.Xml.Repository.EF.Repositories;
using Yunt.Redis;
using Yunt.Xml.Domain.MiddleMap;

namespace Yunt.Xml.Repository.EF.Services
{
   public class BytesParseRepository : XmlRepositoryBase<Datatype, Models.Datatype>, IBytesParseRepository
    {
        private readonly List<Datatype> _types;
        private readonly List<Dataconfig> _configs;
        public BytesParseRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            var dataTypeRep = ServiceProviderServiceExtensions.GetService<IDatatypeRepository>(BootStrap.ServiceProvider);
            var dataConfigRep = ServiceProviderServiceExtensions.GetService<IDataconfigRepository>(BootStrap.ServiceProvider);

            _types = dataTypeRep.GetEntities(null,e=>e.Id).ToList();
            _configs = dataConfigRep.GetEntities(null, e => e.Id).ToList();

        }


        #region extend method
        public DataGramModel Parser(byte[] buffer)
        {

           
            if (_types == null || _configs == null)
            {
                Logger.Error("[MqHandler]Error: Cannot find config Info in SqlDb.");
                return null;
            }

            var bufferIndex = 0;
            var dataGram = new DataGramModel
            {
                CollectdeviceIndex = Extention.ByteArrayToHexString(Extention.ByteCapture(buffer, ref bufferIndex, 0, 6)),
                DeviceId = Extention.byteToInt(Extention.ByteCapture(buffer, ref bufferIndex, 0, 2)),
                Count = Extention.byteToInt(Extention.ByteCapture(buffer, ref bufferIndex, 0, 1))
            };

#if DEBUG
            Logger.Info(dataGram.DeviceId.ToString());
            Logger.Info(dataGram.CollectdeviceIndex);
#endif


            var configModels = _configs.Where(m => m.Collectdevice_Index == dataGram.CollectdeviceIndex).OrderBy(e => e.Datatype_Id)
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
                    var typeid = config.Datatype_Id;
                    var typeModel = _types.FirstOrDefault(t => t.Id == typeid);
                    if (typeModel == null) return dataGram;
                    switch (typeid)
                    {
                        case 4:
                            {
                                //4个字节(记录ID);
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToUInt(temp);
                                    var resultValue = (value == 4294967295) ? -1 : (int)value;
                                    parmValues.Add(resultValue);
                                    //Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 5:
                            {
                                //4个字节(时间);
                                var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                var value = Extention.byteToTime(temp);
                                time = Unix.ConvertIntDateTime(value);
                                //Logger.Info(time.ToString("yyyy-MM-dd HH:mm:ss"));
                                break;
                            }
                        case 7:
                            {
                                //4个字节(整形模拟量)        FFFFFFFF - 4294967295 (uint)
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToUInt(temp);
                                    var resultValue = (value == 4294967295) ? -1 : (int)value;
                                    parmValues.Add(resultValue);
                                    //Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 9:
                            {
                                //4个字节
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToUInt(temp);
                                    var resultValue = (value == 4294967295) ? -1 : (int)value;
                                    parmValues.Add(resultValue);
                                    // Logger.Info(resultValue.ToString());
                                }
                                break;
                            }
                        case 11:
                            {
                                //2个字节(整形模拟量)   FFFF - 65535       
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToInt(temp);
                                    var resultValue = (value == 65535) ? -1 : value;
                                    parmValues.Add(resultValue);
                                    //Logger.Info(resultValue.ToString());
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
                                    var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var values = Extention.byteToInt12(temp);
                                    foreach (var value in values)
                                    {
                                        var tempValue = (value == 4095) ? -1 : value;
                                        tempCount++;
                                        if (tempCount > config.Count)
                                            continue;
                                        parmValues.Add(tempValue);
                                        // Logger.Info(tempValue.ToString());
                                    }
                                }
                                break;
                            }

                        case 13:
                            {
                                //1个字节(整形模拟量)    FF - 255                 
                                for (var j = 0; j < config.Count; j++)
                                {
                                    var temp = Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte);
                                    var value = Extention.byteToInt(temp);
                                    var resultValue = (value == 255) ? -1 : value;
                                    parmValues.Add(resultValue);
                                    //Logger.Info(resultValue.ToString());
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
                                    Extention.ByteCapture(buffer, ref bufferIndex, 0, typeModel.InByte).FirstOrDefault();
                                    var values = Extention.byteToInt1(temp);
                                    foreach (var value in values)
                                    {
                                        tempCount++;
                                        if (tempCount > config.Count)
                                            continue;
                                        parmValues.Add(value);
                                        // Logger.Info(value.ToString());
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

        public bool UniversalParser(byte[] buffer, string typeString, Func<DataGramModel, string, bool> operation)
        {
            try
            {
#if DEBUG

                Logger.Info(typeString);
#endif
                var data = Extention.ByteArrayToHexString(buffer);
#if DEBUG
                Logger.Info(data);
#endif

                var model = Parser(buffer);
#if DEBUG
                Logger.Info("[MqHandler]Analyze End...");
#endif
                var result = false;
                if (model != null)
                {
                    result = operation(model, data);
                }

                //回复确认;
                //TODO: true则回复确认，false则不回复;
                if (!result)
                    Logger.Error("[MqHandler]Error in Write to database...");
#if DEBUG
                else
                    Logger.Info("[MqHandler]wait for next buffer...");
#endif

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("[MqHandler]Error in Write to database: " + ex.Message);
                return false;
            }
        }

        #endregion

    }
}
