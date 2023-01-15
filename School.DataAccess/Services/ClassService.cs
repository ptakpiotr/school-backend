namespace School.DataAccess.Services;

public class ClassService : IClassService
{
    private readonly string _mainConn;

    public ClassService(IOptions<ConnectionStringOptions> options)
    {
        _mainConn = options.Value.MainConn;
    }

    public async Task AddClass(ClassDTO classToAdd)
    {
        await CRUDHelper.Add<ClassModel, ClassDTO>(_mainConn, classToAdd);
    }

    public async Task DeleteClass(int id)
    {
        await CRUDHelper.Delete<ClassModel>(_mainConn, id);
    }

    public async Task<List<ClassModel>> GetAllClasses()
    {
        return await CRUDHelper.GetAll<ClassModel>(_mainConn);
    }

    public async Task<ClassModel> GetClass(int id)
    {
        return await CRUDHelper.GetOne<ClassModel>(_mainConn, id);
    }

    public async Task<List<ClassAvgModel>> GetClassAverages()
    {
        return await CRUDHelper.GetAll<ClassAvgModel>(_mainConn);
    }
}
