using System;

namespace WebAPP.Models.Luong
{
	public class BangLuongListVM
	{
		public Guid Id { get; set; }
		public Guid NhanVienId { get; set; }
		public string? HoTen { get; set; }
		public int Thang { get; set; }
		public int Nam { get; set; }
		public decimal LuongCoBan { get; set; }
		public decimal PhuCap { get; set; }
		public decimal Thuong { get; set; }
		public decimal KhauTru { get; set; }
		public decimal BaoHiem { get; set; }
		public decimal ThueTNCN { get; set; }
		public decimal ThucLinh { get; set; }
	}
}



