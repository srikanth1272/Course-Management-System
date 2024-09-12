using StdSubjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StdSubjectLibrary.Repos
{
    public interface IStdSubjectRepoAsync
    {
        Task<List<StdSubject>> GetAllStudentsSubjects();
        Task<StdSubject> GetStudentSubject(string RollNo, string SubjectId);
        Task AddStudentSubject(StdSubject student);
        Task UpdateStudentSubject(string RollNo, string SubjectId, StdSubject student);
        Task DeleteStudentSubject(string RollNo, string SubjectId);
        Task<List<StdSubject>> GetBySemister(string RollNo, int Semister);
        Task<List<StdSubject>> GetByRollNo(string RollNo);
        
    }
}
