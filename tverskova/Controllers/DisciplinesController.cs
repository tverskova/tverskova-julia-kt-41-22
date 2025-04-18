﻿using Microsoft.AspNetCore.Mvc;
using tverskova.Interfaces.TeacherInterfaces;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using System.Threading;
using System.Threading.Tasks;

namespace tverskova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplinesService _disciplineService;

        public DisciplinesController(IDisciplinesService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        // GET: ВСЕ 
        [HttpGet]
        public async Task<ActionResult<Discipline[]>> GetDisciplines(CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(cancellationToken);
            return Ok(disciplines);
        }

        // GET: по ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<Discipline>> GetDiscipline(int id, CancellationToken cancellationToken)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(id, cancellationToken);

            if (discipline == null)
            {
                return NotFound();
            }

            return Ok(discipline);
        }

        // GET: ByTeacher
        [HttpGet("ByTeacher/{teacherId}")]
        public async Task<ActionResult<Discipline[]>> GetDisciplinesByTeacherId(int teacherId, CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesByTeacherIdAsync(teacherId, cancellationToken);

            if (disciplines == null || disciplines.Length == 0)
            {
                return NotFound($"No disciplines found for teacher with ID {teacherId}");
            }

            return Ok(disciplines);
        }

        // GET: ByWorkload
        [HttpGet("ByWorkloadRange")]
        public async Task<ActionResult<Discipline[]>> GetDisciplinesByWorkloadRange([FromQuery] int min, [FromQuery] int max, CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesByWorkloadRangeAsync(min, max, cancellationToken);

            if (disciplines == null || disciplines.Length == 0)
            {
                return NotFound($"No disciplines found with workload between {min} and {max} hours.");
            }

            return Ok(disciplines);
        }

        // PUT: update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscipline(int id, [FromBody] Discipline discipline, CancellationToken cancellationToken)
        {
            if (id != discipline.DisciplineId)
            {
                return BadRequest("ID in route and in body do not match");
            }

            var existingDiscipline = await _disciplineService.GetDisciplineByIdAsync(id, cancellationToken);
            if (existingDiscipline == null)
            {
                return NotFound();
            }

            await _disciplineService.UpdateDisciplineAsync(discipline, cancellationToken);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id, CancellationToken cancellationToken)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(id, cancellationToken);
            if (discipline == null)
            {
                return NotFound();
            }

            await _disciplineService.DeleteDisciplineAsync(discipline, cancellationToken);

            return NoContent();
        }

        // ADD
        [HttpPost]
        public async Task<ActionResult<Discipline>> PostDiscipline([FromBody] Discipline discipline, CancellationToken cancellationToken)
        {
            await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);

            return CreatedAtAction(nameof(GetDiscipline), new { id = discipline.DisciplineId }, discipline);
        }
    }
}