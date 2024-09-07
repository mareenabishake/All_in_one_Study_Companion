﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandIn.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Account.LandIn"%>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>All in One Study Companion</title>
    <link href="../../Content/css/LandIn.css" rel="stylesheet" />
</head>
<body>
    <header>
        <div class="container">
            <div class="logo">Study Companion</div>
            <nav>
                <ul>
                    <li><a href="/">Home</a></li>
                    <li>
                        <a href="/WeeklySchedule/WeeklySchedulePage">Weekly Schedule</a>
                    </li>
                    <li><a href="/StudyTime/StudyTimePage">Study Time</a></li>
                    <li><a href="/Dashboard/DashboardPage">Dashboard</a></li>
                    <li><a href="/QnA/QnAPage">Q&A</a></li>
                    <li><a href="/SearchLeaderboards/SearchLeaderboardsPage">Search & Leaderboards</a></li>
                    <li><a href="/Settings">Settings</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <section id="hero">
        <div class="container">
            <h1>Welcome to your All in One Study Companion</h1>
            <p>Your personalized learning platform with AI scheduling, gamification, and peer collaboration.</p>

            <!-- Updated Login Form -->
            <form asp-action="Login" method="post">
                <div class="form-group">
                    <input type="text" class="form-control" asp-for="Username" placeholder="Username" />
                </div>
                <div class="form-group">
                    <input type="password" class="form-control" asp-for="Password" placeholder="Password" />
                </div>
                <button type="submit" class="btn">Log In</button>
            </form>

            <div class="signup-text">
                <span>Please <a href="/Signup">Sign up</a> if you don't have an account.</span>
                <a href="Signup.aspx" class="btn signup-btn">Sign Up</a>
            </div>
        </div>
    </section>

    <section id="features">
        <div class="container">
            <h2>Key Features</h2>
            <div class="features-container">
                <div class="feature">
                    <img id="aiScheduling" src="~/img/ai-scheduling-icon.png" alt="AI Scheduling">
                    <h3>AI Scheduling</h3>
                    <p>Dynamic schedules tailored to your needs.</p>
                </div>
                <div class="feature">
                    <img src="~/img/progress-tracking-icon.png" alt="Progress Tracking">
                    <h3>Progress Tracking</h3>
                    <p>Monitor your study progress and achievements.</p>
                </div>
                <div class="feature">
                    <img id="gamifiedLearning" src="~/img/gamified-learning-icon.jpg" alt="Gamified Learning">
                    <h3>Gamified Learning</h3>
                    <p>Engage and motivate yourself with gamified elements.</p>
                </div>
                <div class="feature">
                    <img src="~/img/peer-support-icon.jpg" alt="Peer Support">
                    <h3>Peer Support</h3>
                    <p>Collaborate and learn with your peers.</p>
                </div>
            </div>
        </div>
    </section>

    <section id="how-it-works">
        <div class="container">
            <h2>How It Works</h2>
            <div class="steps">
                <div class="step">
                    <h3>Step 1</h3>
                    <p>Sign Up</p>
                </div>
                <div class="step">
                    <h3>Step 2</h3>
                    <p>Set Your Goals</p>
                </div>
                <div class="step">
                    <h3>Step 3</h3>
                    <p>Follow the Dynamic Schedule</p>
                </div>
                <div class="step">
                    <h3>Step 4</h3>
                    <p>Engage with Peers</p>
                </div>
            </div>
        </div>
    </section>

    <section id="testimonials">
        <div class="container">
            <h2>Testimonials</h2>
            <div class="testimonial">
                <p>"This platform has revolutionized my study habits!" - Student A</p>
            </div>
            <div class="testimonial">
                <p>"The AI scheduling feature is a game-changer." - Student B</p>
            </div>
        </div>
    </section>

    <footer>
        <div class="container">
            <p>&copy; 2024 All in One Study Companion. All rights reserved.</p>
            <ul>
                <li><a href="#contact">Contact Information</a></li>
                <li><a href="#privacy">Privacy Policy</a></li>
                <li><a href="#terms">Terms of Service</a></li>
            </ul>
        </div>
    </footer>
    <script src="../../Content/js/LandIn.js"></script>
</body>
</html>

