using System.Collections.Generic;
using Tutor.Core.DomainModel.AssessmentItems;
using Tutor.Core.DomainModel.KnowledgeComponents;
using Tutor.Core.LearnerModel.DomainOverlay.KnowledgeComponentMasteries;

namespace Tutor.Core.LearnerModel.DomainOverlay
{
    public interface IKcMasteryRepository
    {
        List<Unit> GetEnrolledUnits(int learnerId);
        Unit GetUnitWithKcs(int unitId, int learnerId);

        KnowledgeComponentMastery GetBasicKcMastery(int knowledgeComponentId, int learnerId);
        List<KnowledgeComponentMastery> GetBasicKcMasteries(List<int> kcIds, int learnerId);
        KnowledgeComponentMastery GetFullKcMastery(int knowledgeComponentId, int learnerId);
        KnowledgeComponentMastery GetKcMasteryForAssessmentItem(int assessmentItemId, int learnerId);
        void UpdateKcMastery(KnowledgeComponentMastery kcMastery);

        List<KnowledgeComponent> GetAllKnowledgeComponents();
        AssessmentItem GetDerivedAssessmentItem(int assessmentItemId);
        Submission FindSubmissionWithMaxCorrectness(int assessmentItemId, int learnerId);
    }
}