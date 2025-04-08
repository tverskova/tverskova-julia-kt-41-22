using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using System.Threading;
using System.Threading.Tasks;
using NLog.Filters;

namespace tverskova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: ВСЕ 
        [HttpGet]
        public async Task<ActionResult<Teacher[]>> GetTeacher(CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetTeacherAsync(cancellationToken);
            return Ok(teacher);
        }


        // GET: по ID 
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetTeacherByIdAsync(int id, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id, cancellationToken);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // GET: ByDepartment
        [HttpGet("ByDepartment/{departmentId}")]
        public async Task<ActionResult<Teacher[]>> GetTeacherByDepartmentAsync(int departmentId, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetTeacherByDepartmentAsync(departmentId, cancellationToken);
            if (teachers == null || teachers.Length == 0)
            {
                return NotFound($"No teachers found in the department with ID {departmentId}.");
            }

            return Ok(teachers);
        }

        // GET: ByWorkload
        [HttpGet("ByAcademicDegree")]
        public async Task<ActionResult<Teacher[]>> GetTeacherByAcademicDegreeAsync([FromQuery] int AcademicDegreeId, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetTeacherByAcademicDegreeAsync(AcademicDegreeId, cancellationToken);

            if (teachers == null || teachers.Length == 0)
            {
                return NotFound($"No teachers found with ID.");
            }

            return Ok(teachers);
        }

        // GET: ByStaff
        [HttpGet("getByStaff/{staffId}")]
        public async Task<IActionResult> GetTeacherByStaffAsync(int staffId, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetTeacherByStaffAsync(staffId, cancellationToken);
            if (teachers == null || teachers.Length == 0)
            {
                return NotFound($"No teachers found with staff ID {staffId}.");
            }

            return Ok(teachers);
        }

        // PUT: update
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacherAsync(int id, [FromBody] Teacher teacher, CancellationToken cancellationToken)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest("ID in route and in body do not match");
            }

            var existingTeacher = await _teacherService.GetTeacherByIdAsync(id, cancellationToken);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            await _teacherService.UpdateTeacherAsync(teacher, cancellationToken);

            return NoContent();
        }

        // DELETE
        [HttpDelete("delete/{teacherId}")]
        public async Task<IActionResult> DeleteTeacherAsync(int teacherId, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(teacherId, cancellationToken);
            if (teacher == null)
            {
                return NotFound();
            }

            await _teacherService.DeleteTeacherAsync(teacher, cancellationToken);
            return NoContent();
        }

        // ADD
        [HttpPost]
        public async Task<IActionResult> AddTeacherAsync([FromBody] Teacher teacher, CancellationToken cancellationToken)
        {
            await _teacherService.AddTeacherAsync(teacher, cancellationToken);
            return CreatedAtAction(nameof(GetTeacherByIdAsync), new { id = teacher.TeacherId }, teacher);
        }                 
    }
}