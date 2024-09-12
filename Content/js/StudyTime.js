let timer;
let totalSeconds;
let remainingSeconds;
let initialDuration;

const startButton = document.getElementById('startButton');
const stopButton = document.getElementById('stopButton');
const resetButton = document.getElementById('resetButton');
const timeRemaining = document.getElementById('timeRemaining');
const totalDuration = document.getElementById('totalDuration');
const subjectSelect = document.getElementById('subject');

// Add this function at the beginning of your file
function populateSubjectDropdown() {
    const subjectSelect = document.getElementById('subject');
    subjectSelect.innerHTML = ''; // Clear existing options

    if (typeof userSubjects !== 'undefined' && userSubjects.length > 0) {
        userSubjects.forEach(subject => {
            const option = document.createElement('option');
            option.value = subject;
            option.textContent = subject;
            subjectSelect.appendChild(option);
        });
    } else {
        const option = document.createElement('option');
        option.value = '';
        option.textContent = 'No subjects available';
        subjectSelect.appendChild(option);
    }
}

// Call this function when the page loads
document.addEventListener('DOMContentLoaded', populateSubjectDropdown);

startButton.addEventListener('click', startTimer);
stopButton.addEventListener('click', stopTimer);
resetButton.addEventListener('click', resetTimer);

function startTimer() {
    const hours = parseInt(document.getElementById('hours').value, 10);
    const minutes = parseInt(document.getElementById('minutes').value, 10);

    if (isNaN(hours) || isNaN(minutes) || hours < 0 || minutes < 0) {
        alert("Please enter valid hours and minutes (positive numbers).");
        return;
    }

    totalSeconds = (hours * 3600) + (minutes * 60);
    remainingSeconds = totalSeconds;
    initialDuration = `${hours} hours and ${minutes} minutes`;

    totalDuration.textContent = `Total Duration: ${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;

    if (!timer) {
        timer = setInterval(() => {
            remainingSeconds--;
            updateTimerDisplay();

            if (remainingSeconds <= 0) {
                clearInterval(timer);
                timer = null;
                const subject = subjectSelect.value;
                const duration = totalSeconds / 60; // Convert to minutes
                saveStudyRecord(subject, duration);
            }
        }, 1000);
    }

    startButton.disabled = true;
    stopButton.disabled = false;
    resetButton.disabled = false;
}

function stopTimer() {
    if (timer) {
        clearInterval(timer);
        timer = null;
    }
    startButton.disabled = false;
    stopButton.disabled = true;
}

function resetTimer() {
    stopTimer();
    remainingSeconds = totalSeconds;
    updateTimerDisplay();
    startButton.disabled = false;
    stopButton.disabled = true;
    resetButton.disabled = true;
}

function updateTimerDisplay() {
    const minutes = Math.floor(remainingSeconds / 60);
    const seconds = remainingSeconds % 60;
    timeRemaining.textContent = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;

    const progress = (totalSeconds - remainingSeconds) / totalSeconds * 360;
    document.querySelector('.timer-circle').style.background = 
        `conic-gradient(#4caf50 ${progress}deg, #f0f0f0 ${progress}deg)`;
}

function saveStudyRecord(subject, duration) {
    fetch('StudyTime.aspx/SaveStudyRecord', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ subject: subject, duration: duration })
    })
    .then(response => response.json())
    .then(data => {
        if (data.d) {
            alert(`You have finished studying ${subject} for ${initialDuration}. Record saved successfully!`);
        } else {
            alert('Failed to save study record. Please try again.');
        }
        resetTimer();
    })
    .catch(error => {
        console.error('Error:', error);
        alert('An error occurred while saving the study record.');
        resetTimer();
    });
}

function updateCurrentTime() {
    const now = new Date();
    document.getElementById('currentTime').textContent = now.toLocaleTimeString();
    document.getElementById('dayOfWeek').textContent = now.toLocaleDateString('en-US', { weekday: 'long' });
    document.getElementById('currentDate').textContent = now.toLocaleDateString('en-US', { day: '2-digit', month: '2-digit', year: 'numeric' });
}

setInterval(updateCurrentTime, 1000);
updateCurrentTime();
