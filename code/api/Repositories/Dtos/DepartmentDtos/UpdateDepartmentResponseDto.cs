using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos.DepartmentDtos
{
    public class UpdateDepartmentResponseDto
    {
        public int? Id { get; set; }

        public string? DepartmentName { get; set; }

        public string? Description { get; set; }
    }
}
