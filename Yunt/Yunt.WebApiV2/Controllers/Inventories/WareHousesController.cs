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
using Yunt.Common.ObjectExtentsions;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Xml.Domain.IRepository;
using WareHouses = Yunt.WebApiV2.Models.Inventories.WareHouses;

namespace Yunt.WebApiV2.Controllers
{
    [Produces("application/json")]
    [Route("api/WareHouses")]
    [Authorize]
    public class WareHousesController : Controller
    {
        private readonly IWareHousesRepository _wareHousesRepository;
        private readonly IMotortypeRepository _motortypeRepository;
        public WareHousesController(IWareHousesRepository wareHousesRepository,
            IMotortypeRepository motortypeRepository)
        {
            _wareHousesRepository = wareHousesRepository;
            _motortypeRepository = motortypeRepository;
        }
        // GET: api/WareHouses
        [HttpGet]
        public PaginatedList<WareHouses> Get(int pageindex, int pagesize)
        {
            var datas = _wareHousesRepository.GetEntities().ToList();
            var motorTypes = _motortypeRepository.GetEntities().ToList();
            if(!motorTypes?.Any()??true) return new PaginatedList<WareHouses>(0,0,0,new List<WareHouses>());
            var source= WareHouses.Froms(datas,motorTypes);
            var list = source.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return new PaginatedList<WareHouses>(pageindex, pagesize, source.Count(), list);
        }

        // GET: api/WareHouses/5
        [HttpGet("{id}")]
        public WareHouses Get(int id)
        {
            var data= _wareHousesRepository.GetEntityById(id);
            var motorTypes = _motortypeRepository.GetEntities();
            return data == null ? new WareHouses() : WareHouses.From(data, motorTypes);
        }
        
        // POST: api/WareHouses
        [HttpPost]
        public bool Post([FromBody]WareHouses value)//dynamic
        {
            return _wareHousesRepository.Insert(value.Copy() as Inventory.Domain.Model.WareHouses) > 0;
        }


        // PUT: api/WareHouses/5
        [HttpPut("{id}")]
        public bool Put([FromBody]WareHouses value)
        {
            return _wareHousesRepository.UpdateEntity(value.Copy() as Inventory.Domain.Model.WareHouses) > 0;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _wareHousesRepository.DeleteEntity(e=>e.Id==id) > 0;
        }
    }
}
