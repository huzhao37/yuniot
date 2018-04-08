using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Common;

namespace Yunt.WebApi.Controllers
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
                //ServiceProviderServiceExtensions.GetService<IUserRepository>(Startup.Providers["Auth"]); 
        }
        // GET: api/User
        [HttpGet]
        public PaginatedList<User> Get(int pageindex,int pagesize)
        {        
            return  _userRepository.GetPage(pageindex, pagesize);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/User
        [HttpPost]
        public bool Post([FromBody]User value)//dynamic
        {
             return _userRepository.Insert(value)>0;    
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
