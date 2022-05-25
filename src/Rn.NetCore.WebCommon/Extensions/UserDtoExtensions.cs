using System.Linq;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.WebCommon.Models.Dto;

namespace Rn.NetCore.WebCommon.Extensions;

public static class UserDtoExtensions
{
  // TODO: [MOVE] (UserDtoExtensions) Move to Rn.NetCore.WebCommon
  public static int GetIntAttribute(this UserDto user, string attribute, int fallback)
  {
    var rawAttribute = user.GetRawAttribute(attribute);

    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (rawAttribute == null)
      return fallback;

    if (rawAttribute is int intAttribute)
      return intAttribute;

    if (rawAttribute is string stringAttribute)
      return int.TryParse(stringAttribute, out var parsed) ? parsed : fallback;

    return fallback;
  }

  public static int GetIntAttribute(this UserDto user, string attribute)
    => GetIntAttribute(user, attribute, 0);

  public static bool HasAttribute(this UserDto user, string attribute)
  {
    if (user is null)
      return false;

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if (user.Attributes.Count == 0)
      return false;

    return user.Attributes.Any(x => x.Key.IgnoreCaseEquals(attribute));
  }

  public static object GetRawAttribute(this UserDto user, string attribute)
  {
    if (user is null || !user.HasAttribute(attribute))
      return null;

    return user.Attributes
      .First(x => x.Key
        .IgnoreCaseEquals(attribute)
      ).Value;
  }

  public static UserDto SetAttribute(this UserDto user, string attribute, int value)
  {
    user.Attributes[attribute] = value;
    return user;
  }
}
