﻿using Tutor.BuildingBlocks.Core.Domain;

namespace Tutor.LearningTasks.Core.Domain.Activites;

public class Example : Entity
{
    public string Description { get; private set; }

    public Example(string description)
    {
        Description = description;
    }
}
