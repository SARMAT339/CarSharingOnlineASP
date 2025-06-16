using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Data;
using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Controllers
{
    public class AdminController : Controller
    {
        readonly  ICarsJSRepository carsJSRepository;

        public AdminController(ICarsJSRepository carsJSRepository)
        {
            this.carsJSRepository = carsJSRepository;
        }

        [HttpGet]
        public IActionResult Index(Guid id)
        {
            return View(carsJSRepository.GetAll());
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            if (car.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "Слишком короткое имя");
            }

            if (ModelState.IsValid)
            {
                carsJSRepository.Add(car);
                return RedirectToAction("Products", "Admin");
            }
            else
            {
                return View(car);
            }
        }

        [HttpGet]
        public IActionResult EditCar(Guid id)
        {
            var car = carsJSRepository.TryGetById(id);
            return View(car);
        }

        [HttpPost]
        public IActionResult EditCar(CarEdit car)
        {
            carsJSRepository.Updata(car);
            return RedirectToAction("Index", "Car", car.Id);
        }
    }
}
