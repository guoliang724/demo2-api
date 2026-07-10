using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Users.Commands
{
  public class UpdateUserCommand : IRequest
  {
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
  }
}