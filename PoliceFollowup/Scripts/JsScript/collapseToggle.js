document.addEventListener('DOMContentLoaded', function () {
    document.body.addEventListener('click', function (event) {
        var target = event.target;
        if (target.classList.contains('collapse-toggle') || target.closest('.collapse-toggle')) {
            var collapseToggleDiv = target.classList.contains('collapse-toggle') ? target : target.closest('.collapse-toggle');
            var targetClass = collapseToggleDiv.getAttribute('data-target');
            var targetElement = document.querySelector(targetClass);
            var $targetElement = $(targetElement);

            if ($targetElement.hasClass('show')) {
                $targetElement.slideUp(400, function () {
                    $targetElement.removeClass('show');
                    collapseToggleDiv.querySelector('i').classList.remove('fa-minus');
                    collapseToggleDiv.querySelector('i').classList.add('fa-plus');
                });
            } else {
                $targetElement.slideDown(400, function () {
                    $targetElement.addClass('show');
                    collapseToggleDiv.querySelector('i').classList.remove('fa-plus');
                    collapseToggleDiv.querySelector('i').classList.add('fa-minus');
                });
            }
        }
    });
});

$(document).ready(function () {
    $('#ReportType').change(function () {
        if ($(this).val() == "مصدر") {
            $('#SourceNumberContainer').show();
        } else {
            $('#SourceNumberContainer').hide();
        }
    });
});

function showAdditionalFields() {
    document.getElementById('detaineeGender').style.display = 'block';
    document.getElementById('detaineeNationality').style.display = 'block';
}

function toggleLocationInput() {
    var locationRow = document.getElementById("locationId");
    if (locationRow.style.display === "none") {
        locationRow.style.display = "flex";
    } else {
        locationRow.style.display = "none";
    }
}

