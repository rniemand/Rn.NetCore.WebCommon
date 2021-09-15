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
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Outputs;
using Rn.NetCore.Common.Services;
using Rn.NetCore.WebCommon.Services;

namespace DevConsole
{
  class Program
  {
    private static IServiceProvider _serviceProvider;
    private static ILoggerAdapter<Program> _logger;

    static void Main(string[] args)
    {
      ConfigureDI();

      var path = _serviceProvider.GetRequiredService<IPathAbstraction>();

      var tempFileName = path.GetTempFileName();

      _logger.Info("All Done!");
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

        // Services
        .AddSingleton<IEncryptionService, EncryptionService>()

        // Helpers
        .AddSingleton<IEncryptionHelper, EncryptionHelper>()
        .AddSingleton<IJsonHelper, JsonHelper>()

        // Abstractions
        .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
        .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
        .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
        .AddSingleton<IFileAbstraction, FileAbstraction>()
        .AddSingleton<IPathAbstraction, PathAbstraction>()

        // Metrics
        .AddSingleton<IMetricServiceUtils, MetricServiceUtils>()
        .AddSingleton<IMetricService, MetricService>()
        .AddSingleton<IMetricOutput, CsvMetricOutput>()

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
}
