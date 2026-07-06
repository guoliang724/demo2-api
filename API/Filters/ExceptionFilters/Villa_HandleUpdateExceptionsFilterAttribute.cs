
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Domain.Entities.Villa;
using Application.Villas.DTOs;
using API.Model;

namespace API.Filters.ExceptionFilters
{
  public class Villa_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
  {
    private readonly ApplicationDbContext _db;
    public Villa_HandleUpdateExceptionsFilterAttribute(ApplicationDbContext db)
    {
      _db = db;
    }

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
      //  var testContext = context;
      string? id = context.RouteData.Values["id"] as string;
      if (int.TryParse(id, out var paramId))
      {
        Villa? villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == paramId);
        if (villa == null)
        {
          ApiResponse<VillaDTO>? response = ApiResponse<VillaDTO>.NotFound("Not found the item to update");
          context.Result = new NotFoundObjectResult(response);
        }
        else
        {
          context.HttpContext.Items["validatedVilla"] = villa;
        }
      }

    }
  }
}