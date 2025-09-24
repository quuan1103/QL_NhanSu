using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPP.Models.BoiDuong
{
    public class BoiDuong
    {
        [Key]
        public int ID_BoiDuong { get; set; }
        
        public Guid ID_NV { get; set; }
        
        [StringLength(10)]
        public string MaNhanVien { get; set; }
        
        [StringLength(255)]
        public string HoTen { get; set; }
        
        public int? ID_PhongBan { get; set; }
        
        public int? ID_ChucVu { get; set; }
        
        public int? ID_htboiDuong { get; set; }
        
        public int? ID_QG { get; set; }
        
        public DateTime? ThoiGianTu { get; set; }
        
        public DateTime? ThoiGianDen { get; set; }
        
        public string? QuyetDinh { get; set; }
        
        public int? ID_TrangThai { get; set; }
    }
}
