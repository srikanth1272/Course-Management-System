using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLibrary.Repos;
using UserLibrary.Models;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepoAsync repo;
        public UserController(IUserRepoAsync repository)
        {
            repo = repository;
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<User> users = await repo.GetAllUsersAsync();
            return Ok(users);
        }



        [HttpGet("{UserId}")]
        public async Task<ActionResult> GetUser(int UserId)
        {
            try
            {
                User user = await repo.GetUserAsync(UserId);
                return Ok(user);
            }
            catch (UserException ex)
            {
                return NotFound(ex.Message);
            }
        
        }
 

        [HttpPost]
        public async Task<ActionResult> Insert(User user)
        {
            try
            {
                var pass = user.Password;
                var hash1 = System.Text.Encoding.UTF8.GetBytes(pass);
                user.Password = System.Convert.ToBase64String(hash1);
                await repo.AddUserAsync(user);
                return Ok(user);
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> Update(int userId, User user)
        {
            try
            {
                await repo.UpdateUserAsync(userId, user);
                return Ok();
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(User user)
        {
            var pass = user.Password;
            var hash1 = System.Text.Encoding.UTF8.GetBytes(pass);
            user.Password = System.Convert.ToBase64String(hash1);
            try
            {
                User user1 = await repo.LoginAsync(user.Email);
                if (user1.Password == user.Password)
                {
                    return Ok(user1);
                }
                else
                {
                  return BadRequest("Incorrect Password");
                }
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            try
            {
                await repo.DeleteUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("checkEmail/{email}")]
        public async Task<ActionResult> CheckEmail(string email)
        {
            User user = await repo.LoginAsync(email);
            if (user == null)
                return BadRequest(user);
            else
                return Ok(user);
        }
    }
}
