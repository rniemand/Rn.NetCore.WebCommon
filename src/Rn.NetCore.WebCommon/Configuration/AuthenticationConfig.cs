using Microsoft.Extensions.Configuration;

namespace Rn.NetCore.WebCommon.Configuration;

// DOCS: docs\configuration\AuthenticationConfig.md
public class AuthenticationConfig
{
  public const string Key = "Rn.WebCore:Authentication";

  [ConfigurationKeyName("secret")]
  public string Secret { get; set; } = string.Empty;

  [ConfigurationKeyName("sessionLengthMin")]
  public int SessionLengthMin { get; set; } = 1440;
}
