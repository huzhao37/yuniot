using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SpareParts")]
    [Authorize]
    public class SparePartsController : Controller
    {
        private readonly ISparePartsRepository _sparePartsRepository;

        public SparePartsController(ISparePartsRepository sparePartsRepository)
        {
            _sparePartsRepository = sparePartsRepository;
        }
        // GET: api/SpareParts
        [HttpGet]
        public PaginatedList<SpareParts> Get(int pageindex, int pagesize,SparePartsStatus sparePartsStatus)
        {
            return _sparePartsRepository.GetPage(pageindex, pagesize, sparePartsStatus);
        }

        // GET: api/SpareParts/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/SpareParts
        [HttpPost]
        public bool Post([FromBody]SpareParts value)//dynamic
        {
            return _sparePartsRepository.Insert(value) > 0;
        }

        // PUT: api/SpareParts/5
        [HttpPut("{id}")]
        public bool Put( [FromBody]SpareParts value)//dynamic
        {
            return _sparePartsRepository.UpdateEntity(value) > 0;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _sparePartsRepository.DeleteEntity(e=>e.SparePartsId.Equals(id)) > 0;
        }
    }
}
