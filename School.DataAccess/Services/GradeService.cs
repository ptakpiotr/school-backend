namespace School.DataAccess.Services;

public class GradeService : IGradeService
{
    private readonly string _mainConn;

    public GradeService(IOptions<ConnectionStringOptions> options)
    {
        _mainConn = options.Value.MainConn;
    }

    public async Task AddGrade(GradeModel grade)
    {
        await CRUDHelper.Add<GradeModel, GradeModel>(_mainConn, grade);

    }

    public async Task AddUserGrade(UserGradeDTO userGradeDTO)
    {
        await CRUDHelper.Add<UserGradeModel, UserGradeDTO>(_mainConn, userGradeDTO);
    }

    public async Task DeleteGrade(int id)
    {
        await CRUDHelper.Delete<GradeModel>(_mainConn, id);
    }

    public async Task<GradeModel> GetGrade(int id)
    {
        return await CRUDHelper.GetOne<GradeModel>(_mainConn, id);
    }

    public async Task<List<GradeModel>> GetGrades()
    {
        return await CRUDHelper.GetAll<GradeModel>(_mainConn);
    }

    public async Task<List<GroupedGradesModel>> GetGroupedGrades(int classId, string np)
    {
        using (IDbConnection conn = new NpgsqlConnection(_mainConn))
        {
            return await conn.CallResultFunctionAsync<GroupedGradesModel, FunctionModels.GroupedGradesModel>("public.fn_get_grades", new FunctionModels.GroupedGradesModel() { ClassId = classId, Np = np });
        }
    }

    public async Task<List<StudentGradesModel>> GetStudentGrades(int studentId)
    {
        using (IDbConnection conn = new NpgsqlConnection(_mainConn))
        {
            return await conn.CallResultFunctionAsync<StudentGradesModel, FunctionModels.StudentGradesModel>("public.fn_get_student_grades", new FunctionModels.StudentGradesModel() { StudentId = studentId });
        }
    }

    public async Task<List<UserGradeModel>> GetUserGrades()
    {
        return await CRUDHelper.GetAll<UserGradeModel>(_mainConn);
    }
}
