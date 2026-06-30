
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RoyalVilla.Model;
using RoyalVilla.Model.DTO;

namespace RoyalVilla.Filters
{
  public class Villa_ValidateCreateVillaFilterAttribute : ActionFilterAttribute
  {
    private readonly IMapper _autoMapper;
    private readonly ApplicationDbContext _dbContext;

    public Villa_ValidateCreateVillaFilterAttribute(IMapper autoMapper, ApplicationDbContext dbContext)
    {
      _autoMapper = autoMapper;
      _dbContext = dbContext;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      // context.ActionArguments.TryGetValue("villaCreateDTO", out var createDTO);

      var createDTO = context.ActionArguments["villaCreateDTO"] as VillaCreateDTO;

      if (createDTO is null)
      {

        var response = ApiResponse<VillaDTO>.BadRequest("Request data is null");
        context.Result = new BadRequestObjectResult(response);
      }
      else
      {
        Villa? duplicatedVilla = await _dbContext.villas.FirstOrDefaultAsync(x => x.Name.ToLower() == createDTO.Name.ToLower());
        if (duplicatedVilla != null)
        {
          context.Result = new ConflictObjectResult(ApiResponse<VillaDTO>.Conflicts("This villa has existed!"));
        }
        else await next();
      }
      // 执行后
      // var afterContext = context;

    }
  }
}