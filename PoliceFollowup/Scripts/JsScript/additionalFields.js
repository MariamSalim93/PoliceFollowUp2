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
