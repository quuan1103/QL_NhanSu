using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPP.Models.Luong
{
	public class BangLuong
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public Guid NhanVienId { get; set; }

		[Required]
		[Range(1900, 3000)]
		public int Nam { get; set; }

		[Required]
		[Range(1, 12)]
		public int Thang { get; set; }


		[Required]
		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal LuongCoBan { get; set; }

		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal PhuCap { get; set; }

		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Thuong { get; set; }

		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal KhauTru { get; set; }

		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal BaoHiem { get; set; }

		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal ThueTNCN { get; set; }

		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal ThucLinh { get; set; }

		public DateTime NgayTao { get; set; } = DateTime.UtcNow;
	}
}


