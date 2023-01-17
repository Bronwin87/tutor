using System.Collections.Generic;
using Tutor.Core.Domain.Knowledge.AssessmentItems;

namespace Tutor.Core.Domain.Knowledge.RepositoryInterfaces;

public interface IAssessmentItemRepository
{
    AssessmentItem GetDerivedAssessmentItem(int assessmentItemId);
    List<AssessmentItem> GetDerivedAssessmentItemsForKc(int kcId);
}