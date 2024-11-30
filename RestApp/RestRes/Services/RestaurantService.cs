using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RestRes.Models;

namespace RestRes.Services
{
    public class RestaurantService : IReservationService
    {
        private RestaurantReservationDbContext _dbContext;
        public RestaurantService(RestaurantReservationDbContext dbContext) 
        { 
            _dbContext  = dbContext;
        }
        public void AddRestaurant(Restaurant newRestaurant)
        {
            _dbContext.Restaurants.Add(newRestaurant);
            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
            _dbContext.SaveChanges();
        }
        public void DeleteRestaurant(Restaurant restaurant)
        {
            var restaurantToDelete = _dbContext.Restaurants.Where(r => r.Id == restaurant.Id).FirstOrDefault();
            if(restaurantToDelete != null)
            {
                 _dbContext.Restaurants.Remove(restaurantToDelete);
                 _dbContext.ChangeTracker.DetectChanges();
                 Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                 _dbContext.SaveChanges();
            }
            else
            {
                throw new  ArgumentException("The restaurant to delete cannot be found.");
            }

        }

        public Restaurant? GetRestaurantById(ObjectId Id)
        {
            //var restaurant = _dbContext.Restaurants.Where(r=> r.Id == Id).FirstOrDefault();
            //if(restaurant != null)
            //{
            //    return restaurant;
            //}
            return  _dbContext.Restaurants.Where(r => r.Id == Id).FirstOrDefault();
        }

        public void EditRestaurant(Restaurant updateRestaurant)
        {
            var restaurantToUpdate = _dbContext.Restaurants.Where(x => x.Id == updateRestaurant.Id).FirstOrDefault();
            if(restaurantToUpdate != null)
            {
                restaurantToUpdate.Name = updateRestaurant.Name;
                restaurantToUpdate.Cuisine = updateRestaurant.Cuisine;
                restaurantToUpdate.Borough = updateRestaurant.Borough;

                _dbContext.Restaurants.Update(restaurantToUpdate);
                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                _dbContext.SaveChanges();
            }else
            {
                throw new ArgumentException("The restaurant to update cannot be found.");
            }
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _dbContext.Restaurants.OrderByDescending(x => x.Id).Take(20).AsNoTracking().AsEnumerable();
        }

    }
}
