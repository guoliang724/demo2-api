using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand:IRequest
    {
        public string RoleName {get;set;} = string.Empty;
    }
}