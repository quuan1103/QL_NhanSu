using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using Dapper;
using WebAPP.Models.ModelDanhMuc;

namespace WebAPP.Controllers.DanhMucController
{
    [Route("api/LoaiHopDong")]
    [ApiController]
    public class LoaiHopDongLaoDongDM : ControllerBase
    {
        private readonly DapperContext _context;

        public LoaiHopDongLaoDongDM(DapperContext context)
        {
            _context = context;
        }
        // POST: api/LoaiHopDongLaoDong
        [HttpPost]
        public async Task<IActionResult> PostLoaiHopDongLaoDong([FromBody] LoaiHopDong loaihopdong)
        {
            if (loaihopdong == null || string.IsNullOrEmpty(loaihopdong.TenLoaiHopDong))
                return BadRequest("Dữ liệu không hợp lệ");
            var query = @"INSERT INTO LoaiHopDong (TenLoaiHopDong, MoTaLHD) 
                          VALUES (@TenLoaiHopDong, @MoTaLHD)";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, loaihopdong);
                return Ok(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // GET: api/LoaiHopDongLaoDong
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT * FROM LoaiHopDong";
            try
            {
                using var connection = _context.CreateConnection();
                var list = await connection.QueryAsync<LoaiHopDong>(query);
                return Ok(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // DELETE: api/LoaiHopDongLaoDong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = "DELETE FROM LoaiHopDong WHERE ID_LoaiHopDong = @Id";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return Ok(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // PUT: api/LoaiHopDongLaoDong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoaiHopDong loaihopdong)
        {
            if (loaihopdong == null || string.IsNullOrEmpty(loaihopdong.TenLoaiHopDong))
                return BadRequest("Dữ liệu không hợp lệ");
            var query = @"UPDATE LoaiHopDong 
                          SET TenLoaiHopDong = @TenLoaiHopDong, MoTaLHD = @MoTaLHD 
                          WHERE ID_LoaiHopDong = @Id";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, new { loaihopdong.TenLoaiHopDong, loaihopdong.MoTaLHD, Id = id });
                return Ok(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
    }
}
