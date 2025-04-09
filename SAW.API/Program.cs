using Microsoft.EntityFrameworkCore;
using SAW.API.Extentions;
using Serilog;
using SWA.Core.Common;
using SWA.Core.Models;
using SWA.Core.Models.SwccShared;
using SWA.Core.Repository;
using SWA.Core.Service;
using SWA.Core.Services;
using SWA.Infra.Repository;
using SWA.Infra.Services;


var builder = WebApplication.CreateBuilder(args);

// Logging setup
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
      .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAuthenticationSettings(builder.Configuration);
// Register your EF DbContext
builder.Services.AddDbContext<HajjDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HajjDbConnection")));



builder.Services.AddScoped<ISWCCSharedDbContext>(provider => provider.GetService<SWCCSharedDbContext>());

// Register DI services
builder.Services.AddScoped<IDbContext, SWA.Infra.Common.DbContext>();
builder.Services.AddScoped<IPermitRepository, PermitRepository>();
builder.Services.AddScoped<IPermitService, PermitService>();
builder.Services.AddScoped<ICancelPermitRepository, CancelPermitRepository>();
builder.Services.AddScoped<ICancelPermitService, CancelPermitService>();
builder.Services.AddScoped<INICPermitService, NICPermitService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<JWTModel>(builder.Configuration.GetSection("JWTValidator"));
builder.Services.AddSingleton<AppSettings, AppSettings>();
builder.Services.AddSingleton<JWTModel, JWTModel>();



builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");



app.Use(async (context, next) =>
{
    //context.Response.Headers.Add("X-Developed-By", "Hany Mahmoud");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
    context.Response.Headers.Add("Content-Security-Policy", "default-src * 'unsafe-inline' 'unsafe-eval'; script-src * 'unsafe-inline' 'unsafe-eval'; connect-src * 'unsafe-inline'; img-src * data: blob: 'unsafe-inline'; frame-src *; style-src * 'unsafe-inline';");
    context.Response.Headers.Remove("X-AspNet-Version");
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Remove("Server");

    await next.Invoke();
});


app.MapControllers();


app.Run();