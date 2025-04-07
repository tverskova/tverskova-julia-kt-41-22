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
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetDepartmentAsync([FromBody] DepartmentFilter filter, CancellationToken cancellationToken = default)
        {
            // Применяем значения по умолчанию, если фильтры null
            filter.MinDateOfFoundation ??= DateTime.MinValue;  // Устанавливаем минимальную дату, если фильтр не передан
            filter.MaxDateOfFoundation ??= DateTime.MaxValue;  // Устанавливаем максимальную дату, если фильтр не передан
            filter.MinTeachersCount ??= 0;  // Устанавливаем минимальное количество преподавателей, если фильтр не передан
            filter.MaxTeachersCount ??= int.MaxValue;  // Устанавливаем максимальное количество преподавателей, если фильтр не передан
            filter.HeadTeacherId ??= null;  // Если нет фильтра для HeadTeacherId, оставляем null

            var departments = await _departmentService.GetDepartmentAsync(filter, cancellationToken);
            return Ok(departments);
        }

        // Добавление, обновление и удаление кафедры (методы не меняются)
    


    [HttpPost("add")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] Department department, CancellationToken cancellationToken = default)
        {
            await _departmentService.AddDepartmentAsync(department, cancellationToken);
            return Ok(new { message = "Кафедра успешно добавлена." });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDepartmentAsync([FromBody] Department department, CancellationToken cancellationToken = default)
        {
            await _departmentService.UpdateDepartmentAsync(department, cancellationToken);
            return Ok(new { message = "Кафедра успешно обновлена." });
        }

        [HttpDelete("delete/{departmentId}")]
        public async Task<IActionResult> DeleteDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId, cancellationToken);
            return Ok(new { message = "Кафедра и связанные преподаватели удалены." });
        }
    }
}
