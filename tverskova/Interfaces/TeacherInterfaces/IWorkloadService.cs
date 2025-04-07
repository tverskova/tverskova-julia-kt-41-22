using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface IWorkloadService
    {
        Task<Workload[]> GetWorkloadAsync(WorkloadFilter filter, CancellationToken cancellationToken);
        Task<Workload> AddWorkloadAsync(Workload workload, CancellationToken cancellationToken);
        Task<Workload> UpdateWorkloadAsync(int workloadId, Workload updatedWorkload, CancellationToken cancellationToken);
    }

    public class WorkloadService : IWorkloadService
    {
        private readonly TeacherDbContext _dbContext;

        public WorkloadService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Workload[]> GetWorkloadAsync(WorkloadFilter filter, CancellationToken cancellationToken = default)
        {
            var workloads = _dbContext.Workloads
                .Include(w => w.Teacher)
                    .ThenInclude(t => t.Department)
                .Include(w => w.Discipline)
                .AsQueryable();

            if (filter.TeacherId.HasValue)
            {
                workloads = workloads.Where(w => w.TeacherId == filter.TeacherId.Value);
            }

            if (filter.DepartmentId.HasValue)
            {
                workloads = workloads.Where(w => w.Teacher.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.DisciplineId.HasValue)
            {
                workloads = workloads.Where(w => w.DisciplineId == filter.DisciplineId.Value);
            }

            return await workloads.ToArrayAsync(cancellationToken);
        }

        // Добавление новой нагрузки
        public async Task<Workload> AddWorkloadAsync(Workload workload, CancellationToken cancellationToken)
        {
            if (workload == null)
            {
                throw new ArgumentNullException(nameof(workload), "Нагрузка не может быть нулевой.");
            }

            _dbContext.Workloads.Add(workload);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return workload;
        }

        // Обновление существующей нагрузки
        public async Task<Workload> UpdateWorkloadAsync(int workloadId, Workload updatedWorkload, CancellationToken cancellationToken)
        {
            var workload = await _dbContext.Workloads
                .FirstOrDefaultAsync(w => w.WorkloadId == workloadId, cancellationToken);

            if (workload == null)
            {
                throw new KeyNotFoundException($"Нагрузка с id {workloadId} не найдена.");
            }

            // Обновление полей
            workload.TeacherId = updatedWorkload.TeacherId;
            workload.DisciplineId = updatedWorkload.DisciplineId;
            workload.Hours = updatedWorkload.Hours;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return workload;
        }
    }
}
