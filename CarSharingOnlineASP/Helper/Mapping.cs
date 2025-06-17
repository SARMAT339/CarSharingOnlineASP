using CarSharing.DB.Models;
using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Helper
{
    public class Mapping
    {
        public static Car ToCar(CarDB car)
        {
            return new Car
            {
                Name = car.Name,
                Description = car.Description,
                CostMinute = car.CostMinute,
                Image = car.Image,
                Id = car.Id
            };
        }

        public static CarDB ToCarDB(Car car)
        {
            return new CarDB
            {
                Name = car.Name,
                Description = car.Description,
                CostMinute = car.CostMinute,
                Image = car.Image,
                Id = car.Id
            };
        }

        public static List<Car> ToCarList(List<CarDB> carsDB)
        {
            List<Car> cars = new List<Car>();
            foreach (var car in carsDB)
                cars.Add(ToCar(car));
            return cars;
        }

        public static List<CarDB> ToCarDBList(List<Car> cars)
        {
            List<CarDB> carsDB = new List<CarDB>();
            foreach (var car in cars)
                carsDB.Add(ToCarDB(car));
            return carsDB;
        }
    }
}
