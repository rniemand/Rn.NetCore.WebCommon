using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Extensions
{
  public static class HttpContextExtensions
  {
    public static bool HasApiRequestMetricContext(this HttpContext context)
    {
      // TODO: [TESTS] (HttpContextExtensions.HasApiRequestMetricContext) Add tests
      return context?.Items?.ContainsKey(WebKeys.RequestContextKey) ?? false;
    }

    public static ApiMetricRequestContext GetApiRequestMetricContext(this HttpContext context, DateTime utcNow)
    {
      // TODO: [TESTS] (HttpContextExtensions.GetApiRequestMetricContext) Add tests
      if (!context.HasApiRequestMetricContext())
      {
        context.Items[WebKeys.RequestContextKey] = new ApiMetricRequestContext(utcNow);
      }

      return context.GetApiRequestMetricContext();
    }

    public static ApiMetricRequestContext GetApiRequestMetricContext(this HttpContext context)
    {
      // TODO: [TESTS] (HttpContextExtensions.GetApiRequestMetricContext) Add tests
      if (!context.HasApiRequestMetricContext())
        return null;

      if (!(context.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext metricContext))
        return null;

      return metricContext;
    }

    public static string GetRouteKey(this RouteData routeData, string key, string fallback = null)
    {
      // TODO: [TESTS] (HttpContextExtensions.GetRouteKey) Add tests
      if (!(routeData?.Values?.Keys?.Contains(key) ?? false))
        return fallback;

      return (string) routeData.Values[key];
    }
  }
}
