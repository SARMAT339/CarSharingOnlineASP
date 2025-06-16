using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Models;
using CarSharingOnlineASP.Data;

namespace CarSharingOnlineASP.Services
{
    public class RentService : IRentService
    {
        private readonly IRentsJSRepository _rentsRepository;
        private readonly ICarsJSRepository _carsRepository;
        private readonly IUsersJSRepository _usersRepository;

        public RentService(
            IRentsJSRepository rentsRepository,
            ICarsJSRepository carsRepository,
            IUsersJSRepository usersRepository)
        {
            _rentsRepository = rentsRepository;
            _carsRepository = carsRepository;
            _usersRepository = usersRepository;
        }

        public Rent StartRent(Guid userId, Guid carId, DateTime startTime, string startLocation)
        {
            var user = _usersRepository.TryGetById(userId);
            if (user == null || user.IsBlocked)
                throw new Exception("Пользователь не найден или заблокирован");

            var car = _carsRepository.TryGetById(carId);
            if (car == null)
                throw new Exception("Автомобиль не найден");

            if (!car.IsAvailable)
                throw new Exception("Автомобиль недоступен для аренды");

            var activeRent = _rentsRepository.GetByUserId(userId)
                .FirstOrDefault(r => r.Status == RentStatus.Active);
            if (activeRent != null)
                throw new Exception("У вас уже есть активная аренда");

            var rent = new Rent
            {
                UserID = userId,
                CarId = carId,
                Car = car,
                StartTime = startTime,
                EndTime = DateTime.MinValue,
                TotalCost = 0,
                Status = RentStatus.Active,
                StartLocation = startLocation
            };

            car.IsAvailable = false;
            _carsRepository.Updata(new CarEdit
            {
                Id = car.Id,
                Name = car.Name,
                Description = car.Description,
                CostMinute = car.CostMinute,
                Image = car.Image
            });
            _rentsRepository.Add(rent);
            return rent;
        }

        public Rent EndRent(Guid rentId, DateTime endTime, string endLocation)
        {
            var rent = _rentsRepository.TryGetById(rentId);
            if (rent == null)
                throw new Exception("Аренда не найдена");

            if (rent.Status != RentStatus.Active)
                throw new Exception("Аренда не активна");

            if (endTime < rent.StartTime)
                throw new Exception("Некорректное время завершения");

            rent.EndTime = endTime;
            rent.EndLocation = endLocation;
            rent.Status = RentStatus.Completed;
            rent.TotalCost = CalculateRentCost(rentId);

            var car = _carsRepository.TryGetById(rent.CarId);
            if (car != null)
            {
                car.IsAvailable = true;
                _carsRepository.Updata(new CarEdit
                {
                    Id = car.Id,
                    Name = car.Name,
                    Description = car.Description,
                    CostMinute = car.CostMinute,
                    Image = car.Image
                });
            }

            _rentsRepository.Update(rent);
            return rent;
        }

        public Rent GetRent(Guid rentId)
        {
            return _rentsRepository.TryGetById(rentId) ??
                   throw new Exception("Аренда не найдена");
        }

        public List<Rent> GetUserRents(Guid userId)
        {
            return _rentsRepository.GetByUserId(userId);
        }

        public List<Rent> GetActiveRents()
        {
            return _rentsRepository.GetAll()
                .Where(r => r.Status == RentStatus.Active)
                .ToList();
        }

        public Rent CancelRent(Guid rentId)
        {
            var rent = _rentsRepository.TryGetById(rentId);
            if (rent == null)
                throw new Exception("Аренда не найдена");

            if (rent.Status != RentStatus.Active)
                throw new Exception("Аренда не активна");

            rent.Status = RentStatus.Cancelled;
            rent.EndTime = DateTime.Now;
            rent.TotalCost = CalculateRentCost(rentId);

            var car = _carsRepository.TryGetById(rent.CarId);
            if (car != null)
            {
                car.IsAvailable = true;
                _carsRepository.Updata(new CarEdit
                {
                    Id = car.Id,
                    Name = car.Name,
                    Description = car.Description,
                    CostMinute = car.CostMinute,
                    Image = car.Image
                });
            }

            _rentsRepository.Update(rent);
            return rent;
        }

        public decimal CalculateRentCost(Guid rentId)
        {
            var rent = _rentsRepository.TryGetById(rentId);
            if (rent == null)
                throw new Exception("Аренда не найдена");

            var duration = (decimal)(rent.EndTime - rent.StartTime).TotalMinutes;
            return duration * rent.Car.CostMinute;
        }

        public bool IsCarAvailable(Guid carId)
        {
            var car = _carsRepository.TryGetById(carId);
            return car != null && car.IsAvailable;
        }

        public List<Car> GetAvailableCars()
        {
            return _carsRepository.GetAll()
                .Where(c => c.IsAvailable)
                .ToList();
        }
    }

    public class StartRentRequest
    {
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
    }
}
