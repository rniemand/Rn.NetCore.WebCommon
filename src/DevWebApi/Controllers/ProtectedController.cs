using System;
using Rn.NetCore.WebCommon.Controllers;

namespace DevWebApi.Controllers
{
  public class ProtectedController : RnBaseController<ProtectedController>
  {
    public ProtectedController(IServiceProvider serviceProvider)
      : base(serviceProvider)
    {
      // TODO: [TESTS] (ProtectedController) Add tests
    }
  }
}
