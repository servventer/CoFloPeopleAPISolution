using AutoMapper;

namespace CoFloPeopleAPI.Services
{
    public class MappingProfilePersonModel : Profile
    {
        public MappingProfilePersonModel() 
        {
            // Map from PersonModel to PersonModelDB
            CreateMap<PersonModel, PersonModelDB>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            // Map from PersonModelDB to PersonModel
            CreateMap<PersonModelDB, PersonModel>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
