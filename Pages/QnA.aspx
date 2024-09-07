<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QnA.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.QnA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/css/QnA.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <main>
            <div class="qa-container">
                <h2>Ask a Question</h2>
                <div class="ask-question form-container">
                    <div class="inputs-container">
                        <div class="form-group">
                            <label for="academicLevel">Academic Level:</label>
                            <select id="academicLevel">
                                <option value="highschool">High School</option>
                                <option value="undergraduate">Undergraduate</option>
                                <option value="postgraduate">Postgraduate</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="subjectArea">Subject Area:</label>
                            <select id="subjectArea">
                                <option value="math">Math</option>
                                <option value="science">Science</option>
                                <option value="history">History</option>
                            </select>
                        </div>
                    </div>
                    <div class="text-container">
                        <div class="form-group">
                            <label for="question">Question:</label>
                            <textarea id="question" rows="5"></textarea>
                        </div>
                        <div class="form-group">
                            <label for="imageUpload">Upload an Image (optional):</label>
                            <input type="file" id="imageUpload">
                        </div>
                        <button id="submitQuestion" class="styled-button">Submit</button>
                    </div>
                </div>
            </div>

            <div class="view-questions">
                <h2>Questions & Answers</h2>
                <div class="filters">
                    <div>
                        <label for="filterAcademicLevel">Academic Level:</label>
                        <select id="filterAcademicLevel">
                            <option value="">All Levels</option>
                            <option value="highschool">High School</option>
                            <option value="undergraduate">Undergraduate</option>
                            <option value="postgraduate">Postgraduate</option>
                        </select>
                    </div>
                    <div>
                        <label for="filterSubjectArea">Subject Area:</label>
                        <select id="filterSubjectArea">
                            <option value="">All Subjects</option>
                            <option value="math">Math</option>
                            <option value="science">Science</option>
                            <option value="history">History</option>
                        </select>
                    </div>
                </div>
                <ul id="questionList">
                    <!-- Sample questions for display -->
                    <li>
                        <h3>How do I solve this math problem?</h3>
                        <p><strong>Level:</strong> High School | <strong>Subject:</strong> Math</p>
                        <p>Can someone explain how to solve quadratic equations?</p>
                        <div class="button-container">
                            <button class="answer-button">Answer</button>
                            <button class="view-answer-button">View Answer</button>
                        </div>
                    </li>
                    <li>
                        <h3>What is the chemical formula for water?</h3>
                        <p><strong>Level:</strong> Undergraduate | <strong>Subject:</strong> Science</p>
                        <p>I'm confused about how to write chemical formulas. Can anyone help?</p>
                        <div class="button-container">
                            <button class="answer-button">Answer</button>
                            <button class="view-answer-button">View Answer</button>
                        </div>
                    </li>
                    <li>
                        <h3>Why did the Roman Empire fall?</h3>
                        <p><strong>Level:</strong> Postgraduate | <strong>Subject:</strong> History</p>
                        <p>I'm looking for detailed reasons behind the fall of the Roman Empire.</p>
                        <div class="button-container">
                            <button class="answer-button">Answer</button>
                            <button class="view-answer-button">View Answer</button>
                        </div>
                    </li>
                </ul>
            </div>
        </main>
    </form>
           <script src="../Content/js/QnA.js"></script>
</body>
</html>
