using System.Threading.Tasks;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;

namespace Rn.NetCore.WebCommon.Services
{
  public interface IUserServiceBase
  {
    Task<UserDto> GetFromToken(string token);
    Task<UserDto> Login(AuthenticationRequest request);
  }
}
