
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using API.CustomValidators;
using API.Filters;
using API.Filters.ExceptionFilters;
using API.Model;
using Domain.Entities.Villa;
using Infrastructure.Persistence;
using Application.Villas.DTOs;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VillaController : ControllerBase
  {
    private ApplicationDbContext _applicationDbContext;
    private IMapper _autoMapper;

    public VillaController(ApplicationDbContext applicationDbContext, IMapper autoMapper)
    {
      _applicationDbContext = applicationDbContext;
      _autoMapper = autoMapper;
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
      List<Villa>? villas = await _applicationDbContext.Villas.ToListAsync<Villa>();
      List<VillaDTO>? vlilasDTO = _autoMapper.Map<List<VillaDTO>>(villas);
      ApiResponse<List<VillaDTO>>? villaResponse = ApiResponse<List<VillaDTO>>.Ok(vlilasDTO, "Retrieve Data Successful");
      return Ok(villaResponse);
    }

    [HttpGet("{id}")]
    // [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<VillaDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [TypeFilter(typeof(Villa_ValidateGetByIdFilterAttribute))]

    public async Task<ActionResult<ApiResponse<VillaDTO>>> GetVillaById(int id)
    {
      // id = 0 ==> model validation 
      // id < 0 bad request ==> action filter
      // no found ==> action filter

      VillaDTO? villaDTO = HttpContext.Items["validatedVilla"] as VillaDTO;
      var testField = villaDTO!.Id;
      ApiResponse<VillaDTO>? villaResponse = ApiResponse<VillaDTO>.Ok(villaDTO!, "Retrieve Data Successfully");
      return Ok(villaResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<VillaDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(Villa_ValidateCreateVillaFilterAttribute))]

    public async Task<ActionResult<ApiResponse<VillaDTO>>> CreateVilla(VillaCreateDTO? villaCreateDTO)
    {
      // 1. villaCreateDTO == null; 2. duplicated villa
      Villa? villa = _autoMapper.Map<Villa>(villaCreateDTO);

      await _applicationDbContext.Villas.AddAsync(villa);

      await _applicationDbContext.SaveChangesAsync();

      VillaDTO? villaDTO = _autoMapper.Map<VillaDTO>(villa);

      ApiResponse<VillaDTO>? response = ApiResponse<VillaDTO>.Ok(villaDTO, "Create Successfully");

      return CreatedAtAction(nameof(GetVillaById), new { id = villa.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<VillaDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(Villa_HandleUpdateExceptionsFilterAttribute))]
    public async Task<ActionResult<ApiResponse<VillaDTO>>> UpdateVilla(int Id, VillaUpdateDTO villaUpdateDTO)
    {
      // 1. id < 0; 2. id != villaUpdateDTO.Id; 3. not found villa 
      if (Id != villaUpdateDTO.Id) return BadRequest();

      var villa = HttpContext.Items["validatedVilla"] as Villa;

      // comment this part for exception filter test
      // if (villa == null)
      // {
      //   return NotFound(ApiResponse<object>.NotFound("Villa not fund"));
      // }

      _autoMapper.Map(villaUpdateDTO, villa);
      villa!.UpdatedDate = DateTime.Now;

      //  here need try catch as the item could be deleted already
      await _applicationDbContext.SaveChangesAsync();

      ApiResponse<VillaUpdateDTO>? response = ApiResponse<VillaUpdateDTO>.Ok(villaUpdateDTO, "Update Successfully");
      return Ok(response);
    }


    [HttpDelete]
    [ProducesResponseType(typeof(ApiResponse<VillaDTO>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<VillaDTO>>> Delete(int Id)
    {
      if (Id == 0)
      {
        return BadRequest();
      }

      var villa = await _applicationDbContext.Villas.FirstOrDefaultAsync(x => x.Id == Id);

      if (villa == null)
      {
        return NotFound(ApiResponse<object>.NotFound("Villa not fund"));
      }

      _applicationDbContext.Remove(villa);
      await _applicationDbContext.SaveChangesAsync();

      var response = ApiResponse<object>.NoContent("Delete Successfully");
      return Ok(response);
    }
  }
}