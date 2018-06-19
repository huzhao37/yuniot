using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/WareHouses")]
    // [Authorize]
    public class WareHousesController : ControllerBase
    {
       
        [HttpGet]
        [Route("MotorTypeId")]
        public WareHouseResponseModel MotorTypeId()
        {
            List<MotorTypeClass> mslist = new List<MotorTypeClass>();
            MotorTypeClass ms1 = new MotorTypeClass(){ id=1, MotorTypeName="颚式破碎机" };
            MotorTypeClass ms2 = new MotorTypeClass(){ id=2, MotorTypeName="皮带机" };
            mslist.Add(ms1);
            mslist.Add(ms2);
            WareHouseResponseModel res = new WareHouseResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                mtdata = mslist
            };
            return res;
        }

        [HttpGet]
        public WareHouseResponseModel Get(int skip, int take=0)
        {
            List<WareHouseViewModel> mslist = new List<WareHouseViewModel>();
            WareHouseViewModel ms0 = new WareHouseViewModel(){ id=0, WareHouseId=0, Name="颚破仓库", MotorTypeId=7, MotorTypeName="颚破", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms1 = new WareHouseViewModel(){ id=1, WareHouseId=1, Name="颚破仓库", MotorTypeId=1, MotorTypeName="颚破", Keeper="董斌", CreateTime="2018/04/02" };
            WareHouseViewModel ms2 = new WareHouseViewModel(){ id=2, WareHouseId=2, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms3 = new WareHouseViewModel(){ id=3, WareHouseId=3, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms4 = new WareHouseViewModel(){ id=4, WareHouseId=4, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms5 = new WareHouseViewModel(){ id=5, WareHouseId=5, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms6 = new WareHouseViewModel(){ id=6, WareHouseId=6, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms7 = new WareHouseViewModel(){ id=7, WareHouseId=7, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms8 = new WareHouseViewModel(){ id=8, WareHouseId=8, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
            WareHouseViewModel ms9 = new WareHouseViewModel(){ id=9, WareHouseId=9, Name="皮带机仓库", MotorTypeId=7, MotorTypeName="皮带机", Keeper="于剑峰", CreateTime="2018/05/02" };
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

            List<WareHouseViewModel> wmslist;
            wmslist = (0 == take) ?  mslist : mslist.Skip(skip).Take(take).ToList();

            WareHouseResponseModel res = new WareHouseResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                data = wmslist
            };
            return res;
        }

        [HttpPut]
        public WareHouseResponseModel Put([FromBody]WareHouseRequestModel value)
        {
            WareHouseResponseModel res = new WareHouseResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
        
        [HttpDelete]
        public WareHouseResponseModel Delete(int id)
        {
            WareHouseResponseModel res = new WareHouseResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
    }

    public class WareHouseResponseModel
    {
        public CodeModel status {get;set;}
        public List<WareHouseViewModel> data {get;set;}
        public List<MotorTypeClass> mtdata {get;set;}
    }

    public class WareHouseRequestModel
    {
        public string Name {get;set;}
        public int MotorTypeId {get;set;}
        public string Keeper {get;set;}
        public string CreateTime {get;set;}
    }

    
    public class MotorTypeClass
    {
        public int id {get;set;}
        public string MotorTypeName {get;set;}
    }

    public class WareHouseViewModel
    {
        public int id {get;set;}
        public string Name {get;set;}
        public int MotorTypeId {get;set;}
        public string MotorTypeName {get;set;}
        public int WareHouseId {get;set;}
        public string Keeper {get;set;}
        public string CreateTime {get;set;}
    }    
}