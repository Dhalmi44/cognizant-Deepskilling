using Microsoft.AspNetCore.Mvc;
using EmployeeApiDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR" },
            new Employee { Id = 2, Name = "Bob", Department = "IT" },
            new Employee { Id = 3, Name = "Charlie", Department = "Finance" }
        };

        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var existingEmployee = employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return BadRequest("Invalid employee id");
            }

            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Department = updatedEmployee.Department;

            return Ok(existingEmployee);
        }
    }
}
