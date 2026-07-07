using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Restaurants.Commands.CreateRestaurant;
using Application.Restaurants.Commands.UpdateRestaurant;
using AutoMapper;
using Domain.Entities.Restaurant;

namespace Application.DTOs
{
  public class RestaurantsProfile : Profile
  {
    public RestaurantsProfile()
    {

     CreateMap<UpdateRestaurantCommand, Restaurant>();

      CreateMap<CreateRestaurantCommand, Restaurant>()
          .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
          {
            City = src.City,
            PostalCode = src.PostalCode,
            Street = src.Street
          }));


      CreateMap<Restaurant, RestaurantResponseDTO>()
          .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
           .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
  }
}