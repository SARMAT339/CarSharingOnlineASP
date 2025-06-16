using Microsoft.AspNetCore.Mvc;
using System;
using CarSharingOnlineASP.Data;
using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Controllers
{
    public class CarController : Controller
    {
        readonly ICarsJSRepository carsJSRepository;

        public CarController(ICarsJSRepository carsJSRepository)
        {
            this.carsJSRepository = carsJSRepository;
        }

        public IActionResult Index(Guid id)
        {
            Car car = carsJSRepository.TryGetById(id);
            return View(car);
        }

        public IActionResult Catalog(Guid id) 
        { 
            List<Car> cars = carsJSRepository.GetAll();
            return View(cars);
        }

        /*public IActionResult Index()
        {
            Car car = new Car("Mercedes GLC 250", "Класс люкс. Коробка автомат", 20, "Data/Images/MercedesGLC250.jpg");
            return View(car);
        }*/
    }
}
