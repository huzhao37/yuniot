using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/UnitsIn")]
    // [Authorize]
    public class UnitsInController : ControllerBase
    {

        [HttpGet]
        [Route("Motor")]
        public UnitsInResponseModel Motor()
        {
            List<MotorSP> mslist = new List<MotorSP>();
            MotorSP ms1 = new MotorSP(){ id=1, Name="颚式破碎机1" };
            MotorSP ms2 = new MotorSP(){ id=2, Name="皮带机2" };
            mslist.Add(ms1);
            mslist.Add(ms2);
            UnitsInResponseModel res = new UnitsInResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                mdata = mslist
            };
            return res;
        }

        [HttpGet]
        public UnitsInResponseModel Get(int skip, int take=0)
        {
            List<UnitsInViewModel> mslist = new List<UnitsInViewModel>();
            UnitsInViewModel ms0 = new UnitsInViewModel(){ id=0, SparePartTypeId=11, SparePartTypeName="颚破衬板", BatchNo=1002, WareHouseId=1, WareHouseName="颚破仓库", Count=50, UnitPrice=17.5f, FactoryInfo="A工厂", InTime="2017/06/09", InOperator="于建峰"};
            UnitsInViewModel ms1 = new UnitsInViewModel(){ id=1, SparePartTypeId=13, SparePartTypeName="颚破钢架", BatchNo=1004, WareHouseId=1, WareHouseName="颚破仓库", Count=60, UnitPrice=23.2f, FactoryInfo="B工厂", InTime="2017/06/09", InOperator="于建峰"};
            UnitsInViewModel ms2 = new UnitsInViewModel(){ id=2, SparePartTypeId=12, SparePartTypeName="皮带机轴承", BatchNo=1003, WareHouseId=2, WareHouseName="皮带机仓库", Count=70, UnitPrice=8.2f, FactoryInfo="C工厂", InTime="2017/06/09", InOperator="于建峰"};
            
            mslist.Add(ms0);
            mslist.Add(ms1);
            mslist.Add(ms2);

            List<UnitsInViewModel> wmslist;
            wmslist = (0 == take) ?  mslist : mslist.Skip(skip).Take(take).ToList();

            UnitsInResponseModel res = new UnitsInResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                data = wmslist
            };
            return res;
        }

        [HttpPut]
        public UnitsInResponseModel Put([FromBody]UnitsInRequestModel value)
        {
            UnitsInResponseModel res = new UnitsInResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
        
        [HttpDelete]
        public UnitsInResponseModel Delete(int id)
        {
            UnitsInResponseModel res = new UnitsInResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
    }



    public class UnitsInResponseModel
    {
        public CodeModel status {get;set;}
        public List<UnitsInViewModel> data {get;set;}
        public List<MotorSP> mdata {get;set;}
    }

    public class UnitsInRequestModel
    {
        public int SparePartTypeId {get;set;}
        public int WareHouseId {get;set;}
        public int Count {get;set;}
        public float UnitPrice {get;set;}
        public string FactoryInfo {get;set;}
        public string InTime {get;set;}
        public string InOperator {get;set;}
    }


    public class UnitsInViewModel
    {
        public int id {get;set;}
        public int SparePartTypeId {get;set;}
        public string SparePartTypeName {get;set;}
        public int BatchNo {get;set;}
        public int WareHouseId {get;set;}
        public string WareHouseName {get;set;}
        public int Count {get;set;}
        public float UnitPrice {get;set;}
        public string FactoryInfo {get;set;}
        public string InTime {get;set;}
        public string InOperator {get;set;}
    }

    public class MotorSP
    {
        public int id {get;set;}
        public string Name {get;set;}
    }
}