using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using ShoppingCar.Controllers;

namespace ShoppingCar.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : BaseController<Customer, CustomerDto, IBaseService<Customer>>
    {
        public CustomerController(IBaseService<Customer> manager, IMapper Mapper) : base(manager, Mapper)
        {

        }
    }
}

