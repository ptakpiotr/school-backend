namespace School.DataAccess.Services.Contracts
{
    public interface ITeacherService
    {
        Task<List<TeacherModel>> GetTeachers();
        Task<TeacherModel> GetTeacherById(int id);
        Task AddTeacher(TeacherDTO teacher);
        Task DeleteTeacher(int id);

        Task UpdateTeacher(int id, TeacherModel teacher);
    }
}
