using CarSharing.DB.Models;
using System.Text.Encodings.Web;

namespace CarSharing.DB
{
    public class CarsDBRepository : ICarsDBRepository
    {
        private readonly DatabaseContext dbContext;

        public CarsDBRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(CarDB car)
        {
            dbContext.CarsDB.Add(car);
            dbContext.SaveChangesAsync();
        }

        public List<CarDB> GetAll()
        {
            return dbContext.CarsDB.ToList();
        }

        public CarDB TryGetById(Guid id)
        {
            return dbContext.CarsDB.FirstOrDefault(x => x.Id == id);
        }

        public void Updata(CarDB car)
        {
            var existingProduct = dbContext.CarsDB.FirstOrDefault
                                    (x => x.Id == car.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = car.Name;
            existingProduct.Description = car.Description;
            existingProduct.CostMinute = car.CostMinute;
            existingProduct.Image = car.Image;
            dbContext.SaveChangesAsync();
        }

    }
}
