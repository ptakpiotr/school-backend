namespace School.DataAccess.Services.Contracts
{
    public interface IGradeService
    {
        Task<List<GradeModel>> GetGrades();
        Task<GradeModel> GetGrade(int id);
        Task AddGrade(GradeModel grade);
        Task DeleteGrade(int id);
        Task<List<GroupedGradesModel>> GetGroupedGrades(int classId, string np);
        Task<List<StudentGradesModel>> GetStudentGrades(int studentId);

    }
}
