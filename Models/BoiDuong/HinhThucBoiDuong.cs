using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.BoiDuong
{
    public class HinhThucBoiDuong
    {
        [Key]
        public int ID_htboiDuong { get; set; }
        
        [StringLength(255)]
        public string? HinhThuc { get; set; }


        public string? MoTaHTBD { get; set; }
    }
}
