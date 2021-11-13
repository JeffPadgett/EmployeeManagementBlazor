using EmployeeManagement.Models;
using EmployeeMmanagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMmanagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;
        public EmployeesController(IEmployeeRepository context)
        {
            _empRepository = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _empRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        // GET: api/employees/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _empRepository.GetEmployeeById(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;//ASP.NET Core will automatically serialize the Employee object and return in the response body.
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database with empId:{id}");
            }
        }

        // POST: api/employees
        //When doing just about any post CREATE. We need to do 3 things, return status code 201,
        //return the newly created resource
        //and include the location header in the response.
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee emp)
        {
            try
            {
                if (emp == null)
                {
                    return BadRequest();
                }

                var employee = _empRepository.GetEmployeeByEmail(emp.Email);
                if (employee != null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _empRepository.AddEmployee(emp);

                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database");
                throw;
            }
            return Ok();
        }

    }
}
