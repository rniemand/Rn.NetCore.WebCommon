using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Metrics;
using Rn.NetCore.WebCommon.Extensions;
using Rn.NetCore.WebCommon.Metrics;

namespace Rn.NetCore.WebCommon.Filters;

// DOCS: docs\filters\ApiMetricExceptionFilter.md
public class ApiMetricExceptionFilter : IExceptionFilter
{
  private readonly IDateTimeAbstraction _dateTime;
  private readonly IMetricService _metrics;

  public ApiMetricExceptionFilter(
    IDateTimeAbstraction dateTime,
    IMetricService metrics)
  {
    _dateTime = dateTime;
    _metrics = metrics;
  }

  public void OnException(ExceptionContext context)
  {
    var metricContext = context.HttpContext.GetApiRequestMetricContext();
    if (metricContext == null)
      return;

    metricContext.WithExceptionContext(context, _dateTime.UtcNow);

    _metrics.Submit(new ApiCallMetricBuilder(metricContext));
  }
}
