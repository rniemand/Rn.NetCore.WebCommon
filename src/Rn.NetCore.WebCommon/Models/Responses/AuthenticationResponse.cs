using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Rn.NetCore.WebCommon.Models.Dto;

namespace Rn.NetCore.WebCommon.Models.Responses;

public class AuthenticationResponse
{
  [JsonProperty("token"), JsonPropertyName("token")]
  public string Token { get; set; }

  [JsonProperty("user"), JsonPropertyName("user")]
  public UserDto User { get; set; }

  [JsonProperty("success"), JsonPropertyName("success")]
  public bool Success { get; set; }

  [JsonProperty("message"), JsonPropertyName("message")]
  public string Message { get; set; }


  // Constructor
  public AuthenticationResponse()
  {
    // TODO: [TESTS] (AuthenticationResponse) Add tests
    Token = string.Empty;
    User = null;
    Success = false;
    Message = string.Empty;
  }


  // Builders
  public AuthenticationResponse WithUser(string token, UserDto userDto)
  {
    // TODO: [TESTS] (AuthenticationResponse.WithUser) Add tests
    Token = token;
    User = userDto;
    Success = (Token?.Length ?? 0) > 0 && User is not null;
    return this;
  }

  public AuthenticationResponse WithError(string message)
  {
    // TODO: [TESTS] (AuthenticationResponse.WithError) Add tests
    Success = false;
    Message = message;
    return this;
  }
}