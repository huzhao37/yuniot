using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Auth.Domain
{
   public interface IBootStrap
   {
        IServiceProvider Start(IServiceCollection services);

   }
}
