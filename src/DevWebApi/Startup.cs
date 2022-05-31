using DevApplication.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Metrics.Extensions;
using Rn.NetCore.WebCommon.Filters;
using Rn.NetCore.WebCommon.Helpers;
using Rn.NetCore.WebCommon.Middleware;
using Rn.NetCore.WebCommon.Providers;
using Rn.NetCore.WebCommon.Services;

namespace DevWebApi;

public class Startup
{
  public IConfiguration Configuration { get; }

  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddSwaggerDocument();

    services
      // Abstractions
      .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
      .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()

      // Helpers
      .AddSingleton<IJsonHelper, JsonHelper>()
      .AddSingleton<IJwtTokenHelper, JwtTokenHelper>()

      // Providers
      .AddSingleton<IRnWebCoreConfigProvider, RnWebCoreConfigProvider>()

      // Metrics
      .AddRnMetricsBase(Configuration)

      // Logging
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

    // Consumer specific implementations
    services
      .AddSingleton<IUserServiceBase, UserService>();

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
      app.UseOpenApi(settings =>
      {
        settings.Path = "/swagger/v1/swagger.json";
      });
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevWebApi v1"));
    }

    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseMiddleware<ApiMetricsMiddleware>();
    app.UseMiddleware<JwtMiddleware>();
    app.UseAuthorization();
    app.UseCors(builder =>
    {
      builder.AllowAnyHeader();
      builder.AllowAnyOrigin();
    });

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });
  }
}
