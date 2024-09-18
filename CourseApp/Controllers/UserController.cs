using CourseApp.Models;
using System;
using System.Collections.Generic;
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

        public async Task<ActionResult> Index()
        {
            List<User> users = await client.GetFromJsonAsync<List<User>>("");
            return View(users);
        }
      
        [HttpPost]
        public void Authenticate(int userId, String Role)
        {
            Session["UserId"] = userId;
            Session["Role"] = Role;
        }
        public async Task<ActionResult> Details(int userId)
        {
            User user = await client.GetFromJsonAsync<User>(""+userId);
            return PartialView(user);
        }

        [Route("User/Edit/{UserId}")]
        public async Task<ActionResult> Edit(int userId)
        {
             User user = await client.GetFromJsonAsync<User>("" + userId);
             return PartialView(user);
        }


        public ActionResult Logout()
        {
            Session["Email"] = null;
            return RedirectToAction("Login", "User");
        }

    }
}