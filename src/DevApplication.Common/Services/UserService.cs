﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;
using Rn.NetCore.WebCommon.Services;

namespace DevApplication.Common.Services
{
  public class UserService : IUserServiceBase
  {
    private static UserDto DummyUser = new UserDto
    {
      Username = "niemandr",
      Email = "1@2.com",
      FirstName = "Richard",
      LastName = "Niemand",
      UserId = 1,
      Attributes = new Dictionary<string, object>
      {
        { "Number", 10 },
        { "GUID", "1A07BF45-E038-4F32-B792-75AC5A0ED212" },
        { "Boolean", true }
      }
    };

    // Interface methods
    public async Task<UserDto> GetFromIdAsync(int userId)
    {
      // TODO: [TESTS] (UserService.GetFromIdAsync) Add tests
      await Task.CompletedTask;
      return DummyUser;
    }

    public async Task<UserDto> LoginAsync(AuthenticationRequest request)
    {
      // TODO: [TESTS] (UserService.LoginAsync) Add tests
      await Task.CompletedTask;
      DummyUser.LastSeen = DateTime.Now.AddMinutes(-30);
      return DummyUser;
    }
  }
}
