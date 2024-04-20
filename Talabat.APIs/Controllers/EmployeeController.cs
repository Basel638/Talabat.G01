using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat_Core.Entities;
using Talabat_Core.Repositories.Contract;
using Talabat_Core.Specifications.EmployeeSpecs;

namespace Talabat.APIs.Controllers
{

	public class EmployeeController : BaseApiController
	{
		private readonly IGenericRepository<Employee> _employeesRepo;

		public EmployeeController(IGenericRepository<Employee> employeesRepo)
        {
			_employeesRepo = employeesRepo;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
		{
			var spec = new EmployeeWithDepartmentSpecifications();

			var employees = await _employeesRepo.GetAllWithSpecAsync(spec);

			return Ok(employees);
		}

		[HttpGet("{id}")]

		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications(id);

			var employee = await _employeesRepo.GetWithSpecAsync(spec);
			if(employee is null) 
				return NotFound(new ApiResponse(404));

			return Ok(employee);

		}
	}
}
