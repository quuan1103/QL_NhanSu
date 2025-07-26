using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using WebAPP.Data;

var builder = WebApplication.CreateBuilder(args);


// Dapper
builder.Services.AddSingleton<DapperContext>();

// EF DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller + Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Session
builder.Services.AddSession();


builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyAppAuth";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

// BUILD APP
var app = builder.Build();

// MIDDLEWARE PIPELINE

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
    RequestPath = "/Scripts"
});

app.UseRouting();

// Session và Auth phải nằm đúng thứ tự
app.UseSession();          // Session cho thông tin người dùng
app.UseAuthentication();   // Xác thực
app.UseAuthorization();    // Phân quyền

// Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=indexDangnhap}/{id?}");

app.MapRazorPages();

// app.MapFallbackToController("ReactApp", "Home"); // nếu dùng React thì mở lại

// ✅ 4. CHẠY ỨNG DỤNG
app.Run();
