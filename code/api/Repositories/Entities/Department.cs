using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
