using Eiriklb.WpfTools.Logviewer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiriklb.WpfTools
{
    public static class ServiceCollectionExtension
    {
        public static void AddWpfTools(this IServiceCollection services)
        {

            services.AddScoped<ILogger, WpfLogger>();
            services.AddScoped<LogViewer, LogViewer>();
            services.AddScoped<LogViewerVm, LogViewerVm>();
        }
    }
}
