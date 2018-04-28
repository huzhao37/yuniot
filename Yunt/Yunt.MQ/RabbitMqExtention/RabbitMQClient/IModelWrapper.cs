using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunt.Common;

namespace Yunt.MQ
{
    public class IModelWrapper:IDisposable
    {
        public IModel Model { get; set; }
        public bool IsBusy { get; set; }
        public string QueueName { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }

        public DateTime IdleTime { get; set; }

        public IModelWrapper(IModel model, string routingKey, string exchange, string queueName)
        {
            if (model == null)
                Logger.Error("参数model不能为空，请检查参数");

            if (string.IsNullOrEmpty(exchange))
                Logger.Error("参数exchange不能为空，请检查参数");

            if (string.IsNullOrEmpty(queueName))
                Logger.Error("参数queueName不能为空，请检查参数");

            RoutingKey = routingKey;
            Exchange = exchange;
            QueueName = queueName;
            Model = model;
            IsBusy = true;
        }

        public void Dispose()
        {
            Model.Dispose();
        }

        public void SetNotBusy()
        {
            IsBusy = false;
            IdleTime = DateTime.UtcNow;
        }
    }
}
