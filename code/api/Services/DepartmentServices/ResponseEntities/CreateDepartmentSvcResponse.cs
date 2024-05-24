using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DepartmentServices.ResponseEntities
{
    public class CreateDepartmentSvcResponse
    {
        public Department? Department { get; set; }

        public string? Errors { get; set; }
    }
}
