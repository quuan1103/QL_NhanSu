using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.DaoTao
{
    public class Daotao 
    {
       // public Guid ID_NV { get; set; } = Guid.NewGuid(); // Mã định danh duy nhất cho nhân viên
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public int? ID_PhongBan { get; set; }
        public int? ID_ChucVu { get; set; }
        public int? ID_htdaotao { get; set; }
        public int? ID_QG { get; set; }
        public string? QuyetDinh { get; set; }
        public DateTime? ThoiGianTu { get; set; }
        public DateTime? ThoiGianDen { get; set; }
        public int ID_TrangThai { get; set; }

    }
}
