using Rn.NetCore.Metrics.Builders;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Metrics;

public interface IApiCallMetricBuilder : IMetricBuilder
{
  IApiCallMetricBuilder WithActionTime(ApiMetricRequestContext context);
  IApiCallMetricBuilder WithResultTime(ApiMetricRequestContext context);
  IApiCallMetricBuilder WithMiddlewareTime(ApiMetricRequestContext context);
  IApiCallMetricBuilder WithExceptionTime(ApiMetricRequestContext context);
  IApiCallMetricBuilder WithRequestRunTime(ApiMetricRequestContext context);
  IApiCallMetricBuilder WithController(string controller);
  IApiCallMetricBuilder WithAction(string action);
  IApiCallMetricBuilder WithExceptionName(string exceptionName);
  IApiCallMetricBuilder WithRequestMethod(string method);
  IApiCallMetricBuilder WithRequestContentType(string contentType);
  IApiCallMetricBuilder WithRequestProtocol(string protocol);
  IApiCallMetricBuilder WithRequestScheme(string scheme);
  IApiCallMetricBuilder WithRequestHost(string host);
  IApiCallMetricBuilder WithResponseCode(int responseCode);
  IApiCallMetricBuilder WithResponseContentType(string contentType);
  IApiCallMetricBuilder WithApiMetricRequestContext(ApiMetricRequestContext context);
}