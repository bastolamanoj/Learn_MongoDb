using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using RestRes.Models;

namespace RestRes.Services{

    public class RestaurantReservationDbContext : DbContext{
        public DbSet<Restaurant> Restaurants { get; set; }  

        public DbSet<Reservation> Reservations { get; set; }


        public RestaurantReservationDbContext(DbContextOptions options): base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Restaurant>().ToCollection("Restaurants");  
            modelBuilder.Entity<Restaurant>();  
            modelBuilder.Entity<Reservation>();

        }
    }
}