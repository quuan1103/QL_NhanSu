using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPP.Data;
using WebAPP.Models.Luong;
using WebAPP.Models.HoSo;

namespace WebAPP.Controllers
{
	public class LuongController : Controller
	{
		private readonly ApplicationDbContext _db;

		public LuongController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			var items = await _db.BangLuong
				.Join(_db.LyLich.AsNoTracking(), b => b.NhanVienId, l => l.ID_NV, (b, l) => new BangLuongListVM
				{
					Id = b.Id,
					NhanVienId = b.NhanVienId,
					HoTen = l.HoTen,
					Thang = b.Thang,
					Nam = b.Nam,
					LuongCoBan = b.LuongCoBan,
					PhuCap = b.PhuCap,
					Thuong = b.Thuong,
					KhauTru = b.KhauTru,
					BaoHiem = b.BaoHiem,
					ThueTNCN = b.ThueTNCN,
					ThucLinh = b.ThucLinh
				})
				.OrderByDescending(x => x.Nam)
				.ThenByDescending(x => x.Thang)
				.ToListAsync();
			return View(items);
		}

		public IActionResult Create()
		{
			ViewBag.NhanVienList = new SelectList(_db.LyLich.AsNoTracking().OrderBy(x => x.HoTen).ToList(), "ID_NV", "HoTen");
			return View(new BangLuong());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BangLuong model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.NhanVienList = new SelectList(_db.LyLich.AsNoTracking().OrderBy(x => x.HoTen).ToList(), "ID_NV", "HoTen", model.NhanVienId);
				return View(model);
			}
			model.Id = Guid.NewGuid();
			_db.BangLuong.Add(model);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			var item = await _db.BangLuong.FindAsync(id);
			if (item == null) return NotFound();
			ViewBag.NhanVienList = new SelectList(_db.LyLich.AsNoTracking().OrderBy(x => x.HoTen).ToList(), "ID_NV", "HoTen", item.NhanVienId);
			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, BangLuong model)
		{
			if (id != model.Id) return BadRequest();
			if (!ModelState.IsValid)
			{
				ViewBag.NhanVienList = new SelectList(_db.LyLich.AsNoTracking().OrderBy(x => x.HoTen).ToList(), "ID_NV", "HoTen", model.NhanVienId);
				return View(model);
			}
			_db.Entry(model).State = EntityState.Modified;
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(Guid id)
		{
			var item = await _db.BangLuong.FindAsync(id);
			if (item == null) return NotFound();

			var lyLich = await _db.LyLich.AsNoTracking().FirstOrDefaultAsync(x => x.ID_NV == item.NhanVienId);
			var vm = new BangLuongDetailVM
			{
				BangLuong = item,
				HoTen = lyLich?.HoTen,
				HinhAnh = lyLich?.HinhAnh
			};
			return View(vm);
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			var item = await _db.BangLuong.FindAsync(id);
			if (item == null) return NotFound();
			return View(item);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var item = await _db.BangLuong.FindAsync(id);
			if (item == null) return NotFound();
			_db.BangLuong.Remove(item);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}


