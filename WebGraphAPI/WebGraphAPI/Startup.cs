using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceCollections;

namespace WebGraphAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IConfiguration>(Configuration);

            services.AddRepositories();
            services.AddServices();
            services.AddDbContext(_configuration);
            services.AddJwtService(_configuration);
            services.AddResolvers();
            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddGraphQlService(_hostingEnvironment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserService userService, IClientService clientService)
        {
            // INICIALIZA O DB INMEMORY
            clientService.InicializaDb();
            userService.InicializaDb();

            // TODO: CRIAR AMBIENTES DE ENV
            {
                if (env.IsDevelopment() || env.IsStaging())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
            }

            app.UseCors(x =>  x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            // app.UseGraphiQl("/graphql", "/v2/api/zeeng");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(builder => { builder.MapControllers(); });
        }
    }
}
