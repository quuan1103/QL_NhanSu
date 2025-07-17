using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using Dapper;
using System.Data;
using WebAPP.Models.ModelDanhMuc;
using WebAPP.Models.HoSo;
using WebAPP.Models.DaoTao;
using Microsoft.EntityFrameworkCore;
using WebAPP.Models;

[Route("api/[controller]")]
[ApiController]
public class DaoTaoController : ControllerBase
{
    private readonly DapperContext _context;

    public DaoTaoController(DapperContext context)
    {
        _context = context;
    }

    // API: GET /api/DaoTao/List
    [HttpGet("List")]
    public async Task<IActionResult> GetDanhSachNhanVien()
    {
        var query = "SELECT MaNhanVien, HoTen, ID_PhongBan, ID_ChucVu FROM LyLich";
        using var conn = _context.CreateConnection();
        var list = await conn.QueryAsync(query);
        return Ok(new { success = true, data = list });
    }

    // API: GET /api/DaoTao/GetByMaNV
    [HttpGet("GetByMaNV/{maNV}")]
    public async Task<IActionResult> GetThongTinTheoMaNV(string maNV)
    {
        var query = @"
        SELECT dt.MaNhanVien, dt.HoTen,  dt.ID_PhongBan, dt.ID_ChucVu, cv.TenChucVu, pb.TenPhongBan
        FROM LyLich dt
        INNER JOIN PhongBan pb ON dt.ID_PhongBan = pb.ID_PhongBan
        INNER JOIN ChucVu cv ON dt.ID_ChucVu = cv.ID_ChucVu
        WHERE dt.MaNhanVien = @MaNV";

        using var conn = _context.CreateConnection();
        var result = await conn.QueryFirstOrDefaultAsync(query, new { MaNV = maNV });

        if (result == null)
            return Ok(new { success = false });

        return Ok(new { success = true, data = result });
    }
    //Api load danh sách quốc gia
    [HttpGet("GetDanhSachQuocGia")]
    public async Task<IActionResult> GetDanhSachQuocGia()
    {
        try
        {
            var query = "SELECT ID_QG, TenQuocGia FROM QuocGia";
            using var conn = _context.CreateConnection();
            var list = await conn.QueryAsync<QuocGia>(query);
            return Ok(new { success = true, data = list });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // sẽ hiện rõ lỗi ở client
        }
    }


    // ✅ Thêm nhân viên
    [HttpPost("insert-daotao")]
    public async Task<IActionResult> InsertDaoTao([FromBody] Daotao modal)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MaNhanVien", modal.MaNhanVien);
                parameters.Add("@HoTen", modal.HoTen);
                parameters.Add("@ID_PhongBan", modal.ID_PhongBan);
                parameters.Add("@ID_QG", modal.ID_QG);
                parameters.Add("@ID_ChucVu", modal.ID_ChucVu);
                parameters.Add("@ID_htdaotao", modal.ID_htdaotao);
                parameters.Add("@QuyetDinh", modal.QuyetDinh);
                parameters.Add("@ThoiGianTu", modal.ThoiGianTu);
                parameters.Add("@ThoiGianDen", modal.ThoiGianDen);
                parameters.Add("@ID_TrangThai", modal.ID_TrangThai);

                await connection.ExecuteAsync("sp_InsertQTDaoTao", parameters, commandType: CommandType.StoredProcedure);
            }

            return Ok(new { success = true, message = "Thêm nhân viên thành công!" });
        }
        catch (Exception ex)
        {
            // Log lỗi chi tiết để debug
            Console.WriteLine("Lỗi InsertDaoTao: " + ex.ToString());
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ✅ Lấy danh sách nhân viên
    [HttpGet("get-daotao")]
    public async Task<IActionResult> GetDaotao()
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"
                        SELECT dt.MaNhanVien,  dt.HoTen,
                               dt.ID_PhongBan, dt.ID_ChucVu,dt.ID_htdaotao, dt.ID_QG, dt.QuyetDinh, dt.ThoiGianTu, dt.ThoiGianDen, dt.ID_TrangThai,
                               pb.TenPhongBan, cv.TenChucVu, ht.HinhThuc, qg.TenQuocGia
                             
                        FROM DaoTao dt
                        LEFT JOIN PhongBan pb ON dt.ID_PhongBan = pb.ID_PhongBan
                        LEFT JOIN ChucVu cv ON dt.ID_ChucVu = cv.ID_ChucVu
                        LEFT JOIN HinhThucDaoTao ht ON dt.ID_htdaotao = ht.ID_htdaotao
                        LEFT JOIN QuocGia qg ON dt.ID_QG = qg.ID_QG";


                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var list = connection.Query(sql, new { BaseUrl = baseUrl }).ToList();
                return Ok(list);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // ✅ Lấy chi tiết daotao
    [HttpGet("get-daotao-by-manv/{maNhanVien}")]
    public async Task<IActionResult> GetDaoTaoByMaNV(string maNhanVien)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"
                SELECT 
                    dt.MaNhanVien, dt.HoTen,dt.ID_QG,dt.ID_htdaotao,dt.ID_ChucVu,dt.ID_PhongBan, 
                    pb.TenphongBan, cv.TenChucVu, 
                    ht.HinhThuc, qg.TenQuocGia,
                    dt.ThoiGianTu, dt.ThoiGianDen, dt.QuyetDinh
                FROM DaoTao dt
                LEFT JOIN PhongBan pb ON dt.ID_PhongBan = pb.ID_PhongBan
                LEFT JOIN ChucVu cv ON dt.ID_ChucVu = cv.ID_ChucVu
                LEFT JOIN HinhThucDaoTao ht ON dt.ID_htdaotao = ht.ID_htdaotao
                LEFT JOIN QuocGia qg ON dt.ID_QG = qg.ID_QG
                WHERE dt.MaNhanVien = @maNhanVien";

                var result = await connection.QueryFirstOrDefaultAsync(sql, new { maNhanVien });

                if (result == null)
                    return NotFound(new { success = false, message = "Không tìm thấy nhân viên!" });

                return Ok(new { success = true, data = result });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
    // ✅ Cập nhật đào tạo
    [HttpPut("update-daotao/{maNhanVien}")]
    public async Task<IActionResult> PutDaoTaoByMaNhanVien(string maNhanVien, [FromBody] Daotao model)
    {       
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MaNhanVien", model.MaNhanVien);
            parameters.Add("@HoTen", model.HoTen);
            parameters.Add("@ID_PhongBan", model.ID_PhongBan);
            parameters.Add("@ID_QG", model.ID_QG);
            parameters.Add("@ID_ChucVu", model.ID_ChucVu);
            parameters.Add("@ID_htdaotao", model.ID_htdaotao);
            parameters.Add("@QuyetDinh", model.QuyetDinh);
            parameters.Add("@ThoiGianTu", model.ThoiGianTu);
            parameters.Add("@ThoiGianDen", model.ThoiGianDen);
            parameters.Add("@ID_TrangThai", model.ID_TrangThai);


            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync("sp_UpdateDaoTao", parameters, commandType: CommandType.StoredProcedure);
            return Ok(new { success = true, message = "Cập nhật thành công!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}

