﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tutor.Core.DomainModel.AssessmentEvents;
using Tutor.Core.DomainModel.InstructionalEvents;
using Tutor.Core.DomainModel.KnowledgeComponents;

namespace Tutor.Infrastructure.Database.Repositories.Domain
{
    public class KCDatabaseRepository : IKCRepository
    {
        private readonly TutorContext _dbContext;

        public KCDatabaseRepository(TutorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Unit GetUnit(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Unit> GetUnits()
        {
            return _dbContext.Units.Include(u => u.KnowledgeComponents).ToList();
        }

        public KnowledgeComponent GetKnowledgeComponent(int id)
        {
            return _dbContext.KnowledgeComponents.FirstOrDefault(l => l.Id == id);
        }

        public List<AssessmentEvent> GetAssessmentEventsByKnowledgeComponent(int id)
        {
            var query = _dbContext.AssessmentEvents
                .Where(ae => ae.KnowledgeComponentId == id);
            return query.ToList();
        }

        public List<InstructionalEvent> GetInstructionalEventsByKnowledgeComponent(int id)
        {
            var query = _dbContext.InstructionalEvents
                .Where(ae => ae.KnowledgeComponentId == id);
            return query.ToList();
        }
    }
}