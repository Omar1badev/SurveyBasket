﻿using SurveyBasket.Contracts.Answers;

namespace SurveyBasket.Contracts.Questions;

public record QuestionResponse
(
    int Id,
    string Content,
    List<AnswerResponse> Answers

);
