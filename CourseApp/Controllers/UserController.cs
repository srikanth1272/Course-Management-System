using CourseApp.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;


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
            var pass = user.Password;
            var hash1 = System.Text.Encoding.UTF8.GetBytes(pass);
            user.Password = System.Convert.ToBase64String(hash1);
            var response = await client.PostAsJsonAsync("", user);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorMessage);
            }
            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User user)
        {
            var pass = user.Password;
            var hash1 = System.Text.Encoding.UTF8.GetBytes(pass);
            user.Password = System.Convert.ToBase64String(hash1);

            try
            {
                User user1 = await client.GetFromJsonAsync<User>($"{user.Email}");
                if (user1.Password == user.Password)
                {
                    Session["Email"] = user1.Email;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Password");
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while logging in.";
                ModelState.AddModelError("", "This Email Doesn't Exists");
            }

            return View(user);
        }
        public async Task<ActionResult> Details(string Email)
        {
            User user = await client.GetFromJsonAsync<User>(""+Email);
            return View(user);
        }
        public  ActionResult Logout()
        {
            Session["Email"] = null ;
            return RedirectToAction("Login", "User");
        }


        [Route("User/Edit/{Email}")]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Edit/{Email}")]
        public async Task<ActionResult> Edit(string Email, User user)
        {
            user.Password = " ";
            var response = await client.PutAsJsonAsync($"{Email}", user);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Details), new { Email = Email });
            else
                return View();
        }

    }
}