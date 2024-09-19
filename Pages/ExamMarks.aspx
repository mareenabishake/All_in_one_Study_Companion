<%@ Page Title="Exam Marks" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ExamMarks.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.ExamMarks" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="Title" runat="server">
    Exam Marks
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <!-- Add reference to custom CSS file -->
    <link rel="stylesheet" href="/Content/css/ExamMarks.css" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <!-- Input Section -->
    <div class="input-section">
        <h3>Enter Subject and Marks</h3>
        <div class="form-group">
            <label for="SubjectName">Subject Name:</label>
            <asp:TextBox ID="SubjectName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="CurrentMark">Current Mark:</label>
            <asp:TextBox ID="CurrentMark" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TargetMark">Target Mark:</label>
            <asp:TextBox ID="TargetMark" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>
        <asp:Button ID="SaveMarks" runat="server" Text="Save Marks" OnClick="SaveMarks_Click" CssClass="btn btn-primary mt-3" />
    </div>

    <!-- Exam Marks Table Section -->
    <div class="table-section mt-4">
        <h3>Exam Marks</h3>
        <asp:GridView ID="ExamMarksGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="CurrentMark" HeaderText="Current Mark" />
                <asp:BoundField DataField="TargetMark" HeaderText="Target Mark" />
            </Columns>
        </asp:GridView>
    </div>

    <!-- Charts Section -->
    <div class="charts-section mt-4">
        <h3>Study Progress</h3>
        <div class="row">
            <div class="col-md-6">
                <canvas id="hoursStudiedChart"></canvas>
            </div>
            <div class="col-md-6">
                <canvas id="hoursRemainingChart"></canvas>
            </div>
        </div>
    </div>

    <!-- Include Chart.js -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <!-- Include your custom JavaScript file -->
    <script src="~/Content/js/ExamMarks.js"></script>

    <!-- No Data Label -->
    <asp:Label ID="NoDataLabel" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
