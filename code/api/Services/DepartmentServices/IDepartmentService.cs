using Repositories.Dtos.DepartmentDtos;
using Services.DepartmentServices.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DepartmentServices
{
    public interface IDepartmentService
    {
        CreateDepartmentSvcResponse Create(CreateDepartmentRequestDto requestDto);

        GetDepartmentSvcResponse Get(GetDepartmentRequestDto requestDto);
    }
}
