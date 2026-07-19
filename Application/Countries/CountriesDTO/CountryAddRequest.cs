using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Countries;

namespace Application.Countries.CountriesDTO
{
  public class CountryAddRequest
  {
    public string? CountryName { get; set; }
    public Country ToCountry()
    {
      return new Country() { CountryName = CountryName };
    }
  }
}