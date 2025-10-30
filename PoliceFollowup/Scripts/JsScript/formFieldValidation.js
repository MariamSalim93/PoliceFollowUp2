document.addEventListener('DOMContentLoaded', function () {
    var formFields = document.querySelectorAll('input, select, textarea');

    formFields.forEach(function (field) {
        field.addEventListener('change', function () {
            if (field.value && field.value !== 'يرجى الاختيار') {
                field.classList.remove('is-invalid');
            }
        });
    });
});
