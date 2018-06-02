using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Common.ObjectExtentsions;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;

namespace Yunt.WebApi.Controllers
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
        [EnableCors("any")]
        [HttpGet]
        //[Route("InHouseList")]
        public dynamic Get(int pageindex, int pagesize, string productionLineId)
        {
            try
            {
                var source = new List<dynamic>();
                var wareHouses = _wareHousesRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
                if (!(wareHouses?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                var wareHouseIds = wareHouses.Select(e => e.Id);
                var datas = _inHouseRepository.GetEntities(e => wareHouseIds.Contains(e.WareHousesId) && !e.IsDelete)?.ToList();
                if (!(datas?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                var spares = _sparePartsTypeRepository.GetEntities()?.ToList();
                if (!(spares?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);

                datas.ForEach(d =>
                {
                    var sparePartsTypeName = spares.Where(e => e.Id == d.SparePartsTypeId)?.FirstOrDefault()?.Name;
                    var wareHousesName = wareHouses.Where(e => e.Id == d.WareHousesId)?.FirstOrDefault()?.Name;
                    source.Add(new
                    {
                        d.Id,
                        d.InOperator,
                        d.InTime,
                        d.FactoryInfo,
                        d.Count,
                        //d.BatchNo,
                        d.Remains,
                        d.SparePartsTypeId,
                        SparePartsTypeName = sparePartsTypeName,
                        WareHousesName = wareHousesName,
                        d.UnitPrice,
                        d.WareHousesId,
                    });
                });
                //var source = InHouse.Froms(datas, spares, wareHouses);
                var list = source.OrderBy(x => x.Id).Skip((pageindex - 1) * pagesize).Take(pagesize);
                return new PaginatedList<dynamic>(pageindex, pagesize, source.Count(), list);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>());
            }
        }

        [HttpGet]
        [EnableCors("any")]//==>盘库
        [Route("CheckInventory")]
        public dynamic CheckInventory(int pageindex, int pagesize, string productionLineId)
        {
            try
            {
                var source = new List<dynamic>();
                var wareHouses = _wareHousesRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
                if (!(wareHouses?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                var wareHouseIds = wareHouses.Select(e => e.Id);
                var datas = _inHouseRepository.GetEntities(e => wareHouseIds.Contains(e.WareHousesId) && !e.IsDelete)//?.ToList();
                                ?.GroupBy(e => e.SparePartsTypeId)?.ToList();
                if (!(datas?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                var spares = _sparePartsTypeRepository.GetEntities()?.ToList();
                if (!(spares?.Any() ?? false)) return new PaginatedList<dynamic>(0, 0, 0, source);
                //var source = InHouse.Froms(datas, spares, wareHouses);        
                datas.ForEach(x =>
                {
                    var sparePartTypeId = x.Key;
                    var list = x?.ToList()?.GroupBy(e => e.WareHousesId)?.ToList();
                    if (list != null && list.Any())
                    {
                        list.ForEach(y =>
                        {
                            var wareHouseId = y.Key;
                            var list2 = y?.ToList();
                            if (list2 != null && list2.Any())
                            {
                                var remains = list2.Sum(e => e.Remains);
                                var sparePartsTypeName = spares.Where(e => e.Id == sparePartTypeId)?.FirstOrDefault()?.Name;
                                var wareHousesName = wareHouses.Where(e => e.Id == wareHouseId)?.FirstOrDefault()?.Name;
                                source.Add(new
                                {
                                    WareHousesId = wareHouseId,
                                    SparePartsTypeId = sparePartTypeId,
                                    Remains = remains,
                                    SparePartsTypeName = sparePartsTypeName,
                                    WareHousesName = wareHousesName,

                                });


                            }

                        });

                    }

                });
                // var result = source.Select(e => new {e.Id,e.WareHousesId,e.WareHousesName,e.SparePartsTypeId,e.SparePartsTypeName,e.Remains });
                var results = source.OrderBy(x => (int)x.WareHousesId).Skip((pageindex - 1) * pagesize).Take(pagesize);
                return new PaginatedList<dynamic>(pageindex, pagesize, source.Count(), results);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>());
            }
        }
        [EnableCors("any")]
        [HttpGet()]
        [Route("InventoryList")]
        public dynamic InventoryList(int wareHouseId, int sparePartTypeId)
        {
            try
            {
                var results = new List<dynamic>();
                var wareHouse = _wareHousesRepository.GetEntities(e => e.Id== wareHouseId)?.FirstOrDefault()??null;
                if (wareHouse==null) return results;
                var sparePartType = _sparePartsTypeRepository.GetEntities(e => e.Id == sparePartTypeId)?.FirstOrDefault() ?? null;
                if (sparePartType == null) return results;
                var datas = _inHouseRepository.GetEntities(e => e.WareHousesId== wareHouseId && 
                                e.SparePartsTypeId== sparePartTypeId && e.Remains > 0 && !e.IsDelete)?.ToList();
                if (!(datas?.Any() ?? false)) return results;
                datas.ForEach(d =>
                {
                    var sparePartsTypeName = sparePartType.Name;
                    var wareHousesName = wareHouse.Name;
                    results.Add(new
                    {
                        d.Id,
                        //d.BatchNo,
                        d.SparePartsTypeId,
                        SparePartsTypeName = sparePartsTypeName,
                        WareHousesName = wareHousesName,
                        WareHouseId = d.WareHousesId,
                        d.UnitPrice,
                        d.InOperator,
                        d.InTime,
                        d.FactoryInfo,
                        d.Description,
                        d.Remains
                    });

                });
                return results;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new List<dynamic>();
            }

        }




        // POST: api/InHouse ==>入库
        [EnableCors("any")]
        [HttpPost]
        public bool Post([FromBody]InHouse value)
        {
            try
            {
                if (value.SparePartsTypeId == 0 || value.WareHousesId == 0  ||
                    value.Count == 0)
                    return false;
                value.Time = DateTime.Now.TimeSpan();
                value.Remains = value.Count;
                return _inHouseRepository.Insert(value) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        // PUT: api/InHouse/5

        [HttpPut("{id}")]
        public bool Put([FromBody]InHouse value)//dynamic
        {
            return false;// _inHouseRepository.UpdateEntity(value.Copy() as Inventory.Domain.Model.InHouse) > 0;
        }

        // DELETE: api/ApiWithActions/5
        [EnableCors("any")]
        [HttpDelete()]
        public bool Delete(long id)
        {
            try
            {
                var model = _inHouseRepository.GetEntities(e => e.Id == id)?.FirstOrDefault() ?? null;
                if (model == null)
                    return false;
                model.IsDelete = true;
                model.Time = DateTime.Now.TimeSpan();
                return _inHouseRepository.UpdateEntity(model) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }
    }
}
