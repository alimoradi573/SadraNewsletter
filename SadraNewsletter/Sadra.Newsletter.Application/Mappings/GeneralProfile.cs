using AutoMapper;
using Sadra.Newsletter.Application.DTOs;
using Sadra.Newsletter.Domain.Entities;

namespace Sadra.Newsletter.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<NewsLetterDTO, NewsLetter>().ReverseMap();
            CreateMap<RecipientDTO, Recipient>().ReverseMap();

        }
    }
}
