using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using WebAPP.Data;
using Dapper;
using WebAPP.Models.ModelDanhMuc;

namespace WebAPP.Controllers.DanhMucController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly DapperContext _context;

        public PhongBanController(DapperContext context)
        {
            _context = context;
        }

        // POST: api/PhongBan
        [HttpPost]
        public async Task<IActionResult> PostPhongBan([FromBody] Phongban phongban)
        {
            if (phongban == null || string.IsNullOrEmpty(phongban.Maphongban) || string.IsNullOrEmpty(phongban.Tenphongban) || string.IsNullOrEmpty(phongban.MoTa))
                return BadRequest("Dữ liệu không hợp lệ");

            phongban.ID_trangthai = phongban.ID_trangthai == 0 ? 1 : phongban.ID_trangthai;

            var query = @"INSERT INTO Phongban (Maphongban, Tenphongban, MoTa, ID_trangthai) 
                          VALUES (@Maphongban, @Tenphongban, @MoTa, @ID_trangthai)";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, phongban);
                return Ok(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }

        }
        // GET: api/PhongBan
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT * FROM Phongban";
            using var connection = _context.CreateConnection();
            var list = await connection.QueryAsync<Phongban>(query);
            return Ok(new { success = true, data = list });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = "DELETE FROM Phongban WHERE ID_Phongban = @Id";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return Ok(new { success = true, message = "Xoá thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhongBan(int id, [FromBody] Phongban phongban)
        {
            if (phongban == null || id != phongban.ID_Phongban)
                return BadRequest("Dữ liệu không hợp lệ");

            var query = @"UPDATE Phongban 
                          SET Maphongban = @Maphongban,
                              Tenphongban = @Tenphongban,
                              MoTa = @MoTa,
                              ID_trangthai = @ID_trangthai
                          WHERE ID_Phongban = @ID_Phongban";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, phongban);

                return Ok(new { success = result > 0, message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
    }
}


