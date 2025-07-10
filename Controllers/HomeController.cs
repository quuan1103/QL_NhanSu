using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebAPP.Data;
using WebAPP.Models;
using System.Linq;

namespace WebAPP.Controllers
{
    public class HomeController : Controller
    {
        private const string ViewName = "~/Views/Shared/WebGD.cshtml";
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult indexDangnhap()
        //{
        //    return View("~/Views/Home/Login/indexDangnhap.cshtml");
        //}
        //public IActionResult Dangky()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult indexDangnhap(string username, string password)
        //{
        //    var user = _context.TaiKhoan
        //        .FirstOrDefault(u => u.Username == username && u.MatKhau == password);
        //    Console.WriteLine(user);
        //    if (user != null)
        //    {
            
        //        return View("~/Views/Home/Web/Web.cshtml");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác!";
        //        return View(); 
        //    }
        //}
        // Trang privacy
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult WebGD()
        {
            return View(ViewName);
        }
        //public IActionResult Soyeu()
        //{
        //    return View("~/Views/Home/Web/Soyeu.cshtml");
        //}
        public IActionResult ReactApp()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "react", "index.html");
            return PhysicalFile(filePath, "text/html");
        }

    }
}
