using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary.Models;

namespace UserLibrary.Repos
{
    public class EFUserRepoAsync : IUserRepoAsync
    {
        SqlConnection con;
        SqlCommand cmd;
        public EFUserRepoAsync()
        {
            con = new SqlConnection();
            con.ConnectionString = @"data source =(localdb)\MSSQLLocalDB;database = UserDB; integrated security = true";
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "insert into Users values(@email,@username,@password)";
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@password", user.Password);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch {
                await con.CloseAsync();
                throw new UserException("User Already Exists");               
            }          
        }

        public async Task DeleteUserAsync(string email)
        {
            await con.OpenAsync();
            cmd.CommandText = "Delete from Users where email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            cmd.CommandText = "Select * from Users ";     
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                User user = new User();
                user.Email = (string)reader["email"];
                user.UserName = (string)reader["username"];
                user.Password = (string)reader["PassWord"];
                users.Add(user);
            }
            await con.CloseAsync();
            return users;

        }

        public async Task<User> GetUserAsync(string email)
        {
            User user = new User();
            cmd.CommandText = "Select * from Users where email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user.Email = (string)reader["email"];   
                user.UserName = (string)reader["username"];
                user.Password = (string)reader["PassWord"];
                await con.CloseAsync(); 
                return user;
            }
            else
            {
                await con.CloseAsync();
                throw new UserException("User Doesn't Exists");
            }
        }

            public async Task UpdateUserAsync(string email, string username)
        {
            try
            {
                await con.OpenAsync();
                cmd.CommandText = "update Users set Username = @username where email = @email";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
            catch (UserException ex)
            {
                await con.CloseAsync();
                throw new UserException(ex.Message);
            }

        }
    }
}
