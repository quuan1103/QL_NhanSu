using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using Dapper;
using System.Data;
using WebAPP.Models.DaoTao;

[Route("api/[controller]")]
[ApiController]
public class DaoTaoController : ControllerBase
{
    private readonly DapperContext _context;

    public DaoTaoController(DapperContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sql = "SELECT * FROM DaoTao";
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync(sql);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Daotao model)
    {
        var sql = @"INSERT INTO DaoTao (ID_NV, KhoaHoc, DonViDaoTao, ThoiGianTu, ThoiGianDen, KetQua)
                    VALUES (@ID_NV, @KhoaHoc, @DonViDaoTao, @ThoiGianTu, @ThoiGianDen, @KetQua)";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(sql, model);
        return result > 0 ? Ok("Thêm thành công") : BadRequest("Thêm thất bại");
    }
}
