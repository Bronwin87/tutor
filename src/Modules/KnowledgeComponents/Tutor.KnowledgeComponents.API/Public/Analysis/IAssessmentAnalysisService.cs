﻿using FluentResults;
using Tutor.KnowledgeComponents.API.Dtos.KnowledgeAnalytics;

namespace Tutor.KnowledgeComponents.API.Public.Analysis;

public interface IAssessmentAnalysisService
{
    Result<List<AiStatisticsDto>> GetStatistics(int kcId, int instructorId);
}