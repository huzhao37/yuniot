using System;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services=new ServiceCollection();
            services.AddTransient<ISubscriberService, SubscriberService>();
            Console.WriteLine("Hello World!");
        }

        public interface ISubscriberService
        {
            public void CheckReceivedMessage(Person person);
        }


        public class SubscriberService : ISubscriberService, ICapSubscribe
        {
            [CapSubscribe("xxx.services.account.check")]
            public void CheckReceivedMessage(Person person)
            {

            }
        }
    }
}
