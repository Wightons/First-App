using AutoMapper;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;

namespace TaskBoard.API
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<CardList, CardListDto>().ReverseMap();
            CreateMap<Log, LogDto>().ReverseMap();
        }
    }
}
