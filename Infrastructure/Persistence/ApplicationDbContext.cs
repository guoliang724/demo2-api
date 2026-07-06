

using Domain.Entities.Restaurant;
using Domain.Entities.User;
using Domain.Entities.Villa;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
  public class ApplicationDbContext : DbContext
  {
    internal object villas;

    public DbSet<Villa> Villas { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);

      modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes)
          .WithOne()
          .HasForeignKey(d => d.RestaurantId);

    }
  }
}