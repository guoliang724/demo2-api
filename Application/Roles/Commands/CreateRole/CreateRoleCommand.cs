using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Roles.DTOs;
using MediatR;

namespace Application.Roles.Commands.CreateRole
{
  public class CreateRoleCommand : IRequest<RoleResponseDTO>
  {
    public string RoleName { get; set; } = string.Empty;
  }
}