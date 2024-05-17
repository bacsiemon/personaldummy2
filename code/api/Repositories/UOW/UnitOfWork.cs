using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.UOW
{

    /// <summary>
    /// Copy & Pasted from Microsoft
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<AppUser> _appUserRepository;
        private GenericRepository<Employee> _employeeRepository;

        public GenericRepository<AppUser> AppUserRepository
        {
            get
            {

                if (this._appUserRepository == null)
                {
                    this._appUserRepository = new GenericRepository<AppUser>(context);
                }
                return AppUserRepository;
            }
        }

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {

                if (this._employeeRepository == null)
                {
                    this._employeeRepository = new GenericRepository<Employee>(context);
                }
                return EmployeeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
