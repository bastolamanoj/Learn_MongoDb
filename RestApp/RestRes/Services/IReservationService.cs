using MongoDB.Bson;
using RestRes.Models;

namespace RestRes.Services
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservations();
        Reservation? GetReservationById(ObjectId Id);
        void AddReservation(Reservation newReservation);
        void EditReservation(Reservation updateReservation);
        void DeleteReservation(Reservation reservationToDelete);
    }
}
