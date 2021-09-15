using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.WebCommon.Configuration
{
  public class AuthenticationConfig
  {
    public const string Key = "RnWebCore:Authentication";

    [JsonProperty("Secret"), JsonPropertyName("Secret")]
    public string Secret { get; set; }

    [JsonProperty("SessionLengthMin"), JsonPropertyName("SessionLengthMin")]
    public int SessionLengthMin { get; set; }

    public AuthenticationConfig()
    {
      // TODO: [TESTS] (AuthenticationConfig) Add tests
      Secret = string.Empty;
      SessionLengthMin = 1440;
    }
  }
}
