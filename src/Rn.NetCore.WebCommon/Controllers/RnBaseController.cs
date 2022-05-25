using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.WebCommon.Helpers;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Responses;
using Rn.NetCore.WebCommon.Services;
using Rn.NetCore.WebCommon.Validation;

namespace Rn.NetCore.WebCommon.Controllers;

public class RnBaseController<TController> : ControllerBase
{
  public ILoggerAdapter<TController> Logger { get; set; }
  public IUserServiceBase UserService { get; set; }
  private readonly IJwtTokenHelper _tokenHelper;

  // Constructor
  public RnBaseController(IServiceProvider serviceProvider)
  {
    Logger = serviceProvider.GetRequiredService<ILoggerAdapter<TController>>();
    UserService = serviceProvider.GetRequiredService<IUserServiceBase>();
    _tokenHelper = serviceProvider.GetRequiredService<IJwtTokenHelper>();
  }


  // Public methods
  protected async Task<ActionResult<TResponse>> ProcessResponseAsync<TResponse>(BaseResponse<TResponse> response)
  {
    if (response.FailedValidation)
    {
      return BadRequest(new ValidationError(response.ValidationResult));
    }

    await ExtendUserSession();
    return Ok(response.Response);
  }

  protected ActionResult<TResponse> ProcessResponse<TResponse>(BaseResponse<TResponse> response)
  {
    return ProcessResponseAsync(response).GetAwaiter().GetResult();
  }


  // Internal
  private async Task ExtendUserSession()
  {
    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
    if (HttpContext is null)
      return;

    if (!HttpContext.Items.ContainsKey("User"))
      return;

    if (HttpContext.Items["User"] is not UserDto { UserId: > 0 } user)
      return;

    var token = _tokenHelper.GenerateToken(user.UserId);
    if (string.IsNullOrWhiteSpace(token))
      return;

    await UserService.UserSessionExtended(user);
    HttpContext.Response.Headers.Add("x-rn-session", token);
  }
}
