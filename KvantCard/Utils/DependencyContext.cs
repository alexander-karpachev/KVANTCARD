using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace KvantCard.Utils
{
    public static class DependencyContext
    {

        private static readonly object L = new object();

        public static void AddAutoMapper(this IServiceCollection services)
        {
            lock (L)
            {
                List<Type> allTypes;
                List<Type> profiles;

                try
                {
                    var type = typeof(Profile);
                    allTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(e => e.FullName.ToUpperInvariant().StartsWith("PD2.", StringComparison.InvariantCulture))
                        .SelectMany(s => s.GetTypes())
                        .Where(p => !p.IsInterface)
                        .ToList();
                    profiles = allTypes
                        .Where(p => type.IsAssignableFrom(p))
                        .Where(t => !t.GetTypeInfo().IsAbstract)
                        .ToList();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    var sb = new StringBuilder();
                    foreach (var exSub in ex.LoaderExceptions)
                    {
                        sb.AppendLine(exSub.Message);
                        var exFileNotFound = exSub as FileNotFoundException;
                        if (!string.IsNullOrEmpty(exFileNotFound?.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                        sb.AppendLine();
                    }
                    var errorMessage = sb.ToString();
                    //Display or log the error based on your application.
                    throw new Exception(errorMessage);
                }

                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });

                //Mapper.Initialize(cfg =>
                //{
                //    foreach (var profile in profiles)
                //    {
                //        cfg.AddProfile(profile);
                //    }
                //});

                var openTypes = new[]
                {
                    typeof(IValueResolver<,,>),
                    typeof(IMemberValueResolver<,,,>),
                    typeof(ITypeConverter<,>)
                };
                foreach (var openType in openTypes)
                {
                    foreach (var type in allTypes
                        .Where(t => t.GetTypeInfo().IsClass)
                        .Where(t => !t.GetTypeInfo().IsAbstract)
                        .Where(t => PrimitiveHelper.ImplementsGenericInterface(t, openType)))
                    {
                        services.AddTransient(type);
                    }
                }

                services.AddSingleton<IConfigurationProvider>(config);
                //services.TryAddSingleton<IMapper>(sp => new Mapper(config, sp.GetService));
                services.AddSingleton<IMapper>(sp =>
                    new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            }
        }
    }
}
