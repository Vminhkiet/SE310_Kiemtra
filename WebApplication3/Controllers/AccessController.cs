using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UseName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    if (user.LoaiUser == 1)
                    {
                        return Redirect("https://localhost:7068/Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Registration(TUser newUser)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var existingUser = db.TUsers.FirstOrDefault(x => x.Username == newUser.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(newUser);
                }

                db.TUsers.Add(newUser);
                db.SaveChanges();

                HttpContext.Session.SetString("UserName", newUser.Username);
                if (newUser.LoaiUser == 1)
                {
                    return Redirect("https://localhost:7068/Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
