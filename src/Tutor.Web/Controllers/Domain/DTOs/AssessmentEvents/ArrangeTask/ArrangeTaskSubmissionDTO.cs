﻿using System.Collections.Generic;

namespace Tutor.Web.Controllers.Domain.DTOs.AssessmentEvents.ArrangeTask
{
    public class ArrangeTaskSubmissionDto
    {
        public int AssessmentEventId { get; set; }
        public int LearnerId { get; set; }
        public List<ArrangeTaskContainerSubmissionDto> Containers { get; set; }
    }
}