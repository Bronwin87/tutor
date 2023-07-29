﻿namespace Tutor.LanguageModelConversations.Infrastructure.Http;

public static class LmEndpoints
{
    // izvuci kao konfiguraciju
    public static readonly string BaseApi = "http://localhost:8080/api/";
    public static readonly string AskAiAboutLecture = "ask-ai-about-lecture";
    public static readonly string AskAiAboutTask = "ask-ai-about-task";
    public static readonly string Summarize = "summarize";
    public static readonly string ExtractKeywords = "extract-keywords";
    public static readonly string GenerateLectureQuestions = "generate-lecture-questions";
    public static readonly string GenerateSimilarTask = "generate-similar-task";
}