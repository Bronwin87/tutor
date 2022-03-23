﻿using FluentResults;

namespace Tutor.Core.DomainModel.AssessmentEvents
{
    public interface ISubmissionService
    {
        Result<Evaluation> EvaluateAndSaveSubmission(Submission submission);
    }
}