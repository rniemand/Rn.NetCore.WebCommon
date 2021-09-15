using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.WebCommon.Helpers;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Models.Responses;
using Rn.NetCore.WebCommon.Services;

namespace Rn.NetCore.WebCommon.Controllers
{
  public class AuthControllerBase<TController> : ControllerBase
  {
    public ILoggerAdapter<TController> Logger { get; set; }
    public IUserServiceBase UserService { get; set; }
    private readonly IJwtTokenHelper _jwtTokenHelper;

    // Constructor
    public AuthControllerBase(IServiceProvider serviceProvider)
    {
      // TODO: [TESTS] (AuthControllerBase) Add tests
      Logger = serviceProvider.GetRequiredService<ILoggerAdapter<TController>>();
      UserService = serviceProvider.GetRequiredService<IUserServiceBase>();
      _jwtTokenHelper = serviceProvider.GetRequiredService<IJwtTokenHelper>();
    }


    // Required methods
    [HttpPost, Route("authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
    {
      // TODO: [TESTS] (AuthControllerBase.Authenticate) Add tests
      var loggedInUser = await UserService.Login(request);

      if (loggedInUser == null)
      {
        // TODO: [LOGGING] (AuthControllerBase.Authenticate) Add logging
        return null;
      }

      return Ok(new AuthenticationResponse
      {
        Token = _jwtTokenHelper.GenerateToken(loggedInUser.UserId),
        User = loggedInUser
      });
    }
  }
}
