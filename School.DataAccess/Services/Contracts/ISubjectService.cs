
namespace School.DataAccess.Services.Contracts
{
    public interface ISubjectService
    {
        Task<List<SubjectModel>> GetAllSubjects();
        Task<List<DetailedSubjectModel>> GetDetailedSubjects();
        Task<SubjectModel> GetSubject(int id);
        Task AddSubject(SubjectDTO subject);
        Task DeleteSubject(int id);
    }
}
