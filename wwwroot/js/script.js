document.addEventListener("DOMContentLoaded", function() {
    // Mock data
    const employees = [
        { id: 1, name: "Nguyễn Văn A", department: "Kinh doanh", position: "Nhân viên", status: "Đang làm việc" },
        { id: 2, name: "Trần Thị B", department: "Tài chính", position: "Kế toán", status: "Nghỉ việc" },
        { id: 3, name: "Lê Thị C", department: "Marketing", position: "Chuyên viên", status: "Đang làm việc" },
        { id: 4, name: "Phạm Văn D", department: "Nhân sự", position: "Quản lý", status: "Đang làm việc" }
    ];

    // Hiển thị danh sách nhân viên
    const employeeList = document.getElementById("employeeList");
    employees.forEach((employee, index) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${index + 1}</td>
            <td>${employee.name}</td>
            <td>${employee.department}</td>
            <td>${employee.position}</td>
            <td>${employee.status}</td>
        `;
        employeeList.appendChild(row);
    });

    // Cập nhật thống kê tổng số nhân viên
    document.getElementById("totalEmployees").textContent = employees.length;
    document.getElementById("totalDepartments").textContent = [...new Set(employees.map(e => e.department))].length;
    document.getElementById("activeEmployees").textContent = employees.filter(e => e.status === "Đang làm việc").length;
});
//Load combobox

    // Dữ liệu danh sách giới tính
    const genderList = [
    {value: '', text: '-- Chọn --' },
    {value: 'Nam', text: 'Nam' },
    {value: 'Nu', text: 'Nữ' },
    {value: 'Khac', text: 'Khác' }
    ];

    // Lấy thẻ select
    const genderSelect = document.getElementById("genderSelect");

    // Duyệt mảng và thêm option vào select
    genderList.forEach(gender => {
        const option = document.createElement("option");
    option.value = gender.value;
    option.text = gender.text;
    genderSelect.appendChild(option);
    });
