﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Tutor.Core.DomainModel.AssessmentEvents.Challenges;
using Tutor.Core.ProgressModel.Submissions;
using Tutor.Web.Controllers.Progress.DTOs.SubmissionEvaluation;

namespace Tutor.Web.Controllers.Progress
{
    [Route("api/submissions/")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubmissionService _submissionService;

        public SubmissionController(IMapper mapper, ISubmissionService service)
        {
            _mapper = mapper;
            _submissionService = service;
        }

        [HttpPost("challenge")]
        public ActionResult<ChallengeEvaluationDTO> SubmitChallenge(
            [FromBody] ChallengeSubmissionDTO challengeSubmission)
        {
            ChallengeEvaluation challengeEvaluation;
            try
            {
                challengeEvaluation =
                    _submissionService.EvaluateChallenge(_mapper.Map<ChallengeSubmission>(challengeSubmission));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            if (challengeEvaluation == null) return NotFound();
            return Ok(_mapper.Map<ChallengeEvaluationDTO>(challengeEvaluation));
        }

        [HttpPost("question")]
        public ActionResult<List<AnswerEvaluationDTO>> SubmitQuestionAnswers(
            [FromBody] QuestionSubmissionDTO submission)
        {
            var evaluation = _submissionService.EvaluateAnswers(_mapper.Map<QuestionSubmission>(submission));
            if (evaluation == null) return NotFound();
            return Ok(_mapper.Map<List<AnswerEvaluationDTO>>(evaluation));
        }

        [HttpPost("arrange-task")]
        public ActionResult<List<ArrangeTaskContainerEvaluationDTO>> SubmitArrangeTask(
            [FromBody] ArrangeTaskSubmissionDTO submission)
        {
            var evaluation = _submissionService.EvaluateArrangeTask(_mapper.Map<ArrangeTaskSubmission>(submission));
            if (evaluation == null) return NotFound();
            return Ok(_mapper.Map<List<ArrangeTaskContainerEvaluationDTO>>(evaluation));
        }
    }
}