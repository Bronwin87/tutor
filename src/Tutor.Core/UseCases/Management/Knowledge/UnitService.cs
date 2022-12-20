﻿using FluentResults;
using System.Collections.Generic;
using Tutor.Core.BuildingBlocks.Generics;
using Tutor.Core.Domain.Knowledge.RepositoryInterfaces;
using Tutor.Core.Domain.Knowledge.Structure;

namespace Tutor.Core.UseCases.Management.Knowledge;

public class UnitService : CrudService<KnowledgeUnit>, IUnitService
{
    private readonly IUnitRepository _unitRepository;
    public UnitService(IUnitRepository unitRepository): base(unitRepository)
    {
        _unitRepository = unitRepository;
    }
    public Result<List<KnowledgeUnit>> GetByCourse(int courseId)
    {
        return _unitRepository.GetByCourseId(courseId);
    }
}