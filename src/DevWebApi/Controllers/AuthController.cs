using System;
using Microsoft.AspNetCore.Mvc;
using Rn.NetCore.WebCommon.Controllers;

namespace DevWebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthController : AuthControllerBase<AuthController>
{
  public AuthController(IServiceProvider serviceProvider)
    : base(serviceProvider)
  { }
}
