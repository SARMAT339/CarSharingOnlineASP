using CarSharing.DB.Models;

namespace CarSharing.DB
{
    public interface ICarsDBRepository
    {
        void Add(CarDB car);
        List<CarDB> GetAll();
        CarDB TryGetById(Guid id);
        void Updata(CarDB car);
    }
}