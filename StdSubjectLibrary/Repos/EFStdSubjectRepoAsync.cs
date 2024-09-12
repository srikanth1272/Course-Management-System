using Microsoft.Data.SqlClient;
using StdSubjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StdSubjectLibrary.Repos
{
    public class EFStdSubjectRepoAsync : IStdSubjectRepoAsync
    {
        SqlConnection con;
        SqlCommand cmd;
        public EFStdSubjectRepoAsync()
        {
            con = new SqlConnection();
            con.ConnectionString = @"data source =(localdb)\MSSQLLocalDB;database = CMDB; integrated security = true";
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public async Task AddStudentSubject(StdSubject stdSubject)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "insert into StdSubject values(@RollNo,@SubjectId,@Semister)";
                cmd.Parameters.AddWithValue("@RollNo", stdSubject.RollNo);
                cmd.Parameters.AddWithValue("@SubjectId", stdSubject.SubjectId);
                cmd.Parameters.AddWithValue("@Semister", stdSubject.Semister);
               
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch
            {
                await con.CloseAsync();
                throw new StdSubjectException("Student Already Opted this Subject");
            }
        }

        public async Task DeleteStudentSubject(string RollNo, string SubjectId)
        {
            await con.OpenAsync();
            cmd.CommandText = "Delete from StdSubject where RollNo = @RollNo and SujectId = @SubjectId";
            cmd.Parameters.AddWithValue("@RollNo", RollNo);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        public async Task<List<StdSubject>> GetAllStudentsSubjects()
        {
            List<StdSubject> stdSubjects = new List<StdSubject>();
            cmd.CommandText = "Select * from StdSubject ";
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                StdSubject stdSubject = new()
                {
                    RollNo = (string)reader["RollNo"],
                    SubjectId = (string)reader["SubjectId"],
                    Semister = (int)reader["Semister"]
                };
                stdSubjects.Add(stdSubject);
            }
            await con.CloseAsync();
            return stdSubjects;
        }

        public async Task<List<StdSubject>> GetByRollNo(string RollNo)
        {
            List<StdSubject> stdSubjects = new List<StdSubject>();
            cmd.CommandText = "Select * from StdSubject where RollNo = '"+RollNo+"'";
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                StdSubject stdSubject = new()
                {
                    RollNo = (string)reader["RollNo"],
                    SubjectId = (string)reader["SubjectId"],
                    Semister = (int)reader["Semister"]
                };
                stdSubjects.Add(stdSubject);
            }
            await con.CloseAsync();
            return stdSubjects;
        }

        public async Task<List<StdSubject>> GetBySemister(string RollNo, int Semister)
        {
            List<StdSubject> stdSubjects = new List<StdSubject>();
            cmd.CommandText = "Select * from StdSubject where RollNo = @RollNo and Semister = @Semister";
            cmd.Parameters.AddWithValue("@RollNo", RollNo);
            cmd.Parameters.AddWithValue("@Semister", Semister);
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                StdSubject stdSubject = new()
                {
                    RollNo = (string)reader["RollNo"],
                    SubjectId = (string)reader["SubjectId"],
                    Semister = (int)reader["Semister"]
                };
                stdSubjects.Add(stdSubject);
            }
            await con.CloseAsync();
            return stdSubjects;
        }

        public async Task<StdSubject> GetStudentSubject(string RollNo, string SubjectId)
        {
           
            cmd.CommandText = "Select * from StdSubject where RollNo = @RollNo and SubjectId = @SubjectId";
            cmd.Parameters.AddWithValue("@RollNo", RollNo);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                StdSubject stdSubject = new()
                {
                    RollNo = (string)reader["RollNo"],
                    SubjectId = (string)reader["SubjectId"],
                    Semister = (int)reader["Semister"]
                };
                await con.CloseAsync();
                return stdSubject;
            }
            else
            {
                await con.CloseAsync();
                throw new StdSubjectException("This Subject is not opted by this Student");
            }
           
        }

        public async Task UpdateStudentSubject(string RollNo, string SubjectId, StdSubject stdSubject)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "update stdSubject set Semister=@Semister where RollNo = @RollNo and SubjectId = @SubjectId";
                cmd.Parameters.AddWithValue("@RollNo", RollNo);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                cmd.Parameters.AddWithValue("@Semister", stdSubject.Semister);
                
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                throw new StdSubjectException(ex.Message);
            }
        }
    }
}
