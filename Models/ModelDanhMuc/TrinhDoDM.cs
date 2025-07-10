using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.ModelDanhMuc
{
    public class TrinhDoDM
    {
        [Key]
        public int ID_TrinhDo { get; set; }

        public string TenTrinhDo { get; set; }
        public string? MoTaTD { get; set; }
       // public int ID_TrangThai { get; set; } // Trạng thái hoạt động
    }
}
