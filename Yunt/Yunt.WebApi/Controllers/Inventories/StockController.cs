using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Stock")]
    // [Authorize]
    public class StockController : ControllerBase
    {

        [HttpGet]
        public ResponseModel Get(int skip, int take=0)
        {
            List<StockViewModel> mslist = new List<StockViewModel>();
            StockViewModel ms0 = new StockViewModel(){ id=0, WareHouseId=0, WareHouseName="颚破仓库", SparePartTypeId=7, SparePartTypeName="颚破", Remains=230};
            StockViewModel ms1 = new StockViewModel(){ id=1, WareHouseId=1, WareHouseName="颚破仓库", SparePartTypeId=1, SparePartTypeName="颚破", Remains=170};
            StockViewModel ms2 = new StockViewModel(){ id=2, WareHouseId=2, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=80};
            StockViewModel ms3 = new StockViewModel(){ id=3, WareHouseId=3, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=480};
            StockViewModel ms4 = new StockViewModel(){ id=4, WareHouseId=4, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=180};
            StockViewModel ms5 = new StockViewModel(){ id=5, WareHouseId=5, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=380};
            StockViewModel ms6 = new StockViewModel(){ id=6, WareHouseId=6, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=210};
            StockViewModel ms7 = new StockViewModel(){ id=7, WareHouseId=7, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=270};
            StockViewModel ms8 = new StockViewModel(){ id=8, WareHouseId=8, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=230};
            StockViewModel ms9 = new StockViewModel(){ id=9, WareHouseId=9, WareHouseName="皮带机仓库", SparePartTypeId=7, SparePartTypeName="皮带机", Remains=280};
            mslist.Add(ms0);
            mslist.Add(ms1);
            mslist.Add(ms2);
            mslist.Add(ms3);
            mslist.Add(ms4);
            mslist.Add(ms5);
            mslist.Add(ms6);
            mslist.Add(ms7);
            mslist.Add(ms8);
            mslist.Add(ms9);
    
            List<StockViewModel> wmslist;
            wmslist = (0 == take) ?  mslist : mslist.Skip(skip).Take(take).ToList();

            ResponseModel res = new ResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                data = wmslist
            };
            return res;
        }

    }



    public class ResponseModel
    {
        public CodeModel status {get;set;}
        public List<StockViewModel> data {get;set;}
    }


    public class StockViewModel
    {
        public int id {get;set;}
        public int WareHouseId {get;set;}
        public string WareHouseName {get;set;}
        public int SparePartTypeId {get;set;}
        public string SparePartTypeName {get;set;}
        public int Remains {get;set;}
    }
}