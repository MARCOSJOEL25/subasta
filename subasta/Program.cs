using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using subasta.Context;
using core.Interface;
using Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);


//string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("subasta"));
}); //context/>


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//automapper

// Add services to the container.
builder.Services.AddScoped<IRepoUser, RepoUser>();
builder.Services.AddScoped<IRepoProduct, RepoProduct>();

//Auntenticacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                System.Text.Encoding.ASCII.GetBytes(
                                    builder.Configuration.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
//Auntenticacion/>

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SecurityRequirementsOperationFilter>();

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Autorizacion Standar, Usar Bearer. Ejemplo \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIClientes", Version = "v1" });
});
//cors


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(x => x.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
