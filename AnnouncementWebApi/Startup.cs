using AnnouncementWebApi.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace AnnouncementWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            using var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();
            if (!dbContext.Announcements.Any())
            {
                dbContext.Announcements.AddRange(new Announcement[]
                {
                            new Announcement{  Id = 1, Title = "First announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
                            new Announcement() { Id = 2, Title = "Друге announce", Description = "This is a different from other each", CreatedDate = DateTime.Now },
                            new Announcement() { Id = 3, Title = "Третє announcement", Description = "Something in announcement, ets....", CreatedDate = DateTime.Now },
                            new Announcement() { Id = 4, Title = "Fourth announcement", Description = "Somet in announce, ets....", CreatedDate = DateTime.Now }
                });
                dbContext.SaveChanges();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnnouncementWebApi", Version = "v1" });
            });
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnnouncementWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
