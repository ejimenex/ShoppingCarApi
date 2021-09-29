using AutoMapper;
using Entities.Entity;
using Repository.Dtos;

namespace BussinesLogic.Profiles
{
 public class CutomerProfile: Profile
    {
        public CutomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}

