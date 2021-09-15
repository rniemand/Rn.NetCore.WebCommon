using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Rn.NetCore.WebCommon.Models.Dto;

namespace Rn.NetCore.WebCommon.Models.Responses
{
  public class AuthenticationResponse
  {
    [JsonProperty("token"), JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonProperty("user"), JsonPropertyName("user")]
    public UserDto User { get; set; }

    public AuthenticationResponse()
    {
      // TODO: [TESTS] (AuthenticationResponse) Add tests
      Token = string.Empty;
      User = null;
    }
  }
}
