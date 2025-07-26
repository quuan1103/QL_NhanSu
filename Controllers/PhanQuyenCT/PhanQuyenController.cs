using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPP.Data;
using WebAPP.Models.HoSo;
using Dapper;
using WebAPP.Models.PhanQuyen;

namespace WebAPP.Controllers
{
    public class PhanQuyenController : Controller
    {
        private readonly DapperContext _context;
        private readonly IDbConnection _connection;

        public PhanQuyenController(DapperContext context)
        {
            _context = context;
            _connection = _context.CreateConnection();
        }

        // Hiển thị View Phân quyền
        public async Task<IActionResult> DanhSachVaiTro()
        {
            var nhanViens = await _connection.QueryAsync<VaiTro>("SELECT * FROM VaiTro");
            return View(nhanViens);
        }
    }
}