using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Rn.NetCore.WebCommon.Attributes;
using Rn.NetCore.WebCommon.Controllers;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Models.Responses;

namespace DevWebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class ProtectedController : RnBaseController<ProtectedController>
{
  public ProtectedController(IServiceProvider serviceProvider)
    : base(serviceProvider)
  { }

  [HttpGet, Route("AuthenticatedOnly"), Authorize]
  public async Task<ActionResult<string>> Get()
  {
    var response = new BaseResponse<string>()
      .WithResponse("Hello World");

    return await ProcessResponseAsync(response);
  }

  [HttpGet, Route("OpenToPublic")]
  public async Task<ActionResult<string>> OpenToPublic() =>
    await ProcessResponseAsync(new BaseResponse<string>("Hi"));

  [HttpGet, Route("WhoAmI"), Authorize]
  public async Task<ActionResult<UserDto>> WhoAmI([OpenApiIgnore] BaseApiRequest request) =>
    await ProcessResponseAsync(new BaseResponse<UserDto>(request.User));
}
