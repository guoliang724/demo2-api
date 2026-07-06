using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
  public interface IDataSeed
  {
    public Task SeedDataAsync();
  }
}