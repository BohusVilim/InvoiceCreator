using InvoiceCreator.Controllers;
using InvoiceCreator.Helpers;
using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;
using InvoiceCreator.Models.ViewModels;
using InvoiceCreator.Services;
using InvoiceCreator.Models.EmailModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using static System.Formats.Asn1.AsnWriter;
using KodimWeby.InternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InvoiceCreatorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceCreatorDbContext") ?? throw new InvalidOperationException("Connection string 'InvoiceCreatorDbContext' not found.")));

EmailService.ConfigureEmail(builder.Services);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<Invoice>();
builder.Services.AddScoped<HomeController>();
builder.Services.AddScoped<InvoicesController>();
builder.Services.AddScoped<Helpers>(); 
builder.Services.AddScoped<InvoicePatternController>();
builder.Services.AddScoped<InvoicePdfService>();
builder.Services.AddScoped<SearchingService>();
builder.Services.AddScoped<Searching>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<InvoiceCreator.InvoiceCreatorDbContext.InvoiceCreatorDbContext>();
context.Database.EnsureCreated();

IWebHostEnvironment env = app.Environment;
string fullPath = Path.Combine(env.ContentRootPath, @"wwwroot\Rotativa\wkhtmltopdf\bin");
RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env, fullPath);
app.Run();
