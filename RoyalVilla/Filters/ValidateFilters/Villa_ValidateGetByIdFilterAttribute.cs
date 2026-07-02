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
    private readonly IMapper _mapper; // 这次 _mapper 派上用场了，保留！

    public Villa_ValidateGetByIdFilterAttribute(ApplicationDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      // 1. 安全提取 Id，防范未传参或类型不匹配
      if (!context.ActionArguments.TryGetValue("Id", out var arg) || arg is not int id)
      {
        context.Result = new BadRequestObjectResult(ApiResponse<VillaDTO>.BadRequest("Invalid or missing Id."));
        return;
      }

      // 2. 卫语句：校验 Id 范围
      if (id < 0)
      {
        context.Result = new BadRequestObjectResult(ApiResponse<VillaDTO>.BadRequest("Id cannot be below 0"));
        return;
      }

      // 3. 性能优化：因为只是读取和映射，加 AsNoTracking() 彻底免除 EF 内存追踪开销
      var villa = await _db.villas
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == id);

      // 4. 卫语句：校验是否存在
      if (villa is null)
      {
        context.Result = new NotFoundObjectResult(ApiResponse<VillaDTO>.NotFound("Villa not found"));
        return;
      }

      // 5. 组装数据并传递给 Controller，继续执行
      var villaDTO = _mapper.Map<VillaDTO>(villa);
      context.HttpContext.Items["validatedVilla"] = villaDTO;

      await next();
    }
  }
}