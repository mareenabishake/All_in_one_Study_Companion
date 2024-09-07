<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyTime.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.StudyTime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Study Time</title>
    <link href="../Content/css/StudyTime.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <h1>Study Time</h1>
            <div id="currentTimeContainer">
                <div id="currentTime"></div>
                <div id="dayOfWeek"></div>
                <div id="currentDate"></div>
            </div>
            <div class="study-time-container">
                <div class="timer-container">
                    <div class="timer-circle">
                        <span id="timeRemaining">00:00</span>
                    </div>
                </div>
                <div class="controls-container">
                    <div class="input-container">
                        <label for="hours">Hours:</label>
                        <input type="number" id="hours" min="0" value="0">
                        <label for="minutes">Minutes:</label>
                        <input type="number" id="minutes" min="0" value="0">
                    </div>
                    <div class="dropdown-container">
                        <label for="subject">Subject:</label>
                        <select id="subject">
                            <option value="Math">Math</option>
                            <option value="Science">Science</option>
                            <option value="History">History</option>
                        </select>
                    </div>
                    <p id="totalDuration">Total Duration: 00:00</p>
                    <button id="startButton">Start</button>
                    <button id="stopButton">Stop</button>
                    <button id="resetButton">Reset</button>
                </div>
            </div>
        </main>
    </form>
            <script src="../Content/js/StudyTime.js"></script>
</body>
</html>
