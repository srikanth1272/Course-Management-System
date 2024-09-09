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
    public class SubjectController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5112/api/Subject/") };
  
        public async Task<ActionResult> Index()
        {
            List<Subject> subjects = await client.GetFromJsonAsync<List<Subject>>("");
            return View(subjects);
        }

        public async Task<ActionResult> Details(string subjectId)
        {
            Subject subject = await client.GetFromJsonAsync<Subject>("" +subjectId);
            return PartialView("Details", subject);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Subject subject)
        {
            var response = await client.PostAsJsonAsync<Subject>("", subject);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }

        }

        [Route("Subject/Edit/{subjectId}")]
        public async Task<ActionResult> Edit(string subjectId)
        {
            Subject subject = await client.GetFromJsonAsync<Subject>(""+subjectId);
            return View(subject);
        }

       
        [HttpPost]
        [Route("Subject/Edit/{subjectId}")]

        public async Task<ActionResult> Edit(string subjectId, Subject subject)
        {
            var response = await client.PutAsJsonAsync<Subject>(""+subjectId, subject);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }

        [Route("Subject/Delete/{subjectId}")]
        public async Task<ActionResult> Delete(string subjectId)
        {
            Subject subject = await client.GetFromJsonAsync<Subject>("" + subjectId);
            return View(subject);
        }

       
        [HttpPost]
        [Route("Subject/Delete/{subjectId}")]
        public async Task<ActionResult> Delete(string subjectId, Subject subject)
        {
            await client.DeleteAsync($"{subjectId}");
            return RedirectToAction(nameof(Index));
        }
    }
}
