using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;

namespace tverskova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplinesService _disciplinesService;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplinesService disciplinesService)
        {
            _logger = logger;
            _disciplinesService = disciplinesService;
        }

        // Получение дисциплин с фильтрами
        [HttpPost(Name = "GetDisciplines")]
        public async Task<IActionResult> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplinesService.GetDisciplinesAsync(filter, cancellationToken);
            return Ok(disciplines);
        }

        // Добавление новой дисциплины
        [HttpPost("add")]
        public async Task<IActionResult> AddDisciplineAsync([FromBody] Discipline discipline, CancellationToken cancellationToken = default)
        {
            if (discipline == null)
            {
                return BadRequest("Invalid discipline data.");
            }

            var addedDiscipline = await _disciplinesService.AddDisciplineAsync(discipline, cancellationToken);
            return Ok(new { message = "Discipline successfully added.", discipline = addedDiscipline });
        }

        // Обновление существующей дисциплины
        [HttpPut("update/{disciplineId}")]
        public async Task<IActionResult> UpdateDisciplineAsync(int disciplineId, [FromBody] Discipline updatedDiscipline, CancellationToken cancellationToken = default)
        {
            if (updatedDiscipline == null)
            {
                return BadRequest("Invalid discipline data.");
            }

            var updated = await _disciplinesService.UpdateDisciplineAsync(disciplineId, updatedDiscipline, cancellationToken);

            if (updated == null)
            {
                return NotFound("Discipline not found.");
            }

            return Ok(new { message = "Discipline successfully updated.", discipline = updated });
        }

        // Удаление дисциплины
        [HttpDelete("delete/{disciplineId}")]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var result = await _disciplinesService.DeleteDisciplineAsync(disciplineId, cancellationToken);

            if (!result)
            {
                return NotFound("Discipline not found.");
            }

            return Ok(new { message = "Discipline successfully deleted." });
        }
    }
}
