using AutoMapper;
using QnA.BAL.DTO;
using QnA.DbModels;

namespace QnA.Mappers
{
    public class AnswerMapperProfile : Profile
    {
        public AnswerMapperProfile()
        {
            CreateMap<Answer, AnswerProfile>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.VoteScore, opt => opt.MapFrom(src => src.UpVotes - src.DownVotes));
        }
    }
}
