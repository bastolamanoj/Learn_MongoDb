
using MongoDB.Bson; 
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RestRes.Models
{
    // [Collection("Restaurants")]
    public class Restaurant{
        public ObjectId Id { get; set; }    

        [Required(ErrorMessage = "You must provide a name")]
        [Display(Name = "Name")]
        public string? Name { get; set; }    

        [Required(ErrorMessage ="Cuisine is required")]
        [Display(Name = "Cuisine")]
        public string? Cuisine { get; set; }

        [Required(ErrorMessage ="You must add the borough of the restaurant.")]
        public string? borough { get;}
    
    }

}