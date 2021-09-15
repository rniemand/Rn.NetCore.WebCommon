using System.Threading.Tasks;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Services;

namespace DevApplication.Common.Services
{
  public class UserService : IUserServiceBase
  {
    // Interface methods
    public async Task<UserDto> GetFromIdAsync(int userId)
    {
      // TODO: [TESTS] (UserService.GetFromIdAsync) Add tests
      await Task.CompletedTask;
      return null;
    }

    public async Task<UserDto> LoginAsync(AuthenticationRequest request)
    {
      // TODO: [TESTS] (UserService.LoginAsync) Add tests
      await Task.CompletedTask;
      return null;
    }
  }
}
