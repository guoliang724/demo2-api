using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.Entities.Restaurant;
using Domain.Entities.User;
using Domain.Entities.Villa;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeders
{
  public class DataSeed : IDataSeed
  {
    private readonly ApplicationDbContext _context;

    public DataSeed(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task SeedDataAsync()
    {
      if (await _context.Database.CanConnectAsync())
      {
        // Check if the database is empty and seed data accordingly
        if (!_context.Villas.Any())
        {
          IEnumerable<Villa>? villas = GetVillas();
          await _context.Villas.AddRangeAsync(villas);
          await _context.SaveChangesAsync();
        }

        if (!_context.Restaurants.Any())
        {
          IEnumerable<Restaurant>? restaurants = GetRestaurants();
          await _context.Restaurants.AddRangeAsync(restaurants);
          await _context.SaveChangesAsync();
        }
      }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
      return new List<Restaurant>
      {
          new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        };
    }

    private IEnumerable<Villa> GetVillas()
    {
      return new List<Villa>
      {
           new Villa
                    {
                      Id = 1,
                      Name = "Royal Villa",
                      Details = "Luxurious villa with stunning ocean views and private beach access.",
                      Rate = 500.0,
                      Sqft = 2500,
                      Occupancy = 6,
                      ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg",
                      CreatedDate = new DateTime(2024, 1, 1),
                      UpdatedDate = new DateTime(2024, 1, 1)
                    },
                    new Villa
                    {
                      Id = 2,
                      Name = "Diamond Villa",
                      Details = "Elegant villa with marble interiors and panoramic mountain views.",
                      Rate = 750.0,
                      Sqft = 3200,
                      Occupancy = 8,
                      ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                      CreatedDate = new DateTime(2024, 1, 15),
                      UpdatedDate = new DateTime(2024, 1, 15)
                    },
                    new Villa
                    {
                      Id = 3,
                      Name = "Pool Villa",
                      Details = "Modern villa featuring an infinity pool and outdoor entertainment area.",
                      Rate = 350.0,
                      Sqft = 1800,
                      Occupancy = 4,
                      ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                      CreatedDate = new DateTime(2024, 2, 1),
                      UpdatedDate = new DateTime(2024, 2, 1)
                    },
                    new Villa
                    {
                      Id = 4,
                      Name = "Luxury Villa",
                      Details = "Premium villa with spa facilities and concierge services.",
                      Rate = 900.0,
                      Sqft = 4000,
                      Occupancy = 10,
                      ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg",
                      CreatedDate = new DateTime(2024, 2, 14),
                      UpdatedDate = new DateTime(2024, 2, 14)
                    },
                    new Villa
                    {
                      Id = 5,
                      Name = "Garden Villa",
                      Details = "Charming villa surrounded by tropical gardens and nature trails.",
                      Rate = 275.0,
                      Sqft = 1500,
                      Occupancy = 3,
                      ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg",
                      CreatedDate = new DateTime(2024, 3, 1),
                      UpdatedDate = new DateTime(2024, 3, 1)
                    }
      };
    }
  }
}