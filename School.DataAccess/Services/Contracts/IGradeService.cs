namespace School.DataAccess.Services.Contracts
{
    public interface IGradeService
    {
        Task<List<GradeModel>> GetGrades();
        Task<GradeModel> GetGrade(int id);
        Task AddGrade(GradeModel grade);
        Task DeleteGrade(int id);
    }
}
