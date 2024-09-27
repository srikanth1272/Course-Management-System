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
        HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5037/api/Student/") };
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
        public async Task Authenticate(int userId, String role)
        {
            Session["UserId"] = userId;
            Session["Role"] = role;
            User user = await client.GetFromJsonAsync<User>("" + userId);
            Student student = await client2.GetFromJsonAsync<Student>("profile/" + user.Email);
            Session["RollNo"] = student.RollNo;

        }
        public async Task<ActionResult> Details(int userId)
        {
            User user = await client.GetFromJsonAsync<User>(""+userId);
            return PartialView(user);
        }
        public async Task<ActionResult> Profile(int userId)
        {
            User user = await client.GetFromJsonAsync<User>("" + userId);
            Student student = await client2.GetFromJsonAsync<Student>("profile/" + user.Email);
            Session["RollNo"] = student.RollNo;
            return PartialView(student);
            
        }

        [Route("User/Edit/{UserId}")]
        public async Task<ActionResult> Edit(int userId)
        {
             User user = await client.GetFromJsonAsync<User>("" + userId);
             return PartialView(user);
        }

        [Route("User/Delete/{UserId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            User user = await client.GetFromJsonAsync<User>("" + userId);
            return PartialView(user);
        }

        [HttpPost]
        [Route("User/Delete/{UserId}")]
        public async Task<ActionResult> Delete(int userId,User user1)
        {
            User user = await client.GetFromJsonAsync<User>("" + userId);
            if(user.Role == "Admin")
            {
                return Json(new { success = false, message = "Cannot Delete Admin" });
            }
            else
            {
                var response = await client.DeleteAsync($"{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Deleted successfully" });
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = errorMessage });
                }
            }
        }


        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login", "User");
        }

    }
}