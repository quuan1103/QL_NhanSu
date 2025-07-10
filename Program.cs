using Microsoft.Extensions.FileProviders;
using WebAPP.Data;

var builder = WebApplication.CreateBuilder(args);

// Dapper
builder.Services.AddSingleton<DapperContext>();

// Controller + Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=indexDangnhap}/{id?}");

app.MapRazorPages();


// Route fallback về React nếu không khớp route API/MVC
app.MapFallbackToController("ReactApp", "Home");

app.Run();
