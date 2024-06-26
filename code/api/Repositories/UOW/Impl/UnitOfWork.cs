﻿namespace Repositories.UOW.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IDepartmentRepository Departments { get; private set; }
        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Departments = new DepartmentRepository(_context);
        }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
