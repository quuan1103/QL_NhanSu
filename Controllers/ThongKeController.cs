using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Data;

namespace WebAPP.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ThongKeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var thongKeData = new ThongKeViewModel
            {
                TotalNhanVien = await _db.LyLich.CountAsync(),
                TotalPhongBan = await _db.PhongBan.CountAsync(),
                TotalBangLuong = await _db.BangLuong.CountAsync(),
                TotalDaoTao = await _db.DaoTao.CountAsync(),
                TongThucLinh = await _db.BangLuong.SumAsync(x => x.ThucLinh)
            };

            return View(thongKeData);
        }
    }

    public class ThongKeViewModel
    {
        public int TotalNhanVien { get; set; }
        public int TotalPhongBan { get; set; }
        public int TotalBangLuong { get; set; }
        public int TotalDaoTao { get; set; }
        public decimal TongThucLinh { get; set; }
    }
}
