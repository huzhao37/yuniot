using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/SparePartType")]
    // [Authorize]
    public class SparePartTypeController : ControllerBase
    {
       
        [HttpGet]
        public SparePartTypeResponseModel Get(int skip, int take=0)
        {
            List<SparePartTypeViewModel> mslist = new List<SparePartTypeViewModel>();
            SparePartTypeViewModel ms0 = new SparePartTypeViewModel(){ id=0, Name="颚破衬板", Threshold=180, CreateTime="2018/05/02" };
            SparePartTypeViewModel ms1 = new SparePartTypeViewModel(){ id=1, Name="颚破钢架", Threshold=230, CreateTime="2018/04/02" };
            SparePartTypeViewModel ms2 = new SparePartTypeViewModel(){ id=2, Name="皮带机轴承", Threshold=300, CreateTime="2018/05/02" };
            
            mslist.Add(ms0);
            mslist.Add(ms1);
            mslist.Add(ms2);

            List<SparePartTypeViewModel> wmslist;
            wmslist = (0 == take) ?  mslist : mslist.Skip(skip).Take(take).ToList();

            SparePartTypeResponseModel res = new SparePartTypeResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                data = wmslist
            };
            return res;
        }

        [HttpPut]
        public SparePartTypeResponseModel Put([FromBody]SparePartTypeRequestModel value)
        {
            SparePartTypeResponseModel res = new SparePartTypeResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }
        
        [HttpDelete]
        public SparePartTypeResponseModel Delete(int id)
        {
            SparePartTypeResponseModel res = new SparePartTypeResponseModel(){status = new CodeModel(){code = 200, desc = "ok"}, data = null };
            return res;
        }



        [HttpGet]
        [Route("BatchSetThreshold")]
        public SparePartTypeResponseModel BatchSetThreshold()
        {
            SparePartTypeResponseModel res = new SparePartTypeResponseModel()
            {
                status = new CodeModel(){code=200, desc="ok"}, 
                data = null
            };
            return res;
        }

    }




    public class SparePartTypeResponseModel
    {
        public CodeModel status {get;set;}
        public List<SparePartTypeViewModel> data {get;set;}
    }
    public class SparePartTypeRequestModel
    {
        public string Name {get;set;}
        public int Threshold {get;set;}
        public string CreateTime {get;set;}
    }
    public class SparePartTypeViewModel
    {
        public int id {get;set;}
        public string Name {get;set;}
        public int Threshold {get;set;}
        public string CreateTime {get;set;}
    }
}