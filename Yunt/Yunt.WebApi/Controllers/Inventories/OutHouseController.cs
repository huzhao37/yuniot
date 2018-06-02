using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.WebApi.Models.Inventories;
using Yunt.Device.Domain.IRepository;
using Microsoft.AspNetCore.Cors;
using Yunt.Device.Domain.Model;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/OutHouse")]
    [Authorize]
    public class OutHouseController : Controller
    {
        private readonly IInHouseRepository _inHouseRepository;
        private readonly IOutHouseRepository _outHouseRepository;
        private readonly ISparePartsTypeRepository _sparePartsTypeRepository;
        private readonly IWareHousesRepository _wareHousesRepository;
        private readonly IMotorRepository _motorRepository;
        public OutHouseController(IOutHouseRepository outHouseRepository,
            ISparePartsTypeRepository sparePartsTypeRepository,
            IMotorRepository motorRepository,
               IWareHousesRepository wareHousesRepository,
               IInHouseRepository inHouseRepository)
        {
            _inHouseRepository = inHouseRepository;
            _wareHousesRepository = wareHousesRepository;
            _outHouseRepository = outHouseRepository;
            _motorRepository = motorRepository;
            _sparePartsTypeRepository = sparePartsTypeRepository;
        }
        // GET: api/OutHouse
        [EnableCors("any")]
        [HttpGet]
        [Route("OutHouseList")]
        public dynamic Get(int pageindex, int pagesize, string productionLineId)
        {
            try
            {
                var source = new List<dynamic>();
                var wareHouses = _wareHousesRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
                if (!(wareHouses?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                var wareHouseIds = wareHouses.Select(e => e.Id);
                var batchNos = _inHouseRepository.GetEntities(e => wareHouseIds.Contains(e.WareHousesId))?.Select(e => e.Id)?.ToList();
                if (!(batchNos?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                var datas = _outHouseRepository.GetEntities(e =>!e.IsDelete&& batchNos.Contains(e.BatchNo))?.ToList();

                if (!datas?.Any() ?? true) return new PaginatedList<dynamic>(0, 0, 0, source);
                var spares = _sparePartsTypeRepository.GetEntities()?.ToList();

                if (!spares?.Any() ?? true) return new PaginatedList<dynamic>(0, 0, 0, source);
                var motors = _motorRepository.GetEntities()?.ToList();
                if (!(motors?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                // var source = OutHouse.Froms(datas, spares, motors);
                datas.ForEach(d => {
                    var sparePartsTypeName = spares.Where(e => e.Id == d.SparePartsTypeId)?.FirstOrDefault()?.Name;
                    var wareHousesName = wareHouses.Where(e => e.Id == d.WareHousesId)?.FirstOrDefault()?.Name;
                    var motorName = motors.Where(e => e.MotorId.Equals(d.MotorId))?.FirstOrDefault()?.Name;
                    source.Add(new
                    {
                        d.Id,
                        d.OutOperator,
                        d.OutTime,
                        d.UselessTime,
                        d.MotorId,
                        d.BatchNo,
                        MotorName= motorName,
                        d.SparePartsTypeId,
                        SparePartsTypeName = sparePartsTypeName,
                        WareHousesName = wareHousesName,
                        d.UnitPrice,
                        d.WareHousesId,
                        d.IsDelete
                    });
                });
                var list = source.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
                return new PaginatedList<dynamic>(pageindex, pagesize, source.Count(), list);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>());
            }
        }
        [EnableCors("any")]
        [HttpGet()]
        [Route("MotorList")]
        public dynamic MotorList(int wareHouseId,string productionLineId)
        {
            try
            {
                var wareHouse = _wareHousesRepository.GetEntities(e => e.Id == wareHouseId)?.FirstOrDefault() ?? null;
                if (wareHouse == null)
                    return new List<dynamic>();
                var motorTypeId = wareHouse.MotorTypeId;
                var motors = new List<Motor>();
                if(motorTypeId.Equals("Universal"))
                    motors =_motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
                else
                    motors = _motorRepository.GetEntities(e => e.MotorTypeId.Equals(motorTypeId))?.ToList();
                if (!(motors?.Any() ?? false)) return new List<dynamic>();
                return motors.Select(e => new { e.MotorId, e.Name });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new List<dynamic>();
            }

        }
     

        // POST: api/OutHouse ==> 出库
        [EnableCors("any")]
        [HttpPost]
        public bool Post([FromBody]OutInventories value)//dynamic
        {
            try
            {
                var exist = _inHouseRepository.GetEntities(e=>e.Id==value.InHouseId)?.FirstOrDefault()??null;
                if (exist==null) return false;
                exist.Remains--;
                exist.Time = DateTime.Now.TimeSpan();
                _inHouseRepository.UpdateEntityAsync(exist);
                var entity = new Inventory.Domain.Model.OutHouse()
                {
                    WareHousesId=exist.WareHousesId,
                    SparePartsTypeId=exist.SparePartsTypeId,
                    OutOperator = value.OutOperator,
                    MotorId=value.MotorId,
                    BatchNo=exist.Id,
                    UnitPrice= exist.UnitPrice,
                    SparePartsStatus = SparePartsTypeStatus.Using,
                    OutTime=value.OutTime,
                    Time = DateTime.Now.TimeSpan(),
                };

                 _outHouseRepository.InsertAsync(entity);
                 _outHouseRepository.Batch();
                return true;
            }
            catch (Exception ex)
            {

                Logger.Exception(ex);
                return false;
            }

            //return false;
        }

        // PUT: api/OutHouse/5  ==>报废
        [EnableCors("any")]
        [HttpPut()]
        public bool Put(int id)//dynamic
        {
            try
            {
                if (id == 0)
                    return false;
                var value = _outHouseRepository.GetEntities(e=>e.Id==id)?.FirstOrDefault()??null;
                if (value == null)
                    return false;
                value.SparePartsStatus = SparePartsTypeStatus.Useless;
                value.UselessTime = DateTime.Now.TimeSpan();
                value.Time = DateTime.Now.TimeSpan();
                return _outHouseRepository.UpdateEntity(value) > 0;
            }
            catch (Exception ex)
            {

                Logger.Exception(ex);
                return false;
            }
        }

        // DELETE: api/ApiWithActions/5
        [EnableCors("any")]
        [HttpDelete()]
        public bool Delete(int id)
        {
            try
            {

                var model = _outHouseRepository.GetEntities(e => e.Id == id)?.FirstOrDefault() ?? null;
                if (model == null)
                    return false;
                model.IsDelete = true;
                model.Time = DateTime.Now.TimeSpan();
                return _outHouseRepository.UpdateEntity(model) > 0;
            }
            catch (Exception ex)
            {

                Logger.Exception(ex);
                return false;
            }
        }
    }
}
