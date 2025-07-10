using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPP.Models
{
    public class TaiKhoanNhanVien
    {
        [Key]
        public Guid ID_TaiKhoan { get; set; }             // Khóa chính

        public Guid ID_NV { get; set; }                   // Khóa ngoại liên kết nhân viên

        [Required]
        public string TenDangNhap { get; set; }           // Tên đăng nhập

        public byte[] PasswordHash { get; set; }          // Mật khẩu đã mã hóa

        public byte[] Salt { get; set; }                  // Salt dùng để mã hóa

        public string QuyenHan { get; set; }              // Quyền hạn

        public int? ID_TrangThai { get; set; }            // Trạng thái hoạt động

        public DateTime? NgayTao { get; set; }            // Ngày tạo

        [NotMapped]
        public string Password { get; set; }              // Mật khẩu gốc (không lưu DB)

        [NotMapped]
        public string ConfirmPassword { get; set; }       // Nhập lại mật khẩu
    }
}
