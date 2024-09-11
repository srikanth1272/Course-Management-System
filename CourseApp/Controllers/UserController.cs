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

        public ActionResult Login()
        {
            return View();
        }
      
        [HttpPost]
        public void Authenticate(String Email)
        {
            Session["Email"] = Email;
        }
        public async Task<ActionResult> Details(string Email)
        {
            User user = await client.GetFromJsonAsync<User>(""+Email);
            return View(user);
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

        public ActionResult Logout()
        {
            Session["Email"] = null;
            return RedirectToAction("Login", "User");
        }

    }
}