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
    public class PersonalScheduleController : BaseController<PersonalSchedule,PersonalScheduleDto, IBaseService<PersonalSchedule>>
    {
        public PersonalScheduleController(IBaseService<PersonalSchedule> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
        //[HttpGet]
        //[EnableQuery()]
        //public override IActionResult Get()
        //{
        //    return base.Get();
        //}
        [HttpGet("getByDoctor/{Id}/{DateEvent}")]
        public IActionResult GetByDoctor(Guid Id, DateTime DateEvent)
        {
            try
            {
                var data=_service.FindAll().Where(x => x.DoctorGuid == Id && x.EventDate == DateEvent);
               
                return Ok(_Mapper.Map<IEnumerable<PersonalScheduleDto>>(data));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
}