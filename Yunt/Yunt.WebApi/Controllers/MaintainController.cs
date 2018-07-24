using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/Maintain")]
    public class MaintainController : Controller
    {
        private readonly IMaintainRepository _maintainRepository;

        public MaintainController(IMaintainRepository maintainRepository)
        {
            _maintainRepository = maintainRepository;
        }
        // GET: api/Maintain
        [EnableCors("any")]
        [HttpGet]
        public PaginatedList<Maintain> Get(DateTime start,DateTime end,string operater,  int pageindex,int pagesize)
        {
            try
            {
                long startT = start.TimeSpan(), endT = end.Date.AddDays(1).AddMilliseconds(-1).TimeSpan();
                if (!operater.IsNullOrWhiteSpace())
                    return _maintainRepository.GetPage(pageindex, pagesize, e => e.Operator.Equals(operater) && e.Time >= startT && e.Time <= endT, e => e.Time);
                return _maintainRepository.GetPage(pageindex, pagesize, e => e.Time >= startT && e.Time <= endT, e => e.Time);

            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<Maintain>(0,0,0,new  List<Maintain>());
            }
        
        }
       
        [HttpGet]
        [EnableCors("any")]
        [Route("Operators")]
        public IEnumerable<string> Operators()
        {
            return _maintainRepository.GetEntities()?.Select(e => e.Operator)?.ToList();
        }

        // POST: api/Maintain
        [HttpPost]
        [EnableCors("any")]
        public bool Post([FromBody]Maintain value)//dynamic
        {
            try
            {
                if (value == null)
                    return false;
                value.Time = DateTime.Now.TimeSpan();
                return _maintainRepository.Insert(value) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
  
        }

        // PUT: api/Maintain/5
        [EnableCors("any")]
        [HttpPut("{id}")]
        public bool Put([FromBody]Maintain value)
        {
            return _maintainRepository.UpdateEntity(value) > 0;
        }

        // DELETE: api/ApiWithActions/5
        [EnableCors("any")]
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _maintainRepository.DeleteEntity(e => e.Id.Equals(id)) > 0;
        }
    }
}
