<%@ Page Title="Exam Marks" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ExamMarks.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.ExamMarks" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <!-- Add reference to custom CSS file -->
    <link rel="stylesheet" href="/Content/css/ExamMarks.css" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
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
        <div id="studyTimePieChart" style="width: 900px; height: 500px;"></div>
    </div>

    <!-- Debug Data Literal -->
    <script type="text/javascript">
        google.charts.load('current', {'packages':['corechart']});
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var jsonData = <%= StudyTimeJsonData %>;
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Subject');
            data.addColumn('number', 'Total Duration');

            jsonData.forEach(function(item) {
                data.addRow([item.SubjectName, item.TotalDuration]);
            });

            var options = {
                title: 'Total Study Time by Subject',
                is3D: true,
            };

            var chart = new google.visualization.PieChart(document.getElementById('studyTimePieChart'));
            chart.draw(data, options);
        }
    </script>

    <!-- No Data Label -->
    <asp:Label ID="NoDataLabel" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
