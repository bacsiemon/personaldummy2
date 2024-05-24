using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos.DepartmentDtos
{
    public class CreateDepartmentRequestDto
    {
        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
