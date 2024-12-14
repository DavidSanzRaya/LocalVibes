document.querySelectorAll('.action-button').forEach(button => {
    button.addEventListener('click', function (e) {
        e.preventDefault();

        const clickedDivider = this.closest('.divider');
        const otherDivider = Array.from(document.querySelectorAll('.divider')).find(div => div !== clickedDivider);

        // Remove fish-eye effect on clicked divider
        clickedDivider.classList.add('removing-effect');

        // Expand clicked divider and fade out the other
        clickedDivider.classList.add('expanding');
        if (otherDivider) {
            otherDivider.classList.add('fading');
        }

        // Wait for the animation/transition to complete
        clickedDivider.addEventListener('transitionend', function handleTransitionEnd() {
            window.location.href = e.target.getAttribute('href');
            clickedDivider.removeEventListener('transitionend', handleTransitionEnd); // Clean up event listener
        });
    });
});

var map = L.map('map', {
    center: [41.38879, 2.15899],
    zoom: 13,
    zoomControl: false,
    dragging: false,
    scrollWheelZoom: false,
    doubleClickZoom: false,
    boxZoom: false,
    keyboard: false
});

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data © <a href=\"https://www.openstreetmap.org/copyright\">OpenStreetMap</a> contributors'
}).addTo(map);