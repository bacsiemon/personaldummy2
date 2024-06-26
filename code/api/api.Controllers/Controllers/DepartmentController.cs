﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos.DepartmentDtos;
using Services.DepartmentServices;
using Services.DepartmentServices.ResponseEntities;

namespace api.Controllers.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _deptSvc;

        public DepartmentController(IDepartmentService deptSvc)
        {
            _deptSvc = deptSvc; 
        }

        
        [HttpPost("create")]
        public  IActionResult Create([FromBody] CreateDepartmentRequestDto requestDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            CreateDepartmentSvcResponse result = _deptSvc.Create(requestDto);
            
            if (!string.IsNullOrEmpty(result.Errors)) return StatusCode(500, result.Errors);

            return Ok(new CreateDepartmentResponseDto()
            {
                Id = result.Department.Id,
                DepartmentName = result.Department.DepartmentName,
                Description = result.Department.Description
            });
        }

        [HttpGet("get")]
        public IActionResult Get([FromQuery] GetDepartmentRequestDto requestDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            GetDepartmentSvcResponse result = _deptSvc.Get(requestDto);

            if (!string.IsNullOrEmpty(result.Errors)) return StatusCode(500, result.Errors);

            var responseDtos = result.Departments.Select(d => new GetDepartmentResponseDto()
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
            });
            return Ok(responseDtos);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateDepartmentRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            UpdateDepartmentSvcResponse responseEnt = _deptSvc.Update(requestDto);

            if (!string.IsNullOrEmpty(responseEnt.Errors)) 
                return StatusCode(500, responseEnt.Errors);

            return Ok(responseEnt.DepartmentResponseDto);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] DeleteDepartmentRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            DeleteDepartmentSvcResponse responseEnt = _deptSvc.Delete(requestDto);

            if (!string.IsNullOrEmpty(responseEnt.Errors)) return StatusCode(500,responseEnt.Errors);

            return NoContent();
        }
    }
}
