﻿using Dahomey.Json.Attributes;

namespace Tutor.Web.Controllers.Domain.DTOs.AssessmentEvents.Challenge
{
    [JsonDiscriminator("challenge", Policy = DiscriminatorPolicy.Always)]
    public class ChallengeDto : AssessmentEventDto
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }
}