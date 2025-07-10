function ChiTietHR(id) {
    $.ajax({
        url: `/api/QuanlyhosoApi/get-nhanvien-by-id/${id}`,
        type: "GET",
        success: function (res) {
            if (res.success) {
                let nv = res.data;
                $("#txtMaNhanVien").val(nv.MaNhanVien || '');
                $("#txtHoTen").val(nv.HoTen || '');
                $("#txtGioiTinh").val(nv.GioiTinh || '');
                $("#txtNgaySinh").val(nv.NgaySinh ? nv.NgaySinh.split("T")[0] : '');
                $("#txtCCCD").val(nv.CCCD || '');
                $("#txtNgayCapCCCD").val(nv.NgayCapCCCD ? nv.NgayCapCCCD.split("T")[0] : '');
                $("#txtNoiCapCCCD").val(nv.NoiCapCCCD || '');
                $("#txtDiaChi").val(nv.DiaChi || '');
                $("#txtSoDienThoai").val(nv.SoDienThoai || '');
                $("#txtEmail").val(nv.Email || '');
                $("#txtPhongBan").val(nv.TenPhongBan || '');
                $("#txtChucVu").val(nv.TenChucVu || '');
                $("#txtTrinhDo").val(nv.TenTrinhDo || '');
                $("#txtLoaiHopDong").val(nv.TenLoaiHopDong || ''); 
                $("#txtNgayVaoLam").val(nv.NgayVaoLam ? nv.NgayVaoLam.split("T")[0] : '');
                $("#txtTrangThai").val(nv.TenTrangThai || '');
                $("#txtGhiChu").val(nv.GhiChu || '');
                $("#imgHinhAnh").attr("src", nv.HinhAnh || "/images/default.png");
                $("#modalDetail").modal("show");
            } else {
                alert(res.message || "Không tìm thấy thông tin nhân viên.");
            }
        },
        error: function () {
            alert("Lỗi khi tải chi tiết nhân viên.");
        }
    });
}