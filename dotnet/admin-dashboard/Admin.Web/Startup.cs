using Admin.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AdminContext>(
                o => o.UseSqlServer(_configuration.GetConnectionString("UsersContext"), 
                    s => s.MigrationsAssembly(this.GetType().Assembly.GetName().Name))
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles()
                .UseMvc(o => o.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}")
                );
        }
    }
}