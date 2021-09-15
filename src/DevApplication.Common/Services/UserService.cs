using System.Threading.Tasks;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Models.Responses;
using Rn.NetCore.WebCommon.Services;

namespace DevApplication.Common.Services
{
  public class UserService : IUserServiceBase
  {
    // Interface methods
    public async Task<UserDto> GetFromToken(string token)
    {
      // TODO: [TESTS] (UserService.GetFromToken) Add tests
      await Task.CompletedTask;
      return null;
    }

    public async Task<UserDto> Login(AuthenticationRequest request)
    {
      // TODO: [TESTS] (UserService.Login) Add tests
      await Task.CompletedTask;
      return null;
    }
  }
}
