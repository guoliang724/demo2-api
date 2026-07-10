using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Roles.Commands.CreateRole
{
  public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
  {
    public CreateRoleCommandValidator()
    {
      RuleFor(x => x.RoleName).NotEmpty().WithMessage("RoleName is required.");
    }
  }
}