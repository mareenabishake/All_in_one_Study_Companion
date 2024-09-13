document.addEventListener('DOMContentLoaded', function () {
    const questionList = document.getElementById('questionList');
    const filterAcademicLevel = document.getElementById('filterAcademicLevel');
    const filterSubjectArea = document.getElementById('filterSubjectArea');

    function displayQuestions(questions) {
        questionList.innerHTML = questions.map(question => `
            <li>
                <div class="question-header">
                    <h3>${question.QuestionText}</h3>
                    <span class="question-id">ID: ${question.QuestionID}</span>
                </div>
                <p><strong>Level:</strong> ${question.AcademicLevel} | <strong>Subject:</strong> ${question.SubjectName}</p>
                <div class="button-container">
                    <a href="Answer.aspx?id=${question.QuestionID}" class="answer-button">Answer</a>
                    ${question.AnswerCount > 0 ? 
                        `<a href="ViewAnswers.aspx?id=${question.QuestionID}" class="view-answers-button">View Answers (${question.AnswerCount})</a>` : 
                        ''}
                </div>
            </li>
        `).join('');
    }

    function filterQuestions() {
        const level = filterAcademicLevel.value;
        const subject = filterSubjectArea.value;

        // AJAX call to server to get filtered questions
        $.ajax({
            type: "POST",
            url: "QnA.aspx/GetFilteredQuestions",
            data: JSON.stringify({ academicLevel: level, subjectArea: subject }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                const questions = JSON.parse(response.d);
                displayQuestions(questions);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching questions:", error);
            }
        });
    }

    filterAcademicLevel.addEventListener('change', filterQuestions);
    filterSubjectArea.addEventListener('change', filterQuestions);

    // Initial load of questions
    filterQuestions();
});
