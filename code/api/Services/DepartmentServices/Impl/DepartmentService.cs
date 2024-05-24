using Repositories.Dtos.DepartmentDtos;
using Repositories.Entities;
using Repositories.UOW;
using Repositories.UOW.Impl;
using Services.DepartmentServices.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DepartmentServices.Impl
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uOW;

        public DepartmentService(IUnitOfWork uOW)
        {
            _uOW = uOW;
        }
        public CreateDepartmentSvcResponse Create(CreateDepartmentRequestDto requestDto)
        {
            var responseEnt = new CreateDepartmentSvcResponse();

            var newDept = new Department()
            {
                DepartmentName = requestDto.DepartmentName,
                Description = requestDto.Description,
            };

            try
            {
                _uOW.Departments.Add(newDept);
                var result = _uOW.Complete();
                if (result == 0) 
                    responseEnt.Errors = "No changes has been made";
            } catch (Exception ex)
            {
                responseEnt.Errors = ex.Message;
            }

            responseEnt.Department = newDept;
            return responseEnt;
        }

        public GetDepartmentSvcResponse Get(GetDepartmentRequestDto requestDto)
        {
            var responseEnt = new GetDepartmentSvcResponse();
            IQueryable<Department> departments = _uOW.Departments.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(requestDto.DepartmentName))
            {
                departments = departments.Where(a => a.DepartmentName.Contains(requestDto.DepartmentName, StringComparison.OrdinalIgnoreCase));
            };

            responseEnt.Departments = departments
                .Skip((requestDto.PageNumber -1) * requestDto.PageSize)
                .Take(requestDto.PageSize)
                .ToList();
            return responseEnt;
        }



    }
}
