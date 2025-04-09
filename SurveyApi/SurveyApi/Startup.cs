using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Models;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SurveyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("SurveyAppCon");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SurveyApi", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext_Survey>(options => options.UseSqlServer(connectionString));
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyApi v1")); //OFF 20 Sept 2024 (IIS EXPRESS)

                string virDir = Configuration.GetSection("VirtualDirectory").Value;

                app.UseSwaggerUI(c =>
                {
                    //c.SwaggerEndpoint(virDir + "/swagger/v1/swagger.json", "SurveyApi v1"); //WITH VirDir
                    c.SwaggerEndpoint(virDir + "/swagger/v1/swagger.json", "SurveyApi v1");
                });

                //app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyApi v1");
                //    options.RoutePrefix = string.Empty;
                //});



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
