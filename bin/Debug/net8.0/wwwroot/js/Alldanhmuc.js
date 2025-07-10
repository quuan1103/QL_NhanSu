
const PhongBanModule = (function () {
    //Phòng ban

    let isEditMode = false;

    function SavePhongBan() {
        const TenPhongBan = document.getElementById("TenPhongBan").value.trim();
        const Maphongban = document.getElementById("Maphongban").value.trim();
        const Mota = document.getElementById("MoTa").value.trim();
        const ID_Phongban = document.getElementById("ID_Phongban") ? document.getElementById("ID_Phongban").value : "";

        if (!Maphongban || !TenPhongBan) {
            alert("Vui lòng nhập đầy đủ thông tin.");
            return;
        }
        const confirmMessage = isEditMode ? "Bạn có chắc muốn cập nhật phòng ban này không?" : "Bạn có chắc muốn thêm mới phòng ban này không?";
        if (!confirm(confirmMessage)) {
            return;
        }

        let phongbanObj;
        let url = '/api/PhongBan';
        let method = 'POST';

        if (isEditMode && ID_Phongban) {
            // Cập nhật
            phongbanObj = {
                ID_Phongban: ID_Phongban,
                Maphongban,
                Tenphongban: TenPhongBan,
                Mota
            };
            url = `/api/PhongBan/${ID_Phongban}`;
            method = 'PUT';
        } else {
            // Thêm mới
            phongbanObj = {
                Maphongban,
                Tenphongban: TenPhongBan,
                Mota
            };
        }

        fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(phongbanObj)
        })
            .then(response => response.json())
            .then(res => {
                if (res && (res.success || res.maphongban)) {
                    alert(isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!");
                    LoadPhongBan();
                    fnClearPhongBan();
                } else {
                    alert("Có lỗi xảy ra.");
                }
            })
            .catch(() => alert("Lỗi khi gửi dữ liệu."));
    }
  
    

    function DeletePhongBan() {
        const ID_Phongban = document.getElementById("ID_Phongban").value;

        if (!ID_Phongban) {
            alert("Vui lòng chọn phòng ban để xoá.");
            return;
        }

        if (!confirm("Bạn có chắc muốn xoá phòng ban này?")) {
            return;
        }

        fetch(`/api/PhongBan/${ID_Phongban}`, {
            method: 'DELETE'
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert("Xoá thành công!");
                    LoadPhongBan();
                    fnClearPhongBan();
                } else {
                    alert("Không thể xoá.");
                }
            })
            .catch(() => alert("Lỗi khi xoá."));
    }
    function LoadPhongBan() {

        $.ajax({
            url: '/api/PhongBan',
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    console.log("Dữ liệu phòng ban:", res.data); // Kiểm tra dữ liệu
                    BindTablePhongBan(res.data);
                } else {
                    alert("Không thể tải danh sách.");
                }
            },
            error: function () {
                alert("Lỗi khi tải dữ liệu.");
            }
        });
    }

    function BindTablePhongBan(data) {
        let html = '';
        data.forEach(item => {
            console.log("Dòng dữ liệu:", item); // kiểm tra key
            html += `
                <tr>
                    <td>${item.Maphongban}</td>
                    <td>${item.Tenphongban}</td>
                    <td>${item.MoTa}</td>
                    <td>
                       <button class="btnChitietPB btn btn-sm btn-info"
                    data-idpb="${item.ID_Phongban}"
                    data-mapb="${item.Maphongban}"
                    data-tenpb="${item.Tenphongban}"
                    data-mota="${item.MoTa}">
                    <i class="fas fa-info-circle"></i> Chi tiết
                       </button>
                    </td>
                </tr>
            `;
        });

        $("#tblDataPhongban tbody").html(html);
    }


    // nút bấm Chi tiết
    $(document).on('click', '.btnChitietPB', function () {
        const ID_Phongban = $(this).data('idpb');
        const Maphongban = $(this).data('mapb');
        const TenPhongBan = $(this).data('tenpb');
        const MoTa = $(this).data('mota');
        console.log("Chi tiết:", {
            ID_Phongban, Maphongban, TenPhongBan, MoTa
        });

        $("#ID_Phongban").val(ID_Phongban);
        $("#Maphongban").val(Maphongban).prop('disabled', false);
        $("#TenPhongBan").val(TenPhongBan);
        $("#MoTa").val(MoTa);

        isEditMode = true;
    });
    function fnClearPhongBan() {
        $("#Maphongban").val('').prop('disabled', false);
        $("#TenPhongBan").val('');
        $("#MoTa").val('');
        isEditMode = false; // Reset trạng thái
    }
    return {
        LoadPhongBan,
        SavePhongBan,
        DeletePhongBan,
        fnClearPhongBan
    };
})();

// CHỨC VỤ
const ChucVuDMModule = (function () {
    let isEditMode = false;

    function SaveChucVu() {
        debugger
        const TenChucVu = document.getElementById("TenChucVu").value.trim();
        const MotaCV = document.getElementById("MotaCV").value.trim();
        const ID_ChucVu = document.getElementById("ID_ChucVu") ? document.getElementById("ID_ChucVu").value : "";

        if (!TenChucVu || !MotaCV) {
            alert("Vui lòng nhập đầy đủ thông tin.");
            return;
        }

        const confirmMessage = isEditMode ? "Bạn có chắc muốn cập nhật chức vụ này không?" : "Bạn có chắc muốn thêm mới chức vụ này không?";
        if (!confirm(confirmMessage)) {
            return;
        }

        let chucvuObj = {
            TenChucVu: TenChucVu,
            MotaCV: MotaCV,
            ID_TrangThai: 1 // Mặc định trạng thái là 1
        };

        let url = '/api/ChucVu';
        let method = 'POST';

        if (isEditMode && ID_ChucVu) {
            chucvuObj.ID_ChucVu = ID_ChucVu;
            url = `/api/ChucVu/${ID_ChucVu}`;
            method = 'PUT';
        }

        fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(chucvuObj)
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert(isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!");
                    LoadChucVu();
                    fnClearChucVu();
                } else {
                    alert("Có lỗi xảy ra.");
                }
            })
            .catch(() => alert("Lỗi khi gửi dữ liệu."));
    }

    function DeleteChucVu() {
        const ID_ChucVu = document.getElementById("ID_ChucVu").value;

        if (!ID_ChucVu) {
            alert("Vui lòng chọn chức vụ để xoá.");
            return;
        }

        if (!confirm("Bạn có chắc muốn xoá chức vụ này?")) {
            return;
        }

        fetch(`/api/ChucVu/${ID_ChucVu}`, {
            method: 'DELETE'
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert("Xoá thành công!");
                    LoadChucVu();
                    fnClearChucVu();
                } else {
                    alert("Không thể xoá.");
                }
            })
            .catch(() => alert("Lỗi khi xoá."));
    }

    function LoadChucVu() {
        $.ajax({
            url: '/api/ChucVu',
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    console.log("Dữ liệu chức vụ:", res.data);
                    BindTableChucVu(res.data);
                } else {
                    alert("Không thể tải danh sách.");
                }
            },
            error: function () {
                alert("Lỗi khi tải dữ liệu.");
            }
        });
    }

    function BindTableChucVu(data) {
        let html = '';
        data.forEach(item => {
            html += `
                <tr>
                    <td>${item.TenChucVu}</td>
                    <td>${item.MotaCV}</td>
                    <td>
                        <button class="btnChitietCV btn btn-sm btn-info"
                            data-idcv="${item.ID_ChucVu}"
                            data-tencv="${item.TenChucVu}"
                            data-motacv="${item.MotaCV}">
                            <i class="fas fa-info-circle"></i> Chi tiết
                        </button>
                    </td>
                </tr>
            `;
        });

        $("#tblDataChucVu tbody").html(html);
    }

    // Sự kiện click nút Chi tiết
    $(document).on('click', '.btnChitietCV', function () {
        debugger
        const ID_ChucVu = $(this).data('idcv');
        const TenChucVu = $(this).data('tencv');
        const MotaCV = $(this).data('motacv');
        console.log("Chi tiết:", {
            ID_ChucVu, TenChucVu, MotaCV
        });
        $("#ID_ChucVu").val(ID_ChucVu);
        $("#TenChucVu").val(TenChucVu);
        $("#MotaCV").val(MotaCV);

        isEditMode = true;
    });


    function fnClearChucVu() {
        $("#ID_ChucVu").val('');
        $("#TenChucVu").val('');
        $("#MotaCV").val('');
        isEditMode = false; // Reset trạng thái
    }

    return {
        LoadChucVu,
        SaveChucVu,
        DeleteChucVu,
        fnClearChucVu
    };
})();

//TRÌNH ĐỘ
const TrinhDoDMModule = (function () {
    let isEditMode = false;
    function SaveTrinhDo() {
        const TenTrinhDo = document.getElementById("TenTrinhDo").value.trim();
        const MoTaTD = document.getElementById("MoTaTD").value.trim();
        const ID_TrinhDo = document.getElementById("ID_TrinhDo") ? document.getElementById("ID_TrinhDo").value : "";
        if (!TenTrinhDo || !MoTaTD) {
            alert("Vui lòng nhập đầy đủ thông tin.");
            return;
        }
        const confirmMessage = isEditMode ? "Bạn có chắc muốn cập nhật trình độ này không?" : "Bạn có chắc muốn thêm mới trình độ này không?";
        if (!confirm(confirmMessage)) {
            return;
        }
        let trinhdoObj = {
            TenTrinhDo: TenTrinhDo,
            MoTaTD: MoTaTD
          //  ID_TrangThai: 1 // Mặc định trạng thái là 1
        };
        let url = '/api/TrinhDo';
        let method = 'POST';
        if (isEditMode && ID_TrinhDo) {
            trinhdoObj.ID_TrinhDo = ID_TrinhDo;
            url = `/api/TrinhDo/${ID_TrinhDo}`;
            method = 'PUT';
        }
        fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(trinhdoObj)
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert(isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!");
                    LoadTrinhDo();
                    fnClearTrinhDo();
                } else {
                    alert("Có lỗi xảy ra.");
                }
            })
            .catch(() => alert("Lỗi khi gửi dữ liệu."));
    }

    function DeleteTrinhDo() {
        const ID_TrinhDo = document.getElementById("ID_TrinhDo").value;
        if (!ID_TrinhDo) {
            alert("Vui lòng chọn trình độ để xoá.");
            return;
        }
        if (!confirm("Bạn có chắc muốn xoá trình độ này?")) {
            return;
        }
        fetch(`/api/TrinhDo/${ID_TrinhDo}`, {
            method: 'DELETE'
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert("Xoá thành công!");
                    LoadTrinhDo();
                    fnClearTrinhDo();
                } else {
                    alert("Không thể xoá.");
                }
            })
            .catch(() => alert("Lỗi khi xoá."));
    }
    function LoadTrinhDo() {
        $.ajax({
            url: '/api/TrinhDo',
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    console.log("Dữ liệu trình độ:", res.data);
                    BindTableTrinhDo(res.data);
                } else {
                    alert("Không thể tải danh sách.");
                }
            },
            error: function () {
                alert("Lỗi khi tải dữ liệu.");
            }
        });
    }
    function BindTableTrinhDo(data) {
        let html = '';
        data.forEach(item => {
            html += `
                <tr>
                    <td>${item.TenTrinhDo}</td>
                    <td>${item.MoTaTD}</td>
                    <td>
                        <button class="btnChitietTD btn btn-sm btn-info"
                            data-idtd="${item.ID_TrinhDo}"
                            data-tentd="${item.TenTrinhDo}"
                            data-motatd="${item.MoTaTD}">
                            <i class="fas fa-info-circle"></i> Chi tiết
                        </button>
                    </td>
                </tr>
            `;
        });
        $("#tblDataTrinhDo tbody").html(html);
    }
// Sự kiện click nút Chi tiết
    $(document).on('click', '.btnChitietTD', function () {
        const ID_TrinhDo = $(this).data('idtd');
        const TenTrinhDo = $(this).data('tentd');
        const MoTaTD = $(this).data('motatd');
        console.log("Chi tiết:", {
            ID_TrinhDo, TenTrinhDo, MoTaTD
        });
        $("#ID_TrinhDo").val(ID_TrinhDo);
        $("#TenTrinhDo").val(TenTrinhDo);
        $("#MoTaTD").val(MoTaTD);
        isEditMode = true;
    });
    function fnClearTrinhDo() {
        $("#ID_TrinhDo").val('');
        $("#TenTrinhDo").val('');
        $("#MoTaTD").val('');
        isEditMode = false; // Reset trạng thái
    }
    return {
        LoadTrinhDo,
        SaveTrinhDo,
        DeleteTrinhDo,
        fnClearTrinhDo
    };
}
)();

//LOẠI HỢP ĐỒNG
const LoaiHopDongDMModule = (function () {
    let isEditMode = false;
    function SaveLoaiHopDong() {
        const TenLoaiHopDong = document.getElementById("TenLoaiHopDong").value.trim();
        const MoTaLHD = document.getElementById("MoTaLHD").value.trim();
        const ID_LoaiHopDong = document.getElementById("ID_LoaiHopDong") ? document.getElementById("ID_LoaiHopDong").value : "";
        if (!TenLoaiHopDong || !MoTaLHD) {
            alert("Vui lòng nhập đầy đủ thông tin.");
            return;
        }
        const confirmMessage = isEditMode ? "Bạn có chắc muốn cập nhật loại hợp đồng này không?" : "Bạn có chắc muốn thêm mới loại hợp đồng này không?";
        if (!confirm(confirmMessage)) {
            return;
        }
        let loaihopdongObj = {
            TenLoaiHopDong: TenLoaiHopDong,
            MoTaLHD: MoTaLHD
        };
        let url = '/api/LoaiHopDong';
        let method = 'POST';
        if (isEditMode && ID_LoaiHopDong) {
            loaihopdongObj.ID_LoaiHopDong = ID_LoaiHopDong;
            url = `/api/LoaiHopDong/${ID_LoaiHopDong}`;
            method = 'PUT';
        }
        fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(loaihopdongObj)
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert(isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!");
                    LoadLoaiHopDong();
                    fnClearLoaiHopDong();
                } else {
                    alert("Có lỗi xảy ra.");
                }
            })
            .catch(() => alert("Lỗi khi gửi dữ liệu."));
    }
    function DeleteLoaiHopDong() {
        const ID_LoaiHopDong = document.getElementById("ID_LoaiHopDong").value;
        if (!ID_LoaiHopDong) {
            alert("Vui lòng chọn loại hợp đồng để xoá.");
            return;
        }
        if (!confirm("Bạn có chắc muốn xoá loại hợp đồng này?")) {
            return;
        }
        fetch(`/api/LoaiHopDong/${ID_LoaiHopDong}`, {
            method: 'DELETE'
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.success) {
                    alert("Xoá thành công!");
                    LoadLoaiHopDong();
                    fnClearLoaiHopDong();
                } else {
                    alert("Không thể xoá.");
                }
            })
            .catch(() => alert("Lỗi khi xoá."));
    }
    function LoadLoaiHopDong() {
        $.ajax({
            url: '/api/LoaiHopDong',
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    console.log("Dữ liệu loại hợp đồng:", res.data);
                    BindTableLoaiHopDong(res.data);
                } else {
                    alert("Không thể tải danh sách.");
                }
            },
            error: function () {
                alert("Lỗi khi tải dữ liệu.");
            }
        });
    }
    function BindTableLoaiHopDong(data) {
        let html = '';
        data.forEach(item => {
            html += `
                <tr>
                    <td>${item.TenLoaiHopDong}</td>
                    <td>${item.MoTaLHD}</td>
                    <td>
                        <button class="btnChitietLHD btn btn-sm btn-info"
                            data-idlhd="${item.ID_LoaiHopDong}"
                            data-tenlhd="${item.TenLoaiHopDong}"
                            data-motalhd="${item.MoTaLHD}">
                            <i class="fas fa-info-circle"></i> Chi tiết
                        </button>
                    </td>
                </tr>
            `;
        });
        $("#tblDataLoaiHopDong tbody").html(html);
    }
    // Sự kiện click nút Chi tiết
    $(document).on('click', '.btnChitietLHD', function () {
        const ID_LoaiHopDong = $(this).data('idlhd');
        const TenLoaiHopDong = $(this).data('tenlhd');
        const MoTaLHD = $(this).data('motalhd');
        console.log("Chi tiết:", {
            ID_LoaiHopDong, TenLoaiHopDong, MoTaLHD
        });
        $("#ID_LoaiHopDong").val(ID_LoaiHopDong);
        $("#TenLoaiHopDong").val(TenLoaiHopDong);
        $("#MoTaLHD").val(MoTaLHD);
        isEditMode = true;
    });

    function fnClearLoaiHopDong() {
        $("#ID_LoaiHopDong").val('');
        $("#TenLoaiHopDong").val('');
        $("#MoTaLHD").val('');
        isEditMode = false; // Reset trạng thái
    }
    return {
        LoadLoaiHopDong,
        SaveLoaiHopDong,
        DeleteLoaiHopDong,
        fnClearLoaiHopDong
    };
}
)();

