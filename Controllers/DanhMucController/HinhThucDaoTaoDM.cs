using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using Dapper;
using WebAPP.Models.ModelDanhMuc;

[Route("api/HinhThucDaoTao")]
[ApiController]
public class HinhThucDaoTaoDM : ControllerBase
{
    private readonly DapperContext _context;

    public HinhThucDaoTaoDM(DapperContext context)
    {
        _context = context;
    }

    // POST: api/HinhThucDT
    [HttpPost]
    public async Task<IActionResult> PostChucVu([FromBody] HinhThucDTDM hinhthucdaotao)
    {
        if (hinhthucdaotao == null || string.IsNullOrEmpty(hinhthucdaotao.HinhThuc) || string.IsNullOrEmpty(hinhthucdaotao.MoTa_HT))
            return BadRequest("Dữ liệu không hợp lệ");

        hinhthucdaotao.ID_TrangThai = hinhthucdaotao.ID_TrangThai == 0 ? 1 : hinhthucdaotao.ID_TrangThai;

        var query = @"INSERT INTO HinhThucDaoTao (HinhThuc, MoTa_HT, ID_TrangThai) 
                      VALUES (@HinhThuc, @MoTa_HT, @ID_TrangThai)";
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, hinhthucdaotao);
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }




    // GET: api/HinhThucDT
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = "SELECT * FROM HinhThucDaoTao";
        try
        {
            using var connection = _context.CreateConnection();
            var list = await connection.QueryAsync<HinhThucDTDM>(query);
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
        var query = "DELETE FROM HinhThucDaoTao WHERE ID_htdaotao = @Id";
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
    public async Task<IActionResult> PutHinhThucDaoTao(int id, [FromBody] HinhThucDTDM hinhthucdaotao)
    {
        if (hinhthucdaotao == null || id != hinhthucdaotao.ID_htdaotao)
            return BadRequest("Dữ liệu không hợp lệ");

        var query = @"UPDATE HinhThucDaoTao
                      SET HinhThuc = @HinhThuc, MoTa_HT = @MoTa_HT, ID_TrangThai = @ID_TrangThai 
                      WHERE ID_htdaotao = @ID_htdaotao";

        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, hinhthucdaotao);
            return Ok(new { success = true, message = "Cập nhật thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi: {ex.Message}");
        }
    }
}
