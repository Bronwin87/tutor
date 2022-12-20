﻿using FluentResults;
using Tutor.Core.BuildingBlocks;
using Tutor.Core.BuildingBlocks.Generics;
using Tutor.Core.Domain.Stakeholders;
using Tutor.Core.Domain.Stakeholders.RepositoryInterfaces;

namespace Tutor.Core.UseCases.Management.Stakeholders;

public class LearnerService : CrudService<Learner>, ILearnerService
{
    private readonly ICrudRepository<Learner> _learnerRepository;
    private readonly IUserRepository _userRepository;
    public LearnerService(ICrudRepository<Learner> crudRepository, IUserRepository userRepository) : base(crudRepository)
    {
        _learnerRepository = crudRepository;
        _userRepository = userRepository;
    }
    public Result<Learner> Register(Learner learner, string username, string password)
    {
        var user = _userRepository.Register(username, password, UserRole.Learner);
        learner.UserId = user.Id;
        // Warning: transactional consistency is not supported here (no rollback if Create fails).
        return Create(learner);
    }

    public Result<Learner> Archive(int id, bool archive)
    {
        var learner = _learnerRepository.Get(id);
        if (learner == null) return Result.Fail(FailureCode.NotFound);
        // Warning: Explicit account (User) deactivation is missing.
        learner.IsArchived = archive;
        _learnerRepository.Update(learner);
        return Result.Ok(learner);
    }
}