
//// Phần load combobox
//function loadSelect(id, url, valueField, textField) {
//    fetch(url)
//        .then(response => response.json())
//        .then(data => {
//            if (data.success) {
//                let select = document.getElementById(id);
//                if (!select) {
//                    console.warn(`Không tìm thấy phần tử ${id}`);
//                    return;
//                }
//                data.data.forEach(item => {
//                    let option = document.createElement('option');
//                    option.value = item[valueField];
//                    option.text = item[textField];
//                    select.appendChild(option);
//                });
//            } else {
//                console.error(`Không lấy được dữ liệu từ ${url}`);
//            }
//        })
//        .catch(error => console.error('Lỗi khi gọi API:', error));
//}

//document.addEventListener("DOMContentLoaded", function () {
//    loadSelect('ID_PhongBan', '/api/PhongBan', 'ID_Phongban', 'Tenphongban');
//    loadSelect('ID_ChucVu', '/api/ChucVu', 'ID_ChucVu', 'TenChucVu');
//    loadSelect('ID_TrinhDo', '/api/TrinhDo', 'ID_TrinhDo', 'TenTrinhDo');
//    loadSelect('ID_LoaiHopDong', '/api/LoaiHopDong', 'ID_LoaiHopDong', 'TenLoaiHopDong');
//});



//document.getElementById('HinhAnh').addEventListener('change', function (event) {
//    const file = event.target.files[0];
//    const preview = document.getElementById('previewImage');
//    if (file) {
//        const reader = new FileReader();
//        reader.onload = function (e) {
//            preview.src = e.target.result;
//            preview.style.display = 'block';
//        }
//        reader.readAsDataURL(file);
//    } else {
//        preview.src = '#';
//        preview.style.display = 'none';
//    }
//});


//document.getElementById('formNhanVien').addEventListener('submit', async function (e) {
//    e.preventDefault();

//    const form = e.target;
//    const formData = new FormData(form);

//    // Tạo object để gửi lên API
//    const data = {};
//    formData.forEach((value, key) => {
//        data[key] = value;
//    });

//    // Xử lý file hình ảnh (nếu có)
//    const fileInput = document.getElementById('HinhAnh');
//    if (fileInput && fileInput.files.length > 0) {
//        const file = fileInput.files[0];
//        data.HinhAnh = await toBase64(file);
//    } else {
//        data.HinhAnh = null;
//    }

//    // Gọi API
//    fetch('/api/QuanlyhosoApi/insert-nhanvien', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(data)
//    })
//        .then(async response => {
//            const result = await response.json();
//            document.getElementById('thongBao').innerHTML = result.message;
//            if (result.success) {
//                document.getElementById('thongBao').className = 'alert alert-success mt-2';
//                form.reset();
//                document.getElementById('previewImage').style.display = 'none';
//            } else {
//                document.getElementById('thongBao').className = 'alert alert-danger mt-2';
//            }
//        })
//        .catch(error => {
//            document.getElementById('thongBao').innerHTML = 'Lỗi: ' + error;
//            document.getElementById('thongBao').className = 'alert alert-danger mt-2';
//        });
//});

//// Hàm chuyển file sang base64
//function toBase64(file) {
//    return new Promise((resolve, reject) => {
//        const reader = new FileReader();
//        reader.readAsDataURL(file);
//        reader.onload = () => resolve(reader.result);
//        reader.onerror = error => reject(error);
//    });
//}

//// Xem trước hình ảnh
//document.getElementById('HinhAnh').addEventListener('change', function (event) {
//    const file = event.target.files[0];
//    const preview = document.getElementById('previewImage');
//    if (file) {
//        const reader = new FileReader();
//        reader.onload = function (e) {
//            preview.src = e.target.result;
//            preview.style.display = 'block';
//        }
//        reader.readAsDataURL(file);
//    } else {
//        preview.src = '#';
//        preview.style.display = 'none';
//    }
//});

// Hàm load combobox, hỗ trợ set selected value (dùng cho cả Thêm mới và Chi tiết)
function loadSelect(id, url, valueField, textField, selectedValue) {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            let select = document.getElementById(id);
            if (!select) return;

            select.innerHTML = '<option value="">-- Chọn --</option>';
            let items = Array.isArray(data) ? data : (data.data || []);

            items.forEach(item => {
                let option = document.createElement('option');
                option.value = item[valueField];
                option.text = item[textField];
                if (selectedValue && selectedValue == item[valueField]) {
                    option.selected = true;
                }
                select.appendChild(option);
            });
        })
        .catch(error => console.error('Lỗi khi gọi API:', error));
}
// Load combobox cho form Thêm mới
document.addEventListener("DOMContentLoaded", function () {
    loadSelect('ID_PhongBan', '/api/PhongBan', 'ID_Phongban', 'Tenphongban');
    loadSelect('ID_ChucVu', '/api/ChucVu', 'ID_ChucVu', 'TenChucVu');
    loadSelect('ID_TrinhDo', '/api/TrinhDo', 'ID_TrinhDo', 'TenTrinhDo');
    loadSelect('ID_LoaiHopDong', '/api/LoaiHopDong', 'ID_LoaiHopDong', 'TenLoaiHopDong');
});

// Load combobox cho form Chi tiết (gọi khi mở modal chi tiết)
function loadAllDropdownsDetail(nv) {
    loadSelect('ID_PhongBanDetail', '/api/PhongBan', 'ID_Phongban', 'Tenphongban', nv.ID_PhongBan);
    loadSelect('ID_ChucVuDetail', '/api/ChucVu', 'ID_ChucVu', 'TenChucVu', nv.ID_ChucVu);
    loadSelect('ID_TrinhDoDetail', '/api/TrinhDo', 'ID_TrinhDo', 'TenTrinhDo', nv.ID_TrinhDo);
    loadSelect('ID_LoaiHopDongDetail', '/api/LoaiHopDong', 'ID_LoaiHopDong', 'TenLoaiHopDong', nv.ID_LoaiHopDong);
}

// Xem trước hình ảnh khi chọn file
const inputHinhAnh = document.getElementById('HinhAnh');
if (inputHinhAnh) {
    inputHinhAnh.addEventListener('change', function (event) {
        const file = event.target.files[0];
        const preview = document.getElementById('previewImage');
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                preview.src = e.target.result;
                preview.style.display = 'block';
            };
            reader.readAsDataURL(file);
        } else {
            preview.src = '#';
            preview.style.display = 'none';
        }
    });
}

// Gửi form Thêm mới    => API sau chuyển null

const formNhanVien = document.getElementById('formNhanVien');
if (formNhanVien) {
    formNhanVien.addEventListener('submit', async function (e) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);
        const data = {};

        // Copy các trường input vào object
        formData.forEach((value, key) => {
            data[key] = value;
        });

        // Upload ảnh nếu có
        const fileInput = document.getElementById('HinhAnh');
        if (fileInput && fileInput.files.length > 0) {
            const file = fileInput.files[0];
            const uploadForm = new FormData();
            uploadForm.append("file", file);

            const uploadRes = await fetch('/api/QuanlyhosoApi/upload-hinhanh', {
                method: 'POST',
                body: uploadForm
            });

            const uploadData = await uploadRes.json();
            if (uploadData.success) {
                data.HinhAnh = uploadData.path; // ✅ đường dẫn ảnh
            } else {
                alert("Upload ảnh thất bại: " + uploadData.message);
                return;
            }
        } else {
            data.HinhAnh = null;
        }

        // Gửi dữ liệu nhân viên
        fetch('/api/QuanlyhosoApi/insert-nhanvien', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(async response => {
                const result = await response.json();
                document.getElementById('thongBao').innerHTML = result.message;
                if (result.success) {
                    document.getElementById('thongBao').className = 'alert alert-success mt-2';
                    form.reset();
                    document.getElementById('previewImage').style.display = 'none';
                } else {
                    document.getElementById('thongBao').className = 'alert alert-danger mt-2';
                }
            })
            .catch(error => {
                document.getElementById('thongBao').innerHTML = 'Lỗi: ' + error;
                document.getElementById('thongBao').className = 'alert alert-danger mt-2';
            });
    });
}

//Lưu thông tin chi tiết nhân viên
function luuNhanVienDetail() {
    const form = document.getElementById('formNhanVienDetail');
    const formData = new FormData(form);
    const data = {};
    formData.forEach((value, key) => { data[key] = value; });

    fetch('/api/QuanlyhosoApi/update-nhanvien/' + data.ID_NV, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
        .then(res => res.json())
        .then(result => {
            document.getElementById('thongBaoDetail').innerHTML = result.message;
            if (result.success) {
                document.getElementById('thongBaoDetail').className = 'alert alert-success mt-2';
                // Có thể reload lại danh sách hoặc đóng modal
            } else {
                document.getElementById('thongBaoDetail').className = 'alert alert-danger mt-2';
            }
        });
}

// Hàm chuyển file sang base64
function toBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

// Hàm load dữ liệu chi tiết vào form modal (gọi từ trang Soyeu)
function showNhanVienDetail(nv) {
    // Load dropdowns trước, truyền giá trị cần chọn
    loadAllDropdownsDetail(nv);

    document.getElementById('ID_NV_Detail').value = nv.ID_NV || '';
    document.getElementById('MaNhanVienDetail').value = nv.MaNhanVien || '';
    document.getElementById('HoTenDetail').value = nv.HoTen || '';
    document.getElementById('GioiTinhDetail').value = nv.GioiTinh || '';
    document.getElementById('NgaySinhDetail').value = nv.NgaySinh ? nv.NgaySinh.substring(0, 10) : '';
    document.getElementById('CCCDDetail').value = nv.CCCD || '';
    document.getElementById('NgayCapCCCDDetail').value = nv.NgayCapCCCD ? nv.NgayCapCCCD.substring(0, 10) : '';
    document.getElementById('NoiCapCCCDDetail').value = nv.NoiCapCCCD || '';
    document.getElementById('DiaChiDetail').value = nv.DiaChi || '';
    document.getElementById('SoDienThoaiDetail').value = nv.SoDienThoai || '';
    document.getElementById('EmailDetail').value = nv.Email || '';
    document.getElementById('NgayVaoLamDetail').value = nv.NgayVaoLam ? nv.NgayVaoLam.substring(0, 10) : '';
    document.getElementById('ID_TrangThaiDetail').value = nv.ID_TrangThai || '';
    document.getElementById('GhiChuDetail').value = nv.GhiChu || '';
    // Hình ảnh
    if (nv.HinhAnh) {
        const preview = document.getElementById('previewImageDetail');
        preview.src = nv.HinhAnh.startsWith('/') ? nv.HinhAnh : '/' + nv.HinhAnh;
        preview.style.display = 'block';
    } else {
        document.getElementById('previewImageDetail').style.display = 'none';
    }

}



