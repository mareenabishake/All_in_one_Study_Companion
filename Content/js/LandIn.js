document.addEventListener("DOMContentLoaded", function () {
    const sections = document.querySelectorAll("#features, #how-it-works, #testimonials, footer");

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("visible");
            }
        });
    }, {
        threshold: 0.1
    });

    sections.forEach(section => {
        observer.observe(section);
    });
});

