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
  public string FirstName { get; set; }

  [JsonProperty("lastName"), JsonPropertyName("lastName")]
  public string LastName { get; set; }

  [JsonProperty("username"), JsonPropertyName("username")]
  public string Username { get; set; }

  [JsonProperty("email"), JsonPropertyName("email")]
  public string Email { get; set; }

  [JsonProperty("attributes"), JsonPropertyName("attributes")]
  public Dictionary<string, object> Attributes { get; set; }

  // Constructor
  public UserDto()
  {
    // TODO: [TESTS] (UserDto) Add tests
    UserId = 0;
    LastSeen = null;
    FirstName = string.Empty;
    LastName = string.Empty;
    Username = string.Empty;
    Email = string.Empty;
    Attributes = new Dictionary<string, object>();
  }
}