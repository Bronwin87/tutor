﻿using FluentResults;
using System.Collections.Generic;
using Tutor.Core.Domain.LearningUtilities;

namespace Tutor.Core.UseCases.Learning.Utilities;

public interface INoteService
{
    Result<Note> Create(Note note);

    Result Delete(int id);

    Result Update(Note note);

    Result<List<Note>> GetAppropriateNotes(int learnerId, int unitId);
}