using Invitify.Context;
using Invitify.Privilage;
using Invitify.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel.DataAnnotations;

namespace Invitify
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
            services.AddMvc(a => a.EnableEndpointRouting = false);
            services.AddControllers();

       

            services.AddIdentity<ExtendIdentityUser, IdentityRole>(op =>
            {
                op.Password.RequiredLength = 7;
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.User.RequireUniqueEmail = true;
                op.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                op.SignIn.RequireConfirmedEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<DbContainer>();

  

            services.AddDbContextPool<DbContainer>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("con"));
            });

            services.AddHttpContextAccessor();



            services.AddCors(options =>
            {
                options.AddPolicy("allow",
                                    a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            });

            services.AddAuthentication();

            services.AddScoped<IUserRep, UserRep>();
            services.AddScoped<ITimeRep, TimeRep>();
            services.AddScoped<ICountryRep, CountryRep>();
            services.AddScoped<IContactTypeRep, ContactTypeRep>();
            services.AddScoped<IContactRep, ContactRep>();
            services.AddScoped<IEventRep, EventRep>();
            services.AddScoped<ITempEventRep, TempEventRep>();
            services.AddScoped<IInvitationRep, InvitationRep>();
            services.AddScoped<IEmailRep, EmailRep>();
            services.AddScoped<IPropertiesRep, PropertiesRep>();
            services.AddScoped<ILandingRep, LandingRep>();
            services.AddScoped<IEntryOrganizerRep, EntryOrganizerRep>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment en)
        {

            if (en.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHsts();
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(a =>
            {
                a.AllowAnyHeader();
                a.AllowAnyMethod();
                a.AllowAnyOrigin();
            });
            app.UseEndpoints(a =>
            {

                a.MapDefaultControllerRoute();

            });

        }
    }
}
