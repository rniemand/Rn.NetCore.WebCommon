using System.Threading.Tasks;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Models.Responses;

namespace Rn.NetCore.WebCommon.Services
{
  public interface IUserServiceBase
  {
    Task<UserDto> GetFromToken(string token);
    Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
  }
}
