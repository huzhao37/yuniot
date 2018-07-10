using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/UnitOut")]
    // [Authorize]
    public class UnitOutController : ControllerBase
    {
        [HttpGet]
        public UnitOutResponseModel Get(int skip, int take=0)
        {
            List<UnitOutViewModel> mslist = new List<UnitOutViewModel>();
            UnitOutViewModel ms0 = new UnitOutViewModel(){ id=1002, BatchNo=1002, SparePartTypeId=11, SparePartTypeName="颚破衬板", MotorId=1013, MotorName="颚破1", SparePartTypeStatus="出库", OutTime="2018/05/02", UselessTime="", OutOperator="于建峰" };
            UnitOutViewModel ms1 = new UnitOutViewModel(){ id=1002, BatchNo=1002, SparePartTypeId=11, SparePartTypeName="颚破衬板", MotorId=1013, MotorName="颚破1", SparePartTypeStatus="废弃", OutTime="2018/05/02", UselessTime="2018/07/02", OutOperator="于建峰" };
            UnitOutViewModel ms2 = new UnitOutViewModel(){ id=1003, BatchNo=1003, SparePartTypeId=12, SparePartTypeName="皮带秤轴承", MotorId=1083, MotorName="主皮带", SparePartTypeStatus="出库", OutTime="2018/04/02", UselessTime="", OutOperator="于建峰" };

            mslist.Add(ms0);
            mslist.Add(ms1);
            mslist.Add(ms2);

            List<UnitOutViewModel> wmslist;
            wmslist = (0 == take) ?  mslist : mslist.Skip(skip).Take(take).ToList();

            UnitOutResponseModel res = new UnitOutResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                data = wmslist
            };
            return res;
        }

        [HttpPut]
        public UnitOutResponseModel Put([FromBody]UnitOutRequestModel value)
        {
            UnitOutResponseModel res = new UnitOutResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
        
        [HttpDelete]
        public UnitOutResponseModel Delete(int id)
        {
            UnitOutResponseModel res = new UnitOutResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
    }



    public class UnitOutResponseModel
    {
        public CodeModel status {get;set;}
        public List<UnitOutViewModel> data {get;set;}
    }

    public class UnitOutRequestModel
    {
        public int SparePartTypeId {get;set;}
        public int BatchNo {get;set;}
        public int MotorId {get;set;}
        public string SparePartTypeStatus {get;set;}
        public string UselessTime {get;set;}
        public string OutTime {get;set;}
        public string OutOperator {get;set;}
    }


    public class UnitOutViewModel
    {
        public int id {get;set;}
        public int SparePartTypeId {get;set;}
        public string SparePartTypeName {get;set;}
        public string SparePartTypeStatus {get;set;}
        public int BatchNo {get;set;}
        public int MotorId {get;set;}
        public string MotorName {get;set;}
        public string UselessTime {get;set;}
        public string OutTime {get;set;}
        public string OutOperator {get;set;}
    }
}