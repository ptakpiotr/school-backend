namespace School.DataAccess.Services.Contracts
{
    public interface IClassService
    {
        Task<List<ClassModel>> GetAllClasses();
        Task<ClassModel> GetClass(int id);
        Task AddClass(ClassDTO classToAdd);
        Task DeleteClass(int id);
    }
}
