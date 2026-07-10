using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.Entities.Roles;
using Domain.Entities.User;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Commands.AssignRole
{
  public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand>
  {
    private readonly RoleManager<AppRole> _role;
    private readonly UserManager<AppUser> _user;

    public AssignRoleCommandHandler(RoleManager<AppRole> role, UserManager<AppUser> user)
    {
      _role = role;
      _user = user;
    }





    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
      var role = await _role.FindByNameAsync(request.RoleName) ?? throw new NotFoundException("Role is not existed");
      var user = await _user.FindByIdAsync(request.Id) ?? throw new NotFoundException("User is not existed");

      var result = await _user.AddToRoleAsync(user, role.Name ?? UserRoles.User);

      if (!result.Succeeded) throw new Exception("Binding is failed");
    }
  }
}