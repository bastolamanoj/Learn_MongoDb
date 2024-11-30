
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
namespace RestRes.Models
{
    [Collection("Reservations")]
    public class Reservation{
          public ObjectId Id { get; set; }   
          public ObjectId RestaurantId {get;set;}
          public string? RestaurantName { get; set; }
          [Required(ErrorMessage = "The date and time is required to make this reservation.")]
          [Display(Name ="Name")]
          public DateTime Date {get;set;}
    }
}
