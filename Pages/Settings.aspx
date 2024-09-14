<%@ Page Title="User Settings" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    User Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/css/Settings.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-container">
        
        <asp:Panel ID="pnlUserInfo" runat="server" CssClass="panel">
            <h2>Update User Information</h2>
            <div class="form-group">
                <asp:Label ID="lblFullName" runat="server" AssociatedControlID="txtFullName">Full Name:</asp:Label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblInstitution" runat="server" AssociatedControlID="txtInstitution">Institution:</asp:Label>
                <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblAcademicLevel" runat="server" AssociatedControlID="ddlAcademicLevel">Academic Level:</asp:Label>
                <asp:DropDownList ID="ddlAcademicLevel" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Academic Level" Value=""></asp:ListItem>
                    <asp:ListItem Text="Primary" Value="Primary"></asp:ListItem>
                    <asp:ListItem Text="Middle School" Value="Middle School"></asp:ListItem>
                    <asp:ListItem Text="O Level" Value="O Level"></asp:ListItem>
                    <asp:ListItem Text="A Level" Value="A Level"></asp:ListItem>
                    <asp:ListItem Text="Undergraduate" Value="Undergraduate"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername">Username:</asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword">New Password:</asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword">Confirm New Password:</asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Information" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        </asp:Panel>
        
        <div class="right-panel">
            <asp:Panel ID="pnlDeleteAccount" runat="server" CssClass="panel">
                <h2>Delete Account</h2>
                <p>This action cannot be undone.</p>
                <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete Account" CssClass="btn btn-danger" OnClick="btnDeleteAccount_Click" OnClientClick="return confirm('Are you sure you want to delete your account? This action cannot be undone.');" />
            </asp:Panel>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-secondary btn-logout" OnClick="btnLogout_Click" />
        </div>
    </div>
</asp:Content>
