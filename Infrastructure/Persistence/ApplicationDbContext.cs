

using Domain.Entities.Countries;
using Domain.Entities.Restaurant;
using Domain.Entities.Roles;
using Domain.Entities.User;
using Domain.Entities.Villa;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
  public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
  {
    public DbSet<Villa> Villas { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<User> Own_Implement_Users { get; set; }

    public DbSet<Country> Countries { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);

      modelBuilder.Entity<Restaurant>().Navigation(a => a.Address).IsRequired();

      modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes)
          .WithOne()
          .HasForeignKey(d => d.RestaurantId);

      modelBuilder.Entity<Dish>()
         .Property(d => d.Price)
         .HasPrecision(18, 2);

      modelBuilder.Entity<Villa>().ToTable("villas");

    }
  }
}