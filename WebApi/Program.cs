using Data.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ToDoListDb"));
});

builder.Services.AddAuthorization();

// By default, both cookies and proprietary tokens are activated.
// Cookies and tokens are issued at login if the useCookies query string parameter in the login endpoint is true.
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ToDoListDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapSwagger().RequireAuthorization();
// app.MapSwagger().RequireAuthorization("Admin");

app.MapGet("/hello", (HttpContext httpContext) =>
{
    return "Hello people!";
})
.WithName("GetHello")
.WithOpenApi()
.RequireAuthorization();

app.Run();
