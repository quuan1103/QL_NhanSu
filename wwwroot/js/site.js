// Lấy form và các input
const loginForm = document.querySelector('form');
const inputFields = document.querySelectorAll('input');

// Cho phép nhấn Enter để submit form
inputFields.forEach(input => {
    input.addEventListener('keydown', function (event) {
        if (event.key === 'Enter' || event.keyCode === 13) {
            loginForm.requestSubmit(); // An toàn hơn submit() vì nó chạy qua cơ chế xác thực
        }
    });
});

// Xử lý hiện/ẩn mật khẩu khi click vào icon 👁️
