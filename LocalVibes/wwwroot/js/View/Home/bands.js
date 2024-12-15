
function adjustHeightWithNavbar() {
    const navbar = document.querySelector(".navbar");
    const container = document.querySelector(".main-container");

    if (navbar && container) {
        const navbarHeight = navbar.offsetHeight;

        container.style.marginTop = `${navbarHeight}px`;
    }
}

document.addEventListener("DOMContentLoaded", () => {
    adjustHeightWithNavbar();

    const container = document.querySelector(".main-container");
    container.classList.add("fade-in");

    const searchInput = document.getElementById("search");
    const projectCards = document.querySelectorAll(".project-card");

    searchInput.addEventListener("input", () => {
        const query = searchInput.value.toLowerCase();

        projectCards.forEach(card => {
            const projectName = card.querySelector("h4").innerText.toLowerCase();
            if (projectName.includes(query)) {
                card.style.display = "block";
            } else {
                card.style.display = "none";
            }
        });
    });
});
