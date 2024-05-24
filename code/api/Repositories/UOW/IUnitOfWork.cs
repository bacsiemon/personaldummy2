namespace Repositories.UOW
{
    public interface IUnitOfWork : IDisposable
    {

        IDepartmentRepository Departments { get; }

        int Complete();

    }
}
