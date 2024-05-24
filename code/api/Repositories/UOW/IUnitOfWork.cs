namespace Repositories.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        IDepartmentRepository Departments { get; }

        int Complete();

    }
}
