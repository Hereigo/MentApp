using Data.EF.Database;
using Data.EF.Models;
using Data.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.MediatR;
using WebApi.Middlewares;

const string BEARER = "Bearer";

var builder = WebApplication.CreateBuilder(args);

// By default, both cookies and proprietary tokens are activated.
// Cookies and tokens are issued at login if the useCookies query string parameter in the login endpoint is true.

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(BEARER, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = BEARER
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = BEARER
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();        // Add Identity services
builder.Services.AddDbContext<ToDoListDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ToDoListDb")));
builder.Services.AddControllers();
builder.Services.AddIdentityApiEndpoints<User>(); // Include PreConfigured Roles
builder.Services.AddIdentityCore<User>(options =>
{
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ToDoListDbContext>()
.AddApiEndpoints();

builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRDependencyHandler).Assembly));

builder.Services.AddSingleton<StatsService>(); // Stats Counter.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseEndpoints(endpoints => endpoints.MapControllers());

//app.MapIdentityApi<User>(); // Map Identity routes
app.MapGroup("/auth").MapIdentityApi<User>();
app.MapControllers();
app.MapSwagger().RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
