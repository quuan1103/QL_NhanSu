using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPP.Data;
using WebAPP.Models.PhanQuyen;
using Dapper;
using WebAPP.Models.HoSo;

namespace WebAPP.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhanQuyenApiController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly IDbConnection _connection;

        public PhanQuyenApiController(DapperContext context)
        {
            _context = context;
            _connection = _context.CreateConnection();
        }

        // GET: api/PhanQuyenApi/NhanVien
        [HttpGet("NhanVien")]
        public async Task<IActionResult> GetNhanVien()
        {
            var result = await _connection.QueryAsync<LyLich>("SELECT * FROM LyLich");
            return Ok(result);
        }

        // POST: api/PhanQuyenApi/CapNhatQuyenNhanVien
        [HttpPost("CapNhatQuyenNhanVien")]
        public async Task<IActionResult> CapNhatQuyenNhanVien([FromBody] PhanQuyenNhanVienModel model)
        {
            var sql = "INSERT INTO TaiKhoanNhanVien (ID_NV, ID_Quyen) VALUES (@id_nv, @id_quyen)";
            await _connection.ExecuteAsync(sql, new { model.id_nv, model.id_quyen });
            return Ok(new { success = true });
        }
    }

    public class PhanQuyenNhanVienModel
    {
        public Guid id_nv { get; set; }
        public int id_quyen { get; set; }
    }
}