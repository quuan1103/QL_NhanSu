using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Data;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = new DashboardViewModel
            {
                TotalNhanVien = await _db.LyLich.CountAsync(),
                TotalPhongBan = await _db.PhongBan.CountAsync(),
                TotalBangLuong = await _db.BangLuong.CountAsync(),
                TotalDaoTao = await _db.DaoTao.CountAsync(),
                TongThucLinh = await _db.BangLuong.SumAsync(x => x.ThucLinh),
                NhanVienMoiNhat = await _db.LyLich
                    .OrderByDescending(x => x.NgayVaoLam)
                    .Take(5)
                    .Select(x => new NhanVienMoiNhat
                    {
                        HoTen = x.HoTen,
                        GioiTinh = x.GioiTinh,
                        NgayTao = x.NgayVaoLam
                    })
                    .ToListAsync(),
                ThongKeGioiTinh = await _db.LyLich
                    .GroupBy(x => x.GioiTinh)
                    .Select(g => new ThongKeGioiTinh
                    {
                        GioiTinh = g.Key,
                        SoLuong = g.Count()
                    })
                    .ToListAsync()
            };

            return View(dashboardData);
        }
    }

    public class DashboardViewModel
    {
        public int TotalNhanVien { get; set; }
        public int TotalPhongBan { get; set; }
        public int TotalBangLuong { get; set; }
        public int TotalDaoTao { get; set; }
        public decimal TongThucLinh { get; set; }
        public List<NhanVienMoiNhat> NhanVienMoiNhat { get; set; } = new();
        public List<ThongKeGioiTinh> ThongKeGioiTinh { get; set; } = new();
    }

    public class NhanVienMoiNhat
    {
        public string HoTen { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
    }

    public class ThongKeGioiTinh
    {
        public string GioiTinh { get; set; } = string.Empty;
        public int SoLuong { get; set; }
    }
}
