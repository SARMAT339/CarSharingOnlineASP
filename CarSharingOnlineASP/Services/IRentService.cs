using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Services
{
    public interface IRentService
    {
        Rent EndRent(Guid rentId, DateTime endTime);
        Rent GetRent(Guid rentId);
        List<Rent> GetUserRents(Guid userId);
        Rent StartRent(Guid userId, Guid carId, DateTime startTime);
    }
}