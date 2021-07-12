using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters
{
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
      // TODO: [TESTS] (ApiMetricResourceFilter.OnResourceExecuting) Add tests
      context.HttpContext.GetApiRequestMetricContext(_dateTime.UtcNow)
        ?.WithResourceExecutingContext(context);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (ApiMetricResourceFilter.OnResourceExecuted) Add tests
      context.HttpContext.GetApiRequestMetricContext()
        ?.WithResourceExecutedContext(context, _dateTime.UtcNow);
    }
  }
}
