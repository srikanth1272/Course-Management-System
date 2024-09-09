using Microsoft.Data.SqlClient;
using StudentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Repos
{
    public class EFStudentRepoAsync : IStudentRepoAsync
    {
        SqlConnection con;
        SqlCommand cmd;
        public EFStudentRepoAsync()
        {
            con = new SqlConnection();
            con.ConnectionString = @"data source =(localdb)\MSSQLLocalDB;database = UserDB; integrated security = true";
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public Task AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudent(string RollNo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudent(string RollNo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudent(string RollNo, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
