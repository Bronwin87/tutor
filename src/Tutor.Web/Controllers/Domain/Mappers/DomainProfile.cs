﻿using AutoMapper;
using System.Linq;
using Tutor.Core.DomainModel.AssessmentItems;
using Tutor.Core.DomainModel.AssessmentItems.ArrangeTasks;
using Tutor.Core.DomainModel.AssessmentItems.Challenges;
using Tutor.Core.DomainModel.AssessmentItems.MultiResponseQuestions;
using Tutor.Core.DomainModel.AssessmentItems.ShortAnswerQuestions;
using Tutor.Core.DomainModel.InstructionalItems;
using Tutor.Core.DomainModel.KnowledgeComponents;
using Tutor.Core.LearnerModel.DomainOverlay.KnowledgeComponentMasteries;
using Tutor.Web.Controllers.Domain.DTOs;
using Tutor.Web.Controllers.Domain.DTOs.AssessmentItems;
using Tutor.Web.Controllers.Domain.DTOs.AssessmentItems.ArrangeTasks;
using Tutor.Web.Controllers.Domain.DTOs.AssessmentItems.Challenges;
using Tutor.Web.Controllers.Domain.DTOs.AssessmentItems.MultiResponseQuestions;
using Tutor.Web.Controllers.Domain.DTOs.AssessmentItems.ShortAnswerQuestions;
using Tutor.Web.Controllers.Domain.DTOs.InstructionalItems;
using KnowledgeComponentStatistics = Tutor.Core.LearnerModel.DomainOverlay.KnowledgeComponentMasteries.KnowledgeComponentStatistics;

namespace Tutor.Web.Controllers.Domain.Mappers
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Unit, UnitDto>();
            CreateMap<KnowledgeComponent, KnowledgeComponentDto>()
                .ForMember(dest => dest.Mastery, opt => opt.MapFrom(src => src.KnowledgeComponentMasteries.FirstOrDefault()));
            CreateMap<KnowledgeComponentMastery, KnowledgeComponentMasteryDto>();
            CreateMap<KnowledgeComponentStatistics, KnowledgeComponentStatisticsDto>();

            CreateMap<InstructionalItem, InstructionalItemDto>().IncludeAllDerived();
            CreateMap<Text, TextDto>();
            CreateMap<Image, ImageDto>();
            CreateMap<Video, VideoDto>();

            CreateMap<AssessmentItem, AssessmentItemDto>().IncludeAllDerived();
            CreateMap<Challenge, ChallengeDto>();
            CreateMap<Mrq, MrqDto>();
            CreateMap<MrqItem, MrqItemDto>();
            CreateMap<ArrangeTask, AtDto>()
                .ForMember(dest => dest.UnarrangedElements, opt => opt.MapFrom(src => src.Containers.SelectMany(c => c.Elements).ToList()));
            CreateMap<ArrangeTaskContainer, AtContainerDto>();
            CreateMap<ArrangeTaskElement, AtElementDto>();
            CreateMap<Saq, SaqDto>();
        }
    }
}