﻿using FluentResults;
using System;
using Tutor.Core.Domain.Knowledge.AssessmentItems;
using Tutor.Core.Domain.KnowledgeMastery;

namespace Tutor.Core.UseCases.Learning.Assessment
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IKnowledgeMasteryRepository _knowledgeMasteryRepository;
        private readonly IAssessmentItemRepository _assessmentItemRepository;

        public EvaluationService(IKnowledgeMasteryRepository knowledgeMasteryRepository, IAssessmentItemRepository assessmentItemRepository)
        {
            _knowledgeMasteryRepository = knowledgeMasteryRepository;
            _assessmentItemRepository = assessmentItemRepository;
        }

        public Result<Evaluation> EvaluateAssessmentItemSubmission(int learnerId, int assessmentItemId, Submission submission)
        {
            var assessmentItem = _assessmentItemRepository.GetDerivedAssessmentItem(assessmentItemId);
            if (assessmentItem == null) return Result.Fail("No assessment item with ID: " + assessmentItemId);
            var kcMastery = _knowledgeMasteryRepository.GetFullKcMastery(assessmentItem.KnowledgeComponentId, learnerId);
            if (kcMastery == null) return Result.Fail("Learner not enrolled in KC: " + assessmentItem.KnowledgeComponentId);

            Evaluation evaluation;
            try
            {
                evaluation = assessmentItem.Evaluate(submission);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail(ex.Message);
            }

            var result = kcMastery.RecordAssessmentItemAnswerSubmission(assessmentItemId, submission, evaluation);
            if (result.IsFailed) return result.ToResult<Evaluation>();

            _knowledgeMasteryRepository.UpdateKcMastery(kcMastery);

            return Result.Ok(evaluation);
        }
    }
}