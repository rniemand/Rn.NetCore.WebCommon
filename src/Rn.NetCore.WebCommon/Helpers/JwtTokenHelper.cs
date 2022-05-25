using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.WebCommon.Configuration;
using Rn.NetCore.WebCommon.Providers;

namespace Rn.NetCore.WebCommon.Helpers;

public interface IJwtTokenHelper
{
  string GenerateToken(int userId);
  JwtSecurityToken ExtractToken(string token);
  int ExtractUserId(string token);
}

public class JwtTokenHelper : IJwtTokenHelper
{
  private readonly ILoggerAdapter<JwtTokenHelper> _logger;
  private readonly IDateTimeAbstraction _dateTime;
  private readonly AuthenticationConfig _config;

  public JwtTokenHelper(
    ILoggerAdapter<JwtTokenHelper> logger,
    IRnWebCoreConfigProvider configProvider,
    IDateTimeAbstraction dateTime)
  {
    _logger = logger;
    _dateTime = dateTime;

    _config = configProvider.GetAuthenticationConfig();

    // Ensure that the configuration is valid
    if (string.IsNullOrWhiteSpace(_config.Secret))
    {
      // TODO: [HANDLE] (JwtTokenHelper) Handle this
      throw new Exception("Auth Secret is missing!");
    }
  }


  // Interface methods
  public string GenerateToken(int userId)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_config.Secret);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
      {
        new Claim("id", userId.ToString())
      }),
      Expires = _dateTime.UtcNow.AddMinutes(_config.SessionLengthMin),
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature
      )
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public JwtSecurityToken ExtractToken(string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config.Secret);
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        ClockSkew = TimeSpan.Zero
      }, out var validatedToken);

      return (JwtSecurityToken)validatedToken;
    }
    catch (Exception ex)
    {
      _logger.LogUnexpectedException(ex);
      return null;
    }
  }

  public int ExtractUserId(string token)
  {
    var jwtToken = ExtractToken(token);
    if (jwtToken?.Claims == null)
      return 0;

    try
    {
      return int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
    }
    catch (Exception ex)
    {
      _logger.LogUnexpectedException(ex);
      return 0;
    }
  }
}
