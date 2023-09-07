﻿using System.Text.RegularExpressions;

namespace Tutor.KnowledgeComponents.Core.Domain.Knowledge.AssessmentItems.CodeCompletionQuestions;

public class Ccq : AssessmentItem
{
    public string Text { get; private set; }
    public string Code { get; private set; }
    public List<CcqItem> Items { get; private set; }
    public string Feedback { get; private set; }

    protected override void ClearSolution()
    {
        Items.ForEach(a => a.HideAnswer());
        Feedback = string.Empty;
    }

    public override Evaluation Evaluate(Submission submission)
    {
        if (submission is CcqSubmission ccqSubmission) return EvaluateCcq(ccqSubmission);
        throw new ArgumentException("Incorrect submission supplied to CCQ with ID " + Id);
    }

    private CcqEvaluation EvaluateCcq(CcqSubmission ccqSubmission)
    {
        var correctItems = 0.0;
        foreach (var submittedItem in ccqSubmission.Items)
        {
            var item = Items.Find(a => a.Order == submittedItem.Order) ?? throw new ArgumentException("Invalid submission for item order: " + submittedItem.Order);
            if (IsCorrectItemSubmission(item, submittedItem.Answer)) correctItems++;
        }

        return new CcqEvaluation(correctItems / Items.Count, Items, Feedback);
    }

    private static bool IsCorrectItemSubmission(CcqItem item, string submission)
    {
        if (!item.IgnoreSpace) return submission.Equals(item.Answer);

        submission = Regex.Replace(submission, @"\s", "");
        var answer= Regex.Replace(item.Answer, @"\s", "");
        return submission.Equals(answer);
    }

    public override AssessmentItem Clone()
    {
        return new Ccq
        {
            Text = Text,
            Items = Items,
            Feedback = Feedback,
            Order = Order,
            Hints = Hints
        };
    }
}