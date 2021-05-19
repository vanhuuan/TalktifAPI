using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TalktifAPI.Middleware;
using TalktifAPI.Models;
using TalktifAPI.Repository;
using TalktifAPI.Service;

namespace TalktifAPI
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
            services.AddDbContext<TalktifContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("TalktifConnection")));

            services.AddTransient<IEmailService, EmailService>();

            //new
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IJwtService,JwtService>();
            services.AddScoped<IChatService,ChatService>();
            services.AddScoped<IAdminService,AdminService>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IMessageRepository,MessageRepository>();
            services.AddScoped<IUserChatRoomRepository,UserChatRoomRepository>();
            services.AddScoped<IChatRoomRepository,ChatRoomRepository>();
            services.AddScoped<IReportRepository,ReportRepository>();
            services.AddScoped<IUserRefreshTokenRepository,UserRefreshTokenRepository>();
            services.AddScoped<ICityRepository,CityRepository>();
            services.AddScoped<ICountryRepository,CountryRepository>();

             // configure strongly typed settings object
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.Configure<MailConfig>(Configuration.GetSection("MailConfig"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();

            //services.AddTokenAuthentication(Configuration);  

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        //.AllowCredentials();
                    }
                );
            });
            //luu thong tin version
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TalktifAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TalktifAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();
            // app.UseAuthentication();  

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
