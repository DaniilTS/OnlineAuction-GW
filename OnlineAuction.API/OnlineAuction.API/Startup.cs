using OnlineAuction.API.Extensions;
using OnlineAuction.DBAL.Context;
using OnlineAuction.JWT.Auth.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace OnlineAuction.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(setupAction =>
            {
                var options = new OpenApiInfo()
                {
                    Title = "Online Auction API",
                    Description = "V 1"
                };
                setupAction.SwaggerDoc("v" + 1, options);
                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "JWT with Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },new List<string>()
                }});
                setupAction.CustomSchemaIds(classType => classType.FullName);
            });

            services.AddDbContext<OnlineAuctionContext>();

            services.AddJwtBearerAuth(Configuration);
            services.AddRepositories();
            services.AddOperations();
            services.AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Auction API v1"));
            }

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyHeader()
                                          .AllowAnyOrigin()
                                          .AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
