using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.WebCommon.Models.Dto;

public class UserDto
{
  [JsonProperty("userId"), JsonPropertyName("userId")]
  public int UserId { get; set; }

  [JsonProperty("lastSeen"), JsonPropertyName("lastSeen")]
  public DateTime? LastSeen { get; set; }

  [JsonProperty("firstName"), JsonPropertyName("firstName")]
  public string FirstName { get; set; } = string.Empty;

  [JsonProperty("lastName"), JsonPropertyName("lastName")]
  public string LastName { get; set; } = string.Empty;

  [JsonProperty("username"), JsonPropertyName("username")]
  public string Username { get; set; } = string.Empty;

  [JsonProperty("email"), JsonPropertyName("email")]
  public string Email { get; set; } = string.Empty;

  [JsonProperty("attributes"), JsonPropertyName("attributes")]
  public Dictionary<string, object> Attributes { get; set; } = new();
}
