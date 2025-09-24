using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using Dapper;
using WebAPP.Models.ModelDanhMuc;

[Route("api/HinhThucBoiDuong")]
[ApiController]
public class HinhThucBoiDuongDanhMuc : ControllerBase
{
    private readonly DapperContext _context;

    public HinhThucBoiDuongDanhMuc(DapperContext context)
    {
        _context = context;
    }

    // POST: api/HinhThucBD
    [HttpPost]
    public async Task<IActionResult> PostBoiDuong([FromBody] HinhThucBDDM hinhthucboiduong)
    {
        if (hinhthucboiduong == null || string.IsNullOrEmpty(hinhthucboiduong.HinhThuc) || string.IsNullOrEmpty(hinhthucboiduong.MoTaHTBD))
            return BadRequest("Dữ liệu không hợp lệ");

        hinhthucboiduong.ID_htboiDuong = hinhthucboiduong.ID_htboiDuong == 0 ? 1 : hinhthucboiduong.ID_htboiDuong;

        var query = @"INSERT INTO HinhThucBoiDuong (HinhThuc, MoTaHTDB) 
                      VALUES (@HinhThuc, @MoTaHTDB)";
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, hinhthucboiduong);
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }




    // GET: api/HinhThucBD
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = "SELECT * FROM HinhThucBoiDuong";
        try
        {
            using var connection = _context.CreateConnection();
            var list = await connection.QueryAsync<HinhThucBDDM>(query);
            return Ok(new { success = true, data = list });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }

    //api/delete

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = "DELETE FROM HinhThucBoiDuong WHERE ID_htboiDuong = @Id";
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
    public async Task<IActionResult> PutHinhThucBoiDuong(int id, [FromBody] HinhThucBDDM hinhthucboiduong)
    {
        if (hinhthucboiduong == null || id != hinhthucboiduong.ID_htboiDuong)
            return BadRequest("Dữ liệu không hợp lệ");

        var query = @"UPDATE HinhThucBoiDuong
                      SET HinhThuc = @HinhThuc, MoTaHTBD = @MoTaHTBD
                      WHERE ID_htboiDuong = @ID_htboiDuong";

        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, hinhthucboiduong);
            return Ok(new { success = true, message = "Cập nhật thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }
}
