<%@ Page Title="Study Time" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="StudyTime.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.StudyTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Study Time
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/StudyTime.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Study Time</h1>
    <div id="currentTimeContainer">
        <span id="dayOfWeek"></span>
        <span id="currentTime"></span>
        <span id="currentDate"></span>
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
                <input type="number" id="hours" min="0" value="0" />
                <label for="minutes">Minutes:</label>
                <input type="number" id="minutes" min="0" value="0" />
            </div>
            <div class="dropdown-container">
                <label for="subject">Subject:</label>
                <select id="subject">
                    <!-- Options will be populated dynamically -->
                </select>
            </div>
            <span id="totalDuration">Total Duration: 00:00</span>
            <div class="btn-container">
                <button id="startButton" class="btn">Start</button>
                <button id="stopButton" class="btn">Stop</button>
                <button id="resetButton" class="btn">Reset</button>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <script src="/Content/js/StudyTime.js"></script>
</asp:Content>

