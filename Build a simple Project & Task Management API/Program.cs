using BL.AutoMapper;
using BL.Managers.ApplicationUserManager;
using BL.Managers.ProjectItemManager;
using BL.Managers.TasksItemManager;
using BL.Validation.ProjectItemValidation;
using BL.Validation.TasksItemVaidation;
using BL.Validation.UserValidation;
using Build_a_simple_Project___Task_Management_API_.Filter;
using DAL.Data.Context;
using DAL.Data.Models;
using DAL.GenericRepositories.GenericRepositories;
using DAL.GenericRepositories.ProjectItemRepositories;
using DAL.GenericRepositories.TasksItemRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region DataBase
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("con"))

);
#endregion

#region Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
{
    Options.Password.RequireUppercase = false;
    Options.Password.RequiredLength = 5;

}).AddEntityFrameworkStores<AppDbContext>();

#endregion


#region Add Services
builder.Services.AddScoped<IProjectItemRepositories , ProjectItemRepositories>();
builder.Services.AddScoped<IProjectItemManager, ProjectItemManager>();

builder.Services.AddScoped<ITasksItemRepositories, TasksItemRepositories>();
builder.Services.AddScoped<ITasksItemManager, TasksItemManager>();

builder.Services.AddScoped<IApplicationUserManager, ApplicationUserManager>();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

#endregion

#region Validation

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProjectDTOValidator>());

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateProjectDTOValidator>());


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateTaskDTOValidator>());

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateTaskDTOValidator>());


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginDTOValidator>());

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterDTOValidator>());

#endregion

#region Authentication
builder.Services.AddAuthentication(Options =>
{//to Read Token 
    Options.DefaultAuthenticateScheme = "Default";
    Options.DefaultChallengeScheme = "Default";
})
    .AddJwtBearer("Default", Options =>
    {
        var KeyString = builder.Configuration.GetValue<string>("SecretKey");
        var KeyBytes = Encoding.ASCII.GetBytes(KeyString);
        var Key = new SymmetricSecurityKey(KeyBytes);

        Options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = Key,
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
#endregion

#region Authorization
builder.Services.AddAuthorization(Options =>
{
    Options.AddPolicy("ManagerPolicy", p => p.RequireClaim(ClaimTypes.Role, "Manager"));
    Options.AddPolicy("EmployeePolicy", p => p.RequireClaim(ClaimTypes.Role, "Employee"));
    Options.AddPolicy("Both", p => p.RequireClaim(ClaimTypes.Role, "Manager", "Employee"));
});


#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
