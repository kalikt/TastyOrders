document.addEventListener('DOMContentLoaded', function () {
    console.log('Search script loaded');

    document.getElementById('searchInput').addEventListener('input', function () {
        const query = this.value.toLowerCase();
        const restaurantCards = document.querySelectorAll('.restaurant-card');

        restaurantCards.forEach(card => {
            const nameElement = card.querySelector('.restaurant-name');
            const restaurantName = card.dataset.name.toLowerCase();

            nameElement.innerHTML = card.dataset.name;

            if (restaurantName.includes(query)) {
                const regex = new RegExp(`(${query})`, 'gi');
                nameElement.innerHTML = card.dataset.name.replace(regex, '<span class="text-danger">$1</span>');
                card.style.display = 'block';
            } else {
                card.style.display = 'none';
            }
        });
    });
});