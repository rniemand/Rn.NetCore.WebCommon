using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.WebCommon.Controllers
{
  public class AuthControllerBase<TController> : ControllerBase
  {
    public ILoggerAdapter<TController> Logger { get; set; }

    public AuthControllerBase(IServiceProvider serviceProvider)
    {
      // TODO: [TESTS] (AuthControllerBase) Add tests
      Logger = serviceProvider.GetRequiredService<ILoggerAdapter<TController>>();
    }
  }
}
