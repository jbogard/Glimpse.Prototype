﻿using Glimpse.Common.Initialization;
using Glimpse.Platform;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GlimpseDnxServiceCollectionExtensions
    {
        public static IGlimpseBuilder AddGlimpse(this IServiceCollection services)
        {
            return services.AddGlimpse(true);
        }

        public static IGlimpseBuilder AddGlimpse(this IServiceCollection services, bool autoRegisterComponents)
        {
            //
            // Platform
            //
            services.AddSingleton(PlatformServices.Default.AssemblyLoadContextAccessor);
            services.AddSingleton(PlatformServices.Default.AssemblyLoaderContainer);
            services.AddSingleton(PlatformServices.Default.LibraryManager);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAssemblyProvider, LibraryManagerAssemblyProvider>();
            services.AddSingleton<IExtensionProvider<IRegisterMiddleware>, DefaultExtensionProvider<IRegisterMiddleware>>();
            
            return (IGlimpseBuilder)services.AddGlimpseCore(autoRegisterComponents);
        }
    }
}