using SubjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectLibrary.Repos
{
    public interface ISubjectRepoAsync
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetSubject(string subjectId);
        Task AddSubject(Subject subject);
        Task UpdateSubject(string subjectId, Subject subject);
        Task DeleteSubject(string subjectId);
    }
}
