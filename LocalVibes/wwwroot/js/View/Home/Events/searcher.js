document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("searchEventInput");
    const genreFilter = document.getElementById("genreFilter");
    const eventItems = document.querySelectorAll(".list-item");

    // Filtrar eventos por nombre
    searchInput.addEventListener("input", filterEvents);

    // Filtrar eventos por género
    genreFilter.addEventListener("change", filterEvents);

    function filterEvents() {
        const searchValue = searchInput.value.toLowerCase();
        const selectedGenre = genreFilter.value;

        eventItems.forEach((item) => {
            const titleElement = item.querySelector(".event-title");
            const genreElements = item.querySelectorAll(".genre");

            // Check if the event matches the search and genre filters
            const matchesSearch = titleElement.textContent.toLowerCase().includes(searchValue);
            const matchesGenre =
                selectedGenre === "all" ||
                Array.from(genreElements).some((genre) => genre.textContent === selectedGenre);

            if (matchesSearch && matchesGenre) {
                item.style.display = "block"; // Mostrar evento si coincide
            } else {
                item.style.display = "none"; // Ocultar evento si no coincide
            }
        });
    }
});
