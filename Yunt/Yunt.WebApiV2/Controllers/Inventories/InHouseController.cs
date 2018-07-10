using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Common.ObjectExtentsions;
using Yunt.Inventory.Domain.IRepository;
using Yunt.WebApiV2.Models.Inventories;

namespace Yunt.WebApiV2.Controllers
{
    [Produces("application/json")]
    [Route("api/InHouse")]
    [Authorize]
    public class InHouseController : Controller
    {
        private readonly IInHouseRepository _inHouseRepository;
        private readonly IWareHousesRepository _wareHousesRepository;
        private readonly ISparePartsTypeRepository _sparePartsTypeRepository;
        public InHouseController(IInHouseRepository inHouseRepository,
            IWareHousesRepository wareHousesRepository,
            ISparePartsTypeRepository sparePartsTypeRepository)
        {
            _inHouseRepository = inHouseRepository;
            _wareHousesRepository = wareHousesRepository;
            _sparePartsTypeRepository = sparePartsTypeRepository;
        }
        // GET: api/InHouse
        [HttpGet]
        public PaginatedList<InHouse> Get(int pageindex, int pagesize)
        {
            var datas = _inHouseRepository.GetEntities().ToList();
            if (!datas?.Any() ?? true) return new PaginatedList<InHouse>(0, 0, 0, new List<InHouse>());
            var wareHouses = _wareHousesRepository.GetEntities().ToList();
            if (!wareHouses?.Any() ?? true) return new PaginatedList<InHouse>(0, 0, 0, new List<InHouse>());
            var spares = _sparePartsTypeRepository.GetEntities().ToList();
            if (!spares?.Any() ?? true) return new PaginatedList<InHouse>(0, 0, 0, new List<InHouse>());
            var source = InHouse.Froms(datas, spares, wareHouses);
            var list = source.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return new PaginatedList<InHouse>(pageindex, pagesize, source.Count(), list);

           // return _inHouseRepository.GetPage(pageindex, pagesize);
        }

        // GET: api/InHouse/5
        [HttpGet("{id}")]
        public InHouse Get(int id)
        {
            var data = _inHouseRepository.GetEntityById(id);
            if (data == null) return new InHouse();
            var wareHouses = _wareHousesRepository.GetEntities().ToList();
            var spares = _sparePartsTypeRepository.GetEntities().ToList();
            return InHouse.From(data, spares, wareHouses);
        }

        // POST: api/InHouse
        [HttpPost]
        public bool Post([FromBody]InHouse value)//dynamic
        {
            return _inHouseRepository.Insert(value.Copy() as Inventory.Domain.Model.InHouse) > 0;
            //return false;
        }

        // PUT: api/InHouse/5
        [HttpPut("{id}")]
        public bool Put( [FromBody]InHouse value)//dynamic
        {
             return _inHouseRepository.UpdateEntity(value.Copy() as Inventory.Domain.Model.InHouse) > 0;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var model = _inHouseRepository.GetEntityById(id);
            if (model == null)
                return false;
            model.IsDelete = true;
            return _inHouseRepository.UpdateEntity(model) > 0;
        }
    }
}
