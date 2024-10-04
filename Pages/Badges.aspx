<%@ Page Title="Badges" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Badges.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Badges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Badges
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/Badges.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="badges-container">
        <h1>Badges</h1>
        <asp:Button ID="FetchBadgesButton" runat="server" Text="Fetch Badges" OnClick="FetchBadgesButton_Click" CssClass="fetch-badges-button" />
        <asp:Button ID="ClaimBadgesButton" runat="server" Text="Claim Badges" OnClick="ClaimBadgesButton_Click" CssClass="claim-badges-button" Enabled="false" />
        <div class="badges-grid">
            <asp:Repeater ID="BadgesRepeater" runat="server">
                <ItemTemplate>
                    <div class="badge-item <%# Convert.ToBoolean(Eval("IsEarned")) ? "earned" : "not-earned" %>">
                        <img src='/Content/img/<%# Eval("BadgeID") %>.png' alt='<%# Eval("BadgeName") %>' class="badge-image" />
                        <h3><%# Eval("BadgeName") %></h3>
                        <p class="badge-type"><%# Eval("BadgeType") %></p>
                        <p class="badge-description"><%# Eval("BadgeDescription") %></p>
                        <p class="badge-requirements">
                            Answers Required: <%# Eval("AnswersRequired") %><br />
                            Minutes Required: <%# Eval("MinutesRequired") %>
                        </p>
                        <p class="badge-status"><%# Convert.ToBoolean(Eval("IsEarned")) ? "Earned" : "Not Earned" %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
