using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Server.Models;
using Majorsoft.Blazor.Components.Maps;
using WinchHuntApp.Server.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using WinchHuntApp.Server.Email;
using Microsoft.AspNetCore.Identity;
using WinchHuntApp.Server.Models.Db.Accounts;
using WinchHuntApp.Server.Services.Implementation;

namespace WinchHuntApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WinchHuntDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<InMemoryDbContext>(options => 
                options.UseInMemoryDatabase(databaseName: "WhInMemoryDb"));

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Enable identity roles

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WinchHuntDbContext>();

            // Configure identity server to put the role claim into the id token
            // and the access token and prevent the default mapping for roles 
            // in the JwtSecurityTokenHandler.
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, WinchHuntDbContext>(options => {
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
                });

            // Need to do this as it maps "role" to ClaimTypes.Role and causes issues
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
                .DefaultInboundClaimTypeMap.Remove("role");

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddMapExtensions();


            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IFoxService, FoxService>();
            services.AddScoped<IUplinkAccessService, UplinkAccessService>();
            services.AddScoped<IHunterService, HunterService>();
            services.AddScoped<IUplinkService, UplinkService>();
            services.AddScoped<IAccountService, AccountService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WinchHuntDbContext dataContext)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });


            //CreateDevUser(app);
        }

        private static void CreateDevUser(IApplicationBuilder app)
        {
            // Create a temporary test user
            var scope = app.ApplicationServices.CreateScope();
            WinchHuntDbContext db = scope.ServiceProvider.GetService<WinchHuntDbContext>();

            string email = "";

            if (db.Users.Where(u => u.Id == "1e088930-bc68-421f-9528-7fd60fceef55").Count() < 1)
            {
                ApplicationUser devUser = new ApplicationUser()
                {
                    Id = "1e088930-bc68-421f-9528-7fd60fceef55",
                    UserName = email,
                    Email = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    LockoutEnabled = false,
                    PhoneNumber = "123",
                    EmailConfirmed = true,
                };

                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                devUser.PasswordHash = passwordHasher.HashPassword(devUser, "123Dev!");

                db.Users.Add(devUser);
                db.SaveChanges();
            }
        }
    }
}
