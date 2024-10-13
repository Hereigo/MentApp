using System.Collections.Generic;
using System.Data;
using Data.EF;
using Data.EF.Models;
using Data.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.MediatR;
using SQLitePCL;

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
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRDependencyHandler).Assembly));

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


// fail: Microsoft.EntityFrameworkCore.Database.Command[20102]
//       Failed executing DbCommand (4ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?' (DbType = Guid), @p2='?' (DbType = DateTime), @p3='?' (Size = 6), @p4='?' (DbType = Boolean), @p5='?' (Size = 6), @p6='?'], CommandType='Text', CommandTimeout='30']
//       INSERT INTO "Tasks" ("Id", "CategoryId", "CreatedDate", "Description", "IsCompleted", "Title", "UserId")
//       VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6);
// fail: Microsoft.EntityFrameworkCore.Update[10000]
//       An exception occurred in the database while saving changes for context type 'Data.EF.ToDoListDbContext'.
//       Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
//        ---> Microsoft.Data.Sqlite.SqliteException (0x80004005): SQLite Error 19: 'FOREIGN KEY constraint failed'.
//          at Microsoft.Data.Sqlite.SqliteException.ThrowExceptionForRC(Int32 rc, sqlite3 db)