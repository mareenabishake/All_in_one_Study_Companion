<%@ Page Title="View Answers" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ViewAnswers.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.QnA.ViewAnswers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    View Answers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/ViewAnswers.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="view-answers-container">
        <h2>Question:</h2>
        <asp:Label ID="QuestionText" runat="server" CssClass="question-text"></asp:Label>

        <h3>Answers:</h3>
        <asp:Repeater ID="AnswersRepeater" runat="server">
            <ItemTemplate>
                <div class="answer">
                    <p><%# Eval("AnswerText") %></p>
                    <asp:Image ID="AnswerImage" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Visible='<%# !string.IsNullOrEmpty(Eval("ImagePath") as string) %>' />
                    <small>Answered by: <%# Eval("UserName") %></small>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
