using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StdSubjectLibrary.Models;
using StdSubjectLibrary.Repos;


namespace StdSubjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StdSubjectController : ControllerBase
    {
        IStdSubjectRepoAsync repo;
        public StdSubjectController(IStdSubjectRepoAsync repository)
        {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllStudentsSubjects()
        {
            List<StdSubject> stdSubjects = await repo.GetAllStudentsSubjects();
            return Ok(stdSubjects);
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
