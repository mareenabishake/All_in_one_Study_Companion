<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandIn.aspx.cs" Inherits="All_in_one_Study_Companion.Pages.Account.LandIn"%>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>All in One Study Companion</title>
    <!-- Link to the CSS file for styling -->
    <link href="../../Content/css/LandIn.css" rel="stylesheet" />
</head>
<body>
    <!-- Header section -->
    <header>
        <div class="top-container">
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

    <!-- Hero section with login form -->
    <section id="hero">
        <div class="container">
            <h1>Welcome to your All in One Study Companion</h1>
            <p>Your personalized learning platform with AI scheduling, gamification, and peer collaboration.</p>

            <!-- Login Form -->
            <form id="loginForm" runat="server">
                <div class="form-group">
                    <asp:TextBox ID="Username" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Log In" CssClass="btn" OnClick="btnLogin_Click" />
            </form>
            <!-- Sign up link -->
            <div class="signup-text">
                <span>Please <a href="/Signup">Sign up</a> if you don't have an account.</span>
                <a href="Signup.aspx" class="btn signup-btn">Sign Up</a>
            </div>
        </div>
    </section>

    <!-- Features section -->
    <section id="features">
        <div class="container">
            <h2>Key Features</h2>
            <div class="features-container">
                <!-- AI Scheduling feature -->
                <div class="feature">
                    <img id="aiScheduling" src="../../Content/img/ai-scheduling-icon.png" alt="AI Scheduling">
                    <h3>AI Scheduling</h3>
                    <p>Dynamic schedules tailored to your needs.</p>
                </div>
                <!-- Progress Tracking feature -->
                <div class="feature">
                    <img src="../../Content/img/progress-tracking-icon.png" alt="Progress Tracking">
                    <h3>Progress Tracking</h3>
                    <p>Monitor your study progress and achievements.</p>
                </div>
                <!-- Gamified Learning feature -->
                <div class="feature">
                    <img id="gamifiedLearning" src="../../Content/img/gamified-learning-icon.jpg" alt="Gamified Learning">
                    <h3>Gamified Learning</h3>
                    <p>Engage and motivate yourself with gamified elements.</p>
                </div>
                <!-- Peer Support feature -->
                <div class="feature">
                    <img src="../../Content/img/peer-support-icon.jpg" alt="Peer Support">
                    <h3>Peer Support</h3>
                    <p>Collaborate and learn with your peers.</p>
                </div>
            </div>
        </div>
    </section>

    <!-- How It Works section -->
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

    <!-- Testimonials section -->
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

    <!-- Footer section -->
    <footer>
        <div class="bottom-container">
            <p>&copy; 2024 All in One Study Companion. All rights reserved.</p>
            <nav class="footer-nav">
                <a href="#contact">Contact</a> |
                <a href="#privacy">Privacy</a> |
                <a href="#terms">Terms</a>
            </nav>
        </div>
    </footer>
    <!-- Link to JavaScript file -->
    <script src="../../Content/js/LandIn.js"></script>
</body>
</html>

