using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Roles;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Commands.DeleteRole
{
  public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
  {
    private readonly RoleManager<AppRole> _role;

    public DeleteRoleCommandHandler(RoleManager<AppRole> role)
    {
      _role = role;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
      var role = await _role.FindByNameAsync(request.RoleName) ?? throw new NotFoundException("Role is not existed");

      await _role.DeleteAsync(role);
    }
  }
}