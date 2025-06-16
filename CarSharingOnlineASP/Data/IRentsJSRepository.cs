using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Data
{
    public interface IRentsJSRepository
    {
        List<Rent> GetAll();
        Rent TryGetById(Guid id);
        List<Rent> GetByUserId(Guid userId);
        void Add(Rent rent);
        void Update(Rent rent);
        void Save();
    }
} 