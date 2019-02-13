using System;
using System.Collections.Generic;
using System.Text;
using KvantShared.Repos;
using KvantShared.Services;
using KvantShared.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace KvantShared
{
    public static class ServiceRegister
    {
        public static void RegisterSharedServices(this IAppStarter starter, IServiceCollection services)
        {
            services.AddScoped<ReferenceService>();
            services.AddSingleton<UnitOfWorkFactory>();
        }
    }
}
