using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RoyalVilla.Model;
using RoyalVilla.Model.DTO;

namespace RoyalVilla.Filters
{
  public class Villa_ValidateGetByIdFilterAttribute : ActionFilterAttribute
  {
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public Villa_ValidateGetByIdFilterAttribute(ApplicationDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      var id = context.ActionArguments["Id"] as int?;
      if (id.HasValue)
      {

        if (id < 0)
        {
          var response = ApiResponse<VillaDTO>.BadRequest("Id cannot be below 0");
          context.Result = new BadRequestObjectResult(response);
        }
        else
        {
          Villa? villa = await _db.villas.FirstOrDefaultAsync(x => x.Id == id);

          if (villa is null)
          {
            ApiResponse<VillaDTO>? response = ApiResponse<VillaDTO>.NotFound("Not found");
            context.Result = new NotFoundObjectResult(response);
          }
          else
          {
            VillaDTO? villaDTO = _mapper.Map<VillaDTO>(villa);
            context.HttpContext.Items["validatedVilla"] = villaDTO;
            await next();
          }
        }
      }


    }
  }
}