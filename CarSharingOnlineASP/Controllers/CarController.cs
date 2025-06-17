using Microsoft.AspNetCore.Mvc;
using System;
using CarSharingOnlineASP.Data;
using CarSharingOnlineASP.Models;
using CarSharing.DB.Models;
using CarSharing.DB;
using CarSharingOnlineASP.Helper;

namespace CarSharingOnlineASP.Controllers
{
    public class CarController : Controller
    {
        //readonly ICarsJSRepository carsJSRepository;
        readonly ICarsDBRepository carsDBRepository;

        public CarController(ICarsDBRepository carsDBRepository)
        {
            this.carsDBRepository = carsDBRepository;
        }

        public IActionResult Index(Guid id)
        {
            CarDB car = carsDBRepository.TryGetById(id);
            return View(Mapping.ToCar(car));
        }

        public IActionResult Catalog(Guid id) 
        { 
            List<CarDB> cars = carsDBRepository.GetAll();
            return View(Mapping.ToCarList(cars));
        }

        /*public IActionResult Index()
        {
            Car car = new Car("Mercedes GLC 250", "Класс люкс. Коробка автомат", 20, "Data/Images/MercedesGLC250.jpg");
            return View(car);
        }*/
    }
}
