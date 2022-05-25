using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters;

// DOCS: docs\filters\ApiMetricResourceFilter.md
public class ApiMetricResourceFilter : IResourceFilter
{
  private readonly IDateTimeAbstraction _dateTime;

  public ApiMetricResourceFilter(IDateTimeAbstraction dateTime)
  {
    _dateTime = dateTime;
  }

  // Interface methods
  public void OnResourceExecuting(ResourceExecutingContext context)
  {
    context.HttpContext.GetApiRequestMetricContext(_dateTime.UtcNow)
      ?.WithResourceExecutingContext(context);
  }

  public void OnResourceExecuted(ResourceExecutedContext context)
  {
    context.HttpContext.GetApiRequestMetricContext()
      ?.WithResourceExecutedContext(context, _dateTime.UtcNow);
  }
}
