using Data.EF;
using Data.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

const string BEARER = "Bearer";

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddAuthorization(); // Add Identity services
builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ToDoListDb"));
});

// By default, both cookies and proprietary tokens are activated.
// Cookies and tokens are issued at login if the useCookies query string parameter in the login endpoint is true.
builder.Services
  .AddIdentityApiEndpoints<User>() // PreConfigured Roles
//  .AddIdentityCore<User>()         // More configurable
    .AddEntityFrameworkStores<ToDoListDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>(); // Map Identity routes
// app.MapGroup("/identity").MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapSwagger().RequireAuthorization();

app.Run();
