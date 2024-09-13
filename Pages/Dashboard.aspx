<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Dashboard" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Dashboard - All in One Study Companion</title>
    <link href="../Content/css/Dashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <header class="dashboard-header">
                <h1>Dashboard</h1>
                <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" CssClass="logout-button" />
            </header>
            <main>
                <section class="dashboard-section">
                    <div class="dashboard-item">
                        <a href="WeeklySchedule.aspx" class="dashboard-link">
                            <img src="../img/schedule-icon.png" alt="Weekly Schedule" runat="server" />
                            <span>Weekly Schedule</span>
                        </a>
                    </div>
                    <div class="dashboard-item">
                        <a href="StudyTime.aspx" class="dashboard-link">
                            <img src="../img/studytime-icon.png" alt="Study Time" runat="server" />
                            <span>Study Time</span>
                        </a>
                    </div>
                    <div class="dashboard-item">
                        <a href="ExamMarks.aspx" class="dashboard-link">
                            <img src="../img/exammarks-icon.png" alt="Exam Marks" runat="server" />
                            <span>Exam Marks</span>
                        </a>
                    </div>
                    <div class="dashboard-item">
                        <a href="QnA/QnA.aspx" class="dashboard-link">
                            <img src="../img/qna-icon.png" alt="Q&A" runat="server" />
                            <span>Q&A</span>
                        </a>
                    </div>
                    <div class="dashboard-item">
                        <a href="SearchLeaderboards.aspx" class="dashboard-link">
                            <img src="../img/leaderboards-icon.png" alt="Search & Leaderboards" runat="server" />
                            <span>Search & Leaderboards</span>
                        </a>
                    </div>
                    <div class="dashboard-item">
                        <a href="Settings.aspx" class="dashboard-link">
                            <img src="../img/settings-icon.png" alt="Settings" runat="server" />
                            <span>Settings</span>
                        </a>
                    </div>
                </section>
            </main>
        </div>
    </form>
</body>
</html>
