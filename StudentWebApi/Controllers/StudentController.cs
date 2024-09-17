using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLibrary.Repos;
using StudentLibrary.Models;

namespace StudentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentRepoAsync repo;
        public StudentController(IStudentRepoAsync repository) {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllStudents()
        {
            List<Student> students = await repo.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{sdate}/{edate}")]
        public async Task<ActionResult> GetStudentByDates(DateTime sdate , DateTime edate)
        {
            DateOnly sdate1 = DateOnly.FromDateTime(sdate);
            DateOnly edate1 = DateOnly.FromDateTime(edate);

            List<Student> students = await repo.GetStudentByDates(sdate1,edate1);
            return Ok(students);
        }
        [HttpGet("{rollNo}")]
        public async Task<ActionResult> GetStudent(string rollNo)
        {
            try
            {
                Student student = await repo.GetStudent(rollNo);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Student student)
        {
            try
            {
                await repo.AddStudent(student);
                return Created($"api/Student/{student.RollNo}", student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{rollNo}")]
        public async Task<ActionResult> Update(string rollNo, Student student)
        {
            try
            {
                await repo.UpdateStudent(rollNo, student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{rollNo}")]
        public async Task<ActionResult> Delete(string rollNo)
        {
            try
            {
                await repo.DeleteStudent(rollNo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

}
