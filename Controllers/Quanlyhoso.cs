using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

using System.Data;

//using Microsoft.EntityFrameworkCore;
using WebAPP.Data;
using WebAPP.Models;
using WebAPP.Models.HoSo;

namespace WebAPP.Controllers
{
    public class Quanlyhoso : Controller
    {
        private readonly DapperContext db;
        public Quanlyhoso(DapperContext context)
        {
            db = context;
        }

        public IActionResult Soyeu(int page = 1, int pageSize = 10)
        {
            using var connection = db.CreateConnection();

            // Đếm tổng số dòng
            var countQuery = "SELECT COUNT(*) FROM LyLich";
            var totalRecords = connection.ExecuteScalar<int>(countQuery);

            // Lấy dữ liệu phân trang
            var offset = (page - 1) * pageSize;
            var query = @"SELECT * FROM LyLich
                  ORDER BY HoTen OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var listNhanVien = connection.Query<LyLich>(query, new { Offset = offset, PageSize = pageSize }).ToList();

            // Gửi kèm dữ liệu phân trang qua ViewBag hoặc Model
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = totalRecords;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return View("~/Views/Home/Web/LyLich/Soyeu.cshtml", listNhanVien);
        }
        public IActionResult Quatrinhdaotao(int page = 1, int pageSize = 10)
        {
            using var connection = db.CreateConnection();

            // Đếm tổng số dòng trong bảng DaoTao
            var countQuery = "SELECT COUNT(*) FROM DaoTao";
            var totalRecords = connection.ExecuteScalar<int>(countQuery);

            // Tính toán phân trang
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var offset = (page - 1) * pageSize;

            // Lấy dữ liệu phân trang
            var query = @"SELECT * FROM DaoTao
                  ORDER BY ID_DaoTao DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var listDaoTao = connection.Query<WebAPP.Models.DaoTao.Daotao>(query, new { Offset = offset, PageSize = pageSize }).ToList();

            // Gửi kèm dữ liệu phân trang qua ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = totalRecords;
            ViewBag.TotalPages = totalPages;

            return View("~/Views/Home/Web/QT_DaoTao/Quatrinhdaotao.cshtml", listDaoTao);
        }

        public IActionResult Quatrinhboiduong(int page = 1, int pageSize = 10)
        {
            using var connection = db.CreateConnection();

            // Đếm tổng số dòng trong bảng BoiDuong
            var countQuery = "SELECT COUNT(*) FROM BoiDuong";
            var totalRecords = connection.ExecuteScalar<int>(countQuery);

            // Tính toán phân trang
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var offset = (page - 1) * pageSize;

            // Lấy dữ liệu phân trang
            var query = @"SELECT * FROM BoiDuong
                  ORDER BY ID_BoiDuong DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var listBoiDuong = connection.Query<WebAPP.Models.BoiDuong.BoiDuong>(query, new { Offset = offset, PageSize = pageSize }).ToList();

            // Gửi kèm dữ liệu phân trang qua ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = totalRecords;
            ViewBag.TotalPages = totalPages;

            return View("~/Views/Home/Web/QT_BoiDuong/Quatrinhboiduong.cshtml", listBoiDuong);
        }
        //Lý lịch - Thêm mới
        public IActionResult ThemmoiSYLL()
        {
            return View("~/Views/Home/Web/LyLich/ThemmoiSYLL.cshtml");
        }
        public IActionResult ThemMoiDT()
        {
            return View("~/Views/Home/Web/QT_DaoTao/ThemMoiDT.cshtml");
        }
        public IActionResult ChiTietHR(Guid id)
        {
            using var connection = db.CreateConnection();
            var sql = "SELECT * FROM LyLich WHERE ID_NV = @ID_NV";
            var nhanVien = connection.QueryFirstOrDefault<LyLich>(sql, new { ID_NV = id });

            if (nhanVien == null)
                return NotFound();

            return View("~/Views/Home/Web/ChiTietHR.cshtml", nhanVien);
        }
        public IActionResult DanhSachVaiTro()
        {
            return View("~/Views/PhanQuyen/DanhSachVaiTro.cshtml");
        }
        private byte[] HashPassword(string matKhau, byte[] salt)
        {
            throw new NotImplementedException();
        }

        private byte[] GenerateSalt()
        {
            throw new NotImplementedException();
        }

    }
}