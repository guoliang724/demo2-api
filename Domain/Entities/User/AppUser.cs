
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.User
{
  public class AppUser : IdentityUser
  {

    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
  }
}