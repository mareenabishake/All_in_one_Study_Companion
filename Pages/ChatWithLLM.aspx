<%@ Page Title="Chat with LLM" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ChatWithLLM.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.ChatWithLLM" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Chat with LLM - Study Companion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/ChatWithLLM.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <main>
        <div class="chat-window">
            <div class="chat-header">
                Chat with Open Source LLMs
                <asp:Button ID="ClearChatButton" runat="server" Text="Clear Chat" OnClick="ClearChatButton_Click" CssClass="clear-chat-button" />
            </div>
            <asp:UpdatePanel ID="ChatUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="chatHistory" runat="server" class="chat-history">
                        <!-- Chat messages will be displayed here -->
                    </div>
                    <div class="chat-input">
                        <asp:TextBox ID="MessageInput" runat="server" placeholder="Type your message..."></asp:TextBox>
                        <asp:Button ID="SendButton" runat="server" Text="Send" OnClick="SendButton_Click" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="SendButton" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </main>
</asp:Content>
