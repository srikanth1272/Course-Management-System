using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubjectLibrary.Models;
using SubjectLibrary.Repos;

namespace SubjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectRepoAsync repo;
        public SubjectController(ISubjectRepoAsync repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSubjects()
        {
            List<Subject> subjects = await repo.GetAllSubjects();
            return Ok(subjects);       
        }

        [HttpGet("{subjectId}")]
        public async Task<ActionResult> GetSubject(string subjectId)
        {
            try
            {
                Subject subject = await repo.GetSubject(subjectId);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);  
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Subject subject)
        {
            try
            {
                await repo.AddSubject(subject);
                return Created($"api/Subject/{subject.SubjectId}", subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{subjectId}")]
        public async Task<ActionResult> Update(string subjectId, Subject subject)
        {
            try
            {
                await repo.UpdateSubject(subjectId, subject);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{subjectId}")]
        public async Task<ActionResult> Delete(string subjectId)
        {
            try
            {
                await repo.DeleteSubject(subjectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
