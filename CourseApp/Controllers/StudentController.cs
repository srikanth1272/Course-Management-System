using CourseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseApp.Controllers
{
    public class StudentController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5037/api/Student/") };

        public async Task<ActionResult> Index()
        {
            List<Student> students = await client.GetFromJsonAsync<List<Student>>("");
            return View(students);
        }

        public async Task<ActionResult> Details(string rollNo)
        {
            Student student = await client.GetFromJsonAsync<Student>("" + rollNo);
            return PartialView(student);
        }

        public ActionResult Create()
        {
            return View();
        }

      

        [Route("Student/Edit/{rollNo}")]
        public async Task<ActionResult> Edit(string rollNo)
        {
            Student student = await client.GetFromJsonAsync<Student>("" + rollNo);
            return PartialView(student);
        }



        [Route("Student/Delete/{rollNo}")]
        public async Task<ActionResult> Delete(string rollNo)
        {
            Student student = await client.GetFromJsonAsync<Student>("" + rollNo);
            return PartialView(student);
        }


        [HttpPost]
        [Route("Student/Delete/{rollNo}")]
        public async Task<ActionResult> Delete(string rollNo, Student student)
        {
            var response = await client.DeleteAsync($"{rollNo}");
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
}