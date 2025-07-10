using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.ModelDanhMuc
{
    public class ChucVuDM 
    {
        [Key]
        public int ID_ChucVu { get; set; }

        public string TenChucVu { get; set; }
        public string? MotaCV { get; set; }
        public int ID_TrangThai { get; set; } // Trạng thái hoạt động

        
    }
}
