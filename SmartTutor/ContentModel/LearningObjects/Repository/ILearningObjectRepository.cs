﻿using SmartTutor.ContentModel.LearningObjects.ArrangeTasks;
using SmartTutor.ContentModel.LearningObjects.Challenges;
using SmartTutor.ContentModel.LearningObjects.Questions;
using System.Collections.Generic;

namespace SmartTutor.ContentModel.LearningObjects.Repository
{
    public interface ILearningObjectRepository
    {
        List<LearningObject> GetLearningObjectsForSummary(int summaryId);
        List<LearningObject> GetFirstLearningObjectsForSummaries(List<int> summaries);
        Challenge GetChallenge(int challengeId);
        List<QuestionAnswer> GetQuestionAnswers(int questionId);
        List<ArrangeTaskContainer> GetArrangeTaskContainers(int arrangeTaskId);
        Image GetImageForSummary(int summaryId);
        LearningObject GetInteractiveLOForSummary(int summaryId);
        Text GetTextForSummary(int summaryId);
        Video GetVideoForSummary(int summaryId);
        LearningObject GetLearningObjectForSummary(int summaryId);
    }
}