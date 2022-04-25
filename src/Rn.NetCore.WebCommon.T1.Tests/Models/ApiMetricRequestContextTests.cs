using System;
using NUnit.Framework;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.T1.Tests.Models;

[TestFixture]
public class ApiMetricRequestContextTests
{
  [Test]
  public void ApiMetricRequestContext_Given_Constructed_Should_Default_TimeProperties()
  {
    // arrange
    var context = new ApiMetricRequestContext();

    // assert
    Assert.IsNull(context.StartTime);
    Assert.IsNull(context.EndTime);
    Assert.IsNull(context.ActionStartTime);
    Assert.IsNull(context.ActionEndTime);
    Assert.IsNull(context.ResultsStartTime);
    Assert.IsNull(context.ResultsEndTime);
    Assert.IsNull(context.ExThrownTime);
    Assert.IsNull(context.MiddlewareStartTime);
    Assert.IsNull(context.MiddlewareEndTime);
  }

  [Test]
  public void ApiMetricRequestContext_Given_Constructed_Should_Default_StringProperties()
  {
    // arrange
    var context = new ApiMetricRequestContext();

    // assert
    Assert.AreEqual(string.Empty, context.Controller);
    Assert.AreEqual(string.Empty, context.Action);
    Assert.AreEqual(string.Empty, context.Guid);
    Assert.AreEqual(string.Empty, context.ExceptionName);
    Assert.AreEqual(string.Empty, context.Method);
    Assert.AreEqual(string.Empty, context.ContentType);
    Assert.AreEqual(string.Empty, context.Protocol);
    Assert.AreEqual(string.Empty, context.Scheme);
    Assert.AreEqual(string.Empty, context.Host);
    Assert.AreEqual(string.Empty, context.ResponseContentType);
  }

  [Test]
  public void ApiMetricRequestContext_Given_Constructed_Should_Default_NumericProperties()
  {
    // arrange
    var context = new ApiMetricRequestContext();

    // assert
    Assert.AreEqual(0, context.ContentLength);
    Assert.AreEqual(0, context.CookieCount);
    Assert.AreEqual(0, context.HeaderCount);
    Assert.AreEqual(0, context.Port);
    Assert.AreEqual(0, context.ResponseCode);
    Assert.AreEqual(0, context.ResponseContentLength);
    Assert.AreEqual(0, context.ResponseHeaderCount);
  }

  [Test]
  public void ApiMetricRequestContext_Given_ConstructedWithDateTime_ShouldSet_StartTime()
  {
    // arrange
    var now = DateTime.Now;

    // act
    var context = new ApiMetricRequestContext(now);

    // assert
    Assert.AreEqual(now, context.StartTime);
  }

  [Test]
  public void SetController_Given_EmptyString_ShouldDoNothing()
  {
    // arrange
    var context = new ApiMetricRequestContext();

    // act
    Assert.AreEqual(string.Empty, context.Controller);
    context = context.SetController("");

    // assert
    Assert.IsInstanceOf<ApiMetricRequestContext>(context);
    Assert.AreEqual(string.Empty, context.Controller);
  }

  [Test]
  public void SetController_Given_ControllerSet_NoOverwrite_ShouldDoNothing()
  {
    // arrange
    var context = new ApiMetricRequestContext().SetController("TestController");

    // act
    Assert.AreEqual("TestController", context.Controller);
    context = context.SetController("TestController2");

    // assert
    Assert.IsInstanceOf<ApiMetricRequestContext>(context);
    Assert.AreEqual("TestController", context.Controller);
  }

  [Test]
  public void SetController_Given_ControllerSet_WithOverwrite_ShouldUpdate()
  {
    // arrange
    var context = new ApiMetricRequestContext().SetController("TestController");

    // act
    Assert.AreEqual("TestController", context.Controller);
    context = context.SetController("TestController2", true);

    // assert
    Assert.IsInstanceOf<ApiMetricRequestContext>(context);
    Assert.AreEqual("TestController2", context.Controller);
  }
}