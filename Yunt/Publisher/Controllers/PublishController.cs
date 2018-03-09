using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Publisher.Models;

namespace Publisher.Controllers
{
    public class PublishController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ICapPublisher _publisher;

        public PublishController(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        [Route("~/checkAccountWithTrans")]
        public async Task<IActionResult> PublishMessageWithTransaction([FromServices]AppDbContext dbContext)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                //指定发送的消息标题（供订阅）和内容
                await _publisher.PublishAsync("xxx.services.account.check",
                    new Person { Name = "Foo", Age = 11 });
                // 你的业务代码。
                trans.Commit();
            }
            return Ok();
        }

     //   [NoAction]
        [CapSubscribe("xxx.services.account.check")]
        public async Task CheckReceivedMessage(Person person)
        {
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            await  Task.CompletedTask;
        }
    }
}