using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface IDepartmentService
    {
        Task<Department[]> GetDepartmentAsync(DepartmentFilter filter, CancellationToken cancellationToken);
        Task AddDepartmentAsync(Department department, CancellationToken cancellationToken);
        Task UpdateDepartmentAsync(Department department, CancellationToken cancellationToken);
        Task DeleteDepartmentAsync(int departmentId, CancellationToken cancellationToken);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly TeacherDbContext _dbContext;

        public DepartmentService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department[]> GetDepartmentAsync(DepartmentFilter filter, CancellationToken cancellationToken)
        {
            var departments = _dbContext.Departments
                .Include(d => d.HeadTeacher)
                .Include(d => d.Teachers)
                .AsQueryable();

            // Применение фильтров
            if (filter.MinDateOfFoundation.HasValue)
            {
                departments = departments.Where(d => d.DateOfFoundation >= filter.MinDateOfFoundation.Value);
            }

            if (filter.MaxDateOfFoundation.HasValue)
            {
                departments = departments.Where(d => d.DateOfFoundation <= filter.MaxDateOfFoundation.Value);
            }

            if (filter.MinTeachersCount.HasValue)
            {
                departments = departments.Where(d => d.Teachers.Count >= filter.MinTeachersCount.Value);
            }

            if (filter.MaxTeachersCount.HasValue)
            {
                departments = departments.Where(d => d.Teachers.Count <= filter.MaxTeachersCount.Value);
            }

            if (filter.HeadTeacherId.HasValue)
            {
                departments = departments.Where(d => d.HeadTeacherId == filter.HeadTeacherId.Value);
            }

            return await departments.ToArrayAsync(cancellationToken);
        }

        public async Task AddDepartmentAsync(Department department, CancellationToken cancellationToken)
        {
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateDepartmentAsync(Department department, CancellationToken cancellationToken)
        {
            _dbContext.Departments.Update(department);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDepartmentAsync(int departmentId, CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments
                .Include(d => d.Teachers)
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId, cancellationToken);

            if (department != null)
            {
                _dbContext.Teachers.RemoveRange(department.Teachers);
                _dbContext.Departments.Remove(department);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }

}
