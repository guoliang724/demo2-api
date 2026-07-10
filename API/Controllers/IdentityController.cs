
using Application.Roles.Commands.AssignRole;
using Application.Roles.Commands.CreateRole;
using Application.Roles.Commands.DeleteRole;
using Application.Roles.Commands.UpdateRole;
using Application.Users.Commands.UpdateUser;
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


    [HttpPost]
    [Route("role/create")]
    public async Task<IActionResult> CreateRole(CreateRoleCommand command)
    {
      var role = await _mediator.Send(command);
      return Ok(role);
    }

    [HttpPut("role/update")]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommand command)
    {
      await _mediator.Send(command);
      return Ok(command);
    }

    [HttpDelete("role/delete/{RoleName}")]
    public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommand command)
    {
      await _mediator.Send(command);
      return NoContent();
    }

    [HttpPost("role/assign")]
    public async Task<IActionResult> AssignToRole([FromBody] AssignRoleCommand command)
    {
      await _mediator.Send(command);
      return Ok($"Assigned {command.Id} to {command.RoleName}");
    }
  }
}