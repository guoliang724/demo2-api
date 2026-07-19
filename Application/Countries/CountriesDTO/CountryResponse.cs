using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Countries;

namespace Application.Countries.CountriesDTO
{
  public class CountryResponse
  {
    public Guid CountryID { get; set; }
    public string? CountryName { get; set; }

  }
  public static class CountryExtensions
  {
    //Converts from Country object to CountryResponse object
    public static CountryResponse ToCountryResponse(this Country country)
    {
      return new CountryResponse() { CountryID = country.CountryID, CountryName = country.CountryName };
    }
  }
}