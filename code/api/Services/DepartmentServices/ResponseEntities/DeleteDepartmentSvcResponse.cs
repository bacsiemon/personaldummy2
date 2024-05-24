using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DepartmentServices.ResponseEntities
{
    public class DeleteDepartmentSvcResponse
    {
        public bool Success { get; set; } = false;
        public string? Errors { get; set; } 
    }
}
