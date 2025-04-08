using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface IDepartmentService
    {
        Task<Department[]> GetDepartmentAsync(CancellationToken cancellationToken);
        Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken);
        Task<Department[]> GetDepartmentByDateOfFoundingAsync(DateTime dateoffound, CancellationToken cancellationToken);
        Task<Department[]> GetDepartmentByTeacherCountAsync(int teacherCount, CancellationToken cancellationToken);
        Task AddDepartmentAsync(Department department, CancellationToken cancellationToken);
        Task UpdateDepartmentAsync(Department department, CancellationToken cancellationToken);
        Task DeleteDepartmentAsync(Department department, CancellationToken cancellationToken);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly TeacherDbContext _dbContext;

        public DepartmentService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department[]> GetDepartmentAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Departments
                                   .ToArrayAsync(cancellationToken);
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Departments
                       .FirstOrDefaultAsync(d => d.DepartmentId == id, cancellationToken);
        }

        public async Task<Department[]> GetDepartmentByDateOfFoundingAsync(DateTime dateoffound, CancellationToken cancellationToken)
        {
            return await _dbContext.Departments
                .Where(d => d.DateOfFoundation.Date == dateoffound.Date)
                .Include(d => d.HeadTeacher)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Department[]> GetDepartmentByTeacherCountAsync(int teacherCount, CancellationToken cancellationToken)
        {
            return await _dbContext.Departments
               .Where(d => _dbContext.Teachers.Count(t => t.DepartmentId == d.DepartmentId) == teacherCount)
               .Include(d => d.HeadTeacher)
               .ToArrayAsync(cancellationToken);
        }     


        // Add, Remove, Update
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

        public async Task DeleteDepartmentAsync(Department department, CancellationToken cancellationToken)
        {
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
