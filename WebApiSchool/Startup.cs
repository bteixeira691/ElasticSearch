using ApplicationSchool.AutomMapper;
using ApplicationSchool.Interfaces;
using ApplicationSchool.Services;
using AutoMapper;
using DataBaseSchool;
using DataBaseSchool.Model;
using DataBaseSchool.Operations;
using ElasticSearch;
using ElasticSearch.Operations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentCore.Interfaces;
using WebApiSchool.BasicAuten;

namespace WebApiSchool
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
            var connection = @"Server=(localdb)\mssqllocaldb;Database=SchoolDataBase;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<SchoolContext>
                (options => options.UseSqlServer(connection));

            services.AddElasticsearch(Configuration);

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ICourceRepositorySql, CourceRepository>();
            services.AddScoped<ICourseRepositoryElastic, CourseElastic>();

            services.AddScoped<IStudentRepositorySql, StudentRepository>();
            services.AddScoped<IStudentRepositoryElastic, StudentElastic>();

            services.AddScoped<ISearchService, SearchService>();

            services.AddScoped<ISearchRepository, SearchElastic>();




            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


            services.AddMvc(options =>
            {
             
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbCtx = scope.ServiceProvider.GetService<SchoolContext>();

                    // using ContosoUniversity.Data; 
                    DbInitializer.Initialize(dbCtx);

                    dbCtx.Database.Migrate();
                }
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
