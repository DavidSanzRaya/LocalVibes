const stars = document.querySelectorAll('.star');
const ratingInput = document.getElementById('rating');

document.addEventListener("DOMContentLoaded", () => {
    const toggleButton = document.getElementById("toggle-members-btn");
    const toggleIcon = document.getElementById("toggle-icon");
    const membersContainer = document.getElementById("band-members-container");

    toggleButton.addEventListener("click", () => {
        if (membersContainer.style.maxHeight === "0px" || !membersContainer.style.maxHeight) {
            // Desplegar
            membersContainer.style.maxHeight = membersContainer.scrollHeight + "px";
            toggleIcon.textContent = "▼";
        } else {
            // Plegar
            membersContainer.style.maxHeight = "0";
            toggleIcon.textContent = "▶";
        }
    });
});


stars.forEach(star => {
	star.addEventListener('click', () => {
		const rating = star.getAttribute('data-value');
		ratingInput.value = rating;
		updateStars(rating);
	});
});

function updateStars(rating) {
	stars.forEach(star => {
		if (parseInt(star.getAttribute('data-value')) <= rating) {
			star.textContent = '★';
		} else {
			star.textContent = '☆';
		}
	});
}





