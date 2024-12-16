const stars = document.querySelectorAll('.star');
const ratingInput = document.getElementById('rating');

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


// JavaScript para cambiar el estado de favoritos




