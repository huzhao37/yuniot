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
    public class OriginalNormalize
    {
        private static readonly IDataformmodelRepository DataformmodelRepository;
        static OriginalNormalize()
        {
            DataformmodelRepository = Task.RecoveryTask.DataformmodelRepository;
        }
        /// <summary>
        /// 根据数据精度和数据参数将数值转化为实际值
        /// </summary>
        /// <param name="form">数据表单集合</param>
        /// <param name="values">数据值集合</param>
        /// <returns></returns>
        public static double ConvertToNormal(Dataformmodel form, List<int> values)
        {
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
                case "电流":
                    return (oldValue == -1) ? -1 :
                    Math.Round(oldValue * accu, 2);        
          
            }
            return (oldValue == -1) ? -1
              : Math.Round(oldValue * accu, 2);
        }
    }
}
