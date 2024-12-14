document.addEventListener("DOMContentLoaded", () => {
    var map = L.map('map').setView([41.38879, 2.15899], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: 'Map data © <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    var markers = {}; // Objeto para almacenar marcadores asociados a eventos

    // Crear los marcadores y asociarlos con los eventos
    events.forEach(function (e) {
        var locationIcon = L.icon({
            iconUrl: '/Assets/guitar.png',
            iconSize: [40, 40],
            iconAnchor: [20, 40],
            popupAnchor: [0, -40]
        });

        var marker = L.marker([e.Location.Latitude, e.Location.Longitude], { icon: locationIcon }).addTo(map);

        marker.bindPopup(`
            <div>
                <img src="${e.EventImage}" alt="${e.EventTitle}">
                <h4>${e.EventTitle}</h4>
                <p>${e.EventDescription || 'Sin descripción disponible.'}</p>
                <a href="/events/${e.IdEvent}" class="btn">Ver más</a>
            </div>
        `);

        // Asociar marcador con el evento
        markers[e.IdEvent] = marker; // Usa el ID único del evento como clave
    });

    // Vincular los list-items con los marcadores
    const eventItems = document.querySelectorAll(".list-item");
    eventItems.forEach((item) => {
        item.addEventListener("click", () => {
            const eventId = item.getAttribute("data-event-id"); // Obtén el ID del evento
            const marker = markers[eventId]; // Busca el marcador asociado

            if (marker) {
                map.flyTo(marker.getLatLng(), 15, { duration: 1 });
                marker.openPopup(); // Abre el popup del marcador
            }
        });
    });
});
