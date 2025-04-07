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
    public class WorkloadController : ControllerBase
    {
        private readonly ILogger<WorkloadController> _logger;
        private readonly IWorkloadService _workloadService;

        public WorkloadController(ILogger<WorkloadController> logger, IWorkloadService workloadService)
        {
            _logger = logger;
            _workloadService = workloadService;
        }

        // Получение списка нагрузки
        [HttpPost(Name = "GetWorkload")]
        public async Task<IActionResult> GetWorkloadAsync(WorkloadFilter filter, CancellationToken cancellationToken = default)
        {
            var workloads = await _workloadService.GetWorkloadAsync(filter, cancellationToken);
            return Ok(workloads);
        }

        // Добавление новой нагрузки
        [HttpPost("add")]
        public async Task<IActionResult> AddWorkloadAsync([FromBody] Workload workload, CancellationToken cancellationToken = default)
        {
            if (workload == null)
            {
                return BadRequest("Нагрузка не может быть нулевой.");
            }

            try
            {
                var addedWorkload = await _workloadService.AddWorkloadAsync(workload, cancellationToken);
                return Ok(new { message = "Нагрузка успешно добавлена.", workload = addedWorkload });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка");
                return StatusCode(500, new { message = "Ошибка" });
            }
        }

        // Обновление нагрузки
        [HttpPut("update/{workloadId}")]
        public async Task<IActionResult> UpdateWorkloadAsync(int workloadId, [FromBody] Workload updatedWorkload, CancellationToken cancellationToken = default)
        {
            if (updatedWorkload == null)
            {
                return BadRequest("Некорректное значение");
            }

            try
            {
                var updated = await _workloadService.UpdateWorkloadAsync(workloadId, updatedWorkload, cancellationToken);
                return Ok(new { message = "Нагрузка успешно обновлена.", workload = updated });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка");
                return StatusCode(500, new { message = "Ошибка" });
            }
        }
    }
}
