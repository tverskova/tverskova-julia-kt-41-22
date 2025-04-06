using tverskova.Interfaces.TeacherInterfaces;

namespace tverskova.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            return services;
        }
    }
}
