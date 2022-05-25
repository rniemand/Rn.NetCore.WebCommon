using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.WebCommon.Models.Dto;
using Rn.NetCore.WebCommon.Models.Requests;

namespace Rn.NetCore.WebCommon.ModelBinders;

public class BaseApiRequestModelBinder : IModelBinder
{
  private readonly ILoggerAdapter<BaseApiRequestModelBinder> _logger;
  private readonly IJsonHelper _jsonHelper;

  public BaseApiRequestModelBinder(
    ILoggerAdapter<BaseApiRequestModelBinder> logger,
    IJsonHelper jsonHelper)
  {
    _logger = logger;
    _jsonHelper = jsonHelper;
  }


  // Interface methods
  public async Task BindModelAsync(ModelBindingContext bindingContext)
  {
    if (bindingContext == null)
    {
      throw new ArgumentNullException(nameof(bindingContext));
    }

    var bodyJson = await GetBody(bindingContext);
    var modelType = bindingContext.ModelType;
    var model = _jsonHelper.DeserializeObject(bodyJson, modelType);
    AppendUser(model as BaseApiRequest, bindingContext);

    bindingContext.Result = ModelBindingResult.Success(model);
  }


  // Internal methods
  private async Task<string> GetBody(ModelBindingContext bindingContext)
  {

    try
    {
      using var reader = new StreamReader(
        bindingContext.ActionContext.HttpContext.Request.Body,
        Encoding.UTF8
      );

      var rawBody = await reader.ReadToEndAsync();
      return string.IsNullOrWhiteSpace(rawBody) ? "{}" : rawBody;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Unable to read body: {msg}", ex.Message);
      return "{}";
    }
  }

  private static void AppendUser(BaseApiRequest model, ModelBindingContext bindingContext)
  {
    if (bindingContext.HttpContext.Items.ContainsKey("User"))
    {
      model.User = (UserDto)bindingContext.HttpContext.Items["User"];
    }

    model.UserId = model.User?.UserId ?? 0;
  }
}
