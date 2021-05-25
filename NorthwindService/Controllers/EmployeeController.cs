using Microsoft.AspNetCore.Mvc;
using NorthwinDB;
using NorthwindService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NorthwindService.Controllers
{
    // base address: api/Employee
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository repo;
        // constructor injects repository registered in Startup
        public EmployeesController(IEmployeeRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/Employee
        // GET: api/Employee/?country=[country]
        // this will always return a list of Employee even if its empty
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Employee>))]
        public async Task<IEnumerable<Employee>> GetEmployees(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return await repo.RetrieveAllAsync();
            }
            else
            {
                return (await repo.RetrieveAllAsync())
                .Where(employee => employee.Country == country);
            }
        }
        // GET: api/Employee/[id]
        [HttpGet("{id}", Name = nameof(GetEmployee))] // named route
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetEmployee(string id)
        {
            Employee e = await repo.RetrieveAsync(id);
            if (e == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return Ok(e); // 200 OK with Employee in body
        }

        // POST: api/Employee
        // BODY: Employee (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Employee e)
        {
            if (e == null)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }
            Employee added = await repo.CreateAsync(e);
            return CreatedAtRoute( // 201 Created
            routeName: nameof(GetEmployee),
            routeValues: new { id = added.EmployeeID.ToLower() },
            value: added);
        }

        // PUT: api/Employee/[id]
        // BODY: Employee (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] Employee e)
        {
            id = id.ToUpper();
            e.EmployeeID = e.EmployeeID.ToUpper();
            if (e == null || e.EmployeeID != id)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }
            var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            await repo.UpdateAsync(id, e);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/Employee/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            bool? deleted = await repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value) // short circuit AND
            {
                return new NoContentResult(); // 204 No content
            }
            else
            {
                return BadRequest($"Employee {id} was found but failed to delete.");// 400 Bad request
            }
        }
    }
}