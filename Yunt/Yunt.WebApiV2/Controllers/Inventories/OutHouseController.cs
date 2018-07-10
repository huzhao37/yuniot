using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.WebApiV2.Models.Inventories;
using Yunt.Device.Domain.IRepository;
using Yunt.Common.ObjectExtentsions;

namespace Yunt.WebApiV2.Controllers
{
    [Produces("application/json")]
    [Route("api/OutHouse")]
    [Authorize]
    public class OutHouseController : Controller
    {
        private readonly IOutHouseRepository _outHouseRepository;
        private readonly ISparePartsTypeRepository _sparePartsTypeRepository;
        private readonly IMotorRepository _motorRepository;
        public OutHouseController(IOutHouseRepository outHouseRepository,
            ISparePartsTypeRepository sparePartsTypeRepository,
            IMotorRepository motorRepository)
        {
            _outHouseRepository = outHouseRepository;
            _motorRepository = motorRepository;

        }
        // GET: api/OutHouse
        [HttpGet]
        public PaginatedList<OutHouse> Get(int pageindex, int pagesize)
        {
            var datas = _outHouseRepository.GetEntities().ToList();
            if (!datas?.Any() ?? true) return new PaginatedList<OutHouse>(0, 0, 0, new List<OutHouse>());
            var spares = _sparePartsTypeRepository.GetEntities().ToList();
            if (!spares?.Any() ?? true) return new PaginatedList<OutHouse>(0, 0, 0, new List<OutHouse>());
            var motors = _motorRepository.GetEntities().ToList();
            if (!motors?.Any() ?? true) return new PaginatedList<OutHouse>(0, 0, 0, new List<OutHouse>());
            var source = OutHouse.Froms(datas, spares, motors);
            var list = source.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return new PaginatedList<OutHouse>(pageindex, pagesize, source.Count(), list);
        }

        // GET: api/OutHouse/5
        [HttpGet("{id}")]
        public OutHouse Get(int id)
        {
            var data = _outHouseRepository.GetEntityById(id);
            if (data == null) return new OutHouse();
            var motors = _motorRepository.GetEntities().ToList();
            var spares = _sparePartsTypeRepository.GetEntities().ToList();
            return OutHouse.From(data, spares, motors);
        }

        // POST: api/OutHouse
        [HttpPost]
        public bool Post([FromBody]OutHouse value)//dynamic
        {
            value.SparePartsStatus=SparePartsTypeStatus.Using;
            return _outHouseRepository.Insert(value.Copy() as Inventory.Domain.Model.OutHouse) > 0;
            //return false;
        }

        // PUT: api/OutHouse/5
        [HttpPut("{id}")]
        public bool Put( [FromBody]OutHouse value)//dynamic
        {
             return _outHouseRepository.UpdateEntity(value.Copy() as Inventory.Domain.Model.OutHouse) > 0;            
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var model = _outHouseRepository.GetEntityById(id);
            if (model == null)
                return false;
            model.IsDelete = true;
            return _outHouseRepository.UpdateEntity(model) > 0;
        }
    }
}
