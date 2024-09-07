document.addEventListener("DOMContentLoaded", function () {
    // Get the container elements
    const marksTableContainer = document.getElementById("marksTableContainer");
    const targetMarksContainer = document.getElementById("targetMarksContainer");

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
            }
        });
    }

    // Call the function to initialize charts
    initializeCharts();

    // Function to dynamically update the Marks Table with 14 rows
    function updateMarksTable() {
        marksTableContainer.innerHTML = ""; // Clear the container

        let tableHTML = `<table>
            <thead>
                <tr>
                    <th>Subject</th>
                    <th>Marks</th>
                    <th>Term 1</th>
                    <th>Term 2</th>
                    <th>Term 3</th>
                </tr>
            </thead>
            <tbody>`;

        // Loop to create 14 rows
        for (let i = 0; i < 14; i++) {
            tableHTML += `<tr>
                <td><input type="text" placeholder="Subject Name" /></td>
                <td><input type="number" placeholder="Marks" /></td>
                <td><input type="number" placeholder="Term 1 Marks" /></td>
                <td><input type="number" placeholder="Term 2 Marks" /></td>
                <td><input type="number" placeholder="Term 3 Marks" /></td>
            </tr>`;
        }

        tableHTML += `</tbody>
        </table>`;

        marksTableContainer.innerHTML = tableHTML;
    }

    // Call the function to initialize the marks table
    updateMarksTable();

    // Add event listeners or any additional JavaScript functionality here
});
