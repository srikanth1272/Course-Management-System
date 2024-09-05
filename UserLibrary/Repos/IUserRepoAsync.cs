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
        Task UpdateUserAsync(string email, string username);
        Task DeleteUserAsync(string email);
        Task<User> GetUserAsync(string email);
        Task<List<User>> GetAllUsersAsync();

    }
}
