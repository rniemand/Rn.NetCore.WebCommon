using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Rn.NetCore.WebCommon.Validation;

namespace Rn.NetCore.WebCommon.Models.Responses;

public class BaseResponse<TResponse>
{
  public TResponse Response { get; set; }
  public ValidationResult ValidationResult { get; set; }
  public bool FailedValidation => !(ValidationResult?.IsValid ?? true);
  public bool PassedValidation => ValidationResult?.IsValid ?? true;

  public BaseResponse()
  {
    Response = default;
    ValidationResult = new ValidationResult();
  }

  public BaseResponse(TResponse response)
    : this()
  {
    Response = response;
  }

  public BaseResponse<TResponse> WithResponse(TResponse response)
  {
    Response = response;
    return this;
  }

  public BaseResponse<TResponse> WithValidation<TInstance>(TInstance instance, AbstractValidator<TInstance> validator, string ruleSet)
  {
    // No need to run additional validators if we have failed validation already
    if (FailedValidation)
      return this;

    WithValidation(validator.Validate(instance, strategy => strategy.IncludeRuleSets(ruleSet)));
    return this;
  }

  public BaseResponse<TResponse> WithValidation(AdHockValidator validator)
  {
    // No need to run additional validators if we have failed validation already
    if (FailedValidation)
      return this;

    ValidationResult = validator.Validate();
    return this;
  }

  public BaseResponse<TResponse> WithValidation(ValidationResult result)
  {
    // No need to run additional validators if we have failed validation already
    if (FailedValidation)
      return this;

    ValidationResult = result;
    return this;
  }

  public BaseResponse<TResponse> WithValidationError(string property, string message)
  {
    // No need to run additional validators if we have failed validation already
    if (FailedValidation)
      return this;

    ValidationResult = new ValidationResult(new List<ValidationFailure>
    {
      new ValidationFailure(property, message)
    });

    return this;
  }

  public BaseResponse<TResponse> WithIdNotFound(string objectName, int id)
  {
    // No need to run additional validators if we have failed validation already
    return FailedValidation
      ? this
      : WithValidationError(
        RnWebValidationError.IdNotFound.ToString("G"),
        $"Invalid {objectName} ID ({id})"
      );
  }
}
