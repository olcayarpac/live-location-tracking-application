
var map = L.map('map').setView([37.7796, 29.0360], 15);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '© OpenStreetMap'
}).addTo(map);


const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44319/mapHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

var courierLocations;

connection.on("GetCourierLocations", () => {
    $.get("https://localhost:44319/api/Map/getAllCourierLocations", function (data, status) {
        if (status == 'success') {
            courierLocations = data;
            renderLocations(courierLocations);
        }
    });
});

connection.on("UpdateCourierLocation", (location) => {
    console.log(location)
    updateCourierLocation(location);
});

connection.onclose(async () => {
    await start();
});


var locationsLayerGroup = L.layerGroup().addTo(map);
var courierLocationMarkers = {}

function renderLocations(locations) {
    locations.forEach((location) => {
        var marker = L.marker([location.Lattitude, location.Longtitude]);
        location.LastUpdate = beautifyDateTimeString(location.LastUpdate);
        marker.bindPopup("Last Update: " + location.LastUpdate);
        marker.addTo(map);
        courierLocationMarkers[location.Id] = marker;

        //locationsLayerGroup.addLayer(marker);
        //location.markerId = locationsLayerGroup.getLayerId(marker);
    })

    removeHrefFromPopupClose();
}

function removeHrefFromPopupClose() {
    document.querySelector('.leaflet-pane.leaflet-popup-pane').addEventListener('click', event => {
        event.preventDefault();
    });
}

function updateCourierLocation(location) {
    location.LastUpdate = beautifyDateTimeString(location.LastUpdate);
    courierLocationMarkers[location.Id].bindPopup("Last Update: " + location.LastUpdate);
    var lat = location.Lattitude;
    var lng = location.Longtitude;
    courierLocationMarkers[location.Id].setLatLng([lat, lng]);
}

function beautifyDateTimeString(dateTime) {
    dateTime = dateTime.replace("T", " ");
    dateTime = dateTime.replace("Z", " ");

    dateTime = dateTime.split(".")[0];
    return dateTime;
}

// Start the connection.
start();

