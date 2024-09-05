using CourseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CourseApp.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5299/api/User/") };
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User user)
        {
                var response = await client.PostAsJsonAsync("", user);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User user)
        {
            User user1 = await client.GetFromJsonAsync<User>($"{user.Email}");
            if (user1.Password == user.Password)
                return RedirectToAction(nameof(Details),new { email = user.Email });
            else
                throw new Exception("Incorrect password");        
        }

        public async Task<ActionResult> Details(string Email)
        {
            User user = await client.GetFromJsonAsync<User>(""+Email);
            return View(user);
        }
    }
}