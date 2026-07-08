using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Restaurants;
using Application.Restaurants.Commands.CreateRestaurant;
using Application.Restaurants.Commands.DeleteRestaurant;
using Application.Restaurants.Commands.UpdateRestaurant;
using Application.Restaurants.Queries.GetAllRestaurants;
using Application.Restaurants.Queries.GetAllRestaurants.GetRestaurantById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RestaurantController : ControllerBase
  {
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RestaurantController(IMediator mediator, IMapper mapper)
    {
      _mediator = mediator;
      _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantResponseDTO>>> GetRestaurants()
    {
      IEnumerable<RestaurantResponseDTO>? restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
      return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantResponseDTO>> GetRestaurantById(int id)
    {
      RestaurantResponseDTO? restaurant = await _mediator.Send(new GetRestaurantByIdQuery { Id = id });

      return Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantResponseDTO>> CreateRestaurant(CreateRestaurantCommand restaurantCommand)
    {

      var restaurantId = await _mediator.Send(restaurantCommand);

      return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantId }, restaurantCommand);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RestaurantResponseDTO>> UpdateRestaurant(int id, UpdateRestaurantCommand restaurantCommand)
    {
      if (id != restaurantCommand.Id)
      {
        return BadRequest();
      }
      await _mediator.Send(restaurantCommand);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
      var restaurantCommand = new DeleteRestaurantCommand { Id = id };
      await _mediator.Send(restaurantCommand);
      return NoContent();
    }

  }
}