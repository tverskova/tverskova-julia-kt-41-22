using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface IWorkloadService
    {
        Task<Workload[]> GetWorkloadAsync(CancellationToken cancellationToken);

        Task<Workload> GetWorkloadByIdAsync(int id, CancellationToken cancellationToken);

        Task<Workload[]> GetWorkloadByTeacherIdAsync(int teacherId, CancellationToken cancellationToken);

        Task<Workload[]> GetWorkloadByDepartmentIdAsync(int departmentId, CancellationToken cancellationToken);

        Task<Workload[]> GetWorkloadByDisciplinesIdAsync(int disciplineId, CancellationToken cancellationToken);

        Task AddWorkloadAsync(Workload workload, CancellationToken cancellationToken);
        Task UpdateWorkloadAsync(Workload workload, CancellationToken cancellationToken);
        Task DeleteWorkloadAsync(Workload workload, CancellationToken cancellationToken);
    }

    public class WorkloadService : IWorkloadService
    {
        private readonly TeacherDbContext _dbContext;

        public WorkloadService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Workload[]> GetWorkloadAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                                   .ToArrayAsync(cancellationToken);           
        }
        public async Task<Workload?> GetWorkloadByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                       .FirstOrDefaultAsync(d => d.WorkloadId == id, cancellationToken);
        }

        public async Task<Workload[]> GetWorkloadByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                .Where(w => w.TeacherId == teacherId)
                .Include(w => w.Teacher)
                    .ThenInclude(t => t.Department)
                .Include(w => w.Discipline)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Workload[]> GetWorkloadByDepartmentIdAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                .Where(w => w.Teacher.DepartmentId == departmentId)
                .Include(w => w.Teacher)
                    .ThenInclude(t => t.Department)
                .Include(w => w.Discipline)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Workload[]> GetWorkloadByDisciplinesIdAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Workloads
                .Where(w => w.DisciplineId == disciplineId)
                .Include(w => w.Teacher)
                    .ThenInclude(t => t.Department)
                .Include(w => w.Discipline)
                .ToArrayAsync(cancellationToken);
        }

        // Добавление
        public async Task AddWorkloadAsync(Workload workload, CancellationToken cancellationToken = default)
        {
            _dbContext.Workloads.Add(workload);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Обновление дисциплины
        public async Task UpdateWorkloadAsync(Workload workload, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(workload).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Удаление дисциплины
        public async Task DeleteWorkloadAsync(Workload workload, CancellationToken cancellationToken)
        {
            _dbContext.Workloads.Remove(workload);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }        
    }
}
