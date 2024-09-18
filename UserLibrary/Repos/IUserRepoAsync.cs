using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary.Models;

namespace UserLibrary.Repos
{
    public interface IUserRepoAsync
    {
        Task AddUserAsync(User user);
        
        Task DeleteUserAsync(int userId);
        Task<User> GetUserAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(int userId, string username);
        Task<User> LoginAsync(string email);
    }
}
