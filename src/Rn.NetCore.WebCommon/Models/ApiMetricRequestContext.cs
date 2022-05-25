using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Models;

public class ApiMetricRequestContext
{
  public DateTime? StartTime { get; private set; }
  public DateTime? EndTime { get; private set; }
  public DateTime? ActionStartTime { get; private set; }
  public DateTime? ActionEndTime { get; private set; }
  public DateTime? ResultsStartTime { get; private set; }
  public DateTime? ResultsEndTime { get; private set; }
  public DateTime? ExThrownTime { get; private set; }
  public DateTime? MiddlewareStartTime { get; private set; }
  public DateTime? MiddlewareEndTime { get; private set; }
  public string Controller { get; private set; }
  public string Action { get; private set; }
  public string Guid { get; private set; }
  public string ExceptionName { get; private set; }
  public string Method { get; private set; }
  public string ContentType { get; private set; }
  public long ContentLength { get; private set; }
  public string Protocol { get; private set; }
  public string Scheme { get; private set; }
  public int CookieCount { get; private set; }
  public int HeaderCount { get; private set; }
  public string Host { get; private set; }
  public int Port { get; private set; }
  public int ResponseCode { get; private set; }
  public string ResponseContentType { get; private set; }
  public long ResponseContentLength { get; private set; }
  public int ResponseHeaderCount { get; private set; }


  // Constructors
  public ApiMetricRequestContext()
  {
    StartTime = null;
    EndTime = null;
    ActionStartTime = null;
    ActionEndTime = null;
    ResultsStartTime = null;
    ResultsEndTime = null;
    ExThrownTime = null;
    MiddlewareStartTime = null;
    MiddlewareEndTime = null;
    Controller = string.Empty;
    Action = string.Empty;
    Guid = string.Empty;
    ExceptionName = string.Empty;
    Method = string.Empty;
    ContentType = string.Empty;
    ContentLength = 0;
    Protocol = string.Empty;
    Scheme = string.Empty;
    CookieCount = 0;
    HeaderCount = 0;
    Host = string.Empty;
    Port = 0;
    ResponseCode = 0;
    ResponseContentType = string.Empty;
    ResponseContentLength = 0;
    ResponseHeaderCount = 0;
  }

  public ApiMetricRequestContext(DateTime requestStartTime)
    : this()
  {
    StartTime = requestStartTime;
  }


  // Builder Methods (simple)
  public ApiMetricRequestContext SetController(string controller, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(controller))
      return this;

    if (!string.IsNullOrEmpty(Controller) && !forceOverwrite)
      return this;

    Controller = controller;
    return this;
  }

  public ApiMetricRequestContext SetAction(string action, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(action))
      return this;

    if (!string.IsNullOrEmpty(Action) && !forceOverwrite)
      return this;

    Action = action;
    return this;
  }

  public ApiMetricRequestContext SetRequestMethod(string method, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(method))
      return this;

    if (!string.IsNullOrEmpty(Method) && !forceOverwrite)
      return this;

    Method = method.UpperTrim();
    return this;
  }

  public ApiMetricRequestContext SetRequestContentType(string contentType, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(contentType))
      return this;

    if (!string.IsNullOrEmpty(ContentType) && !forceOverwrite)
      return this;

    ContentType = contentType;
    return this;
  }

  public ApiMetricRequestContext SetRequestContentLength(long? contentLength, bool forceOverwrite = false)
  {
    var safeLength = contentLength ?? 0;
    if (safeLength == 0)
      return this;

    if (ContentLength > 0 && !forceOverwrite)
      return this;

    ContentLength = safeLength;
    return this;
  }

  public ApiMetricRequestContext SetRequestProtocol(string protocol, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(protocol))
      return this;

    if (!string.IsNullOrEmpty(Protocol) && !forceOverwrite)
      return this;

    Protocol = protocol.UpperTrim();
    return this;
  }

  public ApiMetricRequestContext SetRequestScheme(string scheme, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(scheme))
      return this;

    if (!string.IsNullOrEmpty(Scheme) && !forceOverwrite)
      return this;

    Scheme = scheme.UpperTrim();
    return this;
  }

  public ApiMetricRequestContext SetRequestHost(HostString host, bool forceOverwrite = false)
  {
    if (string.IsNullOrWhiteSpace(host.Host))
      return this;

    if (!string.IsNullOrEmpty(Host) && !forceOverwrite)
      return this;

    Host = host.Host.LowerTrim();
    return this;
  }

  public ApiMetricRequestContext SetRequestPort(HostString host, bool forceOverwrite = false)
  {
    var port = host.Port ?? 0;
    if (port == 0 && Scheme.Length > 0)
      port = Scheme == "HTTPS" ? 443 : 80;

    if (port == 0)
      return this;

    if (Port > 0 && !forceOverwrite)
      return this;

    Port = port;
    return this;
  }

  public ApiMetricRequestContext SetRequestEndTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (EndTime.HasValue && !forceOverwrite)
      return this;

    EndTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext SetResultsStartTime(DateTime utcNow, bool forceOverwrite = false)
  {

    if (ResultsStartTime.HasValue && !forceOverwrite)
      return this;

    ResultsStartTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext SetResultsEndTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (ResultsEndTime.HasValue && !forceOverwrite)
      return this;

    ResultsEndTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext SetExThrownTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (ExThrownTime.HasValue && !forceOverwrite)
      return this;

    ExThrownTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext SetActionStartTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (ActionStartTime.HasValue && !forceOverwrite)
      return this;

    ActionStartTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext SetRequestGuid(string requestGuid, bool forceOverwrite = false)
  {
    if (!string.IsNullOrWhiteSpace(Guid) && !forceOverwrite)
      return this;

    Guid = requestGuid;
    return this;
  }

  public ApiMetricRequestContext SetRequestGuid(Guid requestGuid, bool forceOverwrite = false)
  {
    return SetRequestGuid(requestGuid.ToString("D").UpperTrim(), forceOverwrite);
  }

  public ApiMetricRequestContext SetActionEndTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (ActionEndTime.HasValue && !forceOverwrite)
      return this;

    ActionEndTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext WithRouteData(RouteData routeData, bool forceOverwrite = false)
  {
    if (routeData == null)
      return this;

    return SetController(routeData.GetRouteKey("controller"), forceOverwrite)
      .SetAction(routeData.GetRouteKey("action"), forceOverwrite);
  }

  public ApiMetricRequestContext WithHttpRequest(HttpRequest request, bool forceOverwrite = false)
  {
    if (request == null)
      return this;

    return SetRequestMethod(request.Method, forceOverwrite)
      .SetRequestContentType(request.ContentType, forceOverwrite)
      .SetRequestContentLength(request.ContentLength, forceOverwrite)
      .SetRequestProtocol(request.Protocol, forceOverwrite)
      .SetRequestScheme(request.Scheme, forceOverwrite)
      .SetRequestHost(request.Host, forceOverwrite)
      .SetRequestHeaderCount(request, forceOverwrite)
      .SetRequestCookieCount(request, forceOverwrite)
      .SetRequestPort(request.Host, forceOverwrite);
  }

  public ApiMetricRequestContext WithException(Exception ex, bool forceOverwrite = false)
  {
    if (ex == null)
      return this;

    if (!string.IsNullOrWhiteSpace(ExceptionName) && !forceOverwrite)
      return this;

    ExceptionName = ex.GetType().Name;
    return this;
  }

  public ApiMetricRequestContext SetMiddlewareStartTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (MiddlewareStartTime.HasValue && !forceOverwrite)
      return this;

    MiddlewareStartTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext SetMiddlewareEndTime(DateTime utcNow, bool forceOverwrite = false)
  {
    if (MiddlewareEndTime.HasValue && !forceOverwrite)
      return this;

    MiddlewareEndTime = utcNow;
    return this;
  }

  public ApiMetricRequestContext WithResponseCode(int responseCode, bool forceOverwrite = false)
  {
    if (ResponseCode > 0 && !forceOverwrite)
      return this;

    if (responseCode > 0)
      ResponseCode = responseCode;

    return this;
  }

  public ApiMetricRequestContext WithResponseContentType(string contentType, bool forceOverwrite = false)
  {
    if (!string.IsNullOrWhiteSpace(ResponseContentType) && !forceOverwrite)
      return this;

    if (!string.IsNullOrWhiteSpace(contentType))
      ResponseContentType = contentType;

    return this;
  }

  public ApiMetricRequestContext WithResponseContentLength(HttpResponse response, bool forceOverwrite = false)
  {
    if (response == null)
      return this;

    if (ResponseContentLength > 0 && !forceOverwrite)
      return this;

    var contentLength = response.ContentLength ?? 0;

    if (contentLength == 0)
      contentLength = response.Body?.Length ?? 0;

    ResponseContentLength = contentLength;
    return this;
  }

  public ApiMetricRequestContext WithResponseHeaderCount(HttpResponse response, bool forceOverwrite = false)
  {
    if (response == null)
      return this;

    if (ResponseHeaderCount > 0 && !forceOverwrite)
      return this;

    ResponseHeaderCount = response.Headers?.Count ?? 0;
    return this;
  }

  public ApiMetricRequestContext WithHttpResponse(HttpResponse response, bool forceOverwrite = false)
  {
    if (response == null)
      return this;

    return WithResponseCode(response.StatusCode, forceOverwrite)
      .WithResponseContentType(response.ContentType, forceOverwrite)
      .WithResponseContentLength(response, forceOverwrite)
      .WithResponseHeaderCount(response, forceOverwrite);
  }


  // Builder Methods (Filter Hooks)
  public void WithActionExecutingContext(ActionExecutingContext context, DateTime utcNow, bool forceOverwrite = false)
  {
    if (context == null)
      return;

    SetActionStartTime(utcNow, forceOverwrite)
      .WithRouteData(context.RouteData, forceOverwrite)
      .SetRequestGuid(System.Guid.NewGuid());
  }

  public void WithActionExecutedContext(ActionExecutedContext context, DateTime utcNow, bool forceOverwrite = false)
  {
    if (context == null)
      return;

    SetActionEndTime(utcNow, forceOverwrite);
  }

  public void WithResourceExecutingContext(ResourceExecutingContext context, bool forceOverwrite = false)
  {
    if (context == null)
      return;

    WithRouteData(context.RouteData, forceOverwrite)
      .WithHttpRequest(context.HttpContext.Request, forceOverwrite);
  }

  public void WithResourceExecutedContext(ResourceExecutedContext context, DateTime utcNow, bool forceOverwrite = false)
  {
    if (context == null)
      return;

    SetRequestEndTime(utcNow, forceOverwrite)
      .WithRouteData(context.RouteData, forceOverwrite);
  }

  public void WithResultExecutingContext(ResultExecutingContext context, DateTime utcNow, bool forceOverwrite = false)
  {
    if (context == null)
      return;

    WithHttpRequest(context.HttpContext.Request, forceOverwrite)
      .WithRouteData(context.RouteData, forceOverwrite)
      .SetResultsStartTime(utcNow, forceOverwrite);
  }

  public void WithResultExecutedContext(ResultExecutedContext context, DateTime utcNow, bool forceOverwrite = false)
  {
    SetResultsEndTime(utcNow, forceOverwrite);
  }

  public void WithExceptionContext(ExceptionContext context, DateTime utcNow, bool forceOverwrite = true)
  {
    if (context == null)
      return;

    WithRouteData(context.RouteData, forceOverwrite)
      .WithHttpRequest(context.HttpContext.Request, forceOverwrite)
      .WithHttpResponse(context.HttpContext.Response, forceOverwrite)
      .WithException(context.Exception, forceOverwrite)
      .SetExThrownTime(utcNow, forceOverwrite)
      .WithResponseCode(500, forceOverwrite)
      .SetRequestEndTime(utcNow)
      .SetMiddlewareEndTime(utcNow);
  }

  public void CompleteMiddlewareRequest(HttpContext httpContext, DateTime utcNow)
  {
    SetRequestEndTime(utcNow);
    SetMiddlewareEndTime(utcNow);
    WithRouteData(httpContext.GetRouteData());
    WithHttpRequest(httpContext.Request);
    WithHttpResponse(httpContext.Response);
  }


  // Internal methods
  private ApiMetricRequestContext SetRequestCookieCount(HttpRequest request, bool forceOverwrite = false)
  {
    if (CookieCount > 0 && !forceOverwrite)
      return this;

    var cookiesCount = request?.Cookies?.Count ?? 0;
    if (cookiesCount > 0)
      CookieCount = cookiesCount;

    return this;
  }

  private ApiMetricRequestContext SetRequestHeaderCount(HttpRequest request, bool forceOverwrite = false)
  {
    if (HeaderCount > 0 && !forceOverwrite)
      return this;

    var headerCount = request?.Headers?.Count ?? 0;
    if (headerCount > 0)
      HeaderCount = headerCount;

    return this;
  }
}
