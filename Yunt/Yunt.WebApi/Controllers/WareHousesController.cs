using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/WareHouses")]
    [Authorize]
    public class WareHousesController : Controller
    {
        private readonly IWareHousesRepository _wareHousesRepository;

        public WareHousesController(IWareHousesRepository wareHousesRepository)
        {
            _wareHousesRepository = wareHousesRepository;
        }
        // GET: api/WareHouses
        [HttpGet]
        public PaginatedList<WareHouses> Get(int pageindex, int pagesize)
        {
            return _wareHousesRepository.GetPage(pageindex, pagesize);
        }

        // GET: api/WareHouses/5
        [HttpGet("{id}")]
        public PaginatedList<WareHouses> Get(string id,int pageindex, int pagesize)
        {
            return _wareHousesRepository.GetPage(pageindex, pagesize,id);
        }
        
        // POST: api/WareHouses
        [HttpPost]
        public bool Post([FromBody]WareHouses value)//dynamic
        {
            return _wareHousesRepository.Insert(value) > 0;
        }


        // PUT: api/WareHouses/5
        [HttpPut("{id}")]
        public bool Put([FromBody]WareHouses value)
        {
            return _wareHousesRepository.UpdateEntity(value) > 0;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _wareHousesRepository.DeleteEntity(e=>e.WareHousesId.Equals(id)) > 0;
        }
    }
}
