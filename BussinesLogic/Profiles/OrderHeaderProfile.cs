using AutoMapper;
using Entities.Entity;
using Repository.Dtos;

namespace BussinesLogic.Profiles
{
 public class OrderHeaderProfile: Profile
    {
        public OrderHeaderProfile()
        {
            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
        }
    }
}

