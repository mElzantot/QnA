using AutoMapper;
using QnA.BAL.DTO;
using QnA.DbModels;

namespace QnA.Mappers
{
    public class QuestionMapperProfile : Profile
    {
        public QuestionMapperProfile()
        {
            CreateMap<Question, QuestionProfile>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.QuestionRank))
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        }
    }
}
