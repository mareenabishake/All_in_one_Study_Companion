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

    // Function to draw study time pie chart
    function drawStudyTimePieChart(data) {
        console.log('Drawing pie chart with data:', data);
        const ctx = document.getElementById('studyTimePieChart');
        if (!ctx) {
            console.error('Canvas element not found');
            return;
        }

        try {
            const labels = data.map(item => item.SubjectName);
            const values = data.map(item => item.TotalDuration);

            console.log('Labels:', labels);
            console.log('Values:', values);

            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        data: values,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.8)',
                            'rgba(54, 162, 235, 0.8)',
                            'rgba(255, 206, 86, 0.8)',
                            'rgba(75, 192, 192, 0.8)',
                            'rgba(153, 102, 255, 0.8)',
                            'rgba(255, 159, 64, 0.8)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Total Study Time by Subject (hours)'
                        }
                    }
                }
            });
            console.log('Chart drawn successfully');
        } catch (error) {
            console.error('Error drawing chart:', error);
        }
    }
});
