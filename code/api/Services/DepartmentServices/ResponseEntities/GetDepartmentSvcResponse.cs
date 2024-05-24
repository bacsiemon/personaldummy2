using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DepartmentServices.ResponseEntities
{
    public class GetDepartmentSvcResponse
    {
        public List<Department> Departments { get; set; }

        public string Errors { get; set; }
    }
}
