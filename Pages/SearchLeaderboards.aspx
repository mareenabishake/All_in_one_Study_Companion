<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchLeaderboards.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.SearchLeaderboards" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/css/SearchLeaderboards.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <h1>Search & Leaderboards</h1>
            <div class="search-container">
                <input type="text" id="searchInput" placeholder="Search by name...">
                <select id="levelFilter">
                    <option value="all">All Levels</option>
                    <option value="Undergraduate">Undergraduate</option>
                    <option value="Postgraduate">Postgraduate</option>
                </select>
                <button onclick="search()">Search</button>
            </div>
            <div class="leaderboard-container">
                <div class="leaderboard-header">Leaderboards</div>
                <table class="leaderboard-table">
                    <thead>
                        <tr>
                            <th>Rank</th>
                            <th>Name</th>
                            <th>Academic Level</th>
                            <th>Points</th>
                        </tr>
                    </thead>
                    <tbody id="leaderboardBody">
                        <!-- Leaderboard rows will be populated here -->
                    </tbody>
                </table>
            </div>
        </main>
    </form>
            <script src="../Content/js/SearchLeaderboards.js"></script>
</body>
</html>
