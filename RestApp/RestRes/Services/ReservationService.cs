using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RestRes.Models;

namespace RestRes.Services
{
    public class ReservationService : IReservationService
    {
        private RestaurantReservationDbContext _dbContext;

        public ReservationService(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddReservation(Reservation newReservation)
        {
            _dbContext.Reservations.Add(newReservation);
            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
            _dbContext.SaveChanges();
        }
        public void DeleteReservation(Reservation Reservation)
        {
            var ReservationToDelete = _dbContext.Reservations.Where(r => r.Id == Reservation.Id).FirstOrDefault();
            if (ReservationToDelete != null)
            {
                _dbContext.Reservations.Remove(ReservationToDelete);
                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The Reservation to delete cannot be found.");
            }

        }

        public Reservation? GetReservationById(ObjectId Id)
        {
            //var Reservation = _dbContext.Reservations.Where(r=> r.Id == Id).FirstOrDefault();
            //if(Reservation != null)
            //{
            //    return Reservation;
            //}
            return _dbContext.Reservations.Where(r => r.Id == Id).FirstOrDefault();
        }

        public void EditReservation(Reservation updateReservation)
        {
            var ReservationToUpdate = _dbContext.Reservations.Where(x => x.Id == updateReservation.Id).FirstOrDefault();
            if (ReservationToUpdate != null)
            {
                ReservationToUpdate.RestaurantName = updateReservation.RestaurantName;
                ReservationToUpdate.Date = updateReservation.Date

                _dbContext.Reservations.Update(ReservationToUpdate);
                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The Reservation to update cannot be found.");
            }
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _dbContext.Reservations.OrderByDescending(x => x.Id).Take(20).AsNoTracking().AsEnumerable();
        }


    }
}
