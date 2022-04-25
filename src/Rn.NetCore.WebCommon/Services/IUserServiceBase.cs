#nullable enable
using System.Threading.Tasks;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;

namespace Rn.NetCore.WebCommon.Services;

public interface IUserServiceBase
{
  Task<UserDto?> GetFromIdAsync(int userId);
  Task<UserDto?> LoginAsync(AuthenticationRequest request);
  Task UserSessionExtended(UserDto userDto);
}