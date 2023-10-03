using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Auth/Login";
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;  // Şifrede rakam kullanma zorunlu
    options.Password.RequireLowercase = true; // Şifredeki harfler hepsi küçük harf olmak zorunluluğu
    options.Password.RequireNonAlphanumeric = false; // Şifrede sembollerin kullanılması zorunluluğu
    options.Password.RequireUppercase = false; // Şifrede büyük harf kullanma zorunluluğu
    options.Password.RequiredLength = 8; // Şifrenin minimum uzunluğu karakter sayı
    options.Lockout.MaxFailedAccessAttempts = 5; // Login olunca yapa biliceğin hatalara karşı verilen şans, yeniden deneme hakkı
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Yukardaki limit kadar hata yaptıkdan sonra kullanıcıya vericeği ban süresi
    options.User.RequireUniqueEmail = false; // Doğrulanmış Email olma zorunluluğu NOT: Yazmasan bile false olarak geliyor
});


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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
