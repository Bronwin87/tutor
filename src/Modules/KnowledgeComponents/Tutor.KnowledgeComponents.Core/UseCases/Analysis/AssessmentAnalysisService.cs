﻿using AutoMapper;
using FluentResults;
using Tutor.BuildingBlocks.Core.EventSourcing;
using Tutor.BuildingBlocks.Core.UseCases;
using Tutor.KnowledgeComponents.API.Dtos.KnowledgeAnalytics;
using Tutor.KnowledgeComponents.API.Public;
using Tutor.KnowledgeComponents.API.Public.Analysis;
using Tutor.KnowledgeComponents.Core.Domain.KnowledgeAnalytics;
using Tutor.KnowledgeComponents.Core.Domain.KnowledgeMastery.Events;
using Tutor.KnowledgeComponents.Core.Domain.KnowledgeMastery.Events.AssessmentItemEvents;

namespace Tutor.KnowledgeComponents.Core.UseCases.Analysis;

public class AssessmentAnalysisService : IAssessmentAnalysisService
{
    private readonly IMapper _mapper;
    private readonly IAccessService _accessService;
    private readonly IEventStore _eventStore;
    private readonly AssessmentStatisticsCalculator _calculator;

    public AssessmentAnalysisService(IMapper mapper, IAccessService accessService, IEventStore eventStore)
    {
        _mapper = mapper;
        _accessService = accessService;
        _eventStore = eventStore;
        _calculator = new AssessmentStatisticsCalculator();
    }

    public Result<List<AiStatisticsDto>> GetStatistics(int kcId, int instructorId)
    {
        if(!_accessService.IsKcOwner(kcId, instructorId))
            return Result.Fail(FailureCode.Forbidden);

        var events = _eventStore.Events
            .Where(e => e.RootElement.GetProperty("KnowledgeComponentId").GetInt32() == kcId)
            .ToList<KnowledgeComponentEvent>();

        return CalculateStatistics(events);
    }

    private List<AiStatisticsDto> CalculateStatistics(List<KnowledgeComponentEvent> events)
    {
        var sortedAiEvents = events.OfType<AssessmentItemEvent>()
            .OrderBy(e => e.TimeStamp).ToList();
        var aiIds = sortedAiEvents.Select(e => e.AssessmentItemId).Distinct();

        return aiIds.Select(aiId => _mapper.Map<AiStatisticsDto>(_calculator.Calculate(aiId, sortedAiEvents))).ToList();
    }
}