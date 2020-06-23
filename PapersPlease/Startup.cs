using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PapersPlease.Security;

namespace PapersPlease
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication("Home")
        .AddScheme<AuthenticationOptions, CustomBearerAuthenticationHandler>("Home", null);

      services.AddScoped<IAuthorizationHandler, AgeRequirementAuthorizationHandler>();

      services.AddAuthorization(config =>
      {
        config.AddPolicy("Over21", builder =>
        {
          builder.AddRequirements(new AgeRequirement(21));
        });

        config.AddPolicy("Over25", builder =>
        {
          builder.AddRequirements(new AgeRequirement(25));
        });
      });

      services.AddControllers();

      services.AddScoped<CustomBearerAuthenticationHandler>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseCors(policy =>
      {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
      });

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
