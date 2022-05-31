using NUnit.Framework;
using Rn.NetCore.Metrics;

namespace Rn.NetCore.WebCommon.T1.Tests.Root;

[TestFixture]
public class MetricPlaceholderTests
{
  [Test]
  public void MetricPlaceholder_None_ShouldReturn_ExpectedValue()
  {
    Assert.AreEqual("(none)", MetricPlaceholder.None);
  }

  [Test]
  public void MetricPlaceholder_Unknown_ShouldReturn_ExpectedValue()
  {
    Assert.AreEqual("(unknown)", MetricPlaceholder.Unknown);
  }

  [Test]
  public void MetricPlaceholder_Unset_ShouldReturn_ExpectedValue()
  {
    Assert.AreEqual("(unset)", MetricPlaceholder.Unset);
  }
}
