using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Filters.TeacherFilters;

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

        [HttpPost(Name = "GetTeacherByDepartment")]
        public async Task<IActionResult> GetTeacherByDepartmentAsync(TeacherDepartmentFilter filter, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherService.GetTeacherByDepartmentAsync(filter, cancellationToken); return Ok(teacher);
            return Ok(teacher);
        }
    }
}
