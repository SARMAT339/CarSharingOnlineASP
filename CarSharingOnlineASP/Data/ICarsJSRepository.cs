using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Data
{
    public interface ICarsJSRepository
    {
        void Add(Car car);
        List<Car> GetAll();
        Car TryGetById(Guid id);
        void Updata(CarEdit car);
        void Save();
    }
}