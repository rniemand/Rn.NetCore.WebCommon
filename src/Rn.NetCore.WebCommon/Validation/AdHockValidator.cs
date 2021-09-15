using System.Collections.Generic;
using FluentValidation.Results;
using Rn.NetCore.WebCommon.Models.Requests;

namespace Rn.NetCore.WebCommon.Validation
{
  public class AdHockValidator
  {
    public List<ValidationFailure> Errors { get; set; }

    public AdHockValidator()
    {
      // TODO: [TESTS] (AdHockValidator) Add tests
      Errors = new List<ValidationFailure>();
    }

    public AdHockValidator GreaterThanZero(string property, long actual)
    {
      return GreaterThan(property, 0, actual);
    }

    public AdHockValidator GreaterThan(string property, long amount, long actual)
    {
      if (actual <= amount)
      {
        Errors.Add(new ValidationFailure(
          property,
          $"'{property}' must be greater than '{amount}' (was {actual})",
          actual
        ));
      }

      return this;
    }

    public AdHockValidator NotNullOrWhiteSpace(string property, string actual)
    {
      if (string.IsNullOrWhiteSpace(actual))
      {
        Errors.Add(new ValidationFailure(
          property,
          $"'{property}' cannot be null or white space"
        ));
      }

      return this;
    }

    public AdHockValidator IsOwner(BaseApiRequest request, int? userId)
    {
      // TODO: [TESTS] (AdHockValidator.IsOwner) Add tests
      var saveUserId = userId ?? 0;

      if (saveUserId == 0)
      {
        Errors.Add(new ValidationFailure(
          "UserId",
          "Invalid UserId provided (0)"
        ));

        return this;
      }

      if (request.UserId == saveUserId)
        return this;

      Errors.Add(new ValidationFailure(
        nameof(request.UserId),
        $"Resource not owned by current user (resource owner: {saveUserId})"
      ));

      return this;
    }

    public AdHockValidator NotNull(string objName, object obj)
    {
      // TODO: [TESTS] (AdHockValidator.NotNull) Add tests
      if (obj == null)
      {
        Errors.Add(new ValidationFailure(objName, $"{objName} cannot be null"));
      }

      return this;
    }

    public ValidationResult Validate()
    {
      return new(Errors);
    }
  }
}
