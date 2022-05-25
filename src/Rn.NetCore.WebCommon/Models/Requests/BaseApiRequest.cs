using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Rn.NetCore.WebCommon.ModelBinders;
using Rn.NetCore.WebCommon.Models.Dto;

namespace Rn.NetCore.WebCommon.Models.Requests;

[ModelBinder(BinderType = typeof(BaseApiRequestModelBinder))]
public class BaseApiRequest
{
  [OpenApiIgnore]
  public UserDto User { get; set; }

  [OpenApiIgnore]
  public int UserId { get; set; }
}
