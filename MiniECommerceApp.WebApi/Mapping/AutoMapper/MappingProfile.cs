using AutoMapper;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.DTOs;

namespace MiniECommerceApp.WebApi.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddProductDto, Product>();

        }
    }

}
