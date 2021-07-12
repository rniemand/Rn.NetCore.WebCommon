using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.WebCommon.Builders;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Middleware
{
  // https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
  public class ApiMetricsMiddleware
  {
    private readonly ILoggerAdapter<ApiMetricsMiddleware> _logger;
    private readonly IMetricService _metrics;
    private readonly IDateTimeAbstraction _dateTime;
    private readonly RequestDelegate _next;

    public ApiMetricsMiddleware(
      ILoggerAdapter<ApiMetricsMiddleware> logger,
      IMetricService metrics,
      IDateTimeAbstraction dateTime,
      RequestDelegate next)
    {
      _logger = logger;
      _metrics = metrics;
      _next = next;
      _dateTime = dateTime;
    }

    public async Task Invoke(HttpContext context)
    {
      // Ensure that there is a metric context to work with
      var metricContext = context.GetApiRequestMetricContext(_dateTime.UtcNow);
      metricContext?.SetMiddlewareStartTime(_dateTime.UtcNow);

      // Copy a pointer to the original response body stream
      var originalBodyStream = context.Response.Body;
      await using var responseBody = new MemoryStream();
      // ...and use that for the temporary response body
      context.Response.Body = responseBody;

      // Continue down the Middleware pipeline
      metricContext?.SetActionStartTime(_dateTime.UtcNow);
      await _next(context);
      metricContext?.SetActionEndTime(_dateTime.UtcNow);

      await LogApiResponseMetric(context);
      context.Response.Body.Seek(0, SeekOrigin.Begin);

      // Copy the contents of the new memory stream (which contains the response)
      // to the original stream, which is then returned to the client.
      await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task LogApiResponseMetric(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiMetricsMiddleware.LogApiResponseMetric) Add tests
      try
      {
        // Ensure we have something to work with
        var metricContext = httpContext.GetApiRequestMetricContext();
        if (metricContext == null) return;
        metricContext.CompleteMiddlewareRequest(httpContext, _dateTime.UtcNow);

        await _metrics.SubmitPointAsync(
          new ApiCallMetricBuilder(metricContext).Build()
        );
      }
      catch (Exception ex)
      {
        _logger.LogUnexpectedException(ex);
      }
    }
  }
}
