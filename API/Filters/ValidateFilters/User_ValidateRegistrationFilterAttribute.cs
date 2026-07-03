using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using API.Model.DTO;
using Infrastructure.Persistence;

namespace API.Filters.ValidateFilters
{

  public class User_ValidateRegistrationFilterAttribute : ActionFilterAttribute
  {
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public User_ValidateRegistrationFilterAttribute(ApplicationDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
    }


    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

      if (!context.ActionArguments.TryGetValue("registrationRequestDTO", out var arg) ||
            arg is not RegistrationRequestDTO regDTO)
      {

        context.Result = new BadRequestObjectResult(ApiResponse<object>.BadRequest("Invalid registration request."));
        return;
      }

      var isDuplicated = await _db.users.AnyAsync(x => x.Email == regDTO.Email);

      if (isDuplicated)
      {
        var response = ApiResponse<object>.Conflicts("User has registered already");
        context.Result = new ConflictObjectResult(response);
        return;
      }

      await next();


    }
  }
}