<%@ Page Title="Weekly Schedule" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="WeeklySchedule.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.WeeklySchedule" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Content/css/WeeklySchedule.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="schedule-container">
                <div class="input-container">
                    <h2>Add Time Slot</h2>
                    <div class="input-section">
                        <asp:DropDownList ID="DayDropDownList" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="StartTimeDropDownList" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="EndTimeDropDownList" runat="server"></asp:DropDownList>
                        <asp:Button ID="AddTimeSlotButton" runat="server" Text="Add Time Slot" OnClick="AddTimeSlotButton_Click" />
                    </div>
                </div>
                <div class="calendar-container">
                    <asp:Literal ID="WeeklyCalendarLiteral" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="AddTimeSlotButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
