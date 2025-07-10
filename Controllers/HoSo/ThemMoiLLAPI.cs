using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPP.Data;
using WebAPP.Models.HoSo;

namespace WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanlyhosoApiController : ControllerBase
    {
        private readonly DapperContext db;

        public QuanlyhosoApiController(DapperContext context)
        {
            db = context;
        }

        // ✅ Upload ảnh
        [HttpPost("upload-hinhanh")]
        public async Task<IActionResult> UploadHinhAnh(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, message = "Không có file nào được chọn." });

            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Quanlyhoso");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"anh_{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("Quanlyhoso", fileName).Replace("\\", "/");
                return Ok(new { success = true, path = relativePath });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ✅ Thêm nhân viên
        [HttpPost("insert-nhanvien")]
        public async Task<IActionResult> InsertNhanVien([FromBody] LyLich model)
        {
            try
            {
                using (var connection = db.CreateConnection())
                {
                    var existing = await connection.QueryFirstOrDefaultAsync<LyLich>(
                        "SELECT * FROM LyLich WHERE MaNhanVien = @MaNhanVien",
                        new { MaNhanVien = model.MaNhanVien });

                    if (existing != null)
                    {
                        return BadRequest(new { success = false, message = "Mã nhân viên đã tồn tại!" });
                    }

                    // Handle image upload
                    if (model.HinhAnhFile != null && model.HinhAnhFile.Length > 0)
                    {
                        var uploadResult = await UploadHinhAnh(model.HinhAnhFile);
                        if (uploadResult is OkObjectResult okResult)
                        {
                            var resultData = okResult.Value as dynamic;
                            model.HinhAnh = resultData?.path;
                        }
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@MaNhanVien", model.MaNhanVien);
                    parameters.Add("@HoTen", model.HoTen);
                    parameters.Add("@GioiTinh", model.GioiTinh);
                    parameters.Add("@NgaySinh", model.NgaySinh);
                    parameters.Add("@CCCD", model.CCCD);
                    parameters.Add("@NgayCapCCCD", model.NgayCapCCCD);
                    parameters.Add("@NoiCapCCCD", model.NoiCapCCCD);
                    parameters.Add("@DiaChi", model.DiaChi);
                    parameters.Add("@SoDienThoai", model.SoDienThoai);
                    parameters.Add("@Email", model.Email);
                    parameters.Add("@ID_PhongBan", model.ID_PhongBan);
                    parameters.Add("@ID_ChucVu", model.ID_ChucVu);
                    parameters.Add("@ID_TrinhDo", model.ID_TrinhDo);
                    parameters.Add("@ID_LoaiHopDong", model.ID_LoaiHopDong);
                    parameters.Add("@NgayVaoLam", model.NgayVaoLam);
                    parameters.Add("@HinhAnh", model.HinhAnh);
                    parameters.Add("@ID_TrangThai", model.ID_TrangThai);
                    parameters.Add("@GhiChu", model.GhiChu);

                    await connection.ExecuteAsync("sp_InsertNhanVien", parameters, commandType: CommandType.StoredProcedure);
                }

                return Ok(new { success = true, message = "Thêm nhân viên thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ✅ Cập nhật nhân viên
        [HttpPut("update-nhanvien/{id}")]
        public async Task<IActionResult> UpdateNhanVien(Guid id, [FromForm] LyLich model)
        {
            if (id != model.ID_NV)
            {
                return BadRequest(new { message = "ID không khớp" });
            }

            try
            {
                // Handle image upload if new file is provided
                if (model.HinhAnhFile != null && model.HinhAnhFile.Length > 0)
                {
                    var uploadResult = await UploadHinhAnh(model.HinhAnhFile);
                    if (uploadResult is OkObjectResult okResult)
                    {
                        var resultData = okResult.Value as dynamic;
                        model.HinhAnh = resultData?.path;
                    }
                }

                var parameters = new DynamicParameters();
                parameters.Add("@ID_NV", id);
                parameters.Add("@MaNhanVien", model.MaNhanVien);
                parameters.Add("@HoTen", model.HoTen);
                parameters.Add("@GioiTinh", model.GioiTinh);
                parameters.Add("@NgaySinh", model.NgaySinh);
                parameters.Add("@CCCD", model.CCCD);
                parameters.Add("@NgayCapCCCD", model.NgayCapCCCD);
                parameters.Add("@NoiCapCCCD", model.NoiCapCCCD);
                parameters.Add("@DiaChi", model.DiaChi);
                parameters.Add("@SoDienThoai", model.SoDienThoai);
                parameters.Add("@Email", model.Email);
                parameters.Add("@ID_PhongBan", model.ID_PhongBan);
                parameters.Add("@ID_ChucVu", model.ID_ChucVu);
                parameters.Add("@ID_TrinhDo", model.ID_TrinhDo);
                parameters.Add("@ID_LoaiHopDong", model.ID_LoaiHopDong);
                parameters.Add("@NgayVaoLam", model.NgayVaoLam);
                parameters.Add("@HinhAnh", model.HinhAnh);
                parameters.Add("@ID_TrangThai", model.ID_TrangThai);
                parameters.Add("@GhiChu", model.GhiChu);

                using var conn = db.CreateConnection();
                await conn.ExecuteAsync("sp_UpdateNhanVien", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ✅ Lấy danh sách nhân viên
        [HttpGet("get-nhanvien")]
        public async Task<IActionResult> GetNhanVien()
        {
            try
            {
                using (var connection = db.CreateConnection())
                {
                    string sql = @"
                        SELECT nv.ID_NV, nv.MaNhanVien, nv.HoTen, nv.GioiTinh, nv.NgaySinh,  
                               nv.ID_PhongBan, nv.SoDienThoai, nv.Email, nv.CCCD,
                               pb.TenPhongBan,
                               CASE WHEN nv.HinhAnh IS NOT NULL 
                                    THEN CONCAT(@BaseUrl, '/', nv.HinhAnh) 
                                    ELSE NULL END AS HinhAnh
                        FROM LyLich nv
                        LEFT JOIN PhongBan pb ON nv.ID_PhongBan = pb.ID_PhongBan";

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

        // ✅ Lấy chi tiết nhân viên
        [HttpGet("get-nhanvien-by-id/{ID_NV}")]
        public async Task<IActionResult> GetNhanVienById(Guid ID_NV)
        {
            try
            {
                using (var connection = db.CreateConnection())
                {
                    string sql = @"
                        SELECT  
                            nv.ID_NV, nv.MaNhanVien, nv.HoTen, nv.GioiTinh, nv.NgaySinh, nv.CCCD,
                            nv.NgayCapCCCD, nv.NoiCapCCCD, nv.DiaChi, nv.SoDienThoai, nv.Email,
                            nv.ID_PhongBan, pb.TenPhongBan,
                            nv.ID_ChucVu, cv.TenChucVu,
                            nv.ID_TrinhDo, td.TenTrinhDo,
                            nv.ID_LoaiHopDong, hd.TenLoaiHopDong,
                            nv.NgayVaoLam, 
                            CASE WHEN nv.HinhAnh IS NOT NULL 
                                 THEN CONCAT(@BaseUrl, '/', nv.HinhAnh) 
                                 ELSE NULL END AS HinhAnh,
                            nv.ID_TrangThai, tt.TenTrangThai,
                            nv.GhiChu
                        FROM LyLich nv
                        LEFT JOIN PhongBan pb ON nv.ID_PhongBan = pb.ID_PhongBan
                        LEFT JOIN ChucVu cv ON nv.ID_ChucVu = cv.ID_ChucVu
                        LEFT JOIN TrinhDo td ON nv.ID_TrinhDo = td.ID_TrinhDo
                        LEFT JOIN TrangThai tt ON nv.ID_TrangThai = tt.ID_TrangThai
                        LEFT JOIN LoaiHopDong hd ON nv.ID_LoaiHopDong = hd.ID_LoaiHopDong
                        WHERE nv.ID_NV = @ID_NV";

                    var baseUrl = $"{Request.Scheme}://{Request.Host}";
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID_NV", ID_NV, DbType.Guid);
                    parameters.Add("@BaseUrl", baseUrl);

                    var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);

                    if (result == null)
                        return NotFound(new { success = false, message = "Nhân viên không tồn tại!" });

                    return Ok(new { success = true, data = result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}