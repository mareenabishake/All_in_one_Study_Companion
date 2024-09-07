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

startButton.addEventListener('click', startTimer);
stopButton.addEventListener('click', stopTimer);
resetButton.addEventListener('click', resetTimer);

function startTimer() {
    const hours = parseInt(document.getElementById('hours').value, 10);
    const minutes = parseInt(document.getElementById('minutes').value, 10);

    // Input Validation
    if (isNaN(hours) || isNaN(minutes) || hours < 0 || minutes < 0) {
        alert("Please enter valid hours and minutes (positive numbers).");
        return;
    }

    totalSeconds = (hours * 60 * 60) + (minutes * 60);
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
                alert(`You have finished studying ${subjectSelect.value} for ${initialDuration}.`);
                resetTimer();  // Reset the timer after it finishes
            }
        }, 1000);
    }
}

function stopTimer() {
    if (timer) {
        clearInterval(timer);
        timer = null;
    }
}

function resetTimer() {
    stopTimer();
    remainingSeconds = totalSeconds;
    updateTimerDisplay();
}

function updateTimerDisplay() {
    if (remainingSeconds <= 0) {
        stopTimer();
        remainingSeconds = 0;
    }
    const minutes = Math.floor(remainingSeconds / 60);
    const seconds = remainingSeconds % 60;
    timeRemaining.textContent = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
}

function updateCurrentTime() {
    const now = new Date();
    const formattedDate = `${now.getDate().toString().padStart(2, '0')}/${(now.getMonth() + 1).toString().padStart(2, '0')}/${now.getFullYear()}`;
    document.getElementById('currentTime').textContent = now.toLocaleTimeString();
    document.getElementById('dayOfWeek').textContent = now.toLocaleDateString('en-US', { weekday: 'long' });
    document.getElementById('currentDate').textContent = formattedDate;
}

setInterval(updateCurrentTime, 1000);
updateCurrentTime();
