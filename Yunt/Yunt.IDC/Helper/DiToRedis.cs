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
    public class DiToRedis
    {
        #region ctor & fields
        private static DataGramModel _model;
        private static string _buffer;
        private static readonly ICollectdeviceRepository CollectdeviceRepository;
        public static readonly IDataformmodelRepository DataformmodelRepository;
        private static readonly IMotorEventLogRepository MotorEventLogRepository;

        static DiToRedis()
        {

            CollectdeviceRepository = ServiceProviderServiceExtensions.GetService<ICollectdeviceRepository>(Program.Providers["Xml"]);
            DataformmodelRepository = ServiceProviderServiceExtensions.GetService<IDataformmodelRepository>(Program.Providers["Xml"]);
            MotorEventLogRepository = ServiceProviderServiceExtensions.GetService<IMotorEventLogRepository>(Program.Providers["Analysis"]);

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
                    Logger.Info("[DiToRedis]Model Parser is NULL!");
                    return false;
                }

                var emDevice = CollectdeviceRepository.GetEntities(e => e.Index.Equals(_model.CollectdeviceIndex)).FirstOrDefault();
                if (emDevice == null)
                {
                    Logger.Info("[DiToRedis]No Related EmbeddedDevice!");
                    return false;
                }
                foreach (var pvalue in _model.PValues)
                {
                    MotorObj(pvalue);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("[DiToRedis]" + ex.Message);
                return false;
            }
        }

        #region private methods
        private static void MotorObj(KeyValuePair<DateTime, List<int>> pvalue)
        {
            var time = pvalue.Key.TimeSpan();
            var values = pvalue.Value;

            var forms = DataformmodelRepository.GetEntities(e => e.BitDesc.Equals("数字量")).ToList();
            if (forms == null || !forms.Any())
                return;
            for (var i = 0; i < forms.Count(); i++)
            {
                var form = forms[i];
                form.Value = DiNormalize.ConvertToNormal(form, values);
                //数字量存储redis-3个月
                if (form.MotorId.IsNullOrWhiteSpace() || form.MotorId.Equals("0"))
                    form.MotorId = "WDD-P001";
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    MotorEventLogRepository.AddDiLogAsync(new DiHistory()
                    {
                        MotorId = form.MotorId ?? "0",
                        MotorName = form.MachineName ?? "",
                        Param = form.Remark ?? "",
                        Value = (float)form.Value,
                        MotorTypeId = form.MotorTypeName ?? "",
                        Time = time,
                        DataPhysic = form.DataPhysicalFeature ?? ""
                    });
                });


            }
        }
        #endregion

    }
}
