using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class DanhMucController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/DanhMuc_Phongban/Index.cshtml");
    }

    [HttpGet("GetDanhMuc")]
    public IActionResult GetDanhMuc(int type)
    {
        // trả về dữ liệu JSON mẫu
        return Json(new { message = $"Đang gọi type = {type}" });
    }
    //public IActionResult IndexCV()
    //{
    //    return View("~/Views/DanhMuc_Chucvu/IndexCV.cshtml");
    //}
}

