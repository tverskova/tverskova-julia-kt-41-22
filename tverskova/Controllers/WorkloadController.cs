using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Models;
using System.Threading;
using System.Threading.Tasks;

namespace tverskova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkloadController : ControllerBase
    {
        private readonly IWorkloadService _workloadService;

        public WorkloadController(IWorkloadService workloadService)
        {
            _workloadService = workloadService;
        }

        // GET: ВСЕ
        [HttpGet]
        public async Task<ActionResult<Workload[]>> GetWorkloads(CancellationToken cancellationToken)
        {
            var workloads = await _workloadService.GetWorkloadAsync(cancellationToken);
            return Ok(workloads);
        }

        // GET: по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Workload>> GetWorkload(int id, CancellationToken cancellationToken)
        {
            var workload = await _workloadService.GetWorkloadByIdAsync(id, cancellationToken);

            if (workload == null)
            {
                return NotFound();
            }

            return Ok(workload);
        }

        // GET: по TeacherId
        [HttpGet("ByTeacher/{teacherId}")]
        public async Task<ActionResult<Workload[]>> GetWorkloadsByTeacherId(int teacherId, CancellationToken cancellationToken)
        {
            var workloads = await _workloadService.GetWorkloadByTeacherIdAsync(teacherId, cancellationToken);

            if (workloads == null || workloads.Length == 0)
            {
                return NotFound($"No workloads found for teacher with ID {teacherId}");
            }

            return Ok(workloads);
        }

        // GET: по DepartmentId
        [HttpGet("ByDepartment/{departmentId}")]
        public async Task<ActionResult<Workload[]>> GetWorkloadsByDepartmentId(int departmentId, CancellationToken cancellationToken)
        {
            var workloads = await _workloadService.GetWorkloadByDepartmentIdAsync(departmentId, cancellationToken);

            if (workloads == null || workloads.Length == 0)
            {
                return NotFound($"No workloads found for department with ID {departmentId}");
            }

            return Ok(workloads);
        }

        // GET: по DisciplineId
        [HttpGet("ByDiscipline/{disciplineId}")]
        public async Task<ActionResult<Workload[]>> GetWorkloadsByDisciplinesId(int disciplineId, CancellationToken cancellationToken)
        {
            var workloads = await _workloadService.GetWorkloadByDisciplinesIdAsync(disciplineId, cancellationToken);

            if (workloads == null || workloads.Length == 0)
            {
                return NotFound($"No workloads found for discipline with ID {disciplineId}");
            }

            return Ok(workloads);
        }

        // PUT: update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkload(int id, [FromBody] Workload workload, CancellationToken cancellationToken)
        {
            if (id != workload.WorkloadId)
            {
                return BadRequest("ID in route and in body do not match");
            }

            var existingWorkload = await _workloadService.GetWorkloadByIdAsync(id, cancellationToken);
            if (existingWorkload == null)
            {
                return NotFound();
            }

            await _workloadService.UpdateWorkloadAsync(workload, cancellationToken);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkload(int id, CancellationToken cancellationToken)
        {
            var workload = await _workloadService.GetWorkloadByIdAsync(id, cancellationToken);
            if (workload == null)
            {
                return NotFound();
            }

            await _workloadService.DeleteWorkloadAsync(workload, cancellationToken);

            return NoContent();
        }

        // ADD
        [HttpPost]
        public async Task<ActionResult<Workload>> PostWorkload([FromBody] Workload workload, CancellationToken cancellationToken)
        {
            await _workloadService.AddWorkloadAsync(workload, cancellationToken);

            return CreatedAtAction(nameof(GetWorkload), new { id = workload.WorkloadId }, workload);
        }
    }
}
