using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Rn.NetCore.WebCommon.ModelBinders;
using Rn.NetCore.WebCommon.Models.Dto;

namespace Rn.NetCore.WebCommon.Models.Requests
{
  [ModelBinder(BinderType = typeof(BaseApiRequestModelBinder))]
  public abstract class BaseApiRequest
  {
    [OpenApiIgnore]
    public UserDto User { get; set; }

    [OpenApiIgnore]
    public int UserId { get; set; }

    protected BaseApiRequest()
    {
      // TODO: [TESTS] (BaseApiRequest) Add tests
      User = null;
      UserId = 0;
    }
  }
}
