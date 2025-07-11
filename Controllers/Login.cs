using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class Login : Controller
    {
        private readonly DapperContext _context;

        public Login(DapperContext context)
        {
            _context = context;
        }

        // GET: Trang đăng nhập
        public IActionResult indexDangnhap()
        {
            return View("~/Views/Home/Login/indexDangnhap.cshtml");
        }

        // GET: Trang đăng ký
        public IActionResult Dangky()
        {
            return View("~/Views/Home/Login/dangky.cshtml");
        }

        // POST: Xử lý đăng nhập
        [HttpPost]
      
        public IActionResult indexDangnhap(string TenDangNhap, string password)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "SELECT * FROM TaiKhoanNhanVien WHERE TenDangNhap = @TenDangNhap";
                var user = connection.QueryFirstOrDefault<TaiKhoanNhanVien>(sql, new { TenDangNhap });

                if (user != null)
                {
                    var hashedInput = HashPassword(password, user.Salt);
                    if (user.PasswordHash.SequenceEqual(hashedInput))
                    {
                        // Đăng nhập thành công
                        return View("~/Views/Home/Web/LyLich/Soyeu.cshtml");
                    }
                }

                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                return View("~/Views/Home/Login/indexDangnhap.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Dangky(TaiKhoanNhanVien model)
        {
            if (ModelState.IsValid)
            {
                using (var connection = _context.CreateConnection())
                {
                    var checkExistSql = "SELECT COUNT(*) FROM TaiKhoanNhanVien WHERE TenDangNhap = @TenDangNhap";
                    var exist = connection.ExecuteScalar<int>(checkExistSql, new { model.TenDangNhap }) > 0;

                    if (exist)
                    {
                        ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                        return View("~/Views/Home/Login/dangky.cshtml", model);
                    }

                    if (model.Password != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Mật khẩu không khớp.");
                        return View("~/Views/Home/Login/dangky.cshtml", model);
                    }

                    var salt = GenerateSalt();
                    var hash = HashPassword(model.Password, salt);

                    model.Salt = salt;
                    model.PasswordHash = hash;
                    model.NgayTao = DateTime.Now;

                    var insertSql = @"
                INSERT INTO TaiKhoanNhanVien (TenDangNhap, PasswordHash, Salt, NgayTao)
                VALUES (@TenDangNhap, @PasswordHash, @Salt, @NgayTao)
            ";

                    try
                    {
                        connection.Execute(insertSql, model);
                        TempData["SuccessMessage"] = "Đăng ký thành công!";
                        return RedirectToAction("indexDangnhap");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Lỗi khi lưu: " + ex.Message);
                    }
                }
            }

            return View("~/Views/Home/Login/dangky.cshtml", model);
        }

        // Tạo salt ngẫu nhiên
        private byte[] GenerateSalt()
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return hmac.Key;
            }
        }

        // Hash password với salt
        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var combinedBytes = System.Text.Encoding.UTF8.GetBytes(password);
                return sha256.ComputeHash(combinedBytes);
            }
        }
    }
}
