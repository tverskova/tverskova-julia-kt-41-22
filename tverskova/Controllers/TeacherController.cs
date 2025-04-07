using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using System.Threading;
using System.Threading.Tasks;

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

        [HttpPost("get")]
        public async Task<IActionResult> GetTeacherByDepartmentAsync([FromBody] TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherService.GetTeacherByDepartmentAsync(filter, cancellationToken);
            return Ok(teacher);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTeacherAsync([FromBody] Teacher filter, CancellationToken cancellationToken = default)
        {
            await _teacherService.AddTeacherAsync(filter, cancellationToken);
            return Ok(new { message = "Преподаватель успешно добавлен." });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacherAsync(int id, [FromBody] Teacher teacher, CancellationToken cancellationToken = default)
        {
            await _teacherService.UpdateTeacherAsync(id, teacher, cancellationToken);
            return Ok(new { message = "Преподаватель успешно обновлён." });
        }


        [HttpDelete("delete/{teacherId}")]
        public async Task<IActionResult> DeleteTeacherAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            await _teacherService.DeleteTeacherAsync(teacherId, cancellationToken);
            return Ok(new { message = "Препдаватель удален." });
        }


    }
}
