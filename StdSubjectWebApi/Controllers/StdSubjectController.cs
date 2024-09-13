using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StdSubjectLibrary.Models;
using StdSubjectLibrary.Repos;
using SubjectLibrary.Repos;
using StudentLibrary.Repos;
using StudentLibrary.Models;
using SubjectLibrary.Models;


namespace StdSubjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StdSubjectController : ControllerBase
    {
        IStdSubjectRepoAsync repo;
        IStudentRepoAsync std;
        ISubjectRepoAsync sub;
        public StdSubjectController(IStdSubjectRepoAsync repository, IStudentRepoAsync std1, ISubjectRepoAsync sub1)
        {
            repo = repository;
            std = std1;
            sub = sub1;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllStudentsSubjects()
        {
            List<StdSubject> stdSubjects = await repo.GetAllStudentsSubjects();
            return Ok(stdSubjects);
        }
        [HttpGet("student/{rollNo}")]
        public async Task<ActionResult> GetStudent(string rollNo)
        {
            try
            {
                Student student = await std.GetStudent(rollNo);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("subject/{subjectId}")]
        public async Task<ActionResult> GetSubject(string subjectId)
        {
            try
            {
                Subject subject = await sub.GetSubject(subjectId);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{rollNo}/{subjectId}")]
        public async Task<ActionResult> GetStudentSubject(string rollNo, string subjectId)
        {
            try
            {
                StdSubject stdSubject = await repo.GetStudentSubject(rollNo,subjectId);
                return Ok(stdSubject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("BySemister/{rollNo}/{semister}")]
        public async Task<ActionResult> GetBySemister(string rollNo, int semister)
        {
            try
            {
                List<StdSubject> stdSubjects = await repo.GetBySemister(rollNo, semister);
                return Ok(stdSubjects);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByRollNo/{rollNo}")]
        public async Task<ActionResult> GetByRollNo(string rollNo)
        {
            try
            {
                List<StdSubject> stdSubjects = await repo.GetByRollNo(rollNo);
                return Ok(stdSubjects);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(StdSubject stdSubject)
        {
            try
            {
                await repo.AddStudentSubject(stdSubject);
                return Created($"api/StdSubject/{stdSubject.RollNo}/{stdSubject.SubjectId}", stdSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{rollNo}/{subjectId}")]
        public async Task<ActionResult> Update(string rollNo,string subjectId, StdSubject stdSubject)
        {
            try
            {
                await repo.UpdateStudentSubject(rollNo,subjectId, stdSubject);
                return Ok(stdSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{rollNo}/{subjectId}")]
        public async Task<ActionResult> Delete(string rollNo,string subjectId)
        {
            try
            {
                await repo.DeleteStudentSubject(rollNo,subjectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
