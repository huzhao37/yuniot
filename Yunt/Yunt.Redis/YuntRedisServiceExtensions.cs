using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yunt.Common;
using Yunt.Redis.Config;

namespace Yunt.Redis
{
   public static class YuntRedisServiceExtensions
    {
        /// <summary>
        /// Adds the default redis cache.
        /// </summary>
        /// <returns>The default redis cache.</returns>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">Options Action.</param>
        public static IServiceCollection AddDefaultRedisCache(this IServiceCollection services, Action<RedisCacheOptions> optionsAction)
        {
            ArgumentCheck.NotNull(services, nameof(services));
            ArgumentCheck.NotNull(optionsAction, nameof(optionsAction));

            services.AddOptions();
            services.Configure(optionsAction);
            services.TryAddSingleton<IRedisCachingProvider, RedisCachingProvider>();
           // services.TryAddSingleton<IRedisCachingProvider, RedisCachingProvider>();

            return services;
        }

    }
}
