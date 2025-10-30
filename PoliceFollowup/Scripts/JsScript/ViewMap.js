let map;
let marker;

function initMap() {
    const lat = parseFloat(document.getElementById('lat').value) || 25.362629;
    const lng = parseFloat(document.getElementById('lng').value) || 55.400516;
    const location = { lat: lat, lng: lng };

    // Initialize the map
    map = new google.maps.Map(document.getElementById('map'), {
        center: location,
        zoom: 14,  // Initial zoom level
    });

    // Initialize the marker
    marker = new google.maps.Marker({
        position: location,
        map: map,
        title: "Click to view on Google Maps",
    });

    // Add click event listener to the marker to redirect to Google Maps
    marker.addListener('click', function () {
        const googleMapsUrl = `https://www.google.com/maps?q=${lat},${lng}`;
        window.open(googleMapsUrl, '_blank');  // Open Google Maps in a new tab
    });
}

// Initialize map on page load
window.onload = function () {
    initMap();

    // If the user updates latitude/longitude fields, refresh the map
    document.getElementById('lat').addEventListener('input', updateMap);
    document.getElementById('lng').addEventListener('input', updateMap);
};

// Update the map when latitude/longitude changes
function updateMap() {
    const lat = parseFloat(document.getElementById('lat').value) || 25.276987;  // Default latitude
    const lng = parseFloat(document.getElementById('lng').value) || 55.296249;  // Default longitude
    const location = { lat: lat, lng: lng };

    // Update marker position
    marker.setPosition(location);
    map.setCenter(location);
    map.setZoom(8);  // Zoom in after manually updating the latitude/longitude fields
}
