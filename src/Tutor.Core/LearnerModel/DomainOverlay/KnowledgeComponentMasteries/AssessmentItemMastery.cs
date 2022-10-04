﻿using FluentResults;
using System;
using Tutor.Core.BuildingBlocks.EventSourcing;
using Tutor.Core.DomainModel.AssessmentItems;
using Tutor.Core.LearnerModel.DomainOverlay.KnowledgeComponentMasteries.Events.AssessmentItemEvents;

namespace Tutor.Core.LearnerModel.DomainOverlay.KnowledgeComponentMasteries
{
    public class AssessmentItemMastery : EventSourcedEntity
    {
        private const double PassThreshold = 0.9;

        public int AssessmentItemId { get; private set; }
        public double Mastery { get; private set; }
        public int SubmissionCount { get; private set; }
        public DateTime? LastSubmissionTime { get; set; }
        public bool IsAttempted { get => SubmissionCount > 0; }
        public bool IsPassed { get => Mastery > PassThreshold; }

        public void RecordSelection()
        {
            Causes(new AssessmentItemSelected()
            {
                AssessmentItemId = AssessmentItemId,
            });
        }

        public void RecordAnswerSubmission(Submission submission, Evaluation evaluation)
        {
            Causes(new AssessmentItemAnswered
            {
                AssessmentItemId = AssessmentItemId,
                Submission = submission,
                Evaluation = evaluation
            });
        }

        public Result RecordHintRequest()
        {
            Causes(new SoughtHints());
            return Result.Ok();
        }

        public Result RecordSolutionRequest()
        {
            Causes(new SoughtSolution());
            return Result.Ok();
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }

        private void When(AssessmentItemAnswered @event)
        {
            if (Mastery <= @event.Evaluation.CorrectnessLevel) Mastery = @event.Evaluation.CorrectnessLevel;
            SubmissionCount++;
            LastSubmissionTime = @event.TimeStamp;
        }
    }
}