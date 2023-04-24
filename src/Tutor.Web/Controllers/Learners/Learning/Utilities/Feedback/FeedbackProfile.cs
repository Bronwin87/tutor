﻿using AutoMapper;
using Tutor.Core.Domain.LearningUtilities;

namespace Tutor.Web.Controllers.Learners.Learning.Utilities.Feedback;

public class FeedbackProfile : Profile
{
    public FeedbackProfile()
    {
        CreateMap<EmotionsFeedbackDto, EmotionsFeedback>();
        CreateMap<EmotionsFeedback, EmotionsFeedbackDto>();
        CreateMap<TutorImprovementFeedbackDto, TutorImprovementFeedback>();
        CreateMap<TutorImprovementFeedback, TutorImprovementFeedbackDto>();
    }
}