using Microsoft.AspNetCore.Authentication.Cookies;
using Business_Intelligence_ATMs_Focus.Service.Contrato;
using Business_Intelligence_ATMs_Focus.Models;
using Business_Intelligence_ATMs_Focus.Service.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Users/Login";
        options.Cookie.Name = "CajeroInteligentesCk";
        options.AccessDeniedPath = "/Forbidden/Illegal";
        
    });
//builder.Services.AddSession(options => 
//{
//    options.Cookie.Name = ".CajerosInteligentes.Session";
//    options.IdleTimeout = TimeSpan.FromSeconds(100);
//    options.Cookie.IsEssential = true;
//});

builder.Services.AddScoped<IGenericService<OwnBranchesModel>, SucursalesRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Forbidden/Error");
    app.UseExceptionHandler("/Forbidden/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();


app.MapControllerRoute(
    name: "default",
pattern: "{controller=Users}/{action=Login}/{id?}");
//pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
