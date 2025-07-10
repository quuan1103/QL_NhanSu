using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.ModelDanhMuc
{
    public class LoaiHopDong 
    {
        [Key]
        public int ID_LoaiHopDong { get; set; }
        public string TenLoaiHopDong { get; set; } // Mã loại hợp đồng
        public string? MoTaLHD { get; set; } // Mô tả loại hợp đồng
    }
}
