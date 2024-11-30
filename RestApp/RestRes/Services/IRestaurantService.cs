using MongoDB.Bson;
using RestRes.Models;

namespace RestRes.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetAllRestaurant();
        Restaurant? GetRestaurantById(ObjectId Id);
        void AddRestaurant(Restaurant newRestaurant);
        void EditRestaurant(Restaurant updateRestaurant);
        void DeleteRestaurant(Restaurant restaurantToDelete);

    }
}
