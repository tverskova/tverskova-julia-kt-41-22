using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;

namespace tverskova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: ВСЕ 
        [HttpGet]
        public async Task<ActionResult<Department[]>> GetDepartment(CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentAsync(cancellationToken);
            return Ok(department);
        }

        // GET: по ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // GET: ByDateOfFoundation
        [HttpGet("ByDateOfFoundation/{dateoffound}")]
        public async Task<ActionResult<Department[]>> GetDepartmentByDateOfFoundingAsync(DateTime dateoffound, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByDateOfFoundingAsync(dateoffound, cancellationToken);

            if (department == null || department.Length == 0)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // GET: ByTeacherCount
        [HttpGet("ByTeacherCount/{TeacherCount}")]
        public async Task<ActionResult<Department[]>> GetDepartmentByTeacherCountAsync(int teacherCount, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByTeacherCountAsync(teacherCount, cancellationToken);

            if (department == null || department.Length == 0)
            {
                return NotFound();
            }

            return Ok(department);
        }


        // PUT: update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, [FromBody] Department department, CancellationToken cancellationToken)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest("ID in route and in body do not match");
            }

            var existingDepartment = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            await _departmentService.UpdateDepartmentAsync(department, cancellationToken);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return NotFound();
            }

            await _departmentService.DeleteDepartmentAsync(department, cancellationToken);

            return NoContent();
        }

        // ADD
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment([FromBody] Department department, CancellationToken cancellationToken)
        {
            await _departmentService.AddDepartmentAsync(department, cancellationToken);

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
        }
    }
}
