using Rn.NetCore.Common.Metrics.Models;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Metrics
{
  public class NullApiCallMetricBuilder : IApiCallMetricBuilder
  {
    public bool IsNullMetricBuilder { get; }

    public NullApiCallMetricBuilder()
    {
      // TODO: [TESTS] (NullApiCallMetricBuilder) Add tests
      IsNullMetricBuilder = true;
    }

    public IApiCallMetricBuilder WithActionTime(ApiMetricRequestContext context) => this;
    public IApiCallMetricBuilder WithResultTime(ApiMetricRequestContext context) => this;
    public IApiCallMetricBuilder WithMiddlewareTime(ApiMetricRequestContext context) => this;
    public IApiCallMetricBuilder WithExceptionTime(ApiMetricRequestContext context) => this;
    public IApiCallMetricBuilder WithRequestRunTime(ApiMetricRequestContext context) => this;
    public IApiCallMetricBuilder WithController(string controller) => this;
    public IApiCallMetricBuilder WithAction(string action) => this;
    public IApiCallMetricBuilder WithExceptionName(string exceptionName) => this;
    public IApiCallMetricBuilder WithRequestMethod(string method) => this;
    public IApiCallMetricBuilder WithRequestContentType(string contentType) => this;
    public IApiCallMetricBuilder WithRequestProtocol(string protocol) => this;
    public IApiCallMetricBuilder WithRequestScheme(string scheme) => this;
    public IApiCallMetricBuilder WithRequestHost(string host) => this;
    public IApiCallMetricBuilder WithResponseCode(int responseCode) => this;
    public IApiCallMetricBuilder WithResponseContentType(string contentType) => this;
    public IApiCallMetricBuilder WithApiMetricRequestContext(ApiMetricRequestContext context) => this;

    public CoreMetric GetRawMetric() => null;
  }
}