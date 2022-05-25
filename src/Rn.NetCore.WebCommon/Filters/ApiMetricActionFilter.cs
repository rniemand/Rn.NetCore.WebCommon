using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters;

// DOCS: docs\filters\ApiMetricActionFilter.md
public class ApiMetricActionFilter : IActionFilter
{
  private readonly IDateTimeAbstraction _dateTime;

  public ApiMetricActionFilter(IDateTimeAbstraction dateTime)
  {
    _dateTime = dateTime;
  }

  // Interface methods
  public void OnActionExecuting(ActionExecutingContext context)
  {
    context.HttpContext.GetApiRequestMetricContext()
      ?.WithActionExecutingContext(context, _dateTime.UtcNow);
  }

  public void OnActionExecuted(ActionExecutedContext context)
  {
    context.HttpContext.GetApiRequestMetricContext()
      ?.WithActionExecutedContext(context, _dateTime.UtcNow);
  }
}
