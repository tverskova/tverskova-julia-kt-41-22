using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface ITeacherService
    {
        Task<Teacher[]> GetTeacherAsync(CancellationToken cancellationToken);
        Task<Teacher> GetTeacherByIdAsync(int id, CancellationToken cancellationToken);
        Task<Teacher[]> GetTeacherByDepartmentAsync(int id, CancellationToken cancellationToken);
        Task<Teacher[]> GetTeacherByAcademicDegreeAsync(int id, CancellationToken cancellationToken);
        Task<Teacher[]> GetTeacherByStaffAsync(int id, CancellationToken cancellationToken);

        Task AddTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task UpdateTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task DeleteTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly TeacherDbContext _dbContext;

        public TeacherService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Получение всех преподавателей
        public async Task<Teacher[]> GetTeacherAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                                   .ToArrayAsync(cancellationToken);
        }

        // Получение преподавателя по ID
        public async Task<Teacher?> GetTeacherByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                       .FirstOrDefaultAsync(d => d.TeacherId == id, cancellationToken);
        }

        // Получение преподавателей по кафедре
        public async Task<Teacher[]> GetTeacherByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                .Where(t => t.DepartmentId == departmentId)
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Staff)
                .ToArrayAsync(cancellationToken);
        }

        // Получение преподавателей по академической степени
        public async Task<Teacher[]> GetTeacherByAcademicDegreeAsync(int academicDegreeId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                .Where(t => t.AcademicDegreeId == academicDegreeId)
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Staff)
                .ToArrayAsync(cancellationToken);
        }


        // Получение преподавателей по должности
        public async Task<Teacher[]> GetTeacherByStaffAsync(int staffId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                .Where(t => t.StaffId == staffId)
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Staff)
                .ToArrayAsync(cancellationToken);
        }


        // Добавление преподавателя
        public async Task AddTeacherAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Обновление преподавателя
        public async Task UpdateTeacherAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(teacher).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Удаление преподавателя
        public async Task DeleteTeacherAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
