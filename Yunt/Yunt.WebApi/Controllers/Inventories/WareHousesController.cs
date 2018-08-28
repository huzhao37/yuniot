using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Common;
using Yunt.Common.ObjectExtentsions;
using Yunt.Device.Domain.IRepository;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/WareHouse")]
    [Authorize]
    public class WareHousesController : Controller
    {
        private readonly IWareHousesRepository _wareHousesRepository;
        private readonly IMotortypeRepository _motortypeRepository;
        private readonly IMotorRepository _motorRepository;
        public WareHousesController(IWareHousesRepository wareHousesRepository,
            IMotortypeRepository motortypeRepository,
            IMotorRepository motorRepository)
        {
            _wareHousesRepository = wareHousesRepository;
            _motortypeRepository = motortypeRepository;
            _motorRepository = motorRepository;
        }
        // GET: api/WareHouses
        [HttpGet]
        [EnableCors("any")]
        //[Route("")]
        public PaginatedList<dynamic> Get(int pageindex, int pagesize, string productionLineId)
        {
            try
            {
                var source = new List<dynamic>();
                var datas = _wareHousesRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
                var motorTypes = _motortypeRepository.GetEntities().ToList();
                if (!motorTypes?.Any() ?? true) return new PaginatedList<dynamic>(0, 0, 0, source);
                datas.ForEach(d => {
                    var motorTypeName = _motortypeRepository.GetEntities(e=>e.MotorTypeId.Equals(d.MotorTypeId))?.FirstOrDefault()?.MotorTypeName ?? "";
                    source.Add(new {
                        d.Id,d.Name,d.Keeper,d.MotorTypeId,d.ProductionLineId,d.Remark,MotorTypeName=motorTypeName,d.CreateTime
                    });
                });
                //var source = WareHouses.Froms(datas, motorTypes);
                var list = source.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
                return new PaginatedList<dynamic>(pageindex, pagesize, source.Count(), list);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>());
            }
        }

        [Route("WareHouseList")]
        [EnableCors("any")]
        [HttpGet()]
        public dynamic WareHouseList(string productionLineId)
        {
            try
            {
                var data = _wareHousesRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
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

        // POST: api/WareHouses
        [EnableCors("any")]
        [HttpPost]
        public bool Post([FromBody]WareHouses value)//dynamic
        {
            try
            {
                if (value.ProductionLineId.IsNullOrWhiteSpace() || value.Name.IsNullOrWhiteSpace())
                    return false;
                value.CreateTime = DateTime.Now.TimeSpan();

                return _wareHousesRepository.Insert(value) > 0;//.Copy() as Inventory.Domain.Model.WareHouses
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        [HttpGet]
        [EnableCors("any")]
        [Route("MotorTypeList")]
        public IEnumerable<dynamic> MotorTypes()
        {          
            var datas = _motortypeRepository.GetEntities()?.ToList();
            if (datas == null || !datas.Any())
                return new List<Motortype>();            
            return datas;
        }
        // PUT: api/WareHouses/5
        [HttpPut("{id}")]
        public bool Put([FromBody]WareHouses value)
        {
            return false;//_wareHousesRepository.UpdateEntity(value.Copy() as Inventory.Domain.Model.WareHouses) > 0;
        }

        // DELETE: api/ApiWithActions/5
        [EnableCors("any")]
        [HttpDelete()]
        public bool Delete(int id)
        {
            try
            {
                return _wareHousesRepository.DeleteEntity(e => e.Id == id) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        [Route("WareHouseNames")]
        [EnableCors("any")]
        [HttpGet()]
        public dynamic WareHouseNames(string productionLineId)
        {
            try
            {
                var data = _wareHousesRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
                if (data == null || !data.Any())
                    return new List<dynamic>();
                return data.Select(e => new {e.Name });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return ex.Message;
            }
        }
    }
}
