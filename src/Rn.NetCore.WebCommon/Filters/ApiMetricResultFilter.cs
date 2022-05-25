using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters;

// DOCS: docs\filters\ApiMetricResultFilter.md
public class ApiMetricResultFilter : IResultFilter
{
  private readonly IDateTimeAbstraction _dateTime;

  public ApiMetricResultFilter(IDateTimeAbstraction dateTime)
  {
    _dateTime = dateTime;
  }

  public void OnResultExecuting(ResultExecutingContext context)
  {
    context.HttpContext.GetApiRequestMetricContext()
      ?.WithResultExecutingContext(context, _dateTime.UtcNow);
  }

  public void OnResultExecuted(ResultExecutedContext context)
  {
    context.HttpContext.GetApiRequestMetricContext()
      ?.WithResultExecutedContext(context, _dateTime.UtcNow);
  }
}
