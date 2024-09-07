<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeeklySchedule.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.WeeklySchedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/css/WeeklySchedule.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <h1>Weekly Study Schedule</h1>
            <div class="date-navigation">
                <button onclick="changeDay(-1)">Previous Day</button>
                <span id="currentDate">@Model.CurrentDate.ToString("dddd, MMMM dd, yyyy")</span>
                <button onclick="changeDay(1)">Next Day</button>
            </div>
            <div class="table-container">
                <div class="table-wrapper" id="scheduleTable">
                    <div class="morning">
                        <h2>Morning</h2>
                        <table>
                            <thead>
                                <tr>
                                    <th>Time</th>
                                    <th>Task</th>
                                </tr>
                            </thead>
                            <tbody>
                                @for (int i = 4; i < 12; i++)
                                {
                                    <tr>
                                        <td>@i:00 - @i:30</td>
                                        <td>Task for half-hour @i</td>
                                    </tr>
                                    <tr>
                                        <td>@i:30 - @(i + 1):00</td>
                                        <td>Task for half-hour @i:30</td>
                                    </tr>
                                }
                            </tbody>
                        </table>
                    </div>
                    <div class="evening">
                        <h2>Evening</h2>
                        <table>
                            <thead>
                                <tr>
                                    <th>Time</th>
                                    <th>Task</th>
                                </tr>
                            </thead>
                            <tbody>
                                @for (int i = 12; i < 23; i++)
                                {
                                    <tr>
                                        <td>@i:00 - @i:30</td>
                                        <td>Task for half-hour @i</td>
                                    </tr>
                                    <tr>
                                        <td>@i:30 - @(i + 1):00</td>
                                        <td>Task for half-hour @i:30</td>
                                    </tr>
                                }
                                <tr>
                                    <td>23:00 - 23:30</td>
                                    <td>Task for half-hour 23</td>
                                </tr>
                                <tr>
                                    <td>23:30 - 00:00</td>
                                    <td>Task for half-hour 23:30</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </main>
    </form>
            <script src="../Content/js/WeeklySchedule.js"></script>
</body>
</html>
