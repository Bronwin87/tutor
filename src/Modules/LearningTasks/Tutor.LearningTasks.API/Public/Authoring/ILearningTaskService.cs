﻿using FluentResults;
using Tutor.LearningTasks.API.Dtos.LearningTasks;

namespace Tutor.LearningTasks.API.Public.Authoring;

public interface ILearningTaskService
{
    Result<LearningTaskDto> Get(int id, int unitId, int instructorId);
    Result<List<LearningTaskDto>> GetByUnit(int unitId, int instructorId);
    Result<LearningTaskDto> Create(LearningTaskDto learningTask, int instructorId);
    Result<LearningTaskDto> Update(LearningTaskDto learningTask, int instructorId);
    Result Delete(int id, int unitId, int instructorId);
}
