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
      var user = await UserService.Login(request);
      var response = new AuthenticationResponse();

      if (user == null)
      {
        // TODO: [LOGGING] (AuthControllerBase.Authenticate) Add logging
        return response.WithError("Login failed");
      }

      // Login was a success
      var token = _jwtTokenHelper.GenerateToken(user.UserId);
      return Ok(response.WithUser(token, user));
    }
  }
}
