using System;
using System.IO;
using DevApplication.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Metrics.Extensions;
using Rn.NetCore.WebCommon.Helpers;
using Rn.NetCore.WebCommon.Providers;
using Rn.NetCore.WebCommon.Services;

namespace DevConsole;

class Program
{
  private static IServiceProvider _serviceProvider;
  private static ILoggerAdapter<Program> _logger;

  static void Main(string[] args)
  {
    ConfigureDI();

    var path = _serviceProvider.GetRequiredService<IPathAbstraction>();

    var tempFileName = path.GetTempFileName();

    _logger.LogInformation("All Done!");
  }


  // DI related methods
  private static void ConfigureDI()
  {
    var services = new ServiceCollection();

    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .Build();

    services
      // Configuration
      .AddSingleton(config)

      // Helpers
      .AddSingleton<IJsonHelper, JsonHelper>()
      .AddSingleton<IJwtTokenHelper, JwtTokenHelper>()

      // Abstractions
      .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
      .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
      .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()

      // Providers
      .AddSingleton<IRnWebCoreConfigProvider, RnWebCoreConfigProvider>()

      // Metrics
      .AddRnMetricsBase(config)

      // Logging
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
      .AddLogging(loggingBuilder =>
      {
        // configure Logging with NLog
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      });

    // Consumer specific implementations
    services
      .AddSingleton<IUserServiceBase, UserService>();

    _serviceProvider = services.BuildServiceProvider();
    _logger = _serviceProvider.GetService<ILoggerAdapter<Program>>();
  }
}
