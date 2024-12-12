var map = L.map('map').setView([41.38879, 2.15899], 13);
map.zoomControl.remove();

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
  attribution: 'Map data © <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

var genreGroups = {};

generes.forEach(function (genre) {
  genreGroups[genre.GenereMusicName] = L.layerGroup().addTo(map);
});

events.forEach(function (e) {
  var locationIcon = L.icon({
    iconUrl: '/Assets/guitar.png',
    iconSize: [40, 40],
    iconAnchor: [20, 40],
    popupAnchor: [0, -40]
  });

  var marker = L.marker([e.Location.Latitude, e.Location.Longitude], { icon: locationIcon }).addTo(map);

  marker.bindPopup(`
    <div class="card" style="width: 10rem; padding: 0.5rem;">
      <img src="https://placehold.co/100x100" class="card-img-top" alt="..." style="height: 100px; object-fit: cover;">
      <div class="card-body" style="padding: 0.5rem;">
        <h5 class="card-title" style="font-size: 0.8rem; margin-bottom: 0.3rem;">Título del evento</h5>
        <p class="card-text" style="font-size: 0.7rem; line-height: 1rem; margin-bottom: 0.5rem;">
          Descripción más compacta.
        </p>
        <a href="#" class="btn btn-primary btn-sm" style="font-size: 0.7rem; padding: 0.2rem 0.5rem;">Ver más</a>
      </div>
    </div>
  `);


  e.GeneresMusic?.forEach(function (genre) {
    var genreGroup = genreGroups[genre.GenereMusicName];
    if (genreGroup) {
      genreGroup.addLayer(marker);
    }
  });
});

var layersControl = L.control.layers(null, genreGroups).addTo(map);

var layersControlContainer = layersControl.getContainer();

layersControlContainer.style.fontSize = "0.9rem";
layersControlContainer.style.textAlign = "left";

var layersControlButton = document.querySelector('.leaflet-control-layers-toggle');
layersControlButton.style.backgroundImage = "url('/Assets/music-note.png')";


const left = document.querySelector('.Explore__content__left');
let isClosed = false;
const leftSize = {
    true :"0%",
    false :"100%"
}

document.querySelector('#button').addEventListener('click',()=>{
    console.log("close")
    isClosed = !isClosed;
    left.style.width = leftSize[isClosed];
    setTimeout(() => {
        map.invalidateSize();
    }, 300);
});

