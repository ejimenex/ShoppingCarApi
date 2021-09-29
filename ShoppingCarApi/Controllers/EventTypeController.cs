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
    public class EventTypeController : BaseController<EventTypes,EventTypeDto, IBaseService<EventTypes>>
    {
        public EventTypeController(IBaseService<EventTypes> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
        //[HttpGet]
        //[EnableQuery()]
        //public override IActionResult Get()
        //{
        //    return base.Get();
        //}
        [HttpGet("getByDoctor")]
        
        public IActionResult GetByDoctor(int Id)
        {
            return Ok( _service.FindByCondition(x=> x.DoctorId==Id).AsQueryable());
        }
    }
}