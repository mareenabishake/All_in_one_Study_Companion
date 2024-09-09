<%@ Page Title="Search & Leaderboards" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="SearchLeaderboards.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.SearchLeaderboards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Search & Leaderboards
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/SearchLeaderboards.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
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

    <script src="~/Content/js/SearchLeaderboards.js"></script>
</asp:Content>

