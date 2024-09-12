using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseApp.Models
{
    public class Helper
    {
        static HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5112/api/Subject/") };
        static HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5037/api/Student/") };

        public static async Task<List<SelectListItem>> GetStudents()
        {
            List<SelectListItem> students = new List<SelectListItem>();
           
            List<Student> students1 = await client1.GetFromJsonAsync<List<Student>>("");
            foreach (Student student in students1)
            {
                students.Add(new SelectListItem { Text = student.RollNo, Value = student.RollNo });
            }
            return students;
        }
        public static async Task<List<SelectListItem>> GetSubjects()
        {
            List<SelectListItem> subjects = new List<SelectListItem>();

            List<Subject> subjects1 = await client2.GetFromJsonAsync<List<Subject>>("");
            foreach (Subject subject in subjects1)
            {
                subjects.Add(new SelectListItem { Text = $"{subject.SubjectId}:{subject.Title}", Value = subject.SubjectId });
            }
            return subjects;
        }

        public static List<SelectListItem> GetSemisters()
        {
            List<SelectListItem> semisters = new List<SelectListItem>();
            for(int i = 1; i < 9; i++) 
                semisters.Add(new SelectListItem { Text = $"{i}", Value = $"{i}" } );
            return semisters;
        }
    }
}