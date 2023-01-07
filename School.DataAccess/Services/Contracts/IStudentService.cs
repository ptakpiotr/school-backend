namespace School.DataAccess.Services.Contracts
{
    public interface IStudentService
    {
        Task<List<StudentModel>> GetStudents();
        Task<StudentModel> GetStudentById(int id);
        Task AddStudent(StudentDTO student);
        Task DeleteStudent(int id);
    }
}