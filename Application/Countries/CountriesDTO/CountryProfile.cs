using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Countries;

namespace Application.Countries.CountriesDTO
{
  public class CountryProfile : Profile
  {
    protected CountryProfile()
    {
      CreateMap<CountryAddRequest, Country>();
      CreateMap<CountryResponse, Country>();
    }
  }
}