using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Builders
{
  public class ApiCallMetricBuilder
  {
    private readonly MetricBuilder _builder;

    private static class Tags
    {
      public const string Controller = "controller";
      public const string Action = "action";
      public const string RequestMethod = "request_method";
      public const string RequestContentType = "request_content_type";
      public const string RequestProtocol = "request_protocol";
      public const string RequestScheme = "request_scheme";
      public const string RequestHost = "request_host";
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
      public const string RequestContentLength = "request_content_length";
      public const string RequestCookieCount = "request_cookies";
      public const string RequestHeaderCount = "request_headers";
      public const string RequestPort = "request_port";
      public const string ResponseContentLength = "response_content_length";
      public const string ResponseHeaderCount = "response_headers";
    }

    // Constructor
    public ApiCallMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.ApiCallMetricBuilder) Add tests
      _builder = new MetricBuilder(MetricSource.ApiCall, measurement)
        // Tags
        .WithTag(Tags.Controller, MetricPlaceholder.Unset)
        .WithTag(Tags.Action, MetricPlaceholder.Unset)
        .WithTag(Tags.RequestMethod, MetricPlaceholder.Unset)
        .WithTag(Tags.RequestContentType, MetricPlaceholder.None)
        .WithTag(Tags.RequestProtocol, MetricPlaceholder.Unset)
        .WithTag(Tags.RequestScheme, MetricPlaceholder.Unset)
        .WithTag(Tags.RequestHost, MetricPlaceholder.None)
        .WithTag(Tags.ResponseCode, 0)
        .WithTag(Tags.ResponseContentType, MetricPlaceholder.Unset)
        .WithTag(Tags.RanAction, false)
        .WithTag(Tags.RanResult, false)
        // Fields
        .WithField(CoreMetricField.Value, (double)0)
        .WithField(Fields.ActionTime, (double)0)
        .WithField(Fields.ResultTime, (double)0)
        .WithField(Fields.MiddlewareTime, (double)0)
        .WithField(Fields.ExceptionTime, (double)0)
        .WithField(Fields.RequestContentLength, (long)0)
        .WithField(Fields.RequestCookieCount, 0)
        .WithField(Fields.RequestHeaderCount, 0)
        .WithField(Fields.RequestPort, 0)
        .WithField(Fields.ResponseContentLength, (long)0)
        .WithField(Fields.ResponseHeaderCount, 0);
    }

    public ApiCallMetricBuilder(ApiMetricRequestContext metricContext)
      : this()
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.ApiCallMetricBuilder) Add tests
      WithApiMetricRequestContext(metricContext);
    }


    // Builder Methods
    public ApiCallMetricBuilder WithActionTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithActionTime) Add tests
      if (context?.ActionStartTime == null || !context.ActionEndTime.HasValue)
        return this;

      if (context.ActionStartTime.Value > context.ActionEndTime.Value)
        return this;

      _builder.WithTag(Tags.RanAction, true)
        .WithField(
          Fields.ActionTime,
          (context.ActionEndTime.Value - context.ActionStartTime.Value).TotalMilliseconds
        );

      return this;
    }

    public ApiCallMetricBuilder WithResultTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResultTime) Add tests
      if (context?.ResultsStartTime == null || !context.ResultsEndTime.HasValue)
        return this;

      if (context.ResultsStartTime.Value > context.ResultsEndTime.Value)
        return this;

      _builder.WithTag(Tags.RanResult, true)
        .WithField(
          Fields.ResultTime,
          (context.ResultsEndTime.Value - context.ResultsStartTime.Value).TotalMilliseconds
        );

      return this;
    }

    public ApiCallMetricBuilder WithMiddlewareTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithMiddlewareTime) Add tests
      if (context?.MiddlewareStartTime == null || !context.MiddlewareEndTime.HasValue)
        return this;

      if (context.MiddlewareStartTime.Value > context.MiddlewareEndTime.Value)
        return this;

      _builder.WithField(
        Fields.MiddlewareTime,
        (context.MiddlewareEndTime.Value - context.MiddlewareStartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public ApiCallMetricBuilder WithExceptionTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithExceptionTime) Add tests
      if (context?.ExThrownTime == null || !context.RequestStartTime.HasValue)
        return this;

      if (context.RequestStartTime.Value > context.ExThrownTime.Value)
        return this;

      _builder.WithTag(CoreMetricTag.HasException, true)
        .WithField(
          Fields.ExceptionTime,
          (context.ExThrownTime.Value - context.RequestStartTime.Value).TotalMilliseconds
        );

      return this;
    }

    public ApiCallMetricBuilder WithRequestRunTime(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestRunTime) Add tests
      if (context?.RequestStartTime == null || !context.RequestEndTime.HasValue)
        return this;

      if (context.RequestStartTime > context.RequestEndTime)
        return this;

      _builder.WithField(
        CoreMetricField.Value,
        (context.RequestEndTime.Value - context.RequestStartTime.Value).TotalMilliseconds
      );

      return this;
    }

    public ApiCallMetricBuilder WithController(string controller)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithController) Add tests
      if (!string.IsNullOrWhiteSpace(controller))
        _builder.WithTag(Tags.Controller, controller, true);

      return this;
    }

    public ApiCallMetricBuilder WithAction(string action)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithAction) Add tests
      if (!string.IsNullOrWhiteSpace(action))
        _builder.WithTag(Tags.Action, action, true);

      return this;
    }

    public ApiCallMetricBuilder WithExceptionName(string exceptionName)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithExceptionName) Add tests
      if (string.IsNullOrWhiteSpace(exceptionName))
        return this;

      _builder
        .WithTag(CoreMetricTag.ExceptionName, exceptionName, true)
        .WithTag(CoreMetricTag.HasException, true)
        .WithTag(CoreMetricTag.Success, false);

      return this;
    }

    public ApiCallMetricBuilder WithRequestMethod(string method)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestMethod) Add tests
      if (!string.IsNullOrWhiteSpace(method))
        _builder.WithTag(Tags.RequestMethod, method, true);

      return this;
    }

    public ApiCallMetricBuilder WithRequestContentType(string contentType)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestContentType) Add tests
      if (!string.IsNullOrWhiteSpace(contentType))
        _builder.WithTag(Tags.RequestContentType, contentType);

      return this;
    }

    public ApiCallMetricBuilder WithRequestProtocol(string protocol)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestProtocol) Add tests
      if (!string.IsNullOrWhiteSpace(protocol))
        _builder.WithTag(Tags.RequestProtocol, protocol);

      return this;
    }

    public ApiCallMetricBuilder WithRequestScheme(string scheme)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestScheme) Add tests
      if (!string.IsNullOrWhiteSpace(scheme))
        _builder.WithTag(Tags.RequestScheme, scheme);

      return this;
    }

    public ApiCallMetricBuilder WithRequestHost(string host)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestHost) Add tests
      if (!string.IsNullOrWhiteSpace(host))
        _builder.WithTag(Tags.RequestHost, host);

      return this;
    }

    public ApiCallMetricBuilder WithResponseCode(int responseCode)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseCode) Add tests
      _builder.WithTag(Tags.ResponseCode, responseCode);
      return this;
    }

    public ApiCallMetricBuilder WithResponseContentType(string contentType)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseContentType) Add tests
      if (!string.IsNullOrWhiteSpace(contentType))
        _builder.WithTag(Tags.ResponseContentType, contentType);

      return this;
    }

    public ApiCallMetricBuilder WithApiMetricRequestContext(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithApiMetricRequestContext) Add tests
      if (context == null)
        return this;

      _builder
        .WithField(Fields.RequestContentLength, context.RequestContentLength)
        .WithField(Fields.RequestCookieCount, context.RequestCookieCount)
        .WithField(Fields.RequestHeaderCount, context.RequestHeaderCount)
        .WithField(Fields.RequestPort, context.RequestPort)
        .WithField(Fields.ResponseContentLength, context.ResponseContentLength)
        .WithField(Fields.ResponseHeaderCount, context.ResponseHeaderCount);

      return WithRequestRunTime(context)
        .WithActionTime(context)
        .WithResultTime(context)
        .WithMiddlewareTime(context)
        .WithExceptionTime(context)
        .WithController(context.Controller)
        .WithAction(context.Action)
        .WithExceptionName(context.ExceptionName)
        .WithRequestMethod(context.RequestMethod)
        .WithRequestContentType(context.RequestContentType)
        .WithRequestProtocol(context.RequestProtocol)
        .WithRequestScheme(context.RequestScheme)
        .WithRequestHost(context.RequestHost)
        .WithResponseCode(context.ResponseCode)
        .WithResponseContentType(context.RequestContentType);
    }


    // Build()
    public MetricBuilder Build()
    {
      return _builder;
    }
  }
}
