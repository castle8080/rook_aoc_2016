using System.Data.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using rook_aoc_2016.Db;
using rook_aoc_2016.Problems;
using rook_aoc_2016.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ProblemInputService>();
builder.Services.AddTransient<ProblemService>();
builder.Services.AddTransient<IProblem, Problem1>();
builder.Services.AddTransient<IProblem, Problem2>();
builder.Services.AddTransient<IProblem, Problem3>();

// Setup database connection/EF

builder.Services.AddKeyedSingleton<string>("DBConnectionString", (sp, _) => {
    var config = sp.GetRequiredService<IConfiguration>();

    var dbConnStringBuilder = new NpgsqlConnectionStringBuilder(config["Db:ConnectionString"]);
    dbConnStringBuilder.Password = config["Db:Password"];
    
    return dbConnStringBuilder.ConnectionString;
});

builder.Services.AddDbContextFactory<ResultsContext>((sp, opts) => {
    opts.UseNpgsql(sp.GetRequiredKeyedService<string>("DBConnectionString"));
});

// Repositories

builder.Services.AddSingleton<IProblemResultRepository, ProblemResultRepositoryEF>();

// Add controllers
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
