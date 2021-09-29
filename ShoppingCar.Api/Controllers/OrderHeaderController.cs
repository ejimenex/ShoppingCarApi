using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using ShoppingCar.Controllers;
using System;
using System.Linq;

namespace ShoppingCar.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderHeaderController : BaseController<OrderHeader, OrderHeaderDto, IBaseService<OrderHeader>>
    {
        private readonly IOrderHeader order;
        public OrderHeaderController(IOrderHeader order,IBaseService<OrderHeader> manager, IMapper Mapper) : base(manager, Mapper)
        {
            this.order = order;
        }
        [HttpGet]
        [Route("GetCurrentOrder")]
        public IActionResult GetCurrentOrder(Guid customer) {

            return Ok(this.order.GetCurrentOrder(customer));
        }
        
    }
}
