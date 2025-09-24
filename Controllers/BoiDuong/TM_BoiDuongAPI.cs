using Microsoft.AspNetCore.Mvc;
using WebAPP.Data;
using WebAPP.Models.BoiDuong;
using Dapper;

namespace WebAPP.Controllers.BoiDuong
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoiDuongAPI : ControllerBase
    {
        private readonly DapperContext db;

        public BoiDuongAPI(DapperContext db)
        {
            this.db = db;
        }

        // Lấy danh sách bồi dưỡng
        [HttpGet("get-boiduong")]
        public async Task<IActionResult> GetBoiDuong()
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = @"
                    SELECT bd.*, 
                           pb.Tenphongban as TenPhongBan,
                           cv.TenChucVu,
                           htb.HinhThuc,
                           qg.TenQuocGia,
                           CASE 
                               WHEN bd.ID_TrangThai = 1 THEN N'Đã duyệt'
                               WHEN bd.ID_TrangThai = 2 THEN N'Chưa duyệt'
                               WHEN bd.ID_TrangThai = 3 THEN N'Hết hạn'
                               WHEN bd.ID_TrangThai = 4 THEN N'Tạo mới'
                               WHEN bd.ID_TrangThai = 5 THEN N'Huỷ'
                               WHEN bd.ID_TrangThai = 6 THEN N'Gia hạn'
                               ELSE N'Không xác định'
                           END as TenTrangThai
                    FROM BoiDuong bd
                    LEFT JOIN PhongBan pb ON bd.ID_PhongBan = pb.ID_Phongban
                    LEFT JOIN ChucVu cv ON bd.ID_ChucVu = cv.ID_ChucVu
                    LEFT JOIN HinhThucBoiDuong htb ON bd.ID_htboiDuong = htb.ID_htboiDuong
                    LEFT JOIN QuocGia qg ON bd.ID_QG = qg.ID_QG
                    ORDER BY bd.ID_BoiDuong DESC";

                var result = await connection.QueryAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lấy bồi dưỡng theo mã nhân viên
        [HttpGet("get-boiduong-by-manv/{maNhanVien}")]
        public async Task<IActionResult> GetBoiDuongByMaNV(string maNhanVien)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = @"
                    SELECT bd.*, 
                           pb.Tenphongban as TenPhongBan,
                           cv.TenChucVu,
                           htb.HinhThuc,
                           qg.TenQuocGia,
                           CASE 
                               WHEN bd.ID_TrangThai = 1 THEN N'Đã duyệt'
                               WHEN bd.ID_TrangThai = 2 THEN N'Chưa duyệt'
                               WHEN bd.ID_TrangThai = 3 THEN N'Hết hạn'
                               WHEN bd.ID_TrangThai = 4 THEN N'Tạo mới'
                               WHEN bd.ID_TrangThai = 5 THEN N'Huỷ'
                               WHEN bd.ID_TrangThai = 6 THEN N'Gia hạn'
                               ELSE N'Không xác định'
                           END as TenTrangThai
                    FROM BoiDuong bd
                    LEFT JOIN PhongBan pb ON bd.ID_PhongBan = pb.ID_Phongban
                    LEFT JOIN ChucVu cv ON bd.ID_ChucVu = cv.ID_ChucVu
                    LEFT JOIN HinhThucBoiDuong htb ON bd.ID_htboiDuong = htb.ID_htboiDuong
                    LEFT JOIN QuocGia qg ON bd.ID_QG = qg.ID_QG
                    WHERE bd.MaNhanVien = @MaNhanVien";

                var result = await connection.QueryFirstOrDefaultAsync(query, new { MaNhanVien = maNhanVien });
                
                if (result == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bồi dưỡng" });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Thêm mới bồi dưỡng
        [HttpPost("insert-boiduong")]
        public async Task<IActionResult> InsertBoiDuong([FromBody] WebAPP.Models.BoiDuong.BoiDuong model)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = @"
                    INSERT INTO BoiDuong (ID_NV, MaNhanVien, HoTen, ID_PhongBan, ID_ChucVu, 
                                        ID_htboiDuong, ID_QG, ThoiGianTu, ThoiGianDen, QuyetDinh, ID_TrangThai)
                    VALUES (@ID_NV, @MaNhanVien, @HoTen, @ID_PhongBan, @ID_ChucVu, 
                            @ID_htboiDuong, @ID_QG, @ThoiGianTu, @ThoiGianDen, @QuyetDinh, @ID_TrangThai)";

                await connection.ExecuteAsync(query, model);
                return Ok(new { success = true, message = "Thêm bồi dưỡng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Cập nhật bồi dưỡng
        [HttpPut("update-boiduong/{maNhanVien}")]
        public async Task<IActionResult> UpdateBoiDuong(string maNhanVien, [FromBody] WebAPP.Models.BoiDuong.BoiDuong model)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = @"
                    UPDATE BoiDuong 
                    SET HoTen = @HoTen, ID_PhongBan = @ID_PhongBan, ID_ChucVu = @ID_ChucVu,
                        ID_htboiDuong = @ID_htboiDuong, ID_QG = @ID_QG, ThoiGianTu = @ThoiGianTu,
                        ThoiGianDen = @ThoiGianDen, QuyetDinh = @QuyetDinh
                    WHERE MaNhanVien = @MaNhanVien";

                await connection.ExecuteAsync(query, model);
                return Ok(new { success = true, message = "Cập nhật bồi dưỡng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Duyệt bồi dưỡng
        [HttpPost("Duyet-boiduong")]
        public async Task<IActionResult> DuyetBoiDuong([FromBody] List<int> ids)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "UPDATE BoiDuong SET ID_TrangThai = 1 WHERE ID_BoiDuong IN @Ids";
                await connection.ExecuteAsync(query, new { Ids = ids });
                return Ok(new { success = true, message = "Duyệt bồi dưỡng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Hủy duyệt bồi dưỡng
        [HttpPost("HuyDuyet-boiduong")]
        public async Task<IActionResult> HuyDuyetBoiDuong([FromBody] List<int> ids)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "UPDATE BoiDuong SET ID_TrangThai = 2 WHERE ID_BoiDuong IN @Ids";
                await connection.ExecuteAsync(query, new { Ids = ids });
                return Ok(new { success = true, message = "Hủy duyệt bồi dưỡng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Gia hạn bồi dưỡng
        [HttpGet("GiaHanBoiDuong")]
        public async Task<IActionResult> GiaHanBoiDuong(int ID_BoiDuong)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "SELECT * FROM BoiDuong WHERE ID_BoiDuong = @ID_BoiDuong";
                var result = await connection.QueryFirstOrDefaultAsync(query, new { ID_BoiDuong });
                
                if (result == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bồi dưỡng" });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lưu gia hạn
        [HttpPost("LuuGiaHan")]
        public async Task<IActionResult> LuuGiaHan([FromBody] List<int> ids)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "UPDATE BoiDuong SET ID_TrangThai = 6 WHERE ID_BoiDuong IN @Ids";
                await connection.ExecuteAsync(query, new { Ids = ids });
                return Ok(new { success = true, message = "Gia hạn bồi dưỡng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lấy danh sách hình thức bồi dưỡng
        [HttpGet("GetDanhSachHinhThucBoiDuong")]
        public async Task<IActionResult> GetDanhSachHinhThucBoiDuong()
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "SELECT * FROM HinhThucBoiDuong ORDER BY HinhThuc";
                var result = await connection.QueryAsync(query);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lấy danh sách quốc gia
        [HttpGet("GetDanhSachQuocGia")]
        public async Task<IActionResult> GetDanhSachQuocGia()
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "SELECT * FROM QuocGia ORDER BY TenQuocGia";
                var result = await connection.QueryAsync(query);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lấy danh sách mã nhân viên
        [HttpGet("List")]
        public async Task<IActionResult> GetListMaNhanVien()
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "SELECT DISTINCT MaNhanVien FROM BoiDuong WHERE MaNhanVien IS NOT NULL ORDER BY MaNhanVien";
                var result = await connection.QueryAsync(query);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lấy danh sách mã nhân viên từ LyLich
        [HttpGet("GetDanhSachMaNhanVien")]
        public async Task<IActionResult> GetDanhSachMaNhanVien()
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = "SELECT DISTINCT MaNhanVien FROM LyLich WHERE MaNhanVien IS NOT NULL ORDER BY MaNhanVien";
                var result = await connection.QueryAsync(query);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Lấy thông tin nhân viên theo mã
        [HttpGet("GetByMaNV/{maNhanVien}")]
        public async Task<IActionResult> GetByMaNV(string maNhanVien)
        {
            try
            {
                using var connection = db.CreateConnection();
                var query = @"
                    SELECT ll.MaNhanVien, ll.HoTen, pb.Tenphongban as TenPhongBan, cv.TenChucVu,
                           ll.ID_NV, pb.ID_Phongban as ID_PhongBan, cv.ID_ChucVu
                    FROM LyLich ll
                    LEFT JOIN PhongBan pb ON ll.ID_PhongBan = pb.ID_Phongban
                    LEFT JOIN ChucVu cv ON ll.ID_ChucVu = cv.ID_ChucVu
                    WHERE ll.MaNhanVien = @MaNhanVien";

                var result = await connection.QueryFirstOrDefaultAsync(query, new { MaNhanVien = maNhanVien });
                
                if (result == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy nhân viên" });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
