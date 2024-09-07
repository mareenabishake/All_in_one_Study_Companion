<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Account.Signup" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign Up - All in One Study Companion</title>
    <link href="../../Content/css/Signup.css" rel="stylesheet" />
</head>
<body>
    <header>
        <div class="container">
            <div class="logo">Study Companion</div>
            <nav>
                <ul>
                    <li><a href="/">Home</a></li>
                    <li><a href="/WeeklySchedule">Weekly Schedule</a></li>
                    <li><a href="/StudyTime">Study Time</a></li>
                    <li><a href="/ExamMarks">Exam Marks</a></li>
                    <li><a href="/PostQuestionsAnswers">Q&A</a></li>
                    <li><a href="/SearchLeaderboards">Search & Leaderboards</a></li>
                    <li><a href="/Settings">Settings</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <div class="signup-container">
        <h1>Sign Up</h1>
        <form id="form1" runat="server">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFullName">Full Name</asp:Label>
                <asp:TextBox ID="txtFullName" runat="server" required="required"></asp:TextBox>
            </div>
            <div class="form-row">
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="txtInstitution">Institution</asp:Label>
                    <asp:TextBox ID="txtInstitution" runat="server" required="required"></asp:TextBox>
                </div>
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="ddlAcademicLevel">Academic Level</asp:Label>
                    <asp:DropDownList ID="ddlAcademicLevel" runat="server" required="required">
                        <asp:ListItem Value="">Select Academic Level</asp:ListItem>
                        <asp:ListItem Value="primary">Primary</asp:ListItem>
                        <asp:ListItem Value="middleSchool">Middle School</asp:ListItem>
                        <asp:ListItem Value="olevel">O Level</asp:ListItem>
                        <asp:ListItem Value="alevel">A Level</asp:ListItem>
                        <asp:ListItem Value="undergraduate">Undergraduate</asp:ListItem>
                        <asp:ListItem Value="postgraduate">Postgraduate</asp:ListItem>
                        <asp:ListItem Value="doctoral">Doctoral</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="txtUsername">Username</asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server" required="required"></asp:TextBox>
                </div>
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="txtEmail">Email</asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" required="required"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required="required"></asp:TextBox>
                </div>
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="txtConfirmPassword">Confirm Password</asp:Label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" required="required" OnTextChanged="txtConfirmPassword_TextChanged"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="signUpbtn" runat="server" Text="Sign Up" CssClass="btn" OnClick="btnSignUp_Click" />
            <p>Already have an account? <a href="/index">Log In</a></p>
        </form>
    </div>
</body>
</html>