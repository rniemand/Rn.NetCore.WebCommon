using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rn.NetCore.WebCommon.Helpers;
using Rn.NetCore.WebCommon.Services;

namespace Rn.NetCore.WebCommon.Middleware;

public class JwtMiddleware
{
  private readonly RequestDelegate _next;

  public JwtMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context,
    IUserServiceBase userService,
    IJwtTokenHelper tokenHelper)
  {
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    if (token != null)
      await AttachUserToContext(context, token, userService, tokenHelper);

    await _next(context);
  }

  private static async Task AttachUserToContext(HttpContext context,
    string token,
    IUserServiceBase userService,
    IJwtTokenHelper tokenHelper)
  {
    try
    {
      var userId = tokenHelper.ExtractUserId(token);
        
      if (userId == 0)
        return;

      context.Items["User"] = await userService.GetFromIdAsync(userId);
    }
    catch
    {
      // do nothing if jwt validation fails
      // user is not attached to context so request won't have access to secure routes
    }
  }
}