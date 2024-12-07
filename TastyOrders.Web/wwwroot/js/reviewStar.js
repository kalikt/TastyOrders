document.addEventListener('DOMContentLoaded', function () {
    const stars = document.querySelectorAll('.star');
    const ratingInput = document.getElementById('ratingInput');

    stars.forEach(star => {
        star.addEventListener('click', function () {
            const value = this.dataset.value;
            ratingInput.value = value;

            stars.forEach(s => {
                s.classList.remove('active');
                if (s.dataset.value <= value) {
                    s.classList.add('active');
                }
            });
        });
    });
});