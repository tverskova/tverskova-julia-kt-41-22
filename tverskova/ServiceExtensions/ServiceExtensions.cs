using tverskova.Interfaces.TeacherInterfaces;

namespace tverskova.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDisciplinesService, DisciplinesService>();
            services.AddScoped<IWorkloadService, WorkloadService>();
            return services;
        }
    }
}
