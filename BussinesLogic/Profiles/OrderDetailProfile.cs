using AutoMapper;
using Entities.Entity;
using Repository.Dtos;

namespace BussinesLogic.Profiles
{
 public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        }
    }
}

