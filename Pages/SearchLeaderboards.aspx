<%@ Page Title="Leaderboards" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="SearchLeaderboards.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.SearchLeaderboards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Leaderboards
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/SearchLeaderboards.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-container">
        <div class="search-container">
            <h2>Search Users</h2>
            <div class="search-box">
                <asp:TextBox ID="SearchBox" runat="server" placeholder="Search by Full Name"></asp:TextBox>
                <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" CssClass="search-button" />
            </div>
            <div id="searchResults" runat="server" class="search-results">
                <!-- Search results will be populated here -->
            </div>
        </div>
        <div class="leaderboard-container">
            <h1>Leaderboards</h1>
            <table class="leaderboard-table">
                <thead>
                    <tr>
                        <th>Rank</th>
                        <th>Username</th>
                        <th>Points</th>
                        <th>Questions Answered</th>
                        <th>Hours Studied</th>
                        <th>Badge</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="LeaderboardRepeater" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Username") %></td>
                                <td><%# Eval("Points") %></td>
                                <td><%# Eval("QuestionsAnswered") %></td>
                                <td><%# Eval("HoursStudied") %></td>
                                <td><img src='<%# Eval("BadgeIMG") %>' alt="Badge" class="badge-icon" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

