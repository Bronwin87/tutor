﻿using FluentResults;
using System.Collections.Generic;
using System.Linq;
using Tutor.Core.BuildingBlocks;
using Tutor.Core.Domain.CourseIteration;
using Tutor.Core.Domain.Knowledge.RepositoryInterfaces;
using Tutor.Core.Domain.Knowledge.Structure;
using Tutor.Core.Domain.KnowledgeMastery;
using Tutor.Core.Domain.Stakeholders.RepositoryInterfaces;

namespace Tutor.Core.UseCases.Monitoring;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IOwnedCourseRepository _ownedCourseRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IKnowledgeMasteryRepository _knowledgeMasteryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IOwnedCourseRepository ownedCourseRepository, IKnowledgeMasteryRepository knowledgeMasteryRepository, IUnitOfWork unitOfWork, IUnitRepository unitRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _ownedCourseRepository = ownedCourseRepository;
        _unitRepository = unitRepository;
        _knowledgeMasteryRepository = knowledgeMasteryRepository;
        _unitOfWork = unitOfWork;
    }

    public Result<List<Course>> GetEnrolledCourses(int learnerId)
    {
        return _enrollmentRepository.GetEnrolledCourses(learnerId);
    }

    public Result<Course> GetCourseWithEnrolledAndActiveUnits(int courseId, int learnerId)
    {
        return _enrollmentRepository.GetCourseEnrolledAndActiveUnits(courseId, learnerId);
    }

    public Result<List<UnitEnrollment>> GetEnrollments(int unitId, int[] learnerIds, int instructorId)
    {
        if (!_ownedCourseRepository.IsUnitOwner(unitId, instructorId)) return Result.Fail(FailureCode.Forbidden);

        return _enrollmentRepository.GetEnrollments(unitId, learnerIds);
    }

    public Result<List<UnitEnrollment>> BulkEnroll(int unitId, int[] learnerIds, int instructorId)
    {
        if (!_ownedCourseRepository.IsUnitOwner(unitId, instructorId)) return Result.Fail(FailureCode.Forbidden);
        
        var unit = _unitRepository.GetUnitWithKcsAndAssessments(unitId);
        
        var enrollments = learnerIds.Select(learnerId => Enroll(unit, learnerId)).ToList();

        var result = _unitOfWork.Save();
        if (result.IsFailed) return result;

        return enrollments;
    }

    public Result<UnitEnrollment> Enroll(int unitId, int learnerId, int instructorId)
    {
        if (!_ownedCourseRepository.IsUnitOwner(unitId, instructorId)) return Result.Fail(FailureCode.Forbidden);

        var enrollment = Enroll(_unitRepository.GetUnitWithKcsAndAssessments(unitId), learnerId);

        var result = _unitOfWork.Save();
        if (result.IsFailed) return result;

        return enrollment;
    }

    private UnitEnrollment Enroll(KnowledgeUnit unit, int learnerId)
    {
        var existingEnrollment = _enrollmentRepository.GetEnrollment(unit.Id, learnerId);
        if(existingEnrollment != null)
        {
            // Maybe should throw exception instead?
            if (existingEnrollment.Status == EnrollmentStatus.Active) return existingEnrollment;
            
            existingEnrollment.Status = EnrollmentStatus.Active;
            return _enrollmentRepository.Update(existingEnrollment);
        }

        var newEnrollment = new UnitEnrollment(learnerId, unit.Id);
        _enrollmentRepository.Create(newEnrollment);

        CreateMasteries(unit, learnerId);

        return newEnrollment;
    }

    private void CreateMasteries(KnowledgeUnit unit, int learnerId)
    {
        foreach (var kc in unit.KnowledgeComponents)
        {
            var assessmentMasteries = kc.AssessmentItems
                .Select(item => new AssessmentItemMastery(item.Id)).ToList();
            var kcMastery = new KnowledgeComponentMastery(learnerId, kc.Id, assessmentMasteries);
            _knowledgeMasteryRepository.Create(kcMastery);
        }
    }

    public Result Unenroll(int unitId, int learnerId, int instructorId)
    {
        if (!_ownedCourseRepository.IsUnitOwner(unitId, instructorId)) return Result.Fail(FailureCode.Forbidden);

        var enrollment = _enrollmentRepository.GetEnrollment(unitId, learnerId);
        if (enrollment == null) return Result.Fail(FailureCode.NotFound);

        enrollment.Status = EnrollmentStatus.Hidden;
        _enrollmentRepository.Update(enrollment);

        var result = _unitOfWork.Save();
        if (result.IsFailed) return result;

        return Result.Ok();
    }
}