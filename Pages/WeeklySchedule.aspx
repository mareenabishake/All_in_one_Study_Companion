<%@ Page Title="Weekly Schedule" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="WeeklySchedule.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.WeeklySchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Content/css/WeeklySchedule.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="weekly-schedule-section">
        <h1>Weekly Schedule</h1>
        <div class="input-section">
            <asp:DropDownList ID="DayDropDownList" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="StartTimeDropDownList" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="EndTimeDropDownList" runat="server"></asp:DropDownList>
            <asp:Button ID="AddTimeSlotButton" runat="server" Text="Add Time Slot" OnClick="AddTimeSlotButton_Click" />
        </div>
        <asp:Literal ID="WeeklyCalendarLiteral" runat="server" />
    </div>
</asp:Content>
