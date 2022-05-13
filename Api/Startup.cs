using Api.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = Configuration["Auth0:Authority"];
            options.Audience = Configuration["Auth0:Audience"];
        });

        // A scope is converted to a policy and can then be used in Controller as [Authorize("read:weatherforecast")]
        services.AddAuthorization(options =>
        {
            options.AddPolicy("read:weatherforecast", policy =>
                policy.Requirements.Add(new HasScopeRequirement("read:weatherforecast", Configuration["Auth0:Authority"])));
        });

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();     // Added!

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
