
using Application.Villas.DTOs;
using AutoMapper;
using Domain.Entities.Villa;


namespace Application.Villas
{
  public class VillasProfile : Profile
  {
    public VillasProfile()
    {
      CreateMap<VillaDTO, Villa>().ReverseMap();
      CreateMap<VillaCreateDTO, Villa>().ReverseMap();
      CreateMap<VillaUpdateDTO, Villa>().ReverseMap();
    }
  }
}