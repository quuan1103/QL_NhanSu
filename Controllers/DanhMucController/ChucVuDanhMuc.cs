using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using Dapper;
using WebAPP.Models.ModelDanhMuc;

[Route("api/[controller]")]
[ApiController]
public class ChucVuController : ControllerBase
{
    private readonly DapperContext _context;

    public ChucVuController(DapperContext context)
    {
        _context = context;
    }

   // POST: api/ChucVu
   [HttpPost]
    public async Task<IActionResult> PostChucVu([FromBody] ChucVuDM chucvu)
    {
        if (chucvu == null || string.IsNullOrEmpty(chucvu.TenChucVu) || string.IsNullOrEmpty(chucvu.MotaCV))
            return BadRequest("Dữ liệu không hợp lệ");

        chucvu.ID_TrangThai = chucvu.ID_TrangThai == 0 ? 1 : chucvu.ID_TrangThai;

        var query = @"INSERT INTO ChucVu (TenChucVu, MotaCV, ID_TrangThai) 
                      VALUES (@TenChucVu, @MotaCV, @ID_TrangThai)";
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, chucvu);
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }




    // GET: api/ChucVu
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = "SELECT * FROM ChucVu";
        try
        {
            using var connection = _context.CreateConnection();
            var list = await connection.QueryAsync<ChucVuDM>(query);
            return Ok(new { success = true, data = list });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = "DELETE FROM ChucVu WHERE ID_ChucVu = @Id";
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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutChucVu(int id, [FromBody] ChucVuDM chucvu)
    {
        if (chucvu == null || id != chucvu.ID_ChucVu)
            return BadRequest("Dữ liệu không hợp lệ");

        var query = @"UPDATE ChucVu 
                      SET TenChucVu = @TenChucVu, MoTaCV = @MotaCV, ID_TrangThai = @ID_TrangThai 
                      WHERE ID_ChucVu = @ID_ChucVu";

        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, chucvu);
            return Ok(new { success = true, message = "Cập nhật thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }
}
