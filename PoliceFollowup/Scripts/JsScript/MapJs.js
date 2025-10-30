
let map;
let marker;

function initMap() {
    const shjCentre = { lat: 25.362629, lng: 55.400516 };

    document.getElementById("lnglat").value = shjCentre.lat;
    document.getElementById("lnglong").value = shjCentre.lng;

    map = new google.maps.Map(document.getElementById("map"), {
        center: shjCentre,
        zoom: 12,
    });

    marker = new google.maps.Marker({
        draggable: true,
        position: shjCentre,
        map: map,
    });

    google.maps.event.addListener(marker, 'click', function (e) {
        var infoWindow = new google.maps.InfoWindow();
        var curLatLng = marker.getPosition();
        infoWindow.setContent("Coordinates: " + curLatLng.lat() + " , " + curLatLng.lng());
        infoWindow.open(map, marker);
        document.getElementById("lnglong").value = curLatLng.lng();
        document.getElementById("lnglat").value = curLatLng.lat();
    });

    google.maps.event.addListener(marker, 'dragend', function (e) {
        var curLatLng = marker.getPosition();
        document.getElementById("lnglong").value = curLatLng.lng();
        document.getElementById("lnglat").value = curLatLng.lat();
    });

    google.maps.event.addListener(marker, 'dblclick', function (e) {
        var positionDoubleclick = e.latLng;
        marker.setPosition(positionDoubleclick);

        var infoWindow = new google.maps.InfoWindow();
        var curLatLng = marker.getPosition();
        infoWindow.setContent("Coordinates: " + curLatLng.lat() + " , " + curLatLng.lng());
        infoWindow.open(map, marker);
        document.getElementById("lnglong").value = curLatLng.lng();
        document.getElementById("lnglat").value = curLatLng.lat();

        e.stopPropagation();
    });
}

function showLocation() {
    const lat = parseFloat(document.getElementById('lat').value);
    const lng = parseFloat(document.getElementById('lng').value);

    if (isNaN(lat) || isNaN(lng)) {
        alert('يرجى اضافة احداثيات صحيحة');
        return;
    }

    const location = { lat: lat, lng: lng };

    // Update the existing marker's position
    marker.setPosition(location);
    map.setCenter(location);
    map.setZoom(14); // Zoom in on the location

    // Update the hidden fields with the new position
    document.getElementById("lnglat").value = location.lat;
    document.getElementById("lnglong").value = location.lng;
}
