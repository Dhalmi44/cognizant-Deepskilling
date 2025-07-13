using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeApiDemo.Models;
using EmployeeApiDemo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ServiceFilter(typeof(CustomAuthFilter))]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>();

        public EmployeeController()
        {
            if (_employees.Count == 0)
                _employees = GetStandardEmployeeList();
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Salary = 60000,
                    Permanent = true,
                    Department = new Department { Id = 1, Name = "IT" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 1, Name = "C#" },
                        new Skill { Id = 2, Name = "ASP.NET Core" }
                    },
                    DateOfBirth = new DateTime(1990, 5, 5)
                }
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Employee>> GetStandard()
        {
            return Ok(_employees);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        public ActionResult<Employee> PostEmployee([FromBody] Employee employee)
        {
            _employees.Add(employee);
            return Ok(employee);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        public ActionResult<Employee> PutEmployee([FromBody] Employee employee)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing == null) return NotFound();

            existing.Name = employee.Name;
            existing.Salary = employee.Salary;
            existing.Permanent = employee.Permanent;
            existing.Department = employee.Department;
            existing.Skills = employee.Skills;
            existing.DateOfBirth = employee.DateOfBirth;

            return Ok(existing);
        }
    }
}