document.addEventListener("DOMContentLoaded", function () {
    // Initialize the charts
    const hoursStudiedChartCtx = document.getElementById('hoursStudiedChart').getContext('2d');
    const hoursRemainingChartCtx = document.getElementById('hoursRemainingChart').getContext('2d');

    // Function to initialize the charts with sample data
    function initializeCharts() {
        new Chart(hoursStudiedChartCtx, {
            type: 'pie',
            data: {
                labels: ['Studied', 'Remaining'],
                datasets: [{
                    data: [10, 90], // Example data for studied and remaining hours
                    backgroundColor: ['#4caf50', '#f44336'],
                    hoverOffset: 4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });

        new Chart(hoursRemainingChartCtx, {
            type: 'pie',
            data: {
                labels: ['Studied', 'Remaining'],
                datasets: [{
                    data: [30, 70], // Example data for studied and remaining hours
                    backgroundColor: ['#2196f3', '#ffeb3b'],
                    hoverOffset: 4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });
    }

    // Call the function to initialize charts
    initializeCharts();

    // Function to validate numeric input
    function validateNumericInput(input) {
        input.value = input.value.replace(/[^0-9]/g, '');
    }

    // Add event listeners for numeric input validation
    document.querySelectorAll('input[type="number"]').forEach(input => {
        input.addEventListener('input', function() {
            validateNumericInput(this);
        });
    });
});
