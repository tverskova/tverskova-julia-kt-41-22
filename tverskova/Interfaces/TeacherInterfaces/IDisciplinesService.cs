using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface IDisciplinesService
    {
        Task<Discipline[]> GetDisciplinesAsync(CancellationToken cancellationToken);
        Task<Discipline> GetDisciplineByIdAsync(int id, CancellationToken cancellationToken);
        Task<Discipline[]> GetDisciplinesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken);
        Task AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        Task UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        Task DeleteDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        Task<Discipline[]> GetDisciplinesByWorkloadRangeAsync(int minHours, int maxHours, CancellationToken cancellationToken);

    }

    public class DisciplinesService : IDisciplinesService
    {
        private readonly TeacherDbContext _dbContext;

        public DisciplinesService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discipline[]> GetDisciplinesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Disciplines
                                   .ToArrayAsync(cancellationToken);
        }

        public async Task<Discipline?> GetDisciplineByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Disciplines
                       .FirstOrDefaultAsync(d => d.DisciplineId == id, cancellationToken);
        }
        public async Task<Discipline[]> GetDisciplinesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                                   .Where(w => w.TeacherId == teacherId)
                                   .Include(w => w.Discipline)
                                   .Select(w => w.Discipline!)
                                   .Distinct()
                                   .ToArrayAsync(cancellationToken);
        }

        public async Task<Discipline[]> GetDisciplinesByWorkloadRangeAsync(int minHours, int maxHours, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                .Where(w => w.Hours >= minHours && w.Hours <= maxHours)
                .Select(w => w.Discipline)
                .Distinct()
                .ToArrayAsync(cancellationToken);
        }


        // Добавление новой дисциплины
        public async Task AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }       


        // Обновление дисциплины
        public async Task UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(discipline).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Удаление дисциплины
        public async Task DeleteDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            _dbContext.Disciplines.Remove(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
