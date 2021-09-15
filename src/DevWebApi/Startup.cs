using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.WebCommon.Filters;
using Rn.NetCore.WebCommon.Middleware;

namespace DevWebApi
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        // Abstractions
        .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
        .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
        .AddSingleton<IFileAbstraction, FileAbstraction>()
        .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
        .AddSingleton<IPathAbstraction, PathAbstraction>()

        // Helpers
        .AddSingleton<IJsonHelper, JsonHelper>()

        // Metrics
        .AddSingleton<IMetricServiceUtils, MetricServiceUtils>()
        .AddSingleton<IMetricService, MetricService>()

        // Logging
        .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

      services.AddControllers(options =>
      {
        options.Filters.Add<ApiMetricActionFilter>();
        options.Filters.Add<ApiMetricExceptionFilter>();
        options.Filters.Add<ApiMetricResultFilter>();
        options.Filters.Add<ApiMetricResourceFilter>();
      });

      services.AddSwaggerGen(c => { c
        .SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "DevWebApi",
          Version = "v1"
        });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevWebApi v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseMiddleware<ApiMetricsMiddleware>();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
