using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.WebCommon.Models.Requests;

public class AuthenticationRequest
{
  [JsonProperty("username"), JsonPropertyName("username")]
  public string Username { get; set; }

  [JsonProperty("password"), JsonPropertyName("password")]
  public string Password { get; set; }

  public AuthenticationRequest()
  {
    Username = string.Empty;
    Password = string.Empty;
  }

  public AuthenticationRequest(string username, string password)
    : this()
  {
    Username = username;
    Password = password;
  }
}