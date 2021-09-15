using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.WebCommon.Helpers;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Models.Responses;

namespace Rn.NetCore.WebCommon.Controllers
{
  public abstract class AuthControllerBase<TController> : RnBaseController<TController>
  {
    public IJwtTokenHelper TokenHelper { get; set; }


    // Constructor
    protected AuthControllerBase(IServiceProvider serviceProvider)
      : base(serviceProvider)
    {
      // TODO: [TESTS] (AuthControllerBase) Add tests
      TokenHelper = serviceProvider.GetRequiredService<IJwtTokenHelper>();
    }


    // Required methods
    [HttpPost, Route("authenticate")]
    public virtual async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
    {
      // TODO: [TESTS] (AuthControllerBase.Authenticate) Add tests
      var user = await UserService.LoginAsync(request);
      var response = new AuthenticationResponse();

      if (user == null)
      {
        // TODO: [LOGGING] (AuthControllerBase.Authenticate) Add logging
        return response.WithError("Login failed");
      }

      // Login was a success
      var token = TokenHelper.GenerateToken(user.UserId);
      return Ok(response.WithUser(token, user));
    }
  }
}
