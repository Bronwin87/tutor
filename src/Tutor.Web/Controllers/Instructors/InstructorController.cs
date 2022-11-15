﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tutor.Core.UseCases.StakeholderManagement;
using Tutor.Infrastructure.Security.Authentication.Users;
using Tutor.Web.Mappings.Knowledge.DTOs;

namespace Tutor.Web.Controllers.Instructors;

[Authorize(Policy = "instructorPolicy")]
[Route("api/instructors/courses/")]
[ApiController]
public class InstructorController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAvailableCourseService _availableCourseService;
    
    public InstructorController(IMapper mapper, IAvailableCourseService availableCourseService)
    {
        _mapper = mapper;
        _availableCourseService = availableCourseService;
    }

    [HttpGet]
    public ActionResult<List<CourseDto>> GetOwnedCourses()
    {
        var result = _availableCourseService.GetOwnedCourses(User.InstructorId());
        if (result.IsFailed) return BadRequest(result.Errors);
        return Ok(result.Value.Select(_mapper.Map<CourseDto>).ToList());
    }

    [HttpGet("{courseId:int}")]
    public ActionResult<CourseDto> GetCourseWithUnits(int courseId)
    {
        var result = _availableCourseService.GetOwnedCourseWithUnits(courseId, User.InstructorId());
        if (result.IsFailed) return BadRequest(result.Errors);
        return Ok(_mapper.Map<CourseDto>(result.Value));
    }
}