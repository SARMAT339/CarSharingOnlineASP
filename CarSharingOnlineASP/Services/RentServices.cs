using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Services
{
    public class RentService : IRentService
    {
        private readonly List<Rent> _rents = new List<Rent>();
        private readonly List<Car> _cars;
        private readonly List<User> _users;
        private readonly List<Card> _cards;

        public RentService(List<Car> cars, List<User> users, List<Card> cards)
        {
            _cars = cars;
            _users = users;
            _cards = cards;
        }

        public Rent StartRent(Guid userId, Guid carId, DateTime startTime)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null || user.IsBlocked)
                throw new Exception("User not found or blocked");

            var car = _cars.FirstOrDefault(c => c.Id == carId);
            if (car == null)
                throw new Exception("Car not found");

            var userCard = _cards.FirstOrDefault(c => c.UserId == userId);
            if (userCard == null)
                throw new Exception("User has no payment card");

            var rent = new Rent
            {
                UserID = userId,
                car = car,
                StartTime = startTime,
                EndTime = DateTime.MinValue,
                TotalCost = 0
            };

            _rents.Add(rent);
            return rent;
        }

        public Rent EndRent(Guid rentId, DateTime endTime)
        {
            var rent = _rents.FirstOrDefault(r => r.UserID == rentId);
            if (rent == null)
                throw new Exception("Rent not found");

            if (endTime < rent.StartTime)
                throw new Exception("Invalid end time");

            rent.EndTime = endTime;
            var duration = (decimal)(endTime - rent.StartTime).TotalMinutes;
            rent.TotalCost = duration * rent.car.CostMinute;

            return rent;
        }

        public Rent GetRent(Guid rentId)
        {
            return _rents.FirstOrDefault(r => r.UserID == rentId) ??
                   throw new Exception("Rent not found");
        }

        public List<Rent> GetUserRents(Guid userId)
        {
            return _rents.Where(r => r.UserID == userId).ToList();
        }
    }

    public class StartRentRequest
    {
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
    }
}
