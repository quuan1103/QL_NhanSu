using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.ModelDanhMuc
{
    public class HinhThucDTDM
    {
        [Key]
        public int ID_htdaotao { get; set; }

        public string HinhThuc { get; set; }

        public string? MoTa_HT { get; set; }

        public int ID_TrangThai { get; set; } // Trạng thái hoạt động
    }
}
