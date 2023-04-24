﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutor.Core.UseCases.Learning;
using Tutor.Infrastructure.Security.Authentication.Users;

namespace Tutor.Web.Controllers.Learners.Learning;

[Authorize(Policy = "learnerPolicy")]
[Route("api/learning/session/{knowledgeComponentId:int}")]
public class SessionController : BaseApiController
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }
        
    [HttpPost("launch")]
    public ActionResult LaunchSession(int knowledgeComponentId)
    {
        var result = _sessionService.LaunchSession(knowledgeComponentId, User.LearnerId());
        return CreateResponse(result, Ok, CreateErrorResponse);
    }

    [HttpPost("terminate")]
    public ActionResult TerminateSession(int knowledgeComponentId)
    {
        var result = _sessionService.TerminateSession(knowledgeComponentId, User.LearnerId());
        return CreateResponse(result, Ok, CreateErrorResponse);
    }
    
    [HttpPost("pause")]
    public ActionResult Pause(int knowledgeComponentId)
    {
        var result = _sessionService.PauseSession(knowledgeComponentId, User.LearnerId());
        if (result.IsFailed) return CreateErrorResponse(result.Errors);
        return Ok();
    }
    
    [HttpPost("continue")]
    public ActionResult TerminatePause(int knowledgeComponentId)
    {
        var result = _sessionService.ContinueSession(knowledgeComponentId, User.LearnerId());
        if (result.IsFailed) return CreateErrorResponse(result.Errors);
        return Ok();
    }
    
    [HttpPost("abandon")]
    public ActionResult AbandonSession(int knowledgeComponentId)
    {
        var result = _sessionService.AbandonSession(knowledgeComponentId, User.LearnerId());
        if (result.IsFailed) return CreateErrorResponse(result.Errors);
        return Ok();
    }
}