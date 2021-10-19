using FluentResults;

namespace Tutor.Core.ProgressModel.Feedback
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public Result SubmitFeedback(LearningObjectFeedback feedback)
        {
            var loadedFeedback = _feedbackRepository.Get(feedback.LearningObjectId, feedback.LearnerId);
            if (loadedFeedback == null)
            {
                loadedFeedback = feedback;
            }
            else
            {
                loadedFeedback.UpdateRating(feedback.Rating);
            }

            _feedbackRepository.SaveOrUpdate(loadedFeedback);
            return Result.Ok();
        }
    }
}