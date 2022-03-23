using AutoMapper;
using Tutor.Core.LearnerModel.Learners;
using Tutor.Web.Controllers.Learners.DTOs;

namespace Tutor.Web.Controllers.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<LearnerDto, Learner>();
            CreateMap<Learner, LearnerDto>();
        }
    }
}