<%@ Page Title="Q&A" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="QnA.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.QnA.QnA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Q&A
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/QnA.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="qa-container">
        <h2>Ask a Question</h2>
        <div class="form-row">
            <div class="form-group">
                <label for="academicLevel">Academic Level:</label>
                <asp:Textbox ID="academicLevel" runat="server"></asp:Textbox>
            </div>
            <div class="form-group">
                <label for="subjectArea">Subject Area:</label>
                <asp:DropDownList ID="subjectArea" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="question">Question:</label>
            <asp:TextBox ID="question" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="imageUpload">Upload an Image (optional):</label>
            <asp:FileUpload ID="imageUpload" runat="server" />
        </div>
        <asp:Button ID="submitQuestion" runat="server" Text="Submit Question" OnClick="SubmitQuestion_Click" CssClass="btn btn-primary" />
    </div>

    <div class="view-questions">
        <h2>Questions & Answers</h2>
        <div class="filters">
            <div class="form-row">
                <div>
                    <label for="filterAcademicLevel">Academic Level:</label>
                    <asp:DropDownList ID="filterAcademicLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterAcademicLevel_SelectedIndexChanged">
                        <asp:ListItem Text="All Levels" Value=""></asp:ListItem>
                        <asp:ListItem Text="Primary" Value="Primary"></asp:ListItem>
                        <asp:ListItem Text="Middle School" Value="Middle School"></asp:ListItem>
                        <asp:ListItem Text="O Level" Value="O Level"></asp:ListItem>
                        <asp:ListItem Text="A Level" Value="A Level"></asp:ListItem>
                        <asp:ListItem Text="Undergraduate" Value="Undergraduate"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <label for="filterSubjectArea">Subject Area:</label>
                    <asp:TextBox ID="filterSubjectArea" runat="server" AutoPostBack="true" OnTextChanged="FilterSubjectArea_TextChanged"></asp:TextBox>
                    <asp:ListBox ID="subjectSuggestions" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="SubjectSuggestion_Selected"></asp:ListBox>
                </div>
            </div>
        </div>
        <div class="question-container">
            <ul id="questionList" runat="server">
                <!-- Questions will be populated here -->
            </ul>
        </div>
    </div>

    <script src="/Content/js/QnA.js"></script>
</asp:Content>
