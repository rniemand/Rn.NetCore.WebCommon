using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Models
{
  public class ApiMetricRequestContext
  {
    public DateTime? RequestStartTime { get; private set; }
    public DateTime? RequestEndTime { get; private set; }
    public DateTime? ActionStartTime { get; private set; }
    public DateTime? ActionEndTime { get; private set; }
    public DateTime? ResultsStartTime { get; private set; }
    public DateTime? ResultsEndTime { get; private set; }
    public DateTime? ExThrownTime { get; private set; }
    public DateTime? MiddlewareStartTime { get; private set; }
    public DateTime? MiddlewareEndTime { get; private set; }
    public string Controller { get; private set; }
    public string Action { get; private set; }
    public string RequestGuid { get; private set; }
    public string ExceptionName { get; private set; }
    public string RequestMethod { get; private set; }
    public string RequestContentType { get; private set; }
    public long RequestContentLength { get; private set; }
    public string RequestProtocol { get; private set; }
    public string RequestScheme { get; private set; }
    public int RequestCookieCount { get; private set; }
    public int RequestHeaderCount { get; private set; }
    public string RequestHost { get; private set; }
    public int RequestPort { get; private set; }
    public int ResponseCode { get; private set; }
    public string ResponseContentType { get; private set; }
    public long ResponseContentLength { get; private set; }
    public int ResponseHeaderCount { get; private set; }


    // Constructors
    public ApiMetricRequestContext()
    {
      // TODO: [TESTS] (ApiMetricRequestContext) Add tests
      RequestStartTime = null;
      RequestEndTime = null;
      ActionStartTime = null;
      ActionEndTime = null;
      ResultsStartTime = null;
      ResultsEndTime = null;
      ExThrownTime = null;
      MiddlewareStartTime = null;
      MiddlewareEndTime = null;
      Controller = string.Empty;
      Action = string.Empty;
      RequestGuid = string.Empty;
      ExceptionName = string.Empty;
      RequestMethod = string.Empty;
      RequestContentType = string.Empty;
      RequestContentLength = 0;
      RequestProtocol = string.Empty;
      RequestScheme = string.Empty;
      RequestCookieCount = 0;
      RequestHeaderCount = 0;
      RequestHost = string.Empty;
      RequestPort = 0;
      ResponseCode = 0;
      ResponseContentType = string.Empty;
      ResponseContentLength = 0;
      ResponseHeaderCount = 0;
    }

    public ApiMetricRequestContext(DateTime requestStartTime)
      : this()
    {
      // TODO: [TESTS] (ApiMetricRequestContext) Add tests
      RequestStartTime = requestStartTime;
    }


    // Builder Methods (simple)
    public ApiMetricRequestContext SetController(string controller, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetController) Add tests
      if (string.IsNullOrWhiteSpace(controller))
        return this;

      if (!string.IsNullOrEmpty(Controller) && !forceOverwrite)
        return this;

      Controller = controller;
      return this;
    }

    public ApiMetricRequestContext SetAction(string action, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetAction) Add tests
      if (string.IsNullOrWhiteSpace(action))
        return this;

      if (!string.IsNullOrEmpty(Action) && !forceOverwrite)
        return this;

      Action = action;
      return this;
    }

    public ApiMetricRequestContext SetRequestMethod(string method, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestMethod) Add tests
      if (string.IsNullOrWhiteSpace(method))
        return this;

      if (!string.IsNullOrEmpty(RequestMethod) && !forceOverwrite)
        return this;

      RequestMethod = method.UpperTrim();
      return this;
    }

    public ApiMetricRequestContext SetRequestContentType(string contentType, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestContentType) Add tests
      if (string.IsNullOrWhiteSpace(contentType))
        return this;

      if (!string.IsNullOrEmpty(RequestContentType) && !forceOverwrite)
        return this;

      RequestContentType = contentType;
      return this;
    }

    public ApiMetricRequestContext SetRequestContentLength(long? contentLength, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestContentType) Add tests
      var safeLength = contentLength ?? 0;
      if (safeLength == 0)
        return this;

      if (RequestContentLength > 0 && !forceOverwrite)
        return this;

      RequestContentLength = safeLength;
      return this;
    }

    public ApiMetricRequestContext SetRequestProtocol(string protocol, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestProtocol) Add tests
      if (string.IsNullOrWhiteSpace(protocol))
        return this;

      if (!string.IsNullOrEmpty(RequestProtocol) && !forceOverwrite)
        return this;

      RequestProtocol = protocol.UpperTrim();
      return this;
    }

    public ApiMetricRequestContext SetRequestScheme(string scheme, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestScheme) Add tests
      if (string.IsNullOrWhiteSpace(scheme))
        return this;

      if (!string.IsNullOrEmpty(RequestScheme) && !forceOverwrite)
        return this;

      RequestScheme = scheme.UpperTrim();
      return this;
    }

    public ApiMetricRequestContext SetRequestHost(HostString host, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestHost) Add tests
      if (string.IsNullOrWhiteSpace(host.Host))
        return this;

      if (!string.IsNullOrEmpty(RequestHost) && !forceOverwrite)
        return this;

      RequestHost = host.Host.LowerTrim();
      return this;
    }

    public ApiMetricRequestContext SetRequestPort(HostString host, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestPort) Add tests
      var port = host.Port ?? 0;
      if (port == 0 && RequestScheme.Length > 0)
        port = RequestScheme == "HTTPS" ? 443 : 80;

      if (port == 0)
        return this;

      if (RequestPort > 0 && !forceOverwrite)
        return this;

      RequestPort = port;
      return this;
    }

    public ApiMetricRequestContext SetRequestEndTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestEndTime) Add tests
      if (RequestEndTime.HasValue && !forceOverwrite)
        return this;

      RequestEndTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext SetResultsStartTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetResultsStartTime) Add tests

      if (ResultsStartTime.HasValue && !forceOverwrite)
        return this;

      ResultsStartTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext SetResultsEndTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetResultsEndTime) Add tests
      if (ResultsEndTime.HasValue && !forceOverwrite)
        return this;

      ResultsEndTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext SetExThrownTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetExThrownTime) Add tests
      if (ExThrownTime.HasValue && !forceOverwrite)
        return this;

      ExThrownTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext SetActionStartTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetActionStartTime) Add tests
      if (ActionStartTime.HasValue && !forceOverwrite)
        return this;

      ActionStartTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext SetRequestGuid(string requestGuid, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestGuid) Add tests
      if (!string.IsNullOrWhiteSpace(RequestGuid) && !forceOverwrite)
        return this;

      RequestGuid = requestGuid;
      return this;
    }

    public ApiMetricRequestContext SetRequestGuid(Guid requestGuid, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestGuid) Add tests
      return SetRequestGuid(requestGuid.ToString("D").UpperTrim(), forceOverwrite);
    }

    public ApiMetricRequestContext SetActionEndTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetActionEndTime) Add tests
      if (ActionEndTime.HasValue && !forceOverwrite)
        return this;

      ActionEndTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext WithRouteData(RouteData routeData, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithRouteData) Add tests
      if (routeData == null)
        return this;

      return SetController(routeData.GetRouteKey("controller"), forceOverwrite)
        .SetAction(routeData.GetRouteKey("action"), forceOverwrite);
    }

    public ApiMetricRequestContext WithHttpRequest(HttpRequest request, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithHttpRequest) Add tests
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
      // TODO: [TESTS] (ApiMetricRequestContext.WithException) Add tests
      if (ex == null)
        return this;

      if (!string.IsNullOrWhiteSpace(ExceptionName) && !forceOverwrite)
        return this;

      ExceptionName = ex.GetType().Name;
      return this;
    }

    public ApiMetricRequestContext SetMiddlewareStartTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetMiddlewareStartTime) Add tests
      if (MiddlewareStartTime.HasValue && !forceOverwrite)
        return this;

      MiddlewareStartTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext SetMiddlewareEndTime(DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetMiddlewareEndTime) Add tests
      if (MiddlewareEndTime.HasValue && !forceOverwrite)
        return this;

      MiddlewareEndTime = utcNow;
      return this;
    }

    public ApiMetricRequestContext WithResponseCode(int responseCode, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResponseCode) Add tests
      if (ResponseCode > 0 && !forceOverwrite)
        return this;

      if (responseCode > 0)
        ResponseCode = responseCode;

      return this;
    }

    public ApiMetricRequestContext WithResponseContentType(string contentType, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResponseContentType) Add tests
      if (!string.IsNullOrWhiteSpace(ResponseContentType) && !forceOverwrite)
        return this;

      if (!string.IsNullOrWhiteSpace(contentType))
        ResponseContentType = contentType;

      return this;
    }

    public ApiMetricRequestContext WithResponseContentLength(HttpResponse response, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResponseContentLength) Add tests
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
      // TODO: [TESTS] (ApiMetricRequestContext.WithResponseHeaderCount) Add tests
      if (response == null)
        return this;

      if (ResponseHeaderCount > 0 && !forceOverwrite)
        return this;

      ResponseHeaderCount = response.Headers?.Count ?? 0;
      return this;
    }

    public ApiMetricRequestContext WithHttpResponse(HttpResponse response, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithHttpResponse) Add tests
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
      // TODO: [TESTS] (ApiMetricRequestContext.WithActionExecutingContext) Add tests
      if (context == null)
        return;

      SetActionStartTime(utcNow, forceOverwrite)
        .WithRouteData(context.RouteData, forceOverwrite)
        .SetRequestGuid(Guid.NewGuid());
    }

    public void WithActionExecutedContext(ActionExecutedContext context, DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithActionExecutedContext) Add tests
      if (context == null)
        return;

      SetActionEndTime(utcNow, forceOverwrite);
    }

    public void WithResourceExecutingContext(ResourceExecutingContext context, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResourceExecutingContext) Add tests
      if (context == null)
        return;

      WithRouteData(context.RouteData, forceOverwrite)
        .WithHttpRequest(context.HttpContext.Request, forceOverwrite);
    }

    public void WithResourceExecutedContext(ResourceExecutedContext context, DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResourceExecutedContext) Add tests
      if (context == null)
        return;

      SetRequestEndTime(utcNow, forceOverwrite)
        .WithRouteData(context.RouteData, forceOverwrite);
    }

    public void WithResultExecutingContext(ResultExecutingContext context, DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResultExecutingContext) Add tests
      if (context == null)
        return;

      WithHttpRequest(context.HttpContext.Request, forceOverwrite)
        .WithRouteData(context.RouteData, forceOverwrite)
        .SetResultsStartTime(utcNow, forceOverwrite);
    }

    public void WithResultExecutedContext(ResultExecutedContext context, DateTime utcNow, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithResultExecutedContext) Add tests
      SetResultsEndTime(utcNow, forceOverwrite);
    }

    public void WithExceptionContext(ExceptionContext context, DateTime utcNow, bool forceOverwrite = true)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.WithExceptionContext) Add tests
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
      // TODO: [TESTS] (ApiMetricRequestContext.CompleteMiddlewareRequest) Add tests
      SetRequestEndTime(utcNow);
      SetMiddlewareEndTime(utcNow);
      WithRouteData(httpContext.GetRouteData());
      WithHttpRequest(httpContext.Request);
      WithHttpResponse(httpContext.Response);
    }


    // Internal methods
    private ApiMetricRequestContext SetRequestCookieCount(HttpRequest request, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestCookieCount) Add tests
      if (RequestCookieCount > 0 && !forceOverwrite)
        return this;

      var cookiesCount = request?.Cookies?.Count ?? 0;
      if (cookiesCount > 0)
        RequestCookieCount = cookiesCount;

      return this;
    }

    private ApiMetricRequestContext SetRequestHeaderCount(HttpRequest request, bool forceOverwrite = false)
    {
      // TODO: [TESTS] (ApiMetricRequestContext.SetRequestHeaderCount) Add tests
      if (RequestHeaderCount > 0 && !forceOverwrite)
        return this;

      var headerCount = request?.Headers?.Count ?? 0;
      if (headerCount > 0)
        RequestHeaderCount = headerCount;

      return this;
    }
  }
}
