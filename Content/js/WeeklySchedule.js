let currentDate = new Date();

function changeDay(direction) {
    currentDate.setDate(currentDate.getDate() + direction);
    updateDateAndSchedule();
}

function updateDateAndSchedule() {
    document.getElementById('currentDate').textContent = currentDate.toDateString();

    const scheduleTable = document.getElementById('scheduleTable');

    const morningRows = [];
    for (let i = 4; i < 12; i++) {
        morningRows.push(`<tr><td>${i}:00 - ${i}:30</td><td>Task for half-hour ${i}</td></tr>`);
        morningRows.push(`<tr><td>${i}:30 - ${(i + 1) % 24}:00</td><td>Task for half-hour ${i}:30</td></tr>`);
    }

    const eveningRows = [];
    for (let i = 12; i < 23; i++) {
        eveningRows.push(`<tr><td>${i}:00 - ${i}:30</td><td>Task for half-hour ${i}</td></tr>`);
        eveningRows.push(`<tr><td>${i}:30 - ${(i + 1) % 24}:00</td><td>Task for half-hour ${i}:30</td></tr>`);
    }


    scheduleTable.innerHTML = `
        <div class="morning">
            <h2>Morning</h2>
            <table>
                <thead>
                    <tr><th>Time</th><th>Task</th></tr>
                </thead>
                <tbody>${morningRows.join('')}</tbody>
            </table>
        </div>
        <div class="evening">
            <h2>Evening</h2>
            <table>
                <thead>
                    <tr><th>Time</th><th>Task</th></tr>
                </thead>
                <tbody>${eveningRows.join('')}</tbody>
            </table>
        </div>
    `;
}

document.addEventListener('DOMContentLoaded', () => {
    updateDateAndSchedule();
});
