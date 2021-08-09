using Rn.NetCore.Common.Metrics;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Metrics
{
  public class ApiCallMetricBuilder : MetricBuilderBase, IApiCallMetricBuilder
  {
    public bool IsNullMetricBuilder { get; }

    // Constructors
    public ApiCallMetricBuilder()
      : base("api_call")
    {
      // TODO: [TESTS] (ApiCallMetricBuilder) Add tests
      IsNullMetricBuilder = false;

      // Set Tags
      SetTag(Tags.Controller, MetricPlaceholder.Unset);
      SetTag(Tags.Action, MetricPlaceholder.Unset);
      SetTag(Tags.Method, MetricPlaceholder.Unset);
      SetTag(Tags.ContentType, MetricPlaceholder.None);
      SetTag(Tags.Protocol, MetricPlaceholder.Unset);
      SetTag(Tags.Scheme, MetricPlaceholder.Unset);
      SetTag(Tags.Host, MetricPlaceholder.None);
      SetTag(Tags.ResponseCode, "0"); // TODO: INT THIS
      SetTag(Tags.ResponseContentType, MetricPlaceholder.Unset);
      SetTag(Tags.RanAction, false);
      SetTag(Tags.RanResult, false);

      // Set Fields
      SetField(Fields.ActionTime, (double)0);
      SetField(Fields.ResultTime, (double)0);
      SetField(Fields.MiddlewareTime, (double)0);
      SetField(Fields.ExceptionTime, (double)0);
      SetField(Fields.ContentLength, (long)0);
      SetField(Fields.CookieCount, 0);
      SetField(Fields.HeaderCount, 0);
      SetField(Fields.Port, 0);
      SetField(Fields.ResponseContentLength, (long)0);
      SetField(Fields.ResponseHeaderCount, 0);
    }

    public ApiCallMetricBuilder(ApiMetricRequestContext metricContext)
      : this()
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.ApiCallMetricBuilder) Add tests
      WithApiMetricRequestContext(metricContext);
    }


    // Builder Methods
    public IApiCallMetricBuilder WithActionTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithActionTime) Add tests
      if (context?.ActionStartTime == null || !context.ActionEndTime.HasValue)
        return this;

      if (context.ActionStartTime.Value > context.ActionEndTime.Value)
        return this;

      SetTag(Tags.RanAction, true);
      SetField(Fields.ActionTime,
        (context.ActionEndTime.Value - context.ActionStartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public IApiCallMetricBuilder WithResultTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResultTime) Add tests
      if (context?.ResultsStartTime == null || !context.ResultsEndTime.HasValue)
        return this;

      if (context.ResultsStartTime.Value > context.ResultsEndTime.Value)
        return this;

      SetTag(Tags.RanResult, true);
      SetField(Fields.ResultTime,
        (context.ResultsEndTime.Value - context.ResultsStartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public IApiCallMetricBuilder WithMiddlewareTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithMiddlewareTime) Add tests
      if (context?.MiddlewareStartTime == null || !context.MiddlewareEndTime.HasValue)
        return this;

      if (context.MiddlewareStartTime.Value > context.MiddlewareEndTime.Value)
        return this;

      SetField(Fields.MiddlewareTime,
        (context.MiddlewareEndTime.Value - context.MiddlewareStartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public IApiCallMetricBuilder WithExceptionTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithExceptionTime) Add tests
      if (context?.ExThrownTime == null || !context.StartTime.HasValue)
        return this;

      if (context.StartTime.Value > context.ExThrownTime.Value)
        return this;

      SetTag(MetricTag.HasException, true);
      SetField(Fields.ExceptionTime,
        (context.ExThrownTime.Value - context.StartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public IApiCallMetricBuilder WithRequestRunTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestRunTime) Add tests
      if (context?.StartTime == null || !context.EndTime.HasValue)
        return this;

      if (context.StartTime > context.EndTime)
        return this;

      SetField(MetricField.Value,
        (context.EndTime.Value - context.StartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public IApiCallMetricBuilder WithController(string controller)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithController) Add tests
      if (!string.IsNullOrWhiteSpace(controller))
        SetTag(Tags.Controller, controller, true);

      return this;
    }

    public IApiCallMetricBuilder WithAction(string action)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithAction) Add tests
      if (!string.IsNullOrWhiteSpace(action))
        SetTag(Tags.Action, action, true);

      return this;
    }

    public IApiCallMetricBuilder WithExceptionName(string exceptionName)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithExceptionName) Add tests
      if (string.IsNullOrWhiteSpace(exceptionName))
        return this;

      SetException(exceptionName);

      return this;
    }

    public IApiCallMetricBuilder WithRequestMethod(string method)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestMethod) Add tests
      if (!string.IsNullOrWhiteSpace(method))
        SetTag(Tags.Method, method, true);

      return this;
    }

    public IApiCallMetricBuilder WithRequestContentType(string contentType)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestContentType) Add tests
      if (!string.IsNullOrWhiteSpace(contentType))
        SetTag(Tags.ContentType, contentType);

      return this;
    }

    public IApiCallMetricBuilder WithRequestProtocol(string protocol)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestProtocol) Add tests
      if (!string.IsNullOrWhiteSpace(protocol))
        SetTag(Tags.Protocol, protocol);

      return this;
    }

    public IApiCallMetricBuilder WithRequestScheme(string scheme)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestScheme) Add tests
      if (!string.IsNullOrWhiteSpace(scheme))
        SetTag(Tags.Scheme, scheme);

      return this;
    }

    public IApiCallMetricBuilder WithRequestHost(string host)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestHost) Add tests
      if (!string.IsNullOrWhiteSpace(host))
        SetTag(Tags.Host, host);

      return this;
    }

    public IApiCallMetricBuilder WithResponseCode(int responseCode)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseCode) Add tests
      SetTag(Tags.ResponseCode, responseCode);
      return this;
    }

    public IApiCallMetricBuilder WithResponseContentType(string contentType)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseContentType) Add tests
      if (!string.IsNullOrWhiteSpace(contentType))
        SetTag(Tags.ResponseContentType, contentType);

      return this;
    }

    public IApiCallMetricBuilder WithApiMetricRequestContext(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithApiMetricRequestContext) Add tests
      if (context == null)
        return this;

      // Update Fields
      SetField(Fields.ContentLength, context.ContentLength);
      SetField(Fields.CookieCount, context.CookieCount);
      SetField(Fields.HeaderCount, context.HeaderCount);
      SetField(Fields.Port, context.Port);
      SetField(Fields.ResponseContentLength, context.ResponseContentLength);
      SetField(Fields.ResponseHeaderCount, context.ResponseHeaderCount);

      return WithRequestRunTime(context)
        .WithActionTime(context)
        .WithResultTime(context)
        .WithMiddlewareTime(context)
        .WithExceptionTime(context)
        .WithController(context.Controller)
        .WithAction(context.Action)
        .WithExceptionName(context.ExceptionName)
        .WithRequestMethod(context.Method)
        .WithRequestContentType(context.ContentType)
        .WithRequestProtocol(context.Protocol)
        .WithRequestScheme(context.Scheme)
        .WithRequestHost(context.Host)
        .WithResponseCode(context.ResponseCode)
        .WithResponseContentType(context.ContentType);
    }


    // Build()
    public CoreMetric GetMetric()
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.GetMetric) Add tests
      return CoreMetric;
    }


    // Misc.
    private static class Tags
    {
      public const string Controller = "controller";
      public const string Action = "action";
      public const string Method = "request_method";
      public const string ContentType = "request_content_type";
      public const string Protocol = "request_protocol";
      public const string Scheme = "request_scheme";
      public const string Host = "request_host";
      public const string ResponseCode = "response_code";
      public const string ResponseContentType = "response_content_type";
      public const string RanAction = "ran_action";
      public const string RanResult = "ran_result";
    }

    private static class Fields
    {
      public const string ActionTime = "action_ms";
      public const string ResultTime = "result_ms";
      public const string MiddlewareTime = "middleware_ms";
      public const string ExceptionTime = "exception_ms";
      public const string ContentLength = "request_content_length";
      public const string CookieCount = "request_cookies";
      public const string HeaderCount = "request_headers";
      public const string Port = "request_port";
      public const string ResponseContentLength = "response_content_length";
      public const string ResponseHeaderCount = "response_headers";
    }
  }
}
