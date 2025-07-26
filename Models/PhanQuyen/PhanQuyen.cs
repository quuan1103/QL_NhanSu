using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace WebAPP.Models.PhanQuyen
{
    public class VaiTro
    {
        public Guid ID_VaiTro { get; set; }
        public string TenVaiTro { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }

        public ICollection<TaiKhoan_VaiTro> TaiKhoanVaiTros { get; set; }
        public ICollection<PhanQuyenVaiTro> PhanQuyenVaiTros { get; set; }
    }

    public class Quyen
    {
        public Guid ID_Quyen { get; set; }
        public string TenQuyen { get; set; }
        public string MoTa { get; set; }
    }

    public class TaiKhoan_VaiTro
    {
        public Guid ID_TaiKhoan { get; set; }
        public Guid ID_VaiTro { get; set; }

        public VaiTro VaiTro { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }

    public class PhanQuyenVaiTro
    {
        public Guid ID_VaiTro { get; set; }
        public Guid ID_Quyen { get; set; }

        public VaiTro VaiTro { get; set; }
        public Quyen Quyen { get; set; }
    }

    public class TaiKhoan
    {
        public Guid ID_TaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
    }

}
