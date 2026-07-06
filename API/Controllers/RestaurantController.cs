using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Restaurants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RestaurantController : ControllerBase
  {
    private readonly RestaurantService _restaurantService;
    private readonly IMapper _mapper;

    public RestaurantController(RestaurantService restaurantService, IMapper mapper)
    {
      _restaurantService = restaurantService;
      _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantResponseDTO>>> GetRestaurants()
    {
      var restaurants = await _restaurantService.GetRestaurants();
      var restaurantDTOs = _mapper.Map<IEnumerable<RestaurantResponseDTO>>(restaurants);
      return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantResponseDTO>> GetRestaurantById(int id)
    {
      var restaurant = await _restaurantService.GetRestaurantById(id);
      var restaurantDTO = _mapper.Map<RestaurantResponseDTO>(restaurant);

      if (restaurant == null)
      {
        return NotFound();
      }
      return Ok(restaurant);
    }

  }
}