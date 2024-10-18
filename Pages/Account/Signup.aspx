<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Account.Signup" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign Up - All in One Study Companion</title>
    <!-- Link to the CSS file for styling -->
    <link href="../../Content/css/Signup.css" rel="stylesheet" />
</head>
<body>
    <!-- Header section -->
    <header>
        <div class="container">
            <div class="logo">Study Companion</div>
            <!-- Navigation menu -->
            <nav>
                <ul>
                    <li><a href="#hero">Home</a></li>
                    <li><a href="#features">Features</a></li>
                    <li><a href="#how-it-works">How It Works</a></li>
                    <li><a href="#testimonials">Testimonials</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <!-- Sign up form container -->
    <div class="signup-container">
        <h1>Sign Up</h1>
        <form id="form1" runat="server">
            <!-- Full Name input -->
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFullName">Full Name</asp:Label>
                <asp:TextBox ID="txtFullName" runat="server" required="required"></asp:TextBox>
            </div>
            <!-- Institution and Academic Level inputs -->
            <div class="form-row">
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="txtInstitution">Institution</asp:Label>
                    <asp:TextBox ID="txtInstitution" runat="server" required="required"></asp:TextBox>
                </div>
                <div class="form-group half-width">
                    <asp:Label runat="server" AssociatedControlID="ddlAcademicLevel">Academic Level</asp:Label>
                    <asp:DropDownList ID="ddlAcademicLevel" runat="server" required="required">
                        <asp:ListItem Value="">Select Academic Level</asp:ListItem>
                        <asp:ListItem Value="Primary">Primary</asp:ListItem>
                        <asp:ListItem Value="Middle School">Middle School</asp:ListItem>
                        <asp:ListItem Value="O Level">O Level</asp:ListItem>
                        <asp:ListItem Value="A Level">A Level</asp:ListItem>
                        <asp:ListItem Value="Undergraduate">Undergraduate</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <!-- Username and Email inputs -->
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
            <!-- Password and Confirm Password inputs -->
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
            <!-- Sign Up button -->
            <asp:Button ID="signUpbtn" runat="server" Text="Sign Up" CssClass="btn" OnClick="btnSignUp_Click" />
            <!-- Link to login page -->
            <p>Already have an account? <a href="../Account/LandIn.aspx">Log In</a></p>
        </form>
    </div>
    
    <!-- Footer section -->
    <footer>
        <div class="container">
            <p>&copy; 2024 All in One Study Companion. All rights reserved.</p>
            <nav class="footer-nav">
                <a href="#contact">Contact</a> |
                <a href="#privacy">Privacy</a> |
                <a href="#terms">Terms</a>
            </nav>
        </div>
    </footer>
</body>
</html>