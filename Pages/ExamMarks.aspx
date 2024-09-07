<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamMarks.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.ExamMarks" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Exam Marks - All in One Study Companion</title>
    <link href="../Content/css/ExamMarks.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Main Content -->
        <main>
            <h1>Exam Marks</h1>

            <!-- Subjects and Exam Marks Section -->
            <div class="input-section">
                <h2>Enter Subjects and Marks</h2>
                <div class="subject-marks-container">
                    <asp:Repeater ID="SubjectMarksRepeater" runat="server">
                        <HeaderTemplate>
                            <table>
                                <thead>
                                    <tr>
                                        <th>Subject</th>
                                        <th>Marks</th>
                                        <th>Term 1</th>
                                        <th>Term 2</th>
                                        <th>Term 3</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:TextBox ID="txtSubjectName" runat="server" placeholder="Subject Name" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtMarks" runat="server" placeholder="Marks" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTerm1" runat="server" placeholder="Term 1 Marks" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTerm2" runat="server" placeholder="Term 2 Marks" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTerm3" runat="server" placeholder="Term 3 Marks" CssClass="form-control" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Target Marks Section -->
            <div class="input-section">
                <h2>Set Target Marks</h2>
                <div id="targetMarksContainer">
                    <asp:Repeater ID="TargetMarksRepeater" runat="server">
                        <HeaderTemplate>
                            <table>
                                <thead>
                                    <tr>
                                        <th>Subject</th>
                                        <th>Marks</th>
                                        <th>Term 1</th>
                                        <th>Term 2</th>
                                        <th>Term 3</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:TextBox ID="txtTargetSubjectName" runat="server" placeholder="Subject Name" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTargetMarks" runat="server" placeholder="Target Marks" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTargetTerm1" runat="server" placeholder="Term 1 Target" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTargetTerm2" runat="server" placeholder="Term 2 Target" CssClass="form-control" /></td>
                                <td><asp:TextBox ID="txtTargetTerm3" runat="server" placeholder="Term 3 Target" CssClass="form-control" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Study Progress Section -->
            <div class="charts-section">
                <h2>Study Progress</h2>
                <div class="charts-container">
                    <canvas id="hoursStudiedChart" width="400" height="400"></canvas>
                    <canvas id="hoursRemainingChart" width="400" height="400"></canvas>
                </div>
            </div>
        </main>
    </form>

    <!-- JavaScript -->
    <script src="../Content/js/ExamMarks.js"></script>
</body>
</html>
