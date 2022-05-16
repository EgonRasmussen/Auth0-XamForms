using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            options.Authority = $"https://{Configuration["Auth0:Authority"]}";
            options.Audience = Configuration["Auth0:Audience"];
        });

        services.AddAuthorization(o =>
        {
            o.AddPolicy("ReadPolicy", p => p.RequireAuthenticatedUser().RequireClaim("permissions", "read:weatherforecast"));
        });

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
