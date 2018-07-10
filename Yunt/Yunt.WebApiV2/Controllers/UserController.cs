using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Common;

namespace Yunt.WebApiV2.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: api/User
        [HttpGet]
        [EnableCors("any")]
        public PaginatedList<User> Get(int pageindex,int pagesize)
        {
            try
            {
                return _userRepository.GetPage(pageindex, pagesize,null,e=>e.Time);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<User>(0, 0, 0, new List<User>());
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/User
        [HttpPost]
        [EnableCors("any")]
        public bool Post([FromBody]User value)//dynamic
        {
            try
            {
                if (value == null)
                    return false;
                value.Time = DateTime.Now.TimeSpan();
                return _userRepository.Insert(value) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            } 
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        [EnableCors("any")]
        public bool Put([FromBody]User value)
        {
            try
            {
                if (value == null)
                    return false;
                value.Time = DateTime.Now.TimeSpan();
                return _userRepository.UpdateEntity(value) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            try
            {
                if (id.IsNullOrWhiteSpace())
                    return false;
                return _userRepository.DeleteEntity(e => e.UserId.Equals(id)) > 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }

        }
    }
}
