
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Infrastructure.Persistence;
using Application.Users.DTOs;
using API.Model;

namespace API.Filters.ValidateFilters
{
  public class User_ValidateLoginFilterAttribute : ActionFilterAttribute
  {
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public User_ValidateLoginFilterAttribute(ApplicationDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
    }


    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      if (!context.ActionArguments.TryGetValue("loginRequestDTO", out var arg) ||
            arg is not LoginRequestDTO loginDTO)
      {

        context.Result = new BadRequestObjectResult(ApiResponse<object>.BadRequest("Invalid login request."));
        return;
      }
      await next();
    }
  }
}