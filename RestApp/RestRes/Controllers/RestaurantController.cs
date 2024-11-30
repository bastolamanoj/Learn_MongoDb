using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RestRes.Models;
using RestRes.Services;
using RestRes.ViewModel;
namespace RestRes.Controllers
{
    public class RestaurantController : Controller
    {

        private IRestaurantService _RestaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _RestaurantService = restaurantService;
        }

        public IActionResult Index()
        {
            RestaurantListViewModel viewModel = new RestaurantListViewModel()
            {
                Restaurants = _RestaurantService.GetAllRestaurant(),
            };
            return View(viewModel);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RestaurantAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Restaurant newRestaurant = new Restaurant()
                {
                    Name = viewModel.Restaurant.Name,
                    Borough = viewModel.Restaurant.Borough,
                    Cuisine = viewModel.Restaurant.Cuisine
                };
                _RestaurantService.AddRestaurant(newRestaurant);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Edit(ObjectId id)
        {
            if (id == null || id == ObjectId.Empty)
            {
                return NotFound();
            }
            var selectedRestaurant = _RestaurantService.GetRestaurantById(id);
            return View(selectedRestaurant);
        }

        public IActionResult Edit(Restaurant restaurant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _RestaurantService.EditRestaurant(restaurant);
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the restaurant failed, please try again!! Error");
            }

            return View(restaurant);
        }

        public IActionResult Delete(ObjectId id)
        {
            if (id == ObjectId.Empty || id == null)
            {
                return NotFound();
            }

            var selectedRestaurant = _RestaurantService.GetAllRestaurant();
            return View(selectedRestaurant);
        }

        [HttpPost]
        public IActionResult Delete(Restaurant restaurant)
        {
            if (restaurant.Id== ObjectId.Empty)
            {
                ModelState.AddModelError("", $"Deleting the restaurant failed, Invalid ID!");
                return View();
            }
            try
            {
                _RestaurantService.DeleteRestaurant(restaurant);
                TempData["RestaurantDeleted"] = $"Restaurant Deleted successfully.";

            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting restaurant failed, try again!!";
            }

            var selectedRestaurant = _RestaurantService.GetRestaurantById(restaurant.Id);
            return View(selectedRestaurant);

        }



    }
}
