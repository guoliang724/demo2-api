using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Restaurant;

namespace Application.Dishes
{
  public class DishesProfile : Profile
  {
    public DishesProfile()
    {
      CreateMap<Dish, DishDTO>();
    }
  }
}