using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Roles;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Commands.UpdateRole
{
  public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
  {
    private readonly RoleManager<AppRole> _manager;

    public UpdateRoleCommandHandler(RoleManager<AppRole> manager)
    {
      _manager = manager;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
      var role = await _manager.FindByIdAsync(request.Id) ?? throw new NotFoundException("The role is not existed");

      role.Name = request.Name;

      await _manager.UpdateAsync(role);
    }
  }
}