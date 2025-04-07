using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface IDisciplinesService
    {
        Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken);
        Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        Task<Discipline> UpdateDisciplineAsync(int disciplineId, Discipline updatedDiscipline, CancellationToken cancellationToken);
        Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken);
    }

    public class DisciplinesService : IDisciplinesService
    {
        private readonly TeacherDbContext _dbContext;

        public DisciplinesService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var workloads = _dbContext.Workloads
                .Include(w => w.Discipline)
                .AsQueryable();

            if (filter.TeacherId.HasValue)
            {
                workloads = workloads.Where(w => w.TeacherId == filter.TeacherId.Value);
            }

            if (filter.MinHours.HasValue)
            {
                workloads = workloads.Where(w => w.Hours >= filter.MinHours.Value);
            }

            if (filter.MaxHours.HasValue)
            {
                workloads = workloads.Where(w => w.Hours <= filter.MaxHours.Value);
            }

            var disciplines = await workloads
                .Select(w => w.Discipline)
                .Distinct()
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }

        // Добавление новой дисциплины
        public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            if (discipline == null)
                throw new ArgumentNullException(nameof(discipline));

            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return discipline;
        }

        // Обновление дисциплины
        public async Task<Discipline> UpdateDisciplineAsync(int disciplineId, Discipline updatedDiscipline, CancellationToken cancellationToken)
        {
            if (updatedDiscipline == null)
                throw new ArgumentNullException(nameof(updatedDiscipline));

            var existingDiscipline = await _dbContext.Disciplines
                .FirstOrDefaultAsync(d => d.DisciplineId == disciplineId, cancellationToken);

            if (existingDiscipline == null)
                return null; // Или выбрасывать исключение, если не найдено

            existingDiscipline.Name = updatedDiscipline.Name;

            _dbContext.Disciplines.Update(existingDiscipline);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return existingDiscipline;
        }

        // Удаление дисциплины
        public async Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            var discipline = await _dbContext.Disciplines
                .FirstOrDefaultAsync(d => d.DisciplineId == disciplineId, cancellationToken);

            if (discipline == null)
                return false; // Или выбрасывать исключение, если не найдено

            _dbContext.Disciplines.Remove(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
