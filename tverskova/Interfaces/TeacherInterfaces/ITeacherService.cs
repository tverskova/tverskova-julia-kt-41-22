using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface ITeacherService
    {
        public Task<Teacher[]> GetTeacherByDepartmentAsync(TeacherDepartmentFilter filter, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly TeacherDbContext _dbContext;
        public TeacherService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Teacher[]> GetTeacherByDepartmentAsync(TeacherDepartmentFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = _dbContext.Set<Teacher>().Where(w => w.Department.Name == filter.Name).ToArrayAsync(cancellationToken);
            return teachers;
        }
    }
}
