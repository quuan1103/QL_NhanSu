using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using WebAPP.Models.ModelDanhMuc;
using Dapper;

namespace WebAPP.Controllers.DanhMucController
{
    [Route("api/TrinhDo")]
    [ApiController]
    public class TrinhDoDanhMuc : ControllerBase
    {
        private readonly DapperContext _context;
        public TrinhDoDanhMuc(DapperContext context)
        {
            _context = context;
        }
       
        // POST: api/TrinhDo
        [HttpPost]
        public async Task<IActionResult> PostTrinhDo([FromBody] TrinhDoDM trinhdo)
        {
            if (trinhdo == null || string.IsNullOrEmpty(trinhdo.TenTrinhDo) || string.IsNullOrEmpty(trinhdo.MoTaTD))
                return BadRequest("Dữ liệu không hợp lệ");
            // trinhdo.ID_TrangThai = trinhdo.ID_TrangThai == 0 ? 1 : trinhdo.ID_TrangThai;
            var query = @"INSERT INTO TrinhDo (TenTrinhDo, MoTaTD) 
                          VALUES (@TenTrinhDo, @MoTaTD)";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, trinhdo);
                return Ok(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // GET: api/TrinhDo
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT * FROM TrinhDo";
            try
            {
                using var connection = _context.CreateConnection();
                var list = await connection.QueryAsync<TrinhDoDM>(query);
                return Ok(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // DELETE: api/TrinhDo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = "DELETE FROM TrinhDo WHERE ID_TrinhDo = @Id";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, new { Id = id });
                if (result == 0)
                    return NotFound();
                return Ok(new { success = true, message = "Xoá thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // PUT: api/TrinhDo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrinhDoDM trinhdo)
        {
            if (trinhdo == null || string.IsNullOrEmpty(trinhdo.TenTrinhDo) || string.IsNullOrEmpty(trinhdo.MoTaTD))
                return BadRequest("Dữ liệu không hợp lệ");
            var query = @"UPDATE TrinhDo SET TenTrinhDo = @TenTrinhDo, MoTaTD = @MoTaTD WHERE ID_TrinhDo = @ID_TrinhDo";
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.ExecuteAsync(query, trinhdo);
                return Ok(new { success = true, message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
    }
}


