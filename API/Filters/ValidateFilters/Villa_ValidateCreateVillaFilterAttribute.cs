
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using API.Model;
using Application.Villas.DTOs;

namespace API.Filters
{
  public class Villa_ValidateCreateVillaFilterAttribute : ActionFilterAttribute
  {
    private readonly ApplicationDbContext _dbContext;
    // 删除了未使用的 IMapper _autoMapper，符合帕斯卡命名规范

    public Villa_ValidateCreateVillaFilterAttribute(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      // 1. 使用 TryGetValue + 模式匹配，优雅且安全地防御参数缺失
      if (!context.ActionArguments.TryGetValue("villaCreateDTO", out var arg) ||
          arg is not VillaCreateDTO createDTO)
      {
        var response = ApiResponse<VillaDTO>.BadRequest("Request data is null or invalid.");
        context.Result = new BadRequestObjectResult(response);
        return; // 提前返回，降低嵌套层级
      }

      // 2. 性能优化 A：使用 AnyAsync 代替 FirstOrDefaultAsync
      // 3. 性能优化 B：使用 EF.Functions.Like 或直接相等，避免 ToLower() 破坏数据库索引
      var isDuplicated = await _dbContext.Villas
          .AnyAsync(x => EF.Functions.Like(x.Name, createDTO.Name));
      // 如果你的数据库默认就是不区分大小写的（如 SQL Server / MySQL 默认排序规则），
      // 可以直接简化为：x => x.Name == createDTO.Name

      if (isDuplicated)
      {
        context.Result = new ConflictObjectResult(ApiResponse<VillaDTO>.Conflicts("This villa has existed!"));
        return;
      }

      // 4. 验证通过，继续执行后面的逻辑
      await next();

    }
  }
}