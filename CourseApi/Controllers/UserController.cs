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

        [HttpGet("{Email}")]
        public async Task<ActionResult> GetUser(string Email)
        {
            try
            {
                User user = await repo.GetUserAsync(Email);
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
                await repo.AddUserAsync(user);
                return Created($"api/User/{user.Email}", user);
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Email}")]
        public async Task<ActionResult> Update(string Email, User user)
        {
            try
            {
                await repo.UpdateUserAsync(Email, user.UserName);
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
                User user1 = await repo.GetUserAsync(user.Email);
                if (user1.Password == user.Password)
                {
                    return Ok();
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
    }
}
