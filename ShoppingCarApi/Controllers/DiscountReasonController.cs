using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Dtos;
using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class DiscountReasonController : Controller
    {
        readonly IDiscountReason dis;
        protected readonly IMapper _Mapper;
        public DiscountReasonController(IDiscountReason _dis, IMapper Mapper) 
        {
            _Mapper = Mapper;
            dis = _dis;
        }
        [HttpGet]
        public IEnumerable<InvoiceDiscountReason> Get()
        {
                var data = dis.GetAll();
                return data;
        }
    }
}