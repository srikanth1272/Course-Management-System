using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentLibrary.Models;

namespace StudentLibrary.Repos
{
    public interface IStudentRepoAsync
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudent(string RollNo);
        Task AddStudent(Student student);
        Task UpdateStudent(string RollNo, Student student);
        Task DeleteStudent(string RollNo);
        Task<List<Student>> GetStudentByDates(DateOnly sdate, DateOnly edate);
    }
}
