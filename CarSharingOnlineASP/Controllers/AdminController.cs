using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Data;
using CarSharingOnlineASP.Models;
using CarSharing.DB;
using CarSharing.DB.Models;
using CarSharingOnlineASP.Helper;

namespace CarSharingOnlineASP.Controllers
{
    public class AdminController : Controller
    {
        //readonly  ICarsJSRepository carsJSRepository;
        readonly ICarsDBRepository carsDBRepository;

        public AdminController(ICarsDBRepository carsDBRepository)
        {
            this.carsDBRepository = carsDBRepository;
        }

        [HttpGet]
        public IActionResult Cars(Guid id)
        {
            return View(Mapping.ToCarList(carsDBRepository.GetAll()));
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
                carsDBRepository.Add(Mapping.ToCarDB(car));
                return RedirectToAction("Cars", "Admin");
            }
            else
            {
                return View(car);
            }
        }

        [HttpGet]
        public IActionResult EditCar(Guid id)
        {
            CarDB car = carsDBRepository.TryGetById(id);
            return View(Mapping.ToCar(car));
        }

        [HttpPost]
        public IActionResult EditCar(Car car)
        {
            carsDBRepository.Updata(Mapping.ToCarDB(car));
            return RedirectToAction("Index", "Car", new { id = car.Id });
        }
    }
}
