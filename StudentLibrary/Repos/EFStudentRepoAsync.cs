using Microsoft.Data.SqlClient;
using StudentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
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
            con.ConnectionString = @"data source =(localdb)\MSSQLLocalDB;database = CMDB; integrated security = true";
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public async Task AddStudent(Student student)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "insert into Student values(@RollNo,@FirstName,@LastName,@DOB,@Email,@Phone,@Address)";
                cmd.Parameters.AddWithValue("@RollNo", student.RollNo);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DOB", DateOnly.FromDateTime(student.DOB));
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Phone", student.Phone);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch
            {
                await con.CloseAsync();
                throw new StudentException("Student Already Exists");
            }
        }

        public async Task DeleteStudent(string RollNo)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "Delete from Student where RollNo = @RollNo";
                cmd.Parameters.AddWithValue("@RollNo", RollNo);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch (Exception)
            {
                await con.CloseAsync();
                throw new StudentException("Cannot Delete Student.Delete the opted subjects first and try again ");
            }
            
        }

        public async Task<List<Student>> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            cmd.CommandText = "Select * from Student ";
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Student student = new()
                {
                    RollNo = (string)reader["RollNo"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    DOB = (DateTime)reader["DOB"],
                    Email = (string)reader["email"],
                    Phone = (string)reader["phone"],
                    Address = (string)reader["address"]
                    
                };
                students.Add(student);
            }
            await con.CloseAsync();
            return students;
        }

        public async Task<Student> GetStudent(string RollNo)
        {
            cmd.CommandText = "Select * from Student where RollNo = '" + RollNo + "'";
            Student student = new();
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                student.RollNo = (string)reader["RollNo"];
                student.FirstName = (string)reader["FirstName"];
                student.LastName = (string)reader["LastName"];
                student.DOB = (DateTime)reader["DOB"];
                student.Email = (string)reader["email"];
                student.Phone = (string)reader["phone"];
                student.Address = (string)reader["address"];
            } 
            await con.CloseAsync();
            return student;
        }

        public async Task UpdateStudent(string RollNo, Student student)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "update student set FirstName=@FirstName,LastName=@LastName," +
                    "DOB=@DOB,Email=@Email,Phone=@Phone,Address=@Address" +
                    " where RollNo = @RollNo";
                cmd.Parameters.AddWithValue("@RollNo", RollNo);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DOB", DateOnly.FromDateTime(student.DOB));
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Phone", student.Phone);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                throw new StudentException(ex.Message);
            }
        }
    }
}
