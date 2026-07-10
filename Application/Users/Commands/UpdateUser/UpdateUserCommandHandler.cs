using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.User;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Users.Commands.UpdateUser
{
  public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
  {
    private readonly IHttpContextAccessor _http;
    private readonly IUserStore<AppUser> _userStore;

    public UpdateUserCommandHandler(IHttpContextAccessor http, IUserStore<AppUser> userStore)
    {
      _http = http;
      _userStore = userStore;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
      var httpContext = _http.HttpContext ?? throw new InvalidOperationException("User context is not present");
      var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NotFoundException("No Auth");
      var dbUser = await _userStore.FindByIdAsync(userId, cancellationToken);

      if (dbUser == null)
      {
        throw new NotFoundException("User not exist");
      }

      dbUser.Nationality = request.Nationality;
      dbUser.DateOfBirth = request.DateOfBirth;

      await _userStore.UpdateAsync(dbUser, cancellationToken);
    }
  }
}