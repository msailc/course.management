using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PagebaTask.Services;
using PagebaTask.Services.Students;
using PagebaTask.Services.Statuses;
using PagebaTask.Services.Courses;
using Microsoft.AspNetCore.Identity;
using PagebaTask.Entities;

namespace PagebaTask
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<StudentContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(); // Enable sensitive data logging
            });

            services.AddIdentity<Entities.User, IdentityRole>()
                    .AddEntityFrameworkStores<StudentContext>()
                    .AddDefaultTokenProviders();
            
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentStatusService, StudentStatusService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<StudentContext>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
