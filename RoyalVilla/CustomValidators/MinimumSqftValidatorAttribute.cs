using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalVilla.CustomValidators
{
  public class MinimumSqftValidatorAttribute : ValidationAttribute
  {
    public int MinimumSqft { get; set; } = 100;
    public string defaultMessage { get; set; } = "this is a default message {0}";
    public MinimumSqftValidatorAttribute() { }

    public MinimumSqftValidatorAttribute(int minimumSqft)
    {
      MinimumSqft = minimumSqft;
    }

    // 可以接收参数，但默认值是100
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

      if (value != null)
      {
        int sqtf = (int)value;
        if (sqtf < MinimumSqft)
        {
          return new ValidationResult(string.Format(ErrorMessage ?? defaultMessage, MinimumSqft));
        }

        return ValidationResult.Success;
      }

      return null;
    }
  }
}