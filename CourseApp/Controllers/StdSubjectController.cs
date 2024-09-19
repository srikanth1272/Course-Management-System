using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseApp.Models;
namespace CourseApp.Controllers
{
    public class StdSubjectController : Controller
    {

        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5027/api/StdSubject/") };

        public async Task<ActionResult> Index()
        {
            List<StdSubject> stdSubjects = await client.GetFromJsonAsync<List<StdSubject>>("");
            return View(stdSubjects);
        }
        public async Task<ActionResult> Student(string rollNo)
        {
            Student student = await client.GetFromJsonAsync<Student>("student/" + rollNo);
            return PartialView(student);
        }
        public async Task<ActionResult> Subject(string subjectId)
        {
            Subject subject = await client.GetFromJsonAsync<Subject>("subject/" + subjectId);
            return PartialView(subject);
        }


        public async Task<ActionResult> BySemister(string RollNo, int Semister)
        {
            List<StdSubject> stdSubjects = await client.GetFromJsonAsync<List<StdSubject>>($"BySemister/{RollNo}/{Semister}");
            return PartialView(stdSubjects);
        }
        public async Task<ActionResult> ByRollNo(string RollNo)
        {
            List<StdSubject> stdsubjects = await client.GetFromJsonAsync<List<StdSubject>>($"ByRollNo/{RollNo}");
            return View(stdsubjects);
        }

        public async Task<ActionResult> Details(string rollNo,string subjectId)
        {
            StdSubject stdSubject = await client.GetFromJsonAsync<StdSubject>($"{rollNo}/{subjectId}");
            return PartialView("Details", stdSubject);
        }

        public async Task<ActionResult> Create()
        {
            var students = await Helper.GetStudents();
            var subjects = await Helper.GetSubjects();
            var semister = Helper.GetSemisters();
            ViewBag.Students = students;
            ViewBag.Subjects = subjects;
            ViewBag.Semister = semister;    

            
            return View();
        }



        [Route("StdSubject/Edit/{rollNo}/{subjectId}")]
        public async Task<ActionResult> Edit(string rollNo, string subjectId)
        {
            StdSubject stdSubject = await client.GetFromJsonAsync<StdSubject>($"{rollNo}/{subjectId}");
            var semister = Helper.GetSemisters();
            ViewBag.Semister = semister;
            return PartialView(stdSubject);
        }



        [Route("StdSubject/Delete/{rollNo}/{subjectId}")]
        public async Task<ActionResult> Delete(string rollNo, string subjectId)
        {
            StdSubject stdSubject = await client.GetFromJsonAsync<StdSubject>($"{rollNo}/{subjectId}");
            return PartialView(stdSubject);
        }


        [HttpPost]
        [Route("StdSubject/Delete/{rollNo}/{subjectId}")]
        public async Task<ActionResult> Delete(string rollNo, string subjectId, StdSubject stdSubject)
        {
            var response = await client.DeleteAsync($"{rollNo}/{subjectId}");
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
