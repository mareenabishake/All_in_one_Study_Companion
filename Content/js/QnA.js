document.addEventListener('DOMContentLoaded', function () {
    const questionList = document.getElementById('questionList');
    const filterAcademicLevel = document.getElementById('filterAcademicLevel');
    const filterSubjectArea = document.getElementById('filterSubjectArea');

    // Example data for questions
    const questions = [
        {
            title: 'How do I solve this math problem?',
            level: 'High School',
            subject: 'Math',
            content: 'Can someone explain how to solve quadratic equations?',
        },
        {
            title: 'What is the chemical formula for water?',
            level: 'Undergraduate',
            subject: 'Science',
            content: 'I\'m confused about how to write chemical formulas. Can anyone help?',
        },
        {
            title: 'Why did the Roman Empire fall?',
            level: 'Postgraduate',
            subject: 'History',
            content: 'I\'m looking for detailed reasons behind the fall of the Roman Empire.',
        }
    ];

    function displayQuestions(filteredQuestions = questions) {
        questionList.innerHTML = filteredQuestions.map(question => `
            <li>
                <h3>${question.title}</h3>
                <p><strong>Level:</strong> ${question.level} | <strong>Subject:</strong> ${question.subject}</p>
                <p>${question.content}</p>
                <div class="button-container">
                    <button class="answer-button">Answer</button>
                    <button class="view-answer-button">View Answer</button>
                </div>
            </li>
        `).join('');
    }

    function filterQuestions() {
        const level = filterAcademicLevel.value;
        const subject = filterSubjectArea.value;

        const filteredQuestions = questions.filter(q =>
            (level === '' || q.level === level) && (subject === '' || q.subject === subject)
        );

        displayQuestions(filteredQuestions);
    }

    filterAcademicLevel.addEventListener('change', filterQuestions);
    filterSubjectArea.addEventListener('change', filterQuestions);

    displayQuestions();
});
