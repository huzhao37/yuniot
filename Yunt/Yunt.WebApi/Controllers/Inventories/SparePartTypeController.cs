using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Xml.Domain.IRepository;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/SparePartType")]
    // [Authorize]
    public class SparePartTypeController : Controller
    {
        private readonly ISparePartsTypeRepository _sparePartsTypeRepository;
        private readonly IMotortypeRepository _motortypeRepository;
        private readonly IMotorRepository _motorRepository;
        public SparePartTypeController(ISparePartsTypeRepository sparePartsTypeRepository,
            IMotortypeRepository motortypeRepository,
            IMotorRepository motorRepository)
        {
            _sparePartsTypeRepository = sparePartsTypeRepository;
            _motortypeRepository = motortypeRepository;
            _motorRepository = motorRepository;
        }
        [EnableCors("any")]
        [HttpGet]
        public dynamic Get(int pageindex, int pagesize)
        {
            var datas = _sparePartsTypeRepository.GetEntities()?.ToList();
            if (!(datas?.Any() ?? false)) return new PaginatedList<SparePartsType>(0, 0, 0, new List<SparePartsType>());
            var list = datas.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return new PaginatedList<SparePartsType>(pageindex, pagesize, datas.Count(), list);
        }

        [EnableCors("any")]
        [Route("SparePartsTypeList")]
        [HttpGet()]
        public dynamic SparePartsTypeList()
        {
            try
            {
                var data = _sparePartsTypeRepository.GetEntities()?.ToList();
                if (data == null || !data.Any())
                    return new List<dynamic>();
                return data.Select(e => new { e.Id, e.Name });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        // POST: api/SparePartType
        [EnableCors("any")]
        [HttpPost]
        public bool Post([FromBody]SparePartsType value)//dynamic
        {
            try
            {
                if (value.Name.IsNullOrWhiteSpace())
                    return false;
                value.CreateTime = DateTime.Now.TimeSpan();

                return _sparePartsTypeRepository.Insert(value) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }
        [EnableCors("any")]
        [HttpPut]
        public bool Put([FromBody]int limit)
        {
            try
            {
                var sparePartTypeList = _sparePartsTypeRepository.GetEntities()?.ToList();
                var updates = new List<SparePartsType>();
                updates = sparePartTypeList;
                if (sparePartTypeList != null && sparePartTypeList.Any())
                {
                    sparePartTypeList.ForEach(e =>
                    {
                        var item = updates.Where(u => u.Id == e.Id).FirstOrDefault();
                        item.InventoryAlarmLimits = limit;
                        item.CreateTime = DateTime.Now.TimeSpan();

                    });
                }
                return _sparePartsTypeRepository.UpdateEntity(updates) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }

        }

        [EnableCors("any")]
        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                return _sparePartsTypeRepository.DeleteEntity(e => e.Id == id) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

    }
}