using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.WebCommon.Builders;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters
{
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
      // TODO: [TESTS] (ApiMetricExceptionFilter.OnException) Add tests
      var metricContext = context.HttpContext.GetApiRequestMetricContext();
      if (metricContext == null)
        return;

      metricContext.WithExceptionContext(context, _dateTime.UtcNow);

      _metrics.SubmitPoint(
        new ApiCallMetricBuilder(metricContext).Build()
      );
    }
  }
}
