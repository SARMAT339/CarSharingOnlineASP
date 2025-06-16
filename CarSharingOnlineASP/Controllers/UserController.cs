using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Data;
using CarSharingOnlineASP.Models;

namespace CarSharingOnlineASP.Controllers
{
    public class UserController : Controller
    {
        readonly IUsersJSRepository usersJSRepository;
        private const string SessionKeyUserId = "UserId";

        public UserController(IUsersJSRepository usersJSRepository)
        {
            this.usersJSRepository = usersJSRepository;
        }

        public IActionResult Home()
        {
            var userId = HttpContext.Session.GetString(SessionKeyUserId);
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = usersJSRepository.TryGetById(Guid.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString(SessionKeyUserId);
            if (userId != null)
            {
                var user = usersJSRepository.TryGetById(Guid.Parse(userId));
                return View("Home", user); // Перенаправляем на домашнюю страницу
            }
            return View("Guest"); // Гостевая страница для неавторизованных
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (usersJSRepository.UserExists(user.Email))
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
                    return View(user);
                }

                usersJSRepository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKeyUserId);
            return RedirectToAction("Index");
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (usersJSRepository.ValidateUser(email, password))
            {
                var user = usersJSRepository.TryGetByEmail(email);
                HttpContext.Session.SetString(SessionKeyUserId, user.Id.ToString());
                return RedirectToAction("Home");
            }

            ModelState.AddModelError("", "Неверный email или пароль");
            return View();
        }

        // GET: Users/Edit/5
        public IActionResult Edit(Guid id)
        {
            var user = usersJSRepository.TryGetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userEdit = new UserEdit
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email
            };

            return View(userEdit);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, UserEdit userEdit)
        {
            if (id != userEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                usersJSRepository.Updata(userEdit);
                return RedirectToAction(nameof(Index));
            }
            return View(userEdit);
        }
    }
}
