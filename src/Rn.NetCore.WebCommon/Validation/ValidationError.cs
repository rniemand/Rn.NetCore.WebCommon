using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Rn.NetCore.WebCommon.Validation;

public class ValidationError
{
  public bool IsValid { get; set; }
  public List<string> Errors { get; set; }
  public string[] RuleSetsExecuted { get; set; }
  public string Error { get; set; }

  public ValidationError()
  {
    IsValid = false;
    Errors = new List<string>();
    RuleSetsExecuted = Array.Empty<string>();
    Error = string.Empty;
  }

  public ValidationError(ValidationResult result)
    : this()
  {
    IsValid = result.IsValid;

    foreach (var error in result.Errors)
      Errors.Add(error.ToString());

    RuleSetsExecuted = result.RuleSetsExecuted;
    Error = result.ToString(Environment.NewLine);
  }
}
