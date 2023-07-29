﻿using AutoMapper;
using FluentResults;
using Tutor.BuildingBlocks.Core.UseCases;
using Tutor.Courses.API.Dtos;
using Tutor.Courses.API.Interfaces.Learning;
using Tutor.Courses.Core.Domain;
using Tutor.Courses.Core.Domain.RepositoryInterfaces;

namespace Tutor.Courses.Core.UseCases.Learning;

public class EnrolledCourseService : BaseService<CourseDto, Course>, IEnrolledCourseService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrolledCourseService(IMapper mapper, IEnrollmentRepository enrollmentRepository): base(mapper)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public Result<PagedResult<CourseDto>> GetAll(int learnerId, int page, int pageSize)
    {
        var result = _enrollmentRepository.GetEnrolledCourses(learnerId, page, pageSize);
        return MapToDto(result);
    }

    public Result<CourseDto> GetWithActiveUnits(int courseId, int learnerId)
    {
        var course = _enrollmentRepository.GetEnrolledCourse(courseId, learnerId);
        if (course == null) return Result.Fail(FailureCode.Forbidden);

        var allEnrollments = _enrollmentRepository.GetEnrolledUnits(courseId, learnerId);
        var activeUnits = allEnrollments
            .Where(e => e.IsActive())
            .Select(e => e.KnowledgeUnit).ToList();
        course.KnowledgeUnits = activeUnits;

        return MapToDto(course);
    }

    public bool HasActiveEnrollment(int unitId, int learnerId)
    {
        var enrollment = _enrollmentRepository.GetEnrollment(unitId, learnerId);
        return enrollment != null && enrollment.IsActive();
    }
}