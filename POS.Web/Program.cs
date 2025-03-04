using Microsoft.EntityFrameworkCore;
using POS.Application.Common.Interfaces;
using POS.Application.Services.Implementation;
using POS.Application.Services.Interfaces;
using POS.Infrastructure.Data;
using POS.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductBrandServices, ProductBrandService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IProductBrandTypeService, ProductBrandTypeService>();
builder.Services.AddScoped<IProductPackagingService, ProductPackagingService>();
builder.Services.AddScoped<IInventoryProductService, InventoryProductService>();
builder.Services.AddScoped<IProductRenderedService, ProductRenderedService>();
builder.Services.AddScoped<IProductDiscountTypeService, ProductDiscountTypeService>();
builder.Services.AddScoped<IProductRenderedDiscountService, ProductRenderedDiscountService>();


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

app.Run();
