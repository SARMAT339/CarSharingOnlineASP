using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Services
{
    public interface IRentService
    {
        Rent StartRent(Guid userId, Guid carId, DateTime startTime, string startLocation);
        Rent EndRent(Guid rentId, DateTime endTime, string endLocation);
        Rent GetRent(Guid rentId);
        List<Rent> GetUserRents(Guid userId);
        List<Rent> GetActiveRents();
        Rent CancelRent(Guid rentId);
        decimal CalculateRentCost(Guid rentId);
        bool IsCarAvailable(Guid carId);
        List<Car> GetAvailableCars();
    }
}