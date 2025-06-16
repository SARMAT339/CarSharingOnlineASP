using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Services;
using CarSharingOnlineASP.Models;
using System.Security.Claims;

namespace CarSharingOnlineASP.Controllers
{
    public class RentMvcController : Controller
    {
        private readonly IRentService _rentService;

        public RentMvcController(IRentService rentService)
        {
            _rentService = rentService;
        }

        public IActionResult Index()
        {
            var availableCars = _rentService.GetAvailableCars();
            return View(availableCars);
        }

        public IActionResult MyRents()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var rents = _rentService.GetUserRents(userId.Value);
            return View(rents);
        }

        public IActionResult StartRent(Guid carId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var car = _rentService.GetAvailableCars().FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                TempData["Error"] = "Автомобиль не найден или недоступен для аренды.";
                return RedirectToAction("Index");
            }

            return View(car);
        }

        [HttpPost]
        public IActionResult StartRent(Guid carId, string startLocation)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var rent = _rentService.StartRent(userId.Value, carId, DateTime.Now, startLocation);
                TempData["Success"] = "Аренда успешно начата!";
                return RedirectToAction(nameof(MyRents));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var car = _rentService.GetAvailableCars().FirstOrDefault(c => c.Id == carId);
                return View(car);
            }
        }

        [HttpPost]
        public IActionResult EndRent(Guid rentId, string endLocation)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var rent = _rentService.EndRent(rentId, DateTime.Now, endLocation);
                TempData["Success"] = $"Аренда завершена. Итоговая стоимость: {rent.TotalCost:F2} ₽";
                return RedirectToAction(nameof(MyRents));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(MyRents));
            }
        }

        [HttpPost]
        public IActionResult CancelRent(Guid rentId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var rent = _rentService.CancelRent(rentId);
                TempData["Success"] = "Аренда отменена.";
                return RedirectToAction(nameof(MyRents));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(MyRents));
            }
        }

        private Guid? GetCurrentUserId()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return Guid.Parse(userId);
        }
    }
} 