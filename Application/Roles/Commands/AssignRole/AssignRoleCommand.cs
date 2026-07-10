using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Roles.Commands.AssignRole
{
  public class AssignRoleCommand : IRequest
  {
    public string Id { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
  }
}