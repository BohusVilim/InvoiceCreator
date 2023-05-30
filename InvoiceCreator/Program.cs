using InvoiceCreator.Controllers;
using InvoiceCreator.Helpers;
using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;
using InvoiceCreator.Models.ViewModels;
using InvoiceCreator.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InvoiceCreatorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceCreatorDbContext") ?? throw new InvalidOperationException("Connection string 'InvoiceCreatorDbContext' not found.")));

builder.Services.AddDbContext<InvoiceCreatorInMemoryDbContext>(options =>
    options.UseInMemoryDatabase("InvoiceCreatorInMemory"));

builder.Services.AddScoped<HomeController>();
/*builder.Services.AddScoped<Supplier>();
builder.Services.AddScoped<PaymentData>();
builder.Services.AddScoped<Costumer>();
builder.Services.AddScoped<Service>();
builder.Services.AddScoped<List<Service>, List<Service>>();
builder.Services.AddScoped<Invoice>();*/
builder.Services.AddScoped<InvoicesController>();
builder.Services.AddScoped<SuppliersController>();
builder.Services.AddScoped<CostumersController>();
builder.Services.AddScoped<PaymentDatasController>();
builder.Services.AddScoped<ServicesController>(); 
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<Helpers>(); 
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<InvoicePatternController>();
builder.Services.AddScoped<InvoicePdfService>();



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
var context = scope.ServiceProvider.GetRequiredService<InvoiceCreatorDbContext>();
context.Database.EnsureCreated();

IWebHostEnvironment env = app.Environment;
RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env, @"C:\Users\bohus\OneDrive\Poèítaè\Praca\InvoiceCreator\InvoiceCreator\InvoiceCreator\wwwroot\Rotativa\wkhtmltopdf\bin");

app.Run();
