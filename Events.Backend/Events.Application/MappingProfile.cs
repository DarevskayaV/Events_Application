using AutoMapper;
using Events.Application.DTO.Request;
using Events.Application.DTO.Response;
using Events.Core.Entity;
using Microsoft.Extensions.Logging;


namespace Events.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRequestDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<Participant, ParticipantResponseDTO>();
            CreateMap<RegisterParticipantToEventRequestDTO, Participant>();

            CreateMap<Event, EventResponseDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.EventDate));
            CreateMap<EventRequestDTO, Event>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate)); ;

            CreateMap<ParticipantRequestDTO, Participant>();

        }
    }
}

