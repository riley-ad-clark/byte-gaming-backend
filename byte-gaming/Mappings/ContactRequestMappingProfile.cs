using AutoMapper;
using Entities.Entities;
namespace AdvancedCSLab02.Mappings
{
    public class ContactRequestMappingProfile : Profile
    {
        public ContactRequestMappingProfile()
        {
            // maps for obscuring important info (id, and post date should not technically be public)
            CreateMap<ContactRequest, ContactRequestDTO>();
            CreateMap<ContactRequestDTO, ContactRequest>();
        }
    }
}
