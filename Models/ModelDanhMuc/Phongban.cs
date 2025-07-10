using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.ModelDanhMuc
{
    public class Phongban
    {
        [Key]
        public int ID_Phongban { get; set; }

        public string Maphongban { get; set; }
        public string Tenphongban { get; set; }
        public int ID_trangthai { get; set; } // Trạng thái hoạt động

        public string? MoTa { get; set; }
    }
}
