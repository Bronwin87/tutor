﻿using RepositoryCompiler.CodeModel.CaDETModel.CodeItems;
using SmartTutor.ContentModel.LearningObjects.ChallengeModel.FulfillmentStrategy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SmartTutor.ContentModel.Lectures;

namespace SmartTutor.ContentModel.LearningObjects.ChallengeModel
{
    [Table("Challenges")]
    public class Challenge : LearningObject
    {
        public string Url { get; internal set; }
        public string Description { get; internal set; }
        public LearningObjectSummary Solution { get; internal set; }
        public List<ChallengeFulfillmentStrategy> FulfillmentStrategies { get; set; }

        public ChallengeEvaluation CheckChallengeFulfillment(List<CaDETClass> solutionAttempt)
        {
            var evaluation = new ChallengeEvaluation { ChallengeId = Id };
            foreach (var strategy in FulfillmentStrategies)
            {
                var result = strategy.EvaluateSubmission(solutionAttempt);
                evaluation.ApplicableHints.MergeHints(result);
            }

            if (evaluation.ApplicableHints.IsEmpty())
            {
                evaluation.ChallengeCompleted = true;
                evaluation.ApplicableHints.AddAllHints(GetAllChallengeHints());
            }
            return evaluation;
        }

        private List<ChallengeHint> GetAllChallengeHints()
        {
            return FulfillmentStrategies.SelectMany(s => s.GetAllHints().Where(h => h != null)).ToList();
        }
    }
}