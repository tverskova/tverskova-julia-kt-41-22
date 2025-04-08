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
        private readonly ILogger<TeacherController> _logger;
        private readonly ITeacherService _teacherService;

        public TeacherController(ILogger<TeacherController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        // GET: api/Teacher/get
        [HttpPost("get")]
        public async Task<IActionResult> GetTeacherAsync(CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherService.GetTeacherAsync(cancellationToken);
            if (teachers == null || teachers.Length == 0)
            {
                return NotFound("No teachers found.");
            }

            return Ok(teachers);
        }

        // GET: api/Teacher/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetTeacherByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id, cancellationToken);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            return Ok(teacher);
        }

        // GET: api/Teacher/getByDepartment/{departmentId}
        [HttpGet("getByDepartment/{departmentId}")]
        public async Task<IActionResult> GetTeacherByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            var filter = new TeacherFilter { DepartmentId = departmentId };
            var teachers = await _teacherService.GetTeacherByDepartmentAsync(filter, cancellationToken);
            if (teachers == null || teachers.Length == 0)
            {
                return NotFound($"No teachers found in the department with ID {departmentId}.");
            }

            return Ok(teachers);
        }

        // GET: api/Disciplines/ByWorkloadRange?min=20&max=30
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

        // GET: api/Teacher/getByStaff/{staffId}
        [HttpGet("getByStaff/{staffId}")]
        public async Task<IActionResult> GetTeacherByStaffAsync(int staffId, CancellationToken cancellationToken = default)
        {
            var filter = new TeacherFilter { StaffId = staffId };
            var teachers = await _teacherService.GetTeacherByStaffAsync(filter, cancellationToken);
            if (teachers == null || teachers.Length == 0)
            {
                return NotFound($"No teachers found with staff ID {staffId}.");
            }

            return Ok(teachers);
        }

        // POST: api/Teacher/add
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacherAsync([FromBody] Teacher teacher, CancellationToken cancellationToken = default)
        {
            await _teacherService.AddTeacherAsync(teacher, cancellationToken);
            return CreatedAtAction(nameof(GetTeacherByIdAsync), new { id = teacher.TeacherId }, teacher);
        }

        // PUT: api/Teacher/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacherAsync(int id, [FromBody] Teacher teacher, CancellationToken cancellationToken = default)
        {
            teacher.TeacherId = id; // Убедитесь, что ID преподавателя правильный при обновлении
            await _teacherService.UpdateTeacherAsync(teacher, cancellationToken);
            return Ok(new { message = "Teacher successfully updated." });
        }

        // DELETE: api/Teacher/delete/{teacherId}
        [HttpDelete("delete/{teacherId}")]
        public async Task<IActionResult> DeleteTeacherAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(teacherId, cancellationToken);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {teacherId} not found.");
            }

            await _teacherService.DeleteTeacherAsync(teacher, cancellationToken);
            return Ok(new { message = "Teacher successfully deleted." });
        }
    }
}
