﻿using FluentResults;
using System;
using Tutor.Core.BuildingBlocks.EventSourcing;
using Tutor.Core.DomainModel.AssessmentEvents;

namespace Tutor.Core.DomainModel.KnowledgeComponents
{
    public class KnowledgeComponentMastery : EventSourcedAggregateRoot
    {
        public double Mastery { get; private set; }
        public KnowledgeComponent KnowledgeComponent { get; private set; }
        public int LearnerId { get; private set; }

        private KnowledgeComponentMastery() { }

        public KnowledgeComponentMastery(KnowledgeComponent knowledgeComponent)
        {
            Mastery = 0.0;
            KnowledgeComponent = knowledgeComponent;
        }

        public Result<Evaluation> SubmitAEAnswer(Submission submission)
        {
            var assessmentEvent = KnowledgeComponent.GetAssessmentEvent(submission.AssessmentEventId);
            if (assessmentEvent == null)
                return Result.Fail("No assessment event with ID: " + submission.AssessmentEventId);

            Evaluation evaluation = null;
            try
            {
                evaluation = assessmentEvent.EvaluateSubmission(submission);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail(ex.Message);
            }

            if (evaluation.Correct) submission.MarkCorrect();
            submission.CorrectnessLevel = evaluation.CorrectnessLevel;

            Causes(new AssessmentEventAnswered()
            {
                Submission = submission,
                Timestamp = submission.TimeStamp
            });

            return Result.Ok(evaluation);
        }

        public Result<AssessmentEvent> SelectSuitableAssessmentEvent(IAssessmentEventSelector assessmentEventSelector)
        {
            var result = assessmentEventSelector.SelectSuitableAssessmentEvent(KnowledgeComponent.Id, LearnerId);
            if (result.IsSuccess)
                Causes(new AssessmentEventSelected()
                {
                    LearnerId = LearnerId,
                    KnowledgeComponentId = KnowledgeComponent.Id,
                    AssessmentEventId = result.Value.Id
                });
            return result;
        }

        protected override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }

        private void When(AssessmentEventAnswered @event)
        {
            /* TODO: refactor to move the GetMaximumSubmissionCorrectness from AE to KCMastery, or a 
             * child object (which would be created here if it doesn't exist yet). The Apply method
             * should never fail, silently or otherwise, and fetching the AE can fail.
             */
            var assessmentEvent = KnowledgeComponent.GetAssessmentEvent(@event.Submission.AssessmentEventId);
            if (assessmentEvent == null)
                return;

            var currentCorrectnessLevel = assessmentEvent.GetMaximumSubmissionCorrectness();

            assessmentEvent.Submissions.Add(@event.Submission);

            if (currentCorrectnessLevel > @event.Submission.CorrectnessLevel) return;
            var kcMasteryIncrement = 100.0 / KnowledgeComponent.AssessmentEvents.Count
                * (@event.Submission.CorrectnessLevel - currentCorrectnessLevel) / 100.0;
            Mastery += kcMasteryIncrement;
        }

        private void When(AssessmentEventSelected @event)
        {
            /* TODO: save information that the AE has been selected somewhere in the 
             * model, probably in AeMastery when it's added.
             */
        }
    }
}