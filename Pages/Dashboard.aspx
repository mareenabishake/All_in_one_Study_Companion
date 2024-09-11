﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/css/Dashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <section class="dashboard-section">
                <div class="dashboard-item">
                    <a href="WeeklySchedule.aspx">
                        <div class="dashboard-link">
                            <img src="~/img/schedule-icon.png" alt="Weekly Schedule">
                            <span>Weekly Schedule</span>
                        </div>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="StudyTime.aspx">
                        <div class="dashboard-link">
                            <img src="~/img/studytime-icon.png" alt="Study Time">
                            <span>Study Time</span>
                        </div>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="ExamMarks.aspx">
                        <div class="dashboard-link">
                            <img src="~/img/exammarks-icon.png" alt="Exam Marks">
                            <span>Exam Marks</span>
                        </div>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="QnA.aspx">
                        <div class="dashboard-link">
                            <img src="~/img/qna-icon.png" alt="Q&A">
                            <span>Q&A</span>
                        </div>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="SearchLeaderboards.aspx">
                        <div class="dashboard-link">
                            <img src="~/img/leaderboards-icon.png" alt="Search & Leaderboards">
                            <span>Search & Leaderboards</span>
                        </div>
                    </a>
                </div>
                <div class="dashboard-item">
                    <a href="Settings.aspx">
                        <div class="dashboard-link">
                            <img src="~/img/settings-icon.png" alt="Settings">
                            <span>Settings</span>
                        </div>
                    </a>
                </div>
            </section>
        </main>
        <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" CssClass="logout-button" />
    </form>
</body>
</html>
