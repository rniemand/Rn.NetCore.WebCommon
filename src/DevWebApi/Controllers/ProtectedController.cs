using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rn.NetCore.WebCommon.Attributes;
using Rn.NetCore.WebCommon.Controllers;
using Rn.NetCore.WebCommon.Models.Responses;

namespace DevWebApi.Controllers
{
  [ApiController, Route("api/[controller]")]
  public class ProtectedController : RnBaseController<ProtectedController>
  {
    public ProtectedController(IServiceProvider serviceProvider)
      : base(serviceProvider)
    {
      // TODO: [TESTS] (ProtectedController) Add tests
    }

    [HttpGet, Route("AuthenticatedOnly"), Authorize]
    public async Task<ActionResult<string>> Get()
    {
      // TODO: [TESTS] (ProtectedController.Get) Add tests
      var response = new BaseResponse<string>()
        .WithResponse("Hello World");

      return await ProcessResponseAsync(response);
    }
  }
}
