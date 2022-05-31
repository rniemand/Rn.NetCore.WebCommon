using Rn.NetCore.Metrics;
using Rn.NetCore.Metrics.Builders;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Metrics;

public class ApiCallMetricBuilder : CoreMetricBuilder<ApiCallMetricBuilder>
{
  private string _controller = MetricPlaceholder.Unset;
  private string _action = MetricPlaceholder.Unset;
  private string _requestMethod = MetricPlaceholder.Unset;
  private string _requestContentType = MetricPlaceholder.None;
  private string _responseContentType = MetricPlaceholder.Unset;
  private string _requestProtocol = MetricPlaceholder.Unset;
  private string _requestScheme = MetricPlaceholder.Unset;
  private string _requestHost = MetricPlaceholder.None;
  private double _actionTime;
  private double _resultTime;
  private double _middlewareTime;
  private double _exceptionTime;
  private long _requestContentLength;
  private long _responseContentLength;
  private int _responseCode;
  private int _requestCookies;
  private int _requestHeaderCount;
  private int _responseHeaderCount;
  private int _requestPort;
  private bool _ranAction;
  private bool _ranResult;

  public ApiCallMetricBuilder()
    : base("api_call")
  { }

  public ApiCallMetricBuilder(ApiMetricRequestContext metricContext)
    : this()
  {
    WithApiMetricRequestContext(metricContext);
  }

  public ApiCallMetricBuilder WithActionTime(ApiMetricRequestContext context)
  {
    if (context?.ActionStartTime == null || !context.ActionEndTime.HasValue)
      return this;

    if (context.ActionStartTime.Value > context.ActionEndTime.Value)
      return this;

    _ranAction = true;
    _actionTime = (context.ActionEndTime.Value - context.ActionStartTime.Value).TotalMilliseconds;
    return this;
  }

  public ApiCallMetricBuilder WithResultTime(ApiMetricRequestContext context)
  {
    if (context?.ResultsStartTime == null || !context.ResultsEndTime.HasValue)
      return this;

    if (context.ResultsStartTime.Value > context.ResultsEndTime.Value)
      return this;

    _ranResult = true;
    _resultTime = (context.ResultsEndTime.Value - context.ResultsStartTime.Value).TotalMilliseconds;
    return this;
  }

  public ApiCallMetricBuilder WithMiddlewareTime(ApiMetricRequestContext context)
  {
    if (context?.MiddlewareStartTime == null || !context.MiddlewareEndTime.HasValue)
      return this;

    if (context.MiddlewareStartTime.Value > context.MiddlewareEndTime.Value)
      return this;

    _middlewareTime = (context.MiddlewareEndTime.Value - context.MiddlewareStartTime.Value).TotalMilliseconds;
    return this;
  }

  public ApiCallMetricBuilder WithExceptionTime(ApiMetricRequestContext context)
  {
    if (context?.ExThrownTime == null || !context.StartTime.HasValue)
      return this;

    if (context.StartTime.Value > context.ExThrownTime.Value)
      return this;

    SetException(context.ExceptionName);
    _exceptionTime = (context.ExThrownTime.Value - context.StartTime.Value).TotalMilliseconds;
    return this;
  }

  public ApiCallMetricBuilder WithRequestRunTime(ApiMetricRequestContext context)
  {
    if (context?.StartTime == null || !context.EndTime.HasValue)
      return this;

    if (context.StartTime > context.EndTime)
      return this;

    AddAction(m => m.SetField("value", (context.EndTime.Value - context.StartTime.Value).TotalMilliseconds));
    return this;
  }

  public ApiCallMetricBuilder WithController(string controller)
  {
    if (!string.IsNullOrWhiteSpace(controller))
      _controller = controller.Trim();

    return this;
  }

  public ApiCallMetricBuilder WithAction(string action)
  {
    if (!string.IsNullOrWhiteSpace(action))
      _action = action;

    return this;
  }

  public ApiCallMetricBuilder WithExceptionName(string exceptionName)
  {
    if (string.IsNullOrWhiteSpace(exceptionName))
      return this;

    SetException(exceptionName);
    return this;
  }

  public ApiCallMetricBuilder WithRequestMethod(string method)
  {
    if (!string.IsNullOrWhiteSpace(method))
      _requestMethod = method;

    return this;
  }

  public ApiCallMetricBuilder WithRequestContentType(string contentType)
  {
    if (!string.IsNullOrWhiteSpace(contentType))
      _requestContentType = contentType;

    return this;
  }

  public ApiCallMetricBuilder WithRequestProtocol(string protocol)
  {
    if (!string.IsNullOrWhiteSpace(protocol))
      _requestProtocol = protocol;

    return this;
  }

  public ApiCallMetricBuilder WithRequestScheme(string scheme)
  {
    if (!string.IsNullOrWhiteSpace(scheme))
      _requestScheme = scheme;

    return this;
  }

  public ApiCallMetricBuilder WithRequestHost(string host)
  {
    if (!string.IsNullOrWhiteSpace(host))
      _requestHost = host;

    return this;
  }

  public ApiCallMetricBuilder WithResponseCode(int responseCode)
  {
    _responseCode = responseCode;
    return this;
  }

  public ApiCallMetricBuilder WithResponseContentType(string contentType)
  {
    if (!string.IsNullOrWhiteSpace(contentType))
      _responseContentType = contentType;

    return this;
  }

  public ApiCallMetricBuilder WithApiMetricRequestContext(ApiMetricRequestContext context)
  {
    if (context == null)
      return this;

    // Update Fields
    _requestContentLength = context.ContentLength;
    _requestCookies = context.CookieCount;
    _requestHeaderCount = context.HeaderCount;
    _requestPort = context.Port;
    _responseContentLength = context.ResponseContentLength;
    _responseHeaderCount = context.ResponseHeaderCount;

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

  public override CoreMetric Build()
  {
    // Set ApiCallMetricBuilder specific fields and tags
    AddAction(m => m.SetTag("controller", _controller, true))
      .AddAction(m => m.SetTag("action", _action, true))
      .AddAction(m => m.SetTag("request_method", _requestMethod, true))
      .AddAction(m => m.SetTag("request_content_type", _requestContentType))
      .AddAction(m => m.SetTag("request_protocol", _requestProtocol))
      .AddAction(m => m.SetTag("request_scheme", _requestScheme))
      .AddAction(m => m.SetTag("request_host", _requestHost))
      .AddAction(m => m.SetTag("response_code", _responseCode))
      .AddAction(m => m.SetTag("response_content_type", _responseContentType))
      .AddAction(m => m.SetTag("ran_action", _ranAction))
      .AddAction(m => m.SetTag("ran_result", _ranResult))
      // Set default fields
      .AddAction(m => m.SetField("action_ms", _actionTime))
      .AddAction(m => m.SetField("result_ms", _resultTime))
      .AddAction(m => m.SetField("middleware_ms", _middlewareTime))
      .AddAction(m => m.SetField("exception_ms", _exceptionTime))
      .AddAction(m => m.SetField("request_content_length", _requestContentLength))
      .AddAction(m => m.SetField("request_cookies", _requestCookies))
      .AddAction(m => m.SetField("request_headers", _requestHeaderCount))
      .AddAction(m => m.SetField("request_port", _requestPort))
      .AddAction(m => m.SetField("response_content_length", _responseContentLength))
      .AddAction(m => m.SetField("response_headers", _responseHeaderCount));

    // Allow the base metric to build
    return base.Build();
  }
}
