using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using ShoppingCar.Controllers;

namespace ShoppingCar.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController<Product, ProductDto, IBaseService<Product>>
    {
        public ProductController(IBaseService<Product> manager, IMapper Mapper) : base(manager, Mapper)
        {

        }
    }
}
