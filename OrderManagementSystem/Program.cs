using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderManagementApi.Extensions;
using OrderManagementData.Context;
using OrderManagementData.Entities.Identity;
using OrderManagementRepository.Interfaces;
using OrderManagementRepository.Repositories;
using OrderManagementServices.Dto.EmailSending;
using OrderManagementServices.Services.EmailServices;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OrderManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();

            builder.Services.AddControllers();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.WriteIndented = true;
            //});

            //builder.Services.AddDbContext<OrderManagementDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            builder.Services.AddDbContext<OrderManagementDbContext>(options =>
            {
                options.UseInMemoryDatabase("OrderManagementDb");
            });



            builder.Services.AddIdentityCore<User>()
                    .AddEntityFrameworkStores<OrderManagementDbContext>()
                    .AddSignInManager<SignInManager<User>>();

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddSingleton<EmailServices>();

            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<OrderManagementDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]))
                };
            });
            builder.Services.AddOrderServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            /*
            builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("V1", new OpenApiInfo { Title = "OrderManagementSystem", Version = "v1" });

                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Description = "Authorization : Bearer {token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    };


                    options.AddSecurityDefinition("Bearer", securityScheme);

                    var securityRequirements = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] {"Bearer"} }
                };
                    options.AddSecurityRequirement(securityRequirements);

                });
            */

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OrderManagementSystem",
                    Version = "v1",
                    Description = "API documentation for Order Management System"
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Your Name",
                    //    Email = "your.email@example.com",
                    //    Url = new Uri("https://example.com")
                    //}
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };

                options.AddSecurityRequirement(securityRequirements);
            });
            //builder.Services.AddSwaggerDocumentation();

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
