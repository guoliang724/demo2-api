using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Countries.CountriesDTO;
using Microsoft.AspNetCore.Http;

namespace Application.Countries.Service
{
  public interface ICountriesService
  {
    Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
    Task<List<CountryResponse>> GetAllCountries();

    Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);

  }
}