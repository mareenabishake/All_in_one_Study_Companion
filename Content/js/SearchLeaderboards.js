// wwwroot/js/search-leaderboards.js

const sampleData = [
    { rank: 1, name: 'Alice', level: 'Undergraduate', points: 1200 },
    { rank: 2, name: 'Bob', level: 'Postgraduate', points: 1150 },
    { rank: 3, name: 'Charlie', level: 'Undergraduate', points: 1100 },
    { rank: 4, name: 'David', level: 'Postgraduate', points: 1075 },
    { rank: 5, name: 'Eve', level: 'Undergraduate', points: 1050 },
    // Add more sample data as needed
];

function search() {
    const input = document.getElementById('searchInput').value.toLowerCase();
    const levelFilter = document.getElementById('levelFilter').value;

    const filteredData = sampleData.filter(item => {
        const matchesName = item.name.toLowerCase().includes(input);
        const matchesLevel = levelFilter === 'all' || item.level === levelFilter;
        return matchesName && matchesLevel;
    });

    populateLeaderboard(filteredData);
}

function populateLeaderboard(data) {
    const tableBody = document.getElementById('leaderboardBody');
    tableBody.innerHTML = ''; // Clear existing rows

    data.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${item.rank}</td>
            <td>${item.name}</td>
            <td>${item.level}</td>
            <td>${item.points}</td>
        `;
        tableBody.appendChild(row);
    });
}

// Initialize with all data
populateLeaderboard(sampleData);
