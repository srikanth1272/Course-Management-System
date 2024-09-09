using Microsoft.Data.SqlClient;
using SubjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectLibrary.Repos
{
    public class EFSubjectRepoAsync : ISubjectRepoAsync
    {
        SqlConnection con;
        SqlCommand cmd;
        public EFSubjectRepoAsync()
        {
            con = new SqlConnection
            {
                ConnectionString = @"data source =(localdb)\MSSQLLocalDB;database = SubjectDB; integrated security = true"
            };
            cmd = new SqlCommand
            {
                Connection = con
            };
        }
        public async Task AddSubject(Subject subject)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "insert into Subject values(@subjectId,@title,@totalClasses,@credits)";
                cmd.Parameters.AddWithValue("@subjectId", subject.SubjectId);
                cmd.Parameters.AddWithValue("@title", subject.Title);
                cmd.Parameters.AddWithValue("@totalClasses", subject.TotalClasses);
                cmd.Parameters.AddWithValue("@credits", subject.Credits);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch
            {
                await con.CloseAsync();
                throw new SubjectException("Subject Already Exists");
            }
        }

        public async Task DeleteSubject(string subjectId)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "Delete from Subject where subjectId = @subjectId";
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch
            {
                await con.CloseAsync();
                throw new SubjectException("Cannot Delete Subject. It is opted by Subjects");
            }
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            List<Subject> users = new List<Subject>();
            cmd.CommandText = "Select * from Subject ";
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Subject user = new()
                {
                    SubjectId = (string)reader["subjectId"],
                    Title = (string)reader["title"],
                    TotalClasses = (int)reader["totalClasses"],
                    Credits = (int)reader["credits"]
                };
                users.Add(user);
            }
            await con.CloseAsync();
            return users;
        }

        public async Task<Subject> GetSubject(string subjectId)
        {
            cmd.CommandText = "Select * from Subject where SubjectId = '"+subjectId+"'";
            Subject subject = new ();
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                subject.SubjectId = (string)reader["subjectId"];
                subject.Title = (string)reader["title"];
                subject.TotalClasses = (int)reader["totalClasses"];
                subject.Credits = (int)reader["credits"];               
            }
            await con.CloseAsync();
            return subject;
        }

        public async Task UpdateSubject(string subjectId, Subject subject)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "update subject set title = @title,totalClasses = @totalClasses,Credits = @credits" +
                    " where SubjectId = @subjectId";
                cmd.Parameters.AddWithValue("@subjectId", subject.SubjectId);
                cmd.Parameters.AddWithValue("@title", subject.Title);
                cmd.Parameters.AddWithValue("@totalClasses", subject.TotalClasses);
                cmd.Parameters.AddWithValue("@credits", subject.Credits);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch(Exception ex) 
            {
                await con.CloseAsync();
                throw new SubjectException(ex.Message);
            }
        }
    }
}
