using CarSharingOnlineASP.Models;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace CarSharingOnlineASP.Data
{
    public class CarsJSRepository : ICarsJSRepository
    {
        List<Car> cars;

        public CarsJSRepository()
        {
            var jsonString = File.ReadAllText("Data/cars.json");
            cars = JsonSerializer.Deserialize<List<Car>>(jsonString);
        }

        public void Add(Car car)
        {
            cars.Add(car);
            Save();
        }

        public List<Car> GetAll()
        {
            return cars;
        }

        public Car TryGetById(Guid id)
        {
            return cars.FirstOrDefault(x => x.Id == id);
        }

        public void Updata(CarEdit car)
        {
            var existingProduct = cars.FirstOrDefault
                                    (x => x.Id == car.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = car.Name;
            existingProduct.Description = car.Description;
            existingProduct.CostMinute = car.CostMinute;
            existingProduct.Image = car.Image;
            Save();
        }

        public void Save()
        {
            string updatedJsonString = JsonSerializer.Serialize(cars,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText("Data/cars.json", updatedJsonString,
                                       System.Text.Encoding.UTF8);
        }
    }
}
