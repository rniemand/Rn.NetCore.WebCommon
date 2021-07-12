using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters
{
  public class ApiMetricResultFilter : IResultFilter
  {
    private readonly IDateTimeAbstraction _dateTime;

    public ApiMetricResultFilter(IDateTimeAbstraction dateTime)
    {
      _dateTime = dateTime;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
      // TODO: [TESTS] (ApiMetricResultFilter.OnResultExecuting) Add tests
      context.HttpContext.GetApiRequestMetricContext()
        ?.WithResultExecutingContext(context, _dateTime.UtcNow);
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      // TODO: [TESTS] (ApiMetricResultFilter.OnResultExecuted) Add tests
      context.HttpContext.GetApiRequestMetricContext()
        ?.WithResultExecutedContext(context, _dateTime.UtcNow);
    }
  }
}
