using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.WebCommon.Configuration;

namespace Rn.NetCore.WebCommon.Providers;

public interface IRnWebCoreConfigProvider
{
  AuthenticationConfig GetAuthenticationConfig();
}

public class RnWebCoreConfigProvider : IRnWebCoreConfigProvider
{
  private readonly ILoggerAdapter<RnWebCoreConfigProvider> _logger;
  private readonly IConfiguration _configuration;

  private AuthenticationConfig _authenticationConfig;

  public RnWebCoreConfigProvider(
    ILoggerAdapter<RnWebCoreConfigProvider> logger,
    IConfiguration configuration)
  {
    _logger = logger;
    _configuration = configuration;

    _authenticationConfig = null;
  }


  // Interface methods
  public AuthenticationConfig GetAuthenticationConfig()
  {
    if (_authenticationConfig is not null)
      return _authenticationConfig;

    _authenticationConfig = new AuthenticationConfig();
    var section = _configuration.GetSection(AuthenticationConfig.Key);

    if(section.Exists())
      section.Bind(_authenticationConfig);

    return _authenticationConfig;
  }
}
