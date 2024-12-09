using Microsoft.OpenApi.Models;

namespace OrderManagementApi.Extensions
{
    public static class SwaggerServiceExtentions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
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

            return services;
        }
    }
}
