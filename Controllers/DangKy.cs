using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAPP.Data;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class DangKy : Controller
    {
        private readonly ApplicationDbContext _context;
        public DangKy(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
       
            public IActionResult dangky(TaiKhoanNhanVien model, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (_context.TaiKhoanNhanVien.Any(x => x.TenDangNhap == model.TenDangNhap))
                {
                    ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                    return View("~/Views/Home/Login/dangky.cshtml", model);
                }

                if (model.Password != confirmPassword)
                {
                    ModelState.AddModelError("Password", "Mật khẩu không khớp.");
                    return View("~/Views/Home/Login/dangky.cshtml", model);
                }

                try
                {
                    // Băm mật khẩu ở đây nếu cần
                    model.PasswordHash = Encoding.UTF8.GetBytes(model.Password);

                    _context.TaiKhoanNhanVien.Add(model);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                    return RedirectToAction("indexDangnhap", "Login");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm dữ liệu: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }

            return View("~/Views/Home/Login/dangky.cshtml", model);
        }

    }
}
    


