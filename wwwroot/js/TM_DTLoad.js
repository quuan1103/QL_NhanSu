
//Load api getby MaNhanVien
$(document).ready(function () {
    LoadDanhSachMaNhanVien();

    $('#MaNhanVien').on('change', function () {
        var maNV = $(this).val();

        if (!maNV) {
            $('#HoTen').val('');
            $('#TenPhongBan').val('');
            $('#TenChucVu').val('');
            return;
        }

        // API lấy thông tin viên chức theo mã nhân viên
        $.ajax({
            url: '/api/DaoTao/GetByMaNV/' + maNV,
            method: 'GET',
            success: function (res) {
                if (res.success) {
                    $('#HoTen').val(res.data.HoTen);
                    $('#TenPhongBan').val(res.data.TenPhongBan);
                    $('#TenChucVu').val(res.data.TenChucVu);

                    $('#ID_PhongBan').val(res.data.ID_PhongBan);
                    $('#ID_ChucVu').val(res.data.ID_ChucVu);
                } else {
                    alert('Không tìm thấy thông tin viên chức!');
                }
            },
            error: function () {
                alert('Lỗi khi tải thông tin viên chức');
            }
        });
    });
});

//Load danh sách mã nhân viên
function LoadDanhSachMaNhanVien() {
    $.ajax({
        url: '/api/DaoTao/List',
        method: 'GET',
        success: function (res) {
            if (res.success) {
                var select = $('#MaNhanVien');
                select.empty().append('<option value="">-- Chọn mã viên chức --</option>');
                $.each(res.data, function (i, dt) {
                    select.append('<option value="' + dt.MaNhanVien + '">' + dt.MaNhanVien + '</option>');
                });
            } else {
                alert('Không thể tải danh sách mã nhân viên');
            }
        },
        error: function () {
            alert('Lỗi khi tải danh sách nhân viên');
        }
    });
}


//Load danh sách quốc gia
$(document).ready(function () {
    $.ajax({
        url: '/api/DaoTao/GetDanhSachQuocGia',
        method: 'GET',
        success: function (res) {
            if (res.success) {
                var select = $('#ID_QG');
                select.empty().append('<option value="">-- Chọn quốc gia --</option>');

                $.each(res.data, function (i, dt) {
                    select.append('<option value="' + dt.ID_QG + '">' + dt.TenQuocGia + '</option>');
                });
            } else {
                alert('Không thể tải danh sách n');
            }
        },
        error: function () {
            alert('Lỗi khi tải danh sách ');
        }
    });
});

//lOAD HÌNH THỨC ĐÀO TẠO
function loadSelectHTDT(id, url, valueField, textField, selectedValue) {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            let select = document.getElementById(id);
            if (!select) return;

            // Xóa dữ liệu cũ
            select.innerHTML = '<option value="">-- Chọn hình thức --</option>';

            // Nếu data là mảng: dùng trực tiếp, nếu là object có data: dùng data.data
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


function loadSelectHTDTdetail(id, url, valueField, textField, selectedValue) {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            let select = document.getElementById(id);
            if (!select) return;

            // Xóa dữ liệu cũ
            select.innerHTML = '<option value="">-- Chọn hình thức --</option>';

            // Nếu data là mảng: dùng trực tiếp, nếu là object có data: dùng data.data
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

function loadAllDropdownDetailDT(dt) {
    loadSelectHTDTdetail('ID_htdaotaoDetail', '/api/HinhThucDaoTao', 'ID_htdaotao', 'HinhThuc', dt.ID_htdaotao);
    loadSelectHTDTdetail('ID_QGDetail', '/api/DaoTao/GetDanhSachQuocGia', 'ID_QG', 'TenQuocGia', dt.ID_QG);
    loadSelectHTDTdetail('ID_PhongBanDetail', '/api/PhongBan', 'ID_Phongban', 'Tenphongban', dt.ID_PhongBan);
    loadSelectHTDTdetail('ID_ChucVuDetail', '/api/ChucVu', 'ID_ChucVu', 'TenChucVu', dt.ID_ChucVu);


}

//Lưu

document.addEventListener("DOMContentLoaded", function () {
    const btnLuu = document.getElementById("btnLuuDaoTao");

    if (btnLuu) {
        btnLuu.addEventListener("click", async function () {
            const model = {
                MaNhanVien: document.getElementById("MaNhanVien").value || null,
                HoTen: document.getElementById("HoTen").value || null,
                ID_PhongBan: parseInt(document.getElementById("ID_PhongBan").value) || null,
                ID_ChucVu: parseInt(document.getElementById("ID_ChucVu").value) || null,
                ID_htdaotao: parseInt(document.getElementById("ID_htdaotao").value) || null,
                ID_QG: parseInt(document.getElementById("ID_QG").value) || null,
                QuyetDinh: document.getElementById("QuyetDinh").value || "",
                ThoiGianTu: document.querySelector("input[name='ThoiGianTu']").value || null,
                ThoiGianDen: document.querySelector("input[name='ThoiGianDen']").value || null,
                ID_TrangThai: 4 // Mặc định là 4 (Tạo mới), có thể thay đổi nếu cần
            };

            console.log("Dữ liệu gửi:", JSON.stringify(model));

            if (!model.MaNhanVien || !model.ID_htdaotao || !model.ThoiGianTu) {
                alert("Vui lòng điền đầy đủ các trường bắt buộc!");
                return;
            }


            try {
                const response = await fetch("/api/DaoTao/insert-daotao", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(model)
                });

                const contentType = response.headers.get("content-type");

                if (contentType && contentType.includes("application/json")) {
                    const result = await response.json();

                    if (result.success) {
                        alert(result.message || "Lưu thành công!");
                        resetModalForm();
                        $('#modalThemMoi').modal('hide');
                        LoadDaoTao();
                    } else {
                        alert("Lỗi: " + result.message);
                    }
                } else {
                    const text = await response.text();
                    console.error("Phản hồi không phải JSON:", text);
                    alert("Lỗi không xác định từ server:\n" + text);
                }

            } catch (error) {
                console.error("Lỗi khi gửi API:", error);
                alert("Có lỗi xảy ra khi gửi dữ liệu.");
            }
        });
    }
});




//Load nhân viên
function LoadDaoTao() {
    fetch('/api/DaoTao/get-daotao')
        .then(response => response.json())
        .then(data => {
            if (Array.isArray(data)) {
                let html = '';

                // Hàm xác định class theo ID_TrangThai
                function getBadgeClass(idTrangThai) {
                    switch (idTrangThai) {
                        case 3: return "bg-warning";   // Hết hạn
                        case 1: return "bg-success";   // Đã duyệt
                        case 5: return "bg-danger";    // Huỷ
                        case 4: return "bg-info";      // Tạo mới
                        case 6: return "bg-primary";   // Gia hạn
                        default: return "bg-secondary"; // Không xác định
                    }
                }

                data.forEach((dt, index) => {
                    const badgeClass = getBadgeClass(dt.ID_TrangThai);

                    html += `<tr>
                        <td><input type="checkbox" class="rowCheckbox" data-id="${dt.ID_DaoTao}" /></td>
                        
                        <td>${dt.MaNhanVien}</td>
                        <td>${dt.HoTen}</td>
                        <td>${dt.TenPhongBan ?? ''}</td>
                        <td>${dt.TenChucVu}</td>
                        <td>${dt.HinhThuc}</td>
                        <td>${dt.ThoiGianTu ? formatDate(dt.ThoiGianTu) : ''}</td>
                        <td>${dt.ThoiGianDen ? formatDate(dt.ThoiGianDen) : ''}</td>
                        <td><span class="badge ${badgeClass}">${dt.TenTrangThai}</span></td>
                        <td>
                            <button class="btn btn-sm btn-primary" 
                                    onclick="ChiTietDaoTao('${dt.MaNhanVien}')">
                                Chi tiết
                            </button>
                        </td>
                    </tr>`;
                });

                document.getElementById('tblQTdaotao').innerHTML = html;
            }
        })
        .catch(err => console.error("Lỗi:", err));
}




//Chi Tiết
function ChiTietDaoTao(maNhanVien) {
    debugger
    if (!maNhanVien) {
        alert('Mã nhân viên không hợp lệ!');
        return;
    }

    fetch(`/api/DaoTao/get-daotao-by-manv/${maNhanVien}`)
        .then(response => {
            if (!response.ok) throw new Error('Lỗi kết nối');
            return response.json();
        })
        .then(res => {
            if (!res.success) {
                alert(res.message || "Không tìm thấy nhân viên!");
                return;
            }
            const dt = res.data;
            console.log('Dữ liệu nhận được:', dt);
            loadAllDropdownDetailDT(dt)

            // Điền dữ liệu vào modal
            document.getElementById('MaNhanVienDetail').value = dt.MaNhanVien || '';
            document.getElementById('HoTenDetail').value = dt.HoTen || '';
            document.getElementById('ID_PhongBanDetail').value = dt.ID_PhongBan || '';
            document.getElementById('ID_ChucVuDetail').value = dt.ID_ChucVu || '';
            document.getElementById('ID_htdaotaoDetail').value = dt.ID_htdaotao || '';
            document.getElementById('ID_QGDetail').value = dt.ID_QG || '';
            document.getElementById('ThoiGianTuDetail').value = dt.ThoiGianTu ? dt.ThoiGianTu.substring(0, 10) : '';
            document.getElementById('ThoiGianDenDetail').value = dt.ThoiGianDen ? dt.ThoiGianDen.substring(0, 10) : '';
            document.getElementById('QuyetDinhDetail').value = dt.QuyetDinh || '';

            // Hiển thị modal
            var modal = new bootstrap.Modal(document.getElementById('modalChiTiet'));
            modal.show();
        })
        .catch(err => {
            console.error("Lỗi:", err);
            alert('Có lỗi xảy ra: ' + err.message);
        });
}
//Load combobox

// Hàm định dạng ngày tháng
function formatDate(dateString) {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];
}

//Sửa đào tạo Lưu
function LuuDaoTaoDetail() {
    const maNhanVien = document.getElementById('MaNhanVienDetail').value;

    const data = {
        MaNhanVien: document.getElementById('MaNhanVienDetail').value,
        HoTen: document.getElementById('HoTenDetail').value,
        ID_PhongBan: parseInt(document.getElementById('ID_PhongBanDetail').value),
        ID_htdaotao: parseInt(document.getElementById('ID_htdaotaoDetail').value),
        ID_QG: parseInt(document.getElementById('ID_QGDetail').value),
        ID_ChucVu: parseInt(document.getElementById('ID_ChucVuDetail').value),
        ThoiGianTu: document.getElementById('ThoiGianTuDetail').value,
        ThoiGianDen: document.getElementById('ThoiGianDenDetail').value,
        QuyetDinh: document.getElementById('QuyetDinhDetail').value || null,
       // ID_TrangThai: 1 // hoặc giá trị mặc định nếu cần
    };

    fetch('/api/DaoTao/update-daotao/' + data.MaNhanVien, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(async response => {
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText);
            }
            return response.json();
        })
        .then(result => {
            alert('✅ Cập nhật thành công!');
            LoadDaoTao();
            $('#modalChiTiet').modal('hide');
            $('#modalXacNhanCapNhat').modal('hide'); // <-- dòng này thêm vào nếu bạn có modal xác nhận
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove(); // <-- đảm bảo xóa lớp mờ
        })
}



//DUyệt 
$(document).ready(function () {  
        $('#btnDuyet').click(async function () {
            // Hiển thị trạng thái loading
            const btn = $(this);
            btn.prop('disabled', true).html('<i class="fa fa-spinner fa-spin me-2"></i> Đang duyệt...');

            try {
                // Lấy danh sách ID đã chọn
                const selectedIds = [];
                $('.rowCheckbox:checked').each(function () {
                    const id = parseInt($(this).data('id'));
                    if (!isNaN(id)) selectedIds.push(id);
                });

                if (selectedIds.length === 0) {
                    alert("Vui lòng chọn ít nhất một dòng để duyệt.");
                    return;
                }

                // Gọi API bằng fetch()
                const response = await fetch('/api/DaoTao/Duyet-daotao', {
                   
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    },
                    body: JSON.stringify(selectedIds)
                });

                // Xử lý kết quả
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                const result = await response.json();

                if (result.success) {
                    alert(`Duyệt thành công ${selectedIds.length} dòng!`);
                    location.reload();
                } else {
                    alert(result.message || "Có lỗi xảy ra khi duyệt");
                }
            } catch (error) {
                console.error("Lỗi khi duyệt:", error);
                alert("Lỗi hệ thống: " + (error.message || "Không thể kết nối đến server"));
            } finally {
                // Khôi phục trạng thái nút
                btn.prop('disabled', false).html('<i class="fa fa-check-circle me-2"></i> Duyệt');
            }
        });
});

//Huỷ duyệt
$(document).ready(function () {
  
    $('#btnHuyduyet').click(async function () {
        // Hiển thị trạng thái loading
        const btn = $(this);
        btn.prop('disabled', true).html('<i class="fa fa-spinner fa-spin me-2"></i> Đang huỷ duyệt...');

        try {
            // Lấy danh sách ID đã chọn
            const selectedIds = [];
            $('.rowCheckbox:checked').each(function () {
                const id = parseInt($(this).data('id'));
                if (!isNaN(id)) selectedIds.push(id);
            });

            if (selectedIds.length === 0) {
                alert("Vui lòng chọn ít nhất một dòng để huỷ duyệt.");
                return;
            }

            // Gọi API bằng fetch()
            const response = await fetch('/api/DaoTao/HuyDuyet-daotao', {

                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(selectedIds)
            });

            // Xử lý kết quả
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const result = await response.json();

            if (result.success) {
                alert(`Huỷ duyệt thành công ${selectedIds.length} dòng!`);
                location.reload();
            } else {
                alert(result.message || "Có lỗi xảy ra khi huỷ duyệt");
            }
        } catch (error) {
            console.error("Lỗi khi duyệt:", error);
            alert("Lỗi hệ thống: " + (error.message || "Không thể kết nối đến server"));
        } finally {
            // Khôi phục trạng thái nút
            btn.prop('disabled', false).html('<i class="fa fa-check-circle me-2"></i> Huỷ Duyệt');
        }
    });
});



//Gia hạn mở form
document.getElementById('btnGiaHan').addEventListener('click', async function () {
    // Kiểm tra xem có checkbox nào được chọn không
    const selectedCheckbox = document.querySelector('.rowCheckbox:checked');
    if (!selectedCheckbox) {
        alert("Vui lòng chọn ít nhất một đào tạo để gia hạn!");
        return;
    }

    const idDaoTao = selectedCheckbox.getAttribute('data-id');
    try {
        const response = await fetch(`/api/DaoTao/GiaHanDaoTao?ID_DaoTao=${idDaoTao}`);
        const result = await response.json();
        console.log("API Response:", result);

        if (!result.success) {
            alert(result.message || "Lỗi khi lấy dữ liệu đào tạo!");
            return;
        }

        // Hiển thị dữ liệu lên modal
        document.getElementById('ThoiGianTu').value = formatDateForInput(result.data.ThoiGianTu);
        document.getElementById('ThoiGianDen').value = formatDateForInput(result.data.ThoiGianDen);

        // Mở modal
        const modal = new bootstrap.Modal(document.getElementById('modalGiahan'));
        modal.show();

    } catch (error) {
        console.error("Lỗi:", error);
        alert("Đã xảy ra lỗi khi gọi API!");
    }
});

// Hàm chuyển đổi ngày thành định dạng YYYY-MM-DD (dành cho input type="date")
function formatDateForInput(dateString) {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];
}


//Lưu gia hạn 
$(document).ready(function () {
   
    $('#btnGiaHanLuu').click(async function () {
        // Hiển thị trạng thái loading
        const btn = $(this);
        btn.prop('disabled', true).html('<i class="fa fa-spinner fa-spin me-2"></i> Đang gia hạn...');

        try {
            // Lấy danh sách ID đã chọn
            const selectedIds = [];
            $('.rowCheckbox:checked').each(function () {
                const id = parseInt($(this).data('id'));
                if (!isNaN(id)) selectedIds.push(id);
            });

            if (selectedIds.length === 0) {
                alert("Vui lòng chọn ít nhất một dòng để gia hạn.");
                return;
            }

            // Gọi API bằng fetch()
            const response = await fetch('/api/DaoTao/LuuGiaHan', {

                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(selectedIds)
            });

            // Xử lý kết quả
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const result = await response.json();

            if (result.success) {
                alert(`Gia hạn thành công !`);
                location.reload();
            } else {
                alert(result.message || "Có lỗi xảy ra khi huỷ gia hạn");
            }
        } catch (error) {
            console.error("Lỗi khi duyệt:", error);
            alert("Lỗi hệ thống: " + (error.message || "Không thể kết nối đến server"));
        } finally {
            // Khôi phục trạng thái nút
            btn.prop('disabled', false).html('<i class="fa fa-check-circle me-2"></i> Huỷ Gia hạn');
        }
    });
});




