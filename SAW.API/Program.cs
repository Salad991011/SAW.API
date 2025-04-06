using Microsoft.EntityFrameworkCore;
using Serilog;
using SWA.Core.Common;
using SWA.Core.Models;
using SWA.Core.Repository;
using SWA.Core.Service;
using SWA.Core.Services;
using SWA.Infra.Common;
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

// Register your EF DbContext
builder.Services.AddDbContext<HajjDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HajjDbConnection")));

// Register DI services
builder.Services.AddScoped<IDbContext, SWA.Infra.Common.DbContext>();
builder.Services.AddScoped<IPermitRepository, PermitRepository>();
builder.Services.AddScoped<IPermitService, PermitService>();
builder.Services.AddScoped<ICancelPermitRepository, CancelPermitRepository>();
builder.Services.AddScoped<ICancelPermitService, CancelPermitService>();
builder.Services.AddScoped<INICPermitService, NICPermitService>();



builder.Services.AddHttpClient();

var app = builder.Build();

// Swagger in Development only
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
