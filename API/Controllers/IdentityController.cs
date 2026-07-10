using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/identity")]
  public class IdentityController : ControllerBase
  {
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserCommand command)
    {
      await _mediator.Send(command);
      return NoContent();
    }
  }
}