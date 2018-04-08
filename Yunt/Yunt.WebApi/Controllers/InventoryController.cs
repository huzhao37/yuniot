using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Inventory.Domain.IRepository;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Inventory")]
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ISparePartsRepository _sparePartsRepository;

        public InventoryController(ISparePartsRepository sparePartsRepository)
        {
            _sparePartsRepository = sparePartsRepository;
        }
        // GET: api/Inventory
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Inventory/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Inventory
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
