using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using ShoppingCar.Controllers;
using System;

namespace ShoppingCar.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderDetailController : BaseController<OrderDetail, OrderDetailDto, IBaseService<OrderDetail>>
    {
        private readonly IOrderDetail orderDetail;
        public OrderDetailController(IOrderDetail orderDetail,IBaseService<OrderDetail> manager, IMapper Mapper) : base(manager, Mapper)
        {
            this.orderDetail = orderDetail;
        }
        [HttpGet]
        [Route("GetDetailByOrder")]
        public IActionResult GetDetailByOrder(Guid order)
        {

            return Ok(this.orderDetail.GetByOrder(order));
        }
    }
}
