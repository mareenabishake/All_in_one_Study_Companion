<%@ Page Title="View Answers" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ViewAnswers.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.QnA.ViewAnswers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    View Answers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Link to the CSS file for styling -->
    <link href="/Content/css/ViewAnswers.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="view-answers-container">
        <!-- Display the question text -->
        <h2>Question:</h2>
        <asp:Label ID="QuestionText" runat="server" CssClass="question-text"></asp:Label>

        <!-- Display answers using a Repeater control -->
        <h3>Answers:</h3>
        <asp:Repeater ID="AnswersRepeater" runat="server">
            <ItemTemplate>
                <div class="answer">
                    <!-- Display answer text -->
                    <p><%# Eval("AnswerText") %></p>
                    <!-- Display answer image if available -->
                    <asp:Image ID="AnswerImage" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Visible='<%# !string.IsNullOrEmpty(Eval("ImagePath") as string) %>' />
                    <!-- Display answerer's username -->
                    <small>Answered by: <%# Eval("UserName") %></small>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
