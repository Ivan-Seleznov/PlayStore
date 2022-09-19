using PlayStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductRepository, EFProduct>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.EnableRetryOnFailure()));
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cart>(s => SessionCart.GetCart(s));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));
builder.Services.AddIdentity <IdentityUser,IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseRouting();
app.UseStatusCodePages();
app.UseAuthorization();
app.MapControllerRoute(name: null,
    pattern: "{dategory}/Page{productPage:int}",
    defaults: new { controller = "Product", action = "List" });   
   
app.MapControllerRoute(name: null,
    defaults: new {controller = "Product", Action = "List" },
    pattern: "{category}/Page{productPage:int}");
app.MapControllerRoute(
    name: null,
    pattern: "Page{productPage:int}", 
    defaults: new { controller = "Product", Action = "List", productPage = 1 });
app.MapControllerRoute(
    name: null,
    pattern: "{category}",
    defaults: new { controller = "Product", Action = "List", productPage = 1 });
app.MapControllerRoute(
    name: null,
    pattern: "",
    defaults: new { controller = "Product", Action = "List", productPage = 1 });
app.MapControllerRoute(
    name: null,
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Product", Action = "List", productPage = 1 });

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();

