<%@ Page Title="Answer Question" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Answer.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.QnA.Answer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Answer Question
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/Answers.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="answer-question-container">
        <div class="question-container">
            <h2>Question:</h2>
            <asp:Label ID="QuestionText" runat="server" CssClass="question-text"></asp:Label>
            <asp:Image ID="QuestionImage" runat="server" CssClass="question-image" />
        </div>

        <div class="answer-container">
            <h3>Your Answer:</h3>
            <asp:TextBox ID="AnswerText" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="file-upload">
            <asp:FileUpload ID="AnswerFileUpload" runat="server" />
        </div>
        <asp:HiddenField ID="QuestionID" runat="server" />
        <asp:Button ID="ConfirmAnswer" runat="server" Text="Confirm Answer" OnClick="ConfirmAnswer_Click" CssClass="btn btn-primary" />
    </div>
</asp:Content>
