using Microsoft.AspNetCore.Http; // để dùng IFormFile
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPP.Models.HoSo
{
    public class LyLich
    {
        public Guid ID_NV { get; set; } = Guid.NewGuid();
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayCapCCCD { get; set; }
        public string NoiCapCCCD { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public int ID_PhongBan { get; set; }
        public int ID_ChucVu { get; set; }
        public int ID_TrinhDo { get; set; }
        public DateTime NgayVaoLam { get; set; }

        // 👇 Đây là nơi cần đổi string => IFormFile để nhận ảnh từ client
        public string HinhAnh { get; set; }

        [NotMapped] 
        public IFormFile HinhAnhFile { get; set; }

        public int ID_TrangThai { get; set; }
        public string GhiChu { get; set; }
        public int ID_LoaiHopDong { get; set; }
    }
}