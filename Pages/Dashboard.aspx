<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Dashboard - Study Companion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/Dashboard.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="dashboard-container">
        <div class="dashboard-header">
            <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" CssClass="logout-button" />
        </div>
        <main>
            <section class="dashboard-section">
                <div class="dashboard-item">
                    <a href="WeeklySchedule.aspx" class="dashboard-link">
                        <img src="../Content/img/weeklySchedule.jpeg" alt="Weekly Schedule" />
                        <span>Weekly Schedule</span>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="StudyTime.aspx" class="dashboard-link">
                        <img src="../Content/img/StudyTime.jpg" alt="Study Time" />
                        <span>Study Time</span>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="ExamMarks.aspx" class="dashboard-link">
                        <img src="../Content/img/ExamMarks.jpg" alt="Exam Marks" />
                        <span>Exam Marks</span>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="QnA/QnA.aspx" class="dashboard-link">
                        <img src="../Content/img/QnA.png" alt="Q&A" />
                        <span>Q&A</span>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="SearchLeaderboards.aspx" class="dashboard-link">
                        <img src="../Content/img/Leaderboards.jpg" alt="Search & Leaderboards" />
                        <span>Search & Leaderboards</span>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="Settings.aspx" class="dashboard-link">
                        <img src="../Content/img/settings.png" alt="Settings" />
                        <span>Settings</span>
                    </a>
                </div>
            </section>
        </main>
    </div>
</asp:Content>
