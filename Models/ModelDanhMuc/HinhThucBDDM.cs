using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.ModelDanhMuc
{
    public class HinhThucBDDM
    {
        [Key]
        public int ID_htboiDuong { get; set; }

        public string HinhThuc { get; set; }

        public string? MoTaHTBD { get; set; }

       // public int ID_TrangThai { get; set; } // Trạng thái hoạt động
    }
}
