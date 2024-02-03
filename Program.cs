
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using StockTracking.Data;
using StockTracking.Config.Extensions;

namespace StockTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
     
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization Header - utilizado com Bearer Authentication.
                        Digite 'Bearer' [espaço] e então seu token no campo abaixo.
                        Exemplo (informar sem as aspas): 'Bearer 12345abcdef'
                    ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


            builder.Services.AddDbContext<DataContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>();


            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddSectionsExtension(builder.Configuration);

            builder.Services.AddServicesExtension();

            builder.Services.AddRepositoriesExtension();

            builder.Services.AddAuthenticationExtension(builder.Configuration);
           
            builder.Services.Configure<IdentityOptions>(opt =>
            {
                // Password Settings
                opt.Password.RequireDigit = false;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}