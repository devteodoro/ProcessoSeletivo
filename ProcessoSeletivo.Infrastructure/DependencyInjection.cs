using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Application.Services;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Infrastructure.Data;
using ProcessoSeletivo.Infrastructure.Repositories;

namespace ProcessoSeletivo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {

            services
                .AddDbContext<AppDataContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(AppDataContext).Assembly.FullName));
                });

            services
                .AddScoped<ITokenService, TokenService>();

            services
                .AddScoped<IUserRepository, UserRepository>();

            services
                .AddScoped<IPersonRepository, PersonRepository>();

            services
                .AddScoped<IPhotoRepository, PhotoRepository>();

            services
                .AddScoped<IUserService, UserService>();

            services
                .AddScoped<IPersonService, PersonService>();

            services
                .AddScoped<IPhotoService, PhotoService>();

            return services;
        }
    }
}
