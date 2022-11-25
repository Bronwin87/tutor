﻿using System.Collections.Generic;
using Tutor.Core.Domain.Knowledge.Structure;

namespace Tutor.Core.Domain.CourseIteration
{
    public interface IEnrollmentRepository
    {
        int CountAllEnrollmentsInUnit(int unitId);
        List<Course> GetEnrolledCourses(int learnerId);
        List<KnowledgeUnit> GetEnrolledAndActiveUnits(int courseId, int learnerId);
        bool HasActiveEnrollmentForUnit(int unitId, int learnerId);
        bool HasActiveEnrollmentForKc(int knowledgeComponent, int learnerId);
    }
}
