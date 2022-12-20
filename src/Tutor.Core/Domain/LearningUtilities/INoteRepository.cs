﻿using System.Collections.Generic;
using Tutor.Core.BuildingBlocks.Generics;

namespace Tutor.Core.Domain.LearningUtilities;

public interface INoteRepository : ICrudRepository<Note>
{
    List<Note> GetNotesByLearnerAndUnit(int learnerId, int unitId);
}