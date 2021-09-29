using AutoMapper;
using Entities.Entity;
using Repository.Dtos;

namespace BussinesLogic.Profiles
{
 public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}

